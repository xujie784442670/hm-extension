using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace HmExtension.Standard.Extensions;

/// <summary>
/// 字节数组扩展类
/// </summary>
public static class ByteArrayExtension
{
    /// <summary>
    /// 将字节数组转换为short
    /// <example>
    /// <code>
    /// byte[] arr = {0x01, 0x02};
    /// short s = arr.ToShort();
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">当前字节数组</param>
    /// <param name="startIndex">起始位置,默认为0</param>
    /// <returns>short</returns>
    public static short ToShort(this byte[] value, int startIndex = 0)
    {
        // 检查字节数组长度是否足够
        if (value.Length < startIndex + 2)
        {
            throw new ArgumentException("字节数组长度不足");
        }

        return BitConverter.ToInt16(value, startIndex);
    }

    /// <summary>
    /// 将字节数组转换为int
    /// <example>
    /// <code>
    /// byte[] arr = {0x01, 0x02,0x3,0x4};
    /// int s = arr.ToInt();
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">当前字节数组</param>
    /// <param name="startIndex">起始位置,默认为0</param>
    /// <returns>int</returns>
    /// <exception cref="ArgumentException"></exception>
    public static int ToInt(this byte[] value, int startIndex = 0)
    {
        // 检查字节数组长度是否足够
        if (value.Length < startIndex + 4)
        {
            throw new ArgumentException("字节数组长度不足");
        }

        return BitConverter.ToInt32(value, startIndex);
    }

    /// <summary>
    /// 将字节数组转换为long
    /// <example>
    /// <code>
    /// byte[] arr = {0x01,0x02,0x3,0x4,0x5,0x6,0x7,0x8};
    /// long s = arr.ToLong();
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">当前字节数组</param>
    /// <param name="startIndex">起始位置,默认为0</param>
    /// <returns>long</returns>
    /// <exception cref="ArgumentException"></exception>
    public static long ToLong(this byte[] value, int startIndex = 0)
    {
        // 检查字节数组长度是否足够
        if (value.Length < startIndex + 8)
        {
            throw new ArgumentException("字节数组长度不足");
        }

        return BitConverter.ToInt64(value, startIndex);
    }

    /// <summary>
    /// 将字节数组转换为float
    /// <example>
    /// <code>
    /// byte[] arr = {0x01,0x02,0x3,0x4};
    /// float s = arr.ToFloat();
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">当前字节数组</param>
    /// <param name="startIndex">起始位置,默认为0</param>
    /// <returns>float</returns>
    /// <exception cref="ArgumentException"></exception>
    public static float ToFloat(this byte[] value, int startIndex = 0)
    {
        // 检查字节数组长度是否足够
        if (value.Length < startIndex + 4)
        {
            throw new ArgumentException("字节数组长度不足");
        }

        return BitConverter.ToSingle(value, startIndex);
    }

    /// <summary>
    /// 将字节数组转换为double
    /// <example>
    /// <code>
    /// byte[] arr = {0x01,0x02,0x3,0x4,0x5,0x6,0x7,0x8};
    /// double s = arr.ToDouble();
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">当前字节数组</param>
    /// <param name="startIndex">起始位置,默认为0</param>
    /// <returns>double</returns>
    /// <exception cref="ArgumentException"></exception>
    public static double ToDouble(this byte[] value, int startIndex = 0)
    {
        // 检查字节数组长度是否足够
        if (value.Length < startIndex + 8)
        {
            throw new ArgumentException("字节数组长度不足");
        }

        return BitConverter.ToDouble(value, startIndex);
    }

    /// <summary>
    /// 将字节数组转换为ushort
    /// <example>
    /// <code>
    /// byte[] arr = {0x01,0x02};
    /// ushort s = arr.ToUShort();
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">当前字节数组</param>
    /// <param name="startIndex">起始位置,默认为0</param>
    /// <returns>ushort</returns>
    /// <exception cref="ArgumentException"></exception>
    public static ushort ToUShort(this byte[] value, int startIndex = 0)
    {
        // 检查字节数组长度是否足够
        if (value.Length < startIndex + 2)
        {
            throw new ArgumentException("字节数组长度不足");
        }

        return BitConverter.ToUInt16(value, startIndex);
    }

    /// <summary>
    /// 将字节数组转换为uint
    /// <example>
    /// <code>
    /// byte[] arr = {0x01,0x02,0x3,0x4};
    /// uint s = arr.ToUInt();
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">当前字节数组</param>
    /// <param name="startIndex">起始位置,默认为0</param>
    /// <returns>uint</returns>
    /// <exception cref="ArgumentException"></exception>
    public static uint ToUInt(this byte[] value, int startIndex = 0)
    {
        // 检查字节数组长度是否足够
        if (value.Length < startIndex + 4)
        {
            throw new ArgumentException("字节数组长度不足");
        }

        return BitConverter.ToUInt32(value, startIndex);
    }

    /// <summary>
    /// 将字节数组转换为ulong
    /// <example>
    /// <code>
    /// byte[] arr = {0x01,0x02,0x3,0x4,0x5,0x6,0x7,0x8};
    /// ulong s = arr.ToULong();
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">当前字节数组</param>
    /// <param name="startIndex">起始位置,默认为0</param>
    /// <returns>ulong</returns>
    /// <exception cref="ArgumentException"></exception>
    public static ulong ToULong(this byte[] value, int startIndex = 0)
    {
        // 检查字节数组长度是否足够
        if (value.Length < startIndex + 8)
        {
            throw new ArgumentException("字节数组长度不足");
        }

        return BitConverter.ToUInt64(value, startIndex);
    }

    /// <summary>
    /// 使用当前字节数组计算Modbus CRC
    /// <example>
    /// <code>
    /// byte[] arr = {0x01,0x02,0x3,0x4};
    /// ushort crc = arr.GetModbusCRC();
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">当前字节数组</param>
    /// <returns>Modbus CRC</returns>
    public static ushort GetModbusCRC(this byte[] value)
    {
        ushort crc = 0xFFFF;
        for (int i = 0; i < value.Length; i++)
        {
            crc ^= value[i];
            for (int j = 0; j < 8; j++)
            {
                if ((crc & 0x0001) == 1)
                {
                    crc >>= 1;
                    crc ^= 0xA001;
                }
                else
                {
                    crc >>= 1;
                }
            }
        }

        return crc;
    }

    /// <summary>
    /// 使用当前字节数组计算Modbus CRC,并返回字节数组
    /// <example>
    /// <code>
    /// byte[] arr = {0x01,0x02,0x3,0x4};
    /// byte[] crc = arr.GetModbusCRCBytes();
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">当前字节数组</param>
    /// <returns>CRC数组</returns>
    public static byte[] GetModbusCRCBytes(this byte[] value)
    {
        ushort crc = GetModbusCRC(value);
        return BitConverter.GetBytes(crc);
    }

    /// <summary>
    /// 将Modbus CRC追加到当前字节数组末尾
    /// <example>
    /// <code>
    /// byte[] arr = {0x01,0x02,0x3,0x4};
    /// byte[] crc = arr.AppendModbusCrc();
    /// // crc = {0x01,0x02,0x3,0x4,0x5,0x6}
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">当前字节数组</param>
    /// <param name="isBigEndian">CRC模式,默认为BigEndian</param>
    /// <returns>包含CRC的新数组</returns>
    public static byte[] AppendModbusCrc(this byte[] value, bool isBigEndian = true)
    {
        byte[] crcBytes = GetModbusCRCBytes(value);
        if (isBigEndian)
        {
            return value.Concat(crcBytes).ToArray();
        }
        else
        {
            return value.Concat(crcBytes.Reverse()).ToArray();
        }
    }

    /// <summary>
    /// 将字节数组转换为16进制字符串
    /// <example>
    /// <code>
    /// byte[] arr = {0x01,0x02,0x3,0x4};
    /// string hex = arr.ToHexString("-"); // 01-02-03-04
    /// string hex2 = arr.ToHexString(); // 01020304
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">当前字节数组</param>
    /// <param name="separator">分割符,默认为空字符串</param>
    /// <returns>16进制字符串</returns>
    public static string ToHexString(this byte[] value, string separator = "")
    {
        return BitConverter.ToString(value).Replace("-", separator);
    }

    /// <summary>
    /// 将字节数组转换为字符串
    /// <example>
    /// <code>
    /// byte[] arr = {0x30,0x31,0x32,0x33};
    /// string str = arr.ToString(Encoding.UTF8); // "1234"
    /// string str2 = arr.ToString(); // "1234"
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">当前字节数组</param>
    /// <param name="encoder">字符集编码</param>
    /// <returns>字符串</returns>
    public static string FromString(this byte[] value, Encoding encoder = null)
    {
        encoder ??= Encoding.Default;
        return encoder.GetString(value);
    }
    /// <summary>
    /// 将字节数组转换为MD5(v1.0.0.3)
    /// </summary>
    /// <param name="value">当前字节数组</param>
    /// <returns>MD5字节数组</returns>
    public static byte[] ToMd5(this byte[] value)
    {
        using var md5 = System.Security.Cryptography.MD5.Create();
        return md5.ComputeHash(value);
    }

    /// <summary>
    /// 将字节数组转换为Base64字符串
    /// </summary>
    /// <param name="value">当前字节数组</param>
    /// <returns>Base64字符串</returns>
    public static string ToBase64(this byte[] value)
    {
        return Convert.ToBase64String(value);
    }

    /// <summary>
    /// 将字节数组转换为Bitmap
    /// </summary>
    /// <param name="value">当前字节数组</param>
    /// <returns>Bitmap</returns>
    public static Bitmap ToBitmap(this byte[] value)
    {
        using var stream = new MemoryStream(value);
        return new Bitmap(stream);
    }
}