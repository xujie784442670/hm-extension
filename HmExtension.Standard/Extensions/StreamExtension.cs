using System.IO;
using System.Text;

namespace HmExtension.Standard.Extensions;

/// <summary>
/// 文件扩展类(v1.0.0.3)
/// </summary>
public static class StreamExtension
{
    /// <summary>
    /// 读取流中的所有字节
    /// </summary>
    /// <param name="stream">当前流</param>
    /// <returns>字节数组</returns>
    public static byte[] ReadAllBytes(this Stream stream)
    {
        using var reader = new StreamReader(stream);
        // 读取流
        byte[] bytes = new byte[stream.Length];
        int len = 0;
        int loaded = 0;
        do
        {
            len = stream.Read(bytes, loaded, bytes.Length - loaded);
        } while (len != -1);

        return bytes;
    }

    /// <summary>
    /// 读取流中的所有文本
    /// </summary>
    /// <param name="stream">当前流</param>
    /// <param name="encoding">字符集编码,默认为当前系统默认编码集</param>
    /// <returns>字符串</returns>
    public static string ReadAllText(this Stream stream, Encoding encoding = default)
    {
        byte[] bytes = stream.ReadAllBytes();
        return bytes.FromString(encoding);
    }

    /// <summary>
    /// 对流进行MD5签名
    /// </summary>
    /// <param name="stream">当前流</param>
    /// <returns>签名</returns>
    public static byte[] ToMd5(this Stream stream)
    {
        byte[] bytes = stream.ReadAllBytes();
        return bytes.ToMd5();
    }

    /// <summary>
    /// 对流进行MD5签名
    /// </summary>
    /// <param name="stream">当前流</param>
    /// <returns>签名</returns>
    public static string ToMd5Hex(this Stream stream)
    {
        return stream.ToMd5().ToHexString();
    }

    /// <summary>
    /// 将流转换为Base64字符串
    /// </summary>
    /// <param name="stream">当前流</param>
    /// <returns>Base64字符串</returns>
    public static string ToBase64(this Stream stream)
    {
        byte[] bytes = stream.ReadAllBytes();
        return bytes.ToBase64();
    }
}