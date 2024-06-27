using System;

namespace HmExtension.Standard.Commons;

/// <summary>
/// 点
/// </summary>
/// <typeparam name="T">表示坐标值的类型,必须为数字</typeparam>
public class Point<T> where T : struct
{
    /// <summary>
    /// X坐标
    /// </summary>
    public T X { get; set; }

    /// <summary>
    /// Y坐标
    /// </summary>
    public T Y { get; set; }

    /// <summary>
    /// 初始化一个新的实例
    /// </summary>
    public Point()
    {
    }

    /// <summary>
    /// 初始化一个新的实例
    /// </summary>
    /// <param name="x">X坐标</param>
    /// <param name="y">Y坐标</param>
    public Point(T x, T y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// 获取两点之间的距离
    /// </summary>
    /// <param name="point">目标点</param>
    /// <returns></returns>
    public int GetDistance(Point<T> point)
    {
        // 两点之间的距离公式: √(x2-x1)²+(y2-y1)²
        return (int)Math.Sqrt(Math.Pow(Convert.ToDouble(point.X) - Convert.ToDouble(X), 2) +
                              Math.Pow(Convert.ToDouble(point.Y) - Convert.ToDouble(Y), 2));
    }
    /// <summary>
    /// 获取两点之间的距离
    /// </summary>
    /// <param name="x">横坐标</param>
    /// <param name="y">纵坐标</param>
    /// <returns></returns>
    public int GetDistance(T x, T y)
    {
        return GetDistance(new Point<T>(x,y));
    }

    /// <summary>
    /// 获取两点之间的角度
    /// </summary>
    /// <param name="point">目标点</param>
    /// <returns>角度</returns>
    public int GetAngle(Point<T> point)
    {
        // 两点之间的角度公式: arctan((y2-y1)/(x2-x1))
        int angle = (int)Math.Atan2(Convert.ToDouble(point.Y) - Convert.ToDouble(Y),
            Convert.ToDouble(point.X) - Convert.ToDouble(X));
        // 将弧度转换为角度
        return angle * 180 / (int)Math.PI;
    }
    /// <summary>
    /// 获取两点之间的角度
    /// </summary>
    /// <param name="x">横坐标</param>
    /// <param name="y">纵坐标</param>
    /// <returns></returns>
    public int GetAngle(T x, T y)
    {
        return GetAngle(new Point<T>(x, y));
    }
}