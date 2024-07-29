using System;

namespace HmExtension.Commons.Model;

public class Color
{
    /// <summary>
    /// 红色(Red)
    /// </summary>
    public byte R { get; set; }
    /// <summary>
    /// 绿色(Green)
    /// </summary>
    public byte G { get; set; }
    /// <summary>
    /// 蓝色(Blue)
    /// </summary>
    public byte B { get; set; }
    /// <summary>
    /// 透明度(Alpha)
    /// </summary>
    public byte A { get; set; }

    public Color(byte r, byte g, byte b, byte a = 255)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    public Color(string hex)
    {
        var color = FromArgb(hex);
        R = color.R;
        G = color.G;
        B = color.B;
        A = color.A;
    }

    public static string ToHex(Color color)
    {
        return $"#{color.R:X2}{color.G:X2}{color.B:X2}{color.A:X2}";
    }


    public static Color FromArgb(byte a, byte r, byte g, byte b=255)
    {
        return new Color(r, g, b, a);
    }

    public static Color FromArgb(string hex)
    {
        var color = new Color(0,0,0);
        if (hex.StartsWith("#"))
        {
            hex = hex[1..];
        }

        // RRGGBB
        if (hex.Length == 6)
        {
            color.R = byte.Parse(hex[0..2], System.Globalization.NumberStyles.HexNumber);
            color.G = byte.Parse(hex[2..4], System.Globalization.NumberStyles.HexNumber);
            color.B = byte.Parse(hex[4..6], System.Globalization.NumberStyles.HexNumber);
            color.A = 255;
        }
        // RRGGBBAA
        else if (hex.Length == 8)
        {
            color.R = byte.Parse(hex[0..2], System.Globalization.NumberStyles.HexNumber);
            color.G = byte.Parse(hex[2..4], System.Globalization.NumberStyles.HexNumber);
            color.B = byte.Parse(hex[4..6], System.Globalization.NumberStyles.HexNumber);
            color.A = byte.Parse(hex[6..8], System.Globalization.NumberStyles.HexNumber);
        }
        // RGB
        else if (hex.Length == 3)
        {
            color.R = byte.Parse(hex[0] + hex[0].ToString(), System.Globalization.NumberStyles.HexNumber);
            color.G = byte.Parse(hex[1] + hex[1].ToString(), System.Globalization.NumberStyles.HexNumber);
            color.B = byte.Parse(hex[2] + hex[2].ToString(), System.Globalization.NumberStyles.HexNumber);
            color.A = 255;
        }
        // RGBA
        else if (hex.Length == 4)
        {
            color.R = byte.Parse(hex[0] + hex[0].ToString(), System.Globalization.NumberStyles.HexNumber);
            color.G = byte.Parse(hex[1] + hex[1].ToString(), System.Globalization.NumberStyles.HexNumber);
            color.B = byte.Parse(hex[2] + hex[2].ToString(), System.Globalization.NumberStyles.HexNumber);
            color.A = byte.Parse(hex[3] + hex[3].ToString(), System.Globalization.NumberStyles.HexNumber);
        }
        else
        {
            throw new ArgumentException("Invalid hex string");
        }
        return color;
    }
}