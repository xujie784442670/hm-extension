using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HmExtension;
/// <summary>
/// 集合扩展类
/// </summary>
public static class CollectionExtension
{
    /// <summary>
    /// 从集合中移除满足条件的元素
    /// </summary>
    /// <typeparam name="T">集合泛型</typeparam>
    /// <param name="collection">当前集合</param>
    /// <param name="predicate">删除条件</param>
    public static void Remove<T>(this ICollection<T> collection, Func<T, bool> predicate)
    {
        var items = collection.Where(predicate).ToList();
        foreach (var item in items)
        {
            collection.Remove(item);
        }
    }
    /// <summary>
    /// 将指定的集合添加到当前集合中
    /// </summary>
    /// <typeparam name="T">集合泛型</typeparam>
    /// <param name="collection">当前集合</param>
    /// <param name="items">待添加集合</param>
    public static void AddAll<T>(this ICollection<T> collection, IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            collection.Add(item);
        }
    }
}