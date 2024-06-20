using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HmExtension;

/// <summary>
/// 字节数组扩展类
/// </summary>
public static class ByteArrayExtension
{
    /// <summary>
    /// 将字节数组转换为short
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
    /// </summary>
    /// <param name="value">当前字节数组</param>
    /// <param name="encoder">字符集编码</param>
    /// <returns>字符串</returns>
    public static string FromString(this byte[] value, Encoding encoder = null)
    {
        encoder ??= Encoding.Default;
        return encoder.GetString(value);
    }
}