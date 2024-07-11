using System;
using System.Runtime.InteropServices;
using System.Text;

namespace HmExtension.Commons.utils;

/// <summary>
/// INI文件操作类
/// </summary>
public class IniHelper
{
    private string _filePath;

    /// <summary>
    /// 文件路径
    /// </summary>
    public string Path
    {
        get => this._filePath;
        set
        {
            if (value[..1] == "\\" || value[..1] == "/")
            {
                this._filePath = AppDomain.CurrentDomain + value;
            }
            else
            {
                this._filePath = value;
            }
        }
    }

    [DllImport("kernel32")]
    private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);

    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal,
        int size, string filePath);

    /// <summary>
    /// 文件路径
    /// </summary>
    /// <param name="path">首个字符为\\或/则自动前面加路径</param>
    public IniHelper(string path)
    {
        this.Path = path;
    }

    /// <summary>
    /// 写入INI文件指定KEY的值
    /// </summary>
    /// <param name="section">Section</param>
    /// <param name="key">Key</param>
    /// <param name="value">Value</param>
    public void WriteValue(string section, string key, string value)
    {
        WritePrivateProfileString(section, key, value, this._filePath);
    }

    /// <summary>
    /// 读取INI文件指定KEY的值
    /// </summary>
    /// <param name="section">Section</param>
    /// <param name="key">KEY</param>
    public string ReadValue(string section, string key)
    {
        try
        {
            StringBuilder temp = new StringBuilder(204800);
            int i = GetPrivateProfileString(section, key, "", temp, 204800, this._filePath);

            return temp.ToString();
        }
        catch
        {
            return "";
        }
    }

    ///   删除指定Section。
    ///   <param name="section">section</param>
    ///   <returns> 返回删除是否成功</returns>
    public bool RemoveSection(string section)
    {
        return WritePrivateProfileString(section, null, null, this._filePath);
    }

    /// 验证文件是否存在
    public bool Exists()
    {
        return System.IO.File.Exists(this._filePath);
    }
}