using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GodSharp.Opc.Da;

namespace HmExtension.Opc;

/// <summary>
/// Opc节点接口
/// </summary>
public interface IOpcNode : IEnumerable<IOpcNode>
{
    #region 公共事件

    /// <summary>
    /// 数据变化事件
    /// <para>
    ///    Action&lt;分组名称, 旧值, 新值&gt;
    /// </para>
    /// </summary>
    public event Action<string, TagValue, TagValue> OnDataChange;

    #endregion

    #region 公共属性

    /// <summary>
    /// 节点全名称
    /// </summary>
    public string ItemName { get; set; }

    /// <summary>
    /// 节点名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 是否订阅
    /// </summary>
    public bool IsSubscription { get; set; }

    /// <summary>
    /// 是否叶子节点
    /// </summary>
    public bool IsLeaf { get; set; }

    #endregion

    #region 公共方法
    /// <summary>
    /// 添加到分组
    /// </summary>
    /// <param name="group">分组名称</param>
    public void AppendToGroup(string group);

    /// <summary>
    /// 添加子节点
    /// </summary>
    /// <param name="node"></param>
    public void AddChildNode(IOpcNode node);

    /// <summary>
    /// 移除子节点
    /// </summary>
    /// <param name="node"></param>
    public void RemoveChildNode(IOpcNode node);

    /// <summary>
    /// 读取值
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    /// <param name="group">分组名称</param>
    /// <returns></returns>
    public T ReadValue<T>(string group = "default");

    /// <summary>
    /// 写入值
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="group">分组名称</param>
    public void WriteValue(object value, string group = "default");

    /// <summary>
    /// 异步读取值
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    /// <param name="group">分组名称</param>
    /// <returns></returns>
    public Task<T> ReadValueAsync<T>(string group = "default");

    /// <summary>
    /// 异步写入值
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="group">分组名称</param>
    /// <returns></returns>
    public Task WriteValueAsync(object value, string group = "default");

    /// <summary>
    /// 订阅节点
    /// </summary>
    /// <param name="group">分组名称</param>
    /// <param name="updateRate">更新频率</param>
    public void Subscription(string group = "default", int updateRate = 100);

    /// <summary>
    /// 取消订阅节点
    /// </summary>
    /// <param name="group">分组名称</param>
    public void UnSubscription(string group = "default");

    #endregion
}