using System;
using System.Collections.Generic;
using System.Linq;

namespace HmExtension.Standard.Extensions;

/// <summary>
/// 集合扩展类
/// </summary>
public static class CollectionExtension
{
    /// <summary>
    /// 从集合中移除满足条件的元素(注意: 此方法不会修改原集合,而是返回一个新的集合)
    /// <example>
    /// <code>
    /// List&lt;int&gt; list = new List&lt;int&gt; {1, 2, 3, 4, 5};
    /// list = list.Remove(x => x % 2 == 0); // 移除偶数
    /// // list: {1, 3, 5}
    /// </code>
    /// </example>
    /// </summary>
    /// <typeparam name="T">集合泛型</typeparam>
    /// <param name="collection">当前集合</param>
    /// <param name="predicate">删除条件</param>
    public static IEnumerable<T> Remove<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
    {
        foreach (var item in collection)
        {
            if(!predicate(item))
            {
                yield return item;
            }
        }
    }
    /// <summary>
    /// 从集合中移除满足条件的元素
    /// <example>
    /// <code>
    /// List&lt;int&gt; list = new List&lt;int&gt; {1, 2, 3, 4, 5};
    /// list = list.Remove(x => x % 2 == 0); // 移除偶数
    /// // list: {1, 3, 5}
    /// </code>
    /// </example>
    /// </summary>
    /// <typeparam name="T">集合泛型</typeparam>
    /// <param name="collection">当前集合</param>
    /// <param name="predicate">删除条件</param>
    public static void Remove<T>(this ICollection<T> collection, Func<T, bool> predicate)
    {
        foreach (var item in collection)
        {
            if(predicate(item))
            {
                collection.Remove(item);
            }
        }
    }

    /// <summary>
    /// 将指定的集合添加到当前集合中
    /// <example>
    /// <code>
    /// List&lt;int&gt; list = new List&lt;int&gt; {1, 2, 3};
    /// List&lt;int&gt; list2 = new List&lt;int&gt; {4, 5, 6};
    /// list.AddRange(list2);
    /// // list: {1, 2, 3, 4, 5, 6}
    /// </code>
    /// </example>
    /// </summary>
    /// <typeparam name="T">集合泛型</typeparam>
    /// <param name="collection">当前集合</param>
    /// <param name="items">待添加集合</param>
    public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            collection.Add(item);
        }
    }

    /// <summary>
    /// 将指定的集合添加到当前集合中
    /// </summary>
    /// <typeparam name="T">集合泛型</typeparam>
    /// <param name="collection">当前集合</param>
    /// <param name="items">待添加集合</param>
    public static void AddRange<T>(this IEnumerable<T> collection, ICollection<T> items)
    {
        collection.AddRange(items.ToArray());
    }

    /// <summary>
    /// 将集合转换为字符串
    /// <example>
    /// <code>
    /// List&lt;int&gt; list = new List&lt;int&gt; {1, 2, 3};
    /// string str = list.ToString(''); // str=> 123
    /// string str1 = list.ToString('-'); // str1=> 1-2-3
    /// </code>
    /// </example>
    /// </summary>
    /// <typeparam name="T">集合类型</typeparam>
    /// <param name="collection">集合</param>
    /// <param name="separator">拼接时的分隔符</param>
    /// <returns>拼接后字符串</returns>
    public static string ToString<T>(this IEnumerable<T> collection, string separator = ",")
    {
        return string.Join(separator, collection.ToArray());
    }

    /// <summary>
    /// 将集合中的元素转换为字符串,并使用指定的分隔符拼接
    /// </summary>
    /// <example>
    /// <code>
    /// var list = new List&lt;int&gt; {1, 2, 3, 4, 5};
    /// list.Join().Println(); //输出:  1,2,3,4,5
    /// </code>
    /// </example>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection">集合</param>
    /// <param name="separator">分隔符</param>
    /// <returns></returns>
    public static string Join<T>(this IEnumerable<T> collection, string separator = ",")
    {
        return string.Join(separator, collection.ToArray());
    }


    /// <summary>
    /// 将集合中的元素转换为short
    /// </summary>
    /// <param name="collection">集合</param>
    /// <returns></returns>
    public static short ToShort(this IEnumerable<byte> collection)
    {
        return collection.ToArray().ToShort();
    }

    /// <summary>
    /// 将集合中的元素转换为ushort
    /// </summary>
    /// <param name="collection">集合</param>
    /// <returns></returns>
    public static ushort ToUShort(this IEnumerable<byte> collection)
    {
        return collection.ToArray().ToUShort();
    }

    /// <summary>
    /// 将集合中的元素转换为int
    /// </summary>
    /// <param name="collection">集合</param>
    /// <returns></returns>
    public static int ToInt(this IEnumerable<byte> collection)
    {
        return collection.ToArray().ToInt();
    }

    /// <summary>
    /// 将集合中的元素转换为uint
    /// </summary>
    /// <param name="collection">集合</param>
    /// <returns></returns>
    public static uint ToUInt(this IEnumerable<byte> collection)
    {
        return collection.ToArray().ToUInt();
    }

    /// <summary>
    /// 将集合中的元素转换为long
    /// </summary>
    /// <param name="collection">集合</param>
    /// <returns></returns>
    public static long ToLong(this IEnumerable<byte> collection)
    {
        return collection.ToArray().ToLong();
    }

    /// <summary>
    /// 将集合中的元素转换为ulong
    /// </summary>
    /// <param name="collection">集合</param>
    /// <returns></returns>
    public static ulong ToULong(this IEnumerable<byte> collection)
    {
        return collection.ToArray().ToULong();
    }

    /// <summary>
    /// 将集合中的元素转换为float
    /// </summary>
    /// <param name="collection">集合</param>
    /// <returns></returns>
    public static float ToFloat(this IEnumerable<byte> collection)
    {
        return collection.ToArray().ToFloat();
    }

    /// <summary>
    /// 将集合中的元素转换为double
    /// </summary>
    /// <param name="collection">集合</param>
    /// <returns></returns>
    public static double ToDouble(this IEnumerable<byte> collection)
    {
        return collection.ToArray().ToDouble();
    }

    /// <summary>
    /// 将集合中的元素打印到控制台
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection"></param>
    public static void Println<T>(this IEnumerable<T> collection)
    {
        collection.Join().Println();
    }

    /// <summary>
    /// 将集合转换为字典
    /// </summary>
    /// <param name="collection">集合</param>
    /// <param name="keySelector">集合的Key的计算方式</param>
    /// <param name="valueSelector">集合值的计算方式</param>
    /// <typeparam name="TK">字典Key的类型</typeparam>
    /// <typeparam name="TV">字典Value的类型</typeparam>
    /// <typeparam name="TS">集合值类型</typeparam>
    /// <returns></returns>
    public static Dictionary<TK,TV> ToDictionary<TK,TV,TS>(this IEnumerable<TS> collection, Func<TS,TK> keySelector, Func<TS,TV> valueSelector) => collection.ToDictionary(keySelector, valueSelector);

    /// <summary>
    /// 将集合转换为字典,将集合中元素作为字典的Value
    /// </summary>
    /// <param name="collection">集合</param>
    /// <param name="keySelector">集合的Key的计算方式</param>
    /// <typeparam name="TK">字典Key的类型</typeparam>
    /// <typeparam name="TV">字典Value的类型</typeparam>
    /// <returns></returns>
    public static Dictionary<TK, TV>
        ToDictionary<TK, TV>(this IEnumerable<TV> collection, Func<TV, TK> keySelector) =>
        ToDictionary(collection, keySelector, s => s);
    /// <summary>
    /// 将集合转换为字典,将集合中元素作为字典的Value,Key为元素的指定属性值
    /// </summary>
    /// <typeparam name="TK">字典Key的类型</typeparam>
    /// <typeparam name="TV">字典Value的类型</typeparam>
    /// <param name="collection">集合</param>
    /// <param name="propertyName">属性名称</param>
    /// <returns></returns>
    public static Dictionary<TK, TV> ToDictionary<TK, TV>(this IEnumerable<TV> collection,string propertyName) =>
        collection.ToDictionary(s => s.GetPropertyValue<TK>(propertyName));
}