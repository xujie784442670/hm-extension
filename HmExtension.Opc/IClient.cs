using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GodSharp.Opc.Da;
using Opc.Ua;

namespace HmExtension.Opc;

public interface IClient
{
    #region 公共属性

    /// <summary>
    /// 主机地址
    /// </summary>
    public string Host { get; set; }

    /// <summary>
    /// 服务名称
    /// </summary>
    public string Server { get; set; }

    /// <summary>
    /// 连接状态
    /// </summary>
    public bool IsConnected { get; set; }

    #endregion

    #region 事件

    /// <summary>
    /// 数据变化事件
    /// </summary>
    public event Action<string, string, TagValue, TagValue> OnDataChange;

    /// <summary>
    /// 连接状态变化事件
    /// </summary>
    public event Action<bool> OnConnectChanged;

    #endregion

    #region API方法

    /// <summary>
    /// 获取服务列表
    /// </summary>
    /// <param name="host">主机地址</param>
    /// <returns></returns>
    public string[] GetServers(string host = "localhost");

    /// <summary>
    /// 连接
    /// </summary>
    /// <returns></returns>
    public Task Connect();
    /// <summary>
    /// 将节点添加到分组
    /// </summary>
    /// <param name="itemName">节点名称</param>
    /// <param name="group">分组名称</param>
    public void AppendToGroup(string itemName,string group= "default");

    /// <summary>
    /// 浏览节点
    /// </summary>
    /// <param name="itemName">根节点名称</param>
    /// <param name="includeChild">是否包含子节点</param>
    /// <param name="isLeaf">是否是叶子节点</param>
    /// <returns></returns>
    public List<IOpcNode> Browses(string itemName = null, bool includeChild = true, bool isLeaf = false);

    /// <summary>
    /// 浏览节点树
    /// </summary>
    /// <param name="itemName">根节点名称</param>
    /// <returns></returns>
    public List<IOpcNode> BrowseTrees(string itemName = null);

    /// <summary>
    /// 获取节点信息
    /// </summary>
    /// <param name="itemName"></param>
    /// <returns></returns>
    public IOpcNode GetOpcNode(string itemName);

    /// <summary>
    /// 断开连接
    /// </summary>
    public void Disconnect();

    /// <summary>
    /// 读取节点
    /// </summary>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <param name="group">分组名称</param>
    /// <param name="itemName">节点名称</param>
    /// <returns></returns>
    public T ReadNode<T>(string group, string itemName);

    /// <summary>
    /// 读取节点
    /// </summary>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <param name="group">分组名称</param>
    /// <param name="itemName">节点名称</param>
    /// <returns></returns>
    public Task<T> ReadNodeAsync<T>(string group, string itemName);

    /// <summary>
    /// 写入节点
    /// </summary>
    /// <param name="group">分组名称</param>
    /// <param name="itemName">节点名称</param>
    /// <param name="value">节点值</param>
    public void WriteNode(string group, string itemName, object value);

    /// <summary>
    /// 写入节点
    /// </summary>
    /// <param name="group">分组名称</param>
    /// <param name="itemName">节点名称</param>
    /// <param name="value">节点值</param>
    public Task WriteNodeAsync(string group, string itemName, object value);

    /// <summary>
    /// 添加分组
    /// </summary>
    /// <param name="groupName">分组名称</param>
    /// <param name="isSubscribed">是否订阅</param>
    /// <param name="updateRate">更新频率</param>
    public void AddGroup(string groupName, bool isSubscribed = false, int updateRate = 100);

    /// <summary>
    /// 添加分组
    /// </summary>
    /// <param name="group">分组信息</param>
    public void AddGroup(Group group);

    /// <summary>
    /// 订阅节点
    /// </summary>
    /// <param name="group">分组名称</param>
    /// <param name="itemNames">节点名称列表</param>
    /// <returns></returns>
    public bool Subscription(string group, params string[] itemNames);

    /// <summary>
    /// 取消订阅节点
    /// </summary>
    /// <param name="group">分组名称</param>
    /// <param name="itemNames">节点名称列表</param>
    /// <returns></returns>
    public void UnSubscription(string group, params string[] itemNames);

    #endregion
}