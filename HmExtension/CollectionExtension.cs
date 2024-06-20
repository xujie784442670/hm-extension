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
    /// <example>
    /// <code>
    /// List&lt;int&gt; list = new List&lt;int&gt; {1, 2, 3, 4, 5};
    /// list.Remove(x => x % 2 == 0); // 移除偶数
    /// // list: {1, 3, 5}
    /// </code>
    /// </example>
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
    /// <example>
    /// <code>
    /// List&lt;int&gt; list = new List&lt;int&gt; {1, 2, 3};
    /// List&lt;int&gt; list2 = new List&lt;int&gt; {4, 5, 6};
    /// list.AddAll(list2);
    /// // list: {1, 2, 3, 4, 5, 6}
    /// </code>
    /// </example>
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
}