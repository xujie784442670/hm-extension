using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace HmExtension.Standard.utils;

// <summary>
/// 文件合并改变事件
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
public delegate void FileMergeProgressChangedEventHandler(object sender, int e);

/// <summary>
/// 多线程下载器
/// </summary>
public class MultiThreadDownloader
{
    #region 属性

    private string _url;
    private bool _rangeAllowed;

    #endregion

    #region 公共属性

    /// <summary>
    /// RangeAllowed
    /// </summary>
    public bool RangeAllowed
    {
        get => _rangeAllowed;
        set => _rangeAllowed = value;
    }

    /// <summary>
    /// 临时文件夹
    /// </summary>
    public string TempFileDirectory { get; set; }

    /// <summary>
    /// url地址
    /// </summary>
    public string Url
    {
        get => _url;
        set => _url = value;
    }

    /// <summary>
    /// 第几部分
    /// </summary>
    public int NumberOfParts { get; set; }

    /// <summary>
    /// 已接收字节数
    /// </summary>
    public long TotalBytesReceived => PartialDownloaderList.Where(t => t != null).Sum(t => t.TotalBytesRead);

    /// <summary>
    /// 总进度
    /// </summary>
    public float TotalProgress { get; private set; }

    /// <summary>
    /// 文件大小
    /// </summary>
    public long Size { get; private set; }

    /// <summary>
    /// 下载速度
    /// </summary>
    public float TotalSpeedInBytes => PartialDownloaderList.Sum(t => t.SpeedInBytes);

    /// <summary>
    /// 下载块
    /// </summary>
    public List<PartialDownloader> PartialDownloaderList { get; }

    /// <summary>
    /// 文件路径
    /// </summary>
    public string FilePath { get; set; }

    #endregion

    #region 变量

    /// <summary>
    /// 总下载进度更新事件
    /// </summary>
    public event EventHandler TotalProgressChanged;

    /// <summary>
    /// 文件合并事件
    /// </summary>
    public event FileMergeProgressChangedEventHandler FileMergeProgressChanged;

    private readonly AsyncOperation _aop;

    #endregion

    #region 下载管理器

    /// <summary>
    /// 多线程下载管理器
    /// </summary>
    /// <param name="sourceUrl">下载地址</param>
    /// <param name="tempDir">临时文件路金</param>
    /// <param name="savePath">保存路金</param>
    /// <param name="numOfParts"></param>
    public MultiThreadDownloader(string sourceUrl, string tempDir, string savePath, int numOfParts = 1)
    {
        _url = sourceUrl;
        NumberOfParts = numOfParts;
        TempFileDirectory = tempDir;
        PartialDownloaderList = new List<PartialDownloader>();
        _aop = AsyncOperationManager.CreateOperation(null);
        FilePath = savePath;
    }

    /// <summary>
    /// 多线程下载管理器
    /// </summary>
    /// <param name="sourceUrl">下载地址</param>
    /// <param name="savePath">保存路</param>
    /// <param name="numOfParts">线程数</param>
    public MultiThreadDownloader(string sourceUrl, string savePath, int numOfParts) : this(sourceUrl, null, savePath, numOfParts)
    {
        TempFileDirectory = Environment.GetEnvironmentVariable("temp");
    }

    /// <summary>
    /// 多线程下载管理器
    /// </summary>
    /// <param name="sourceUrl">下载地址</param>
    /// <param name="numOfParts"></param>
    public MultiThreadDownloader(string sourceUrl, int numOfParts) : this(sourceUrl, null, numOfParts)
    {
    }

    #endregion

    #region 事件

    private void temp_DownloadPartCompleted(object sender, EventArgs e)
    {
        WaitOrResumeAll(PartialDownloaderList, true);

        if (TotalBytesReceived == Size)
        {
            UpdateProgress();
            MergeParts();
            return;
        }

        OrderByRemaining(PartialDownloaderList);

        int rem = PartialDownloaderList[0].RemainingBytes;
        if (rem < 50 * 1024)
        {
            WaitOrResumeAll(PartialDownloaderList, false);
            return;
        }

        int from = PartialDownloaderList[0].CurrentPosition + rem / 2;
        int to = PartialDownloaderList[0].To;
        if (from > to)
        {
            WaitOrResumeAll(PartialDownloaderList, false);
            return;
        }

        PartialDownloaderList[0].To = from - 1;

        WaitOrResumeAll(PartialDownloaderList, false);

        PartialDownloader temp = new PartialDownloader(_url, TempFileDirectory, Guid.NewGuid().ToString(), from, to, true);
        temp.DownloadPartCompleted += temp_DownloadPartCompleted;
        temp.DownloadPartProgressChanged += temp_DownloadPartProgressChanged;
        PartialDownloaderList.Add(temp);
        temp.Start();
    }

    void temp_DownloadPartProgressChanged(object sender, EventArgs e)
    {
        UpdateProgress();
    }

    void UpdateProgress()
    {
        int pr = (int)(TotalBytesReceived * 1d / Size * 100);
        if (TotalProgress != pr)
        {
            TotalProgress = pr;
            if (TotalProgressChanged != null)
            {
                _aop.Post(state => TotalProgressChanged(this, EventArgs.Empty), null);
            }
        }
    }

    #endregion

    #region 方法

    void CreateFirstPartitions()
    {
        Size = GetContentLength(_url, ref _rangeAllowed, ref _url);
        int maximumPart = (int)(Size / (25 * 1024));
        maximumPart = maximumPart == 0 ? 1 : maximumPart;
        if (!_rangeAllowed)
            NumberOfParts = 1;
        else if (NumberOfParts > maximumPart)
            NumberOfParts = maximumPart;

        for (int i = 0; i < NumberOfParts; i++)
        {
            PartialDownloader temp = CreateNewPd(i, NumberOfParts, Size);
            temp.DownloadPartProgressChanged += temp_DownloadPartProgressChanged;
            temp.DownloadPartCompleted += temp_DownloadPartCompleted;
            PartialDownloaderList.Add(temp);
            temp.Start();
        }
    }

    void MergeParts()
    {
        List<PartialDownloader> mergeOrderedList = SortPDsByFrom(PartialDownloaderList);
        using (var fs = new FileStream(FilePath, FileMode.Create, FileAccess.ReadWrite))
        {
            long totalBytesWritten = 0;
            int mergeProgress = 0;
            foreach (var item in mergeOrderedList)
            {
                using (FileStream pds = new FileStream(item.FullPath, FileMode.Open, FileAccess.Read))
                {
                    byte[] buffer = new byte[4096];
                    int read;
                    while ((read = pds.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fs.Write(buffer, 0, read);
                        totalBytesWritten += read;
                        int temp = (int)(totalBytesWritten * 1d / Size * 100);
                        if (temp != mergeProgress && FileMergeProgressChanged != null)
                        {
                            mergeProgress = temp;
                            _aop.Post(state => FileMergeProgressChanged(this, temp), null);
                        }
                    }
                }

                File.Delete(item.FullPath);
            }
        }
    }

    PartialDownloader CreateNewPd(int order, int parts, long contentLength)
    {
        int division = (int)contentLength / parts;
        int remaining = (int)contentLength % parts;
        int start = division * order;
        int end = start + division - 1;
        end += (order == parts - 1) ? remaining : 0;
        return new PartialDownloader(_url, TempFileDirectory, Guid.NewGuid().ToString(), start, end, true);
    }

    /// <summary>
    /// 暂停或继续
    /// </summary>
    /// <param name="list"></param>
    /// <param name="wait"></param>
    public static void WaitOrResumeAll(List<PartialDownloader> list, bool wait)
    {
        foreach (PartialDownloader item in list)
        {
            if (wait)
                item.Wait();
            else
                item.ResumeAfterWait();
        }
    }

    /// <summary>
    /// 冒泡排序
    /// </summary>
    /// <param name="list"></param>
    private static void BubbleSort(List<PartialDownloader> list)
    {
        bool switched = true;
        while (switched)
        {
            switched = false;
            for (int i = 0; i < list.Count - 1; i++)
            {
                if (list[i].RemainingBytes < list[i + 1].RemainingBytes)
                {
                    PartialDownloader temp = list[i];
                    list[i] = list[i + 1];
                    list[i + 1] = temp;
                    switched = true;
                }
            }
        }
    }

    /// <summary>
    /// Sorts the downloader by From property to merge the parts
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static List<PartialDownloader> SortPDsByFrom(List<PartialDownloader> list)
    {
        return list.OrderBy(x => x.From).ToList();
    }

    /// <summary>
    /// 按剩余时间排序
    /// </summary>
    /// <param name="list"></param>
    public static void OrderByRemaining(List<PartialDownloader> list)
    {
        BubbleSort(list);
    }

    /// <summary>
    /// 获取内容长度
    /// </summary>
    /// <param name="url"></param>
    /// <param name="rangeAllowed"></param>
    /// <param name="redirectedUrl"></param>
    /// <returns></returns>
    public static long GetContentLength(string url, ref bool rangeAllowed, ref string redirectedUrl)
    {
        HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
        req.UserAgent = "Mozilla/4.0 (compatible; MSIE 11.0; Windows NT 6.2; .NET CLR 1.0.3705;)";
        req.ServicePoint.ConnectionLimit = 4;
        long ctl;
        using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
        {
            redirectedUrl = resp.ResponseUri.OriginalString;
            ctl = resp.ContentLength;
            rangeAllowed = resp.Headers.AllKeys.Select((v, i) => new
            {
                HeaderName = v,
                HeaderValue = resp.Headers[i]
            }).Any(k => k.HeaderName.ToLower().Contains("range") && k.HeaderValue.ToLower().Contains("byte"));
            resp.Close();
        }

        req.Abort();
        return ctl;
    }

    #endregion

    #region 公共方法

    /// <summary>
    /// 暂停下载
    /// </summary>
    public void Pause()
    {
        foreach (var t in PartialDownloaderList)
        {
            if (!t.Completed)
                t.Stop();
        }

        //Setting a Thread.Sleep ensures all downloads are stopped and exit from loop.
        Thread.Sleep(200);
    }

    /// <summary>
    /// 开始下载
    /// </summary>
    public void Start()
    {
        Task th = new Task(CreateFirstPartitions);
        th.Start();
    }

    /// <summary>
    /// 唤醒下载
    /// </summary>
    public void Resume()
    {
        int count = PartialDownloaderList.Count;
        for (int i = 0; i < count; i++)
        {
            if (PartialDownloaderList[i].Stopped)
            {
                int from = PartialDownloaderList[i].CurrentPosition + 1;
                int to = PartialDownloaderList[i].To;
                if (from > to) continue;
                PartialDownloader temp = new PartialDownloader(_url, TempFileDirectory, Guid.NewGuid().ToString(), from, to, _rangeAllowed);

                temp.DownloadPartProgressChanged += temp_DownloadPartProgressChanged;
                temp.DownloadPartCompleted += temp_DownloadPartCompleted;
                PartialDownloaderList.Add(temp);
                PartialDownloaderList[i].To = PartialDownloaderList[i].CurrentPosition;
                temp.Start();
            }
        }
    }

    #endregion

}

/// <summary>
/// 部分下载器
/// </summary>
public class PartialDownloader
{
    #region Variables

    /// <summary>
    /// 这部分完成事件
    /// </summary>
    public event EventHandler DownloadPartCompleted;

    /// <summary>
    /// 部分下载进度改变事件
    /// </summary>
    public event EventHandler DownloadPartProgressChanged;

    /// <summary>
    /// 部分下载停止事件
    /// </summary>
    public event EventHandler DownloadPartStopped;

    HttpWebRequest _req;
    HttpWebResponse _resp;
    Stream _tempStream;
    FileStream _file;
    private readonly AsyncOperation _aop = AsyncOperationManager.CreateOperation(null);
    private readonly Stopwatch _stp;
    readonly int[] _lastSpeeds;
    int _counter;
    bool _stop, _wait;

    #endregion

    #region PartialDownloader

    /// <summary>
    /// 部分块下载
    /// </summary>
    /// <param name="url"></param>
    /// <param name="dir"></param>
    /// <param name="fileGuid"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="rangeAllowed"></param>
    public PartialDownloader(string url, string dir, string fileGuid, int from, int to, bool rangeAllowed)
    {
        _from = from;
        _to = to;
        _url = url;
        _rangeAllowed = rangeAllowed;
        _fileGuid = fileGuid;
        _directory = dir;
        _lastSpeeds = new int[10];
        _stp = new Stopwatch();
    }

    #endregion

    void DownloadProcedure()
    {
        _file = new FileStream(FullPath, FileMode.Create, FileAccess.ReadWrite);

        #region Request-Response

        _req = WebRequest.Create(_url) as HttpWebRequest;
        if (_req != null)
        {
            _req.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";
            _req.AllowAutoRedirect = true;
            _req.MaximumAutomaticRedirections = 5;
            _req.ServicePoint.ConnectionLimit += 1;
            _req.ServicePoint.Expect100Continue = true;
            _req.ProtocolVersion = HttpVersion.Version10;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            ServicePointManager.Expect100Continue = true;
            if (_rangeAllowed)
                _req.AddRange(_from, _to);
            _resp = _req.GetResponse() as HttpWebResponse;

            #endregion

            #region Some Stuff

            if (_resp != null)
            {
                _contentLength = _resp.ContentLength;
                if (_contentLength <= 0 || (_rangeAllowed && _contentLength != _to - _from + 1))
                    throw new Exception("Invalid response content");
                _tempStream = _resp.GetResponseStream();
                int bytesRead;
                byte[] buffer = new byte[4096];
                _stp.Start();

                #endregion

                #region Procedure Loop

                while (_tempStream != null && (bytesRead = _tempStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    while (_wait)
                    {
                    }

                    if (_totalBytesRead + bytesRead > _contentLength)
                        bytesRead = (int)(_contentLength - _totalBytesRead);
                    _file.Write(buffer, 0, bytesRead);
                    _totalBytesRead += bytesRead;
                    _lastSpeeds[_counter] = (int)(_totalBytesRead / Math.Ceiling(_stp.Elapsed.TotalSeconds));
                    _counter = (_counter >= 9) ? 0 : _counter + 1;
                    int tempProgress = (int)(_totalBytesRead * 100 / _contentLength);
                    if (_progress != tempProgress)
                    {
                        _progress = tempProgress;
                        _aop.Post(state =>
                        {
                            DownloadPartProgressChanged?.Invoke(this, EventArgs.Empty);
                        }, null);
                    }

                    if (_stop || (_rangeAllowed && _totalBytesRead == _contentLength))
                    {
                        break;
                    }
                }

                #endregion

                #region Close Resources

                _file.Close();
                _resp.Close();
            }

            _tempStream?.Close();
            _req.Abort();
        }

        _stp.Stop();

        #endregion

        #region Fire Events

        if (!_stop && DownloadPartCompleted != null)
            _aop.Post(state =>
            {
                _completed = true;
                DownloadPartCompleted(this, EventArgs.Empty);
            }, null);

        if (_stop && DownloadPartStopped != null)
            _aop.Post(state => DownloadPartStopped(this, EventArgs.Empty), null);

        #endregion
    }

    #region Public Methods

    /// <summary>
    /// 启动下载
    /// </summary>
    public void Start()
    {
        _stop = false;
        Thread procThread = new Thread(DownloadProcedure);
        procThread.Start();
    }

    /// <summary>
    /// 下载停止
    /// </summary>
    public void Stop()
    {
        _stop = true;
    }

    /// <summary>
    /// 暂停等待下载
    /// </summary>
    public void Wait()
    {
        _wait = true;
    }

    /// <summary>
    /// 稍后唤醒
    /// </summary>
    public void ResumeAfterWait()
    {
        _wait = false;
    }

    #endregion

    #region Property Variables

    private readonly int _from;
    private int _to;
    private readonly string _url;
    private readonly bool _rangeAllowed;
    private long _contentLength;
    private int _totalBytesRead;
    private readonly string _fileGuid;
    private readonly string _directory;
    private int _progress;
    private bool _completed;

    #endregion

    #region Properties

    /// <summary>
    /// 下载已停止
    /// </summary>
    public bool Stopped => _stop;

    /// <summary>
    /// 下载已完成
    /// </summary>
    public bool Completed => _completed;

    /// <summary>
    /// 下载进度
    /// </summary>
    public int Progress => _progress;

    /// <summary>
    /// 下载目录
    /// </summary>
    public string Directory => _directory;

    /// <summary>
    /// 文件名
    /// </summary>
    public string FileName => _fileGuid;

    /// <summary>
    /// 已读字节数
    /// </summary>
    public long TotalBytesRead => _totalBytesRead;

    /// <summary>
    /// 内容长度
    /// </summary>
    public long ContentLength => _contentLength;

    /// <summary>
    /// RangeAllowed
    /// </summary>
    public bool RangeAllowed => _rangeAllowed;

    /// <summary>
    /// url
    /// </summary>
    public string Url => _url;

    /// <summary>
    /// to
    /// </summary>
    public int To
    {
        get => _to;
        set
        {
            _to = value;
            _contentLength = _to - _from + 1;
        }
    }

    /// <summary>
    /// from
    /// </summary>
    public int From => _from;

    /// <summary>
    /// 当前位置
    /// </summary>
    public int CurrentPosition => _from + _totalBytesRead - 1;

    /// <summary>
    /// 剩余字节数
    /// </summary>
    public int RemainingBytes => (int)(_contentLength - _totalBytesRead);

    /// <summary>
    /// 完整路径
    /// </summary>
    public string FullPath => Path.Combine(_directory, _fileGuid);

    /// <summary>
    /// 下载速度
    /// </summary>
    public int SpeedInBytes
    {
        get
        {
            if (_completed)
                return 0;

            int totalSpeeds = _lastSpeeds.Sum();

            return totalSpeeds / 10;
        }
    }

    #endregion
}