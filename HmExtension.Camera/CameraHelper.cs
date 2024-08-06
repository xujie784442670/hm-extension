using System;
using System.Linq;
using OpenCvSharp;
using 摄像头枚举工具;

namespace HmExtension.Camera;
/// <summary>
/// 摄像头帮助类
/// </summary>
public class CameraHelper
{
    private static VideoCapture _capture;

    /// <summary>
    /// 当前活动摄像头
    /// </summary>
    private static int CameraIndex { get; set; } = -1;

    /// <summary>
    /// 摄像头列表
    /// </summary>
    /// <returns></returns>
    public static string[] CameraNames => CameraDevices.Devices.ToArray();

    /// <summary>
    /// 打开摄像头
    /// </summary>
    /// <param name="index">摄像头索引</param>
    /// <exception cref="IndexOutOfRangeException"></exception>
    /// <exception cref="Exception"></exception>
    public static void OpenCamera(int index)
    {
        if (index < 0 || index >= CameraNames.Length)
        {
            throw new IndexOutOfRangeException("摄像头索引超出范围");
        }

        if (CameraIndex != -1)
        {
            throw new Exception($"已有活动摄像头: {CameraNames[CameraIndex]}({CameraIndex}) 请关闭后在打开新的摄像头");
        }
        _capture = new VideoCapture(index);
        CameraIndex = index;
        if (!_capture.IsOpened())
        {
            throw new Exception("摄像头打开失败");
        }
    }
    /// <summary>
    /// 读取一帧图像
    /// </summary>
    /// <returns></returns>
    public static byte[] ReadFame()
    { 
        using var frame = new Mat();
        _capture.Read(frame);
        return frame.ToBytes();
    }

    /// <summary>
    /// 关闭摄像头
    /// </summary>
    public static void CloseCamera()
    {
        _capture?.Release();
        CameraIndex = -1;
        _capture = null;
    }
}