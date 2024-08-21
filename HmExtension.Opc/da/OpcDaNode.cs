using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GodSharp.Opc.Da;
using Opc.Ua.Server;

namespace HmExtension.Opc.da;

/// <summary>
/// OpcDa节点
/// </summary>
public class OpcDaNode : IOpcNode
{
    /// <summary>
    /// 客户端
    /// </summary>
    public readonly HmOpcDaClient Client;

    /// <summary>
    /// 浏览节点
    /// </summary>
    public readonly BrowseNode BrowseNode;

    /// <summary>
    /// 子节点
    /// </summary>
    public readonly List<IOpcNode> Nodes = [];

    /// <summary>
    /// 数据变化事件
    /// </summary>
    public event Action<string, TagValue, TagValue> OnDataChange;

    /// <summary>
    /// 子节点数量
    /// </summary>
    public int Length => Nodes.Count;

    /// <summary>
    /// 节点全名称
    /// </summary>
    public string ItemName
    {
        get => BrowseNode.Full;
        set { }
    }

    /// <summary>
    /// 节点名称
    /// </summary>
    public string Name
    {
        get => BrowseNode.Name;
        set { }
    }

    private bool _isSubscription;

    /// <summary>
    /// 是否订阅
    /// </summary>
    public bool IsSubscription
    {
        get => _isSubscription;
        set { }
    }

    /// <summary>
    /// 是否叶子节点
    /// </summary>
    public bool IsLeaf
    {
        get => BrowseNode.IsLeaf;
        set { }
    }

    /// <summary>
    /// 子节点索引器
    /// </summary>
    /// <param name="index"></param>
    public IOpcNode this[int index] => Nodes[index];

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="client"></param>
    /// <param name="browseNode"></param>
    public OpcDaNode(HmOpcDaClient client, BrowseNode browseNode)
    {
        Client = client;
        BrowseNode = browseNode;
        InitChildes();
    }

    private void InitChildes()
    {
        if (BrowseNode?.Childs != null)
        {
            foreach (var browseNodeChild in BrowseNode.Childs)
            {
                AddChildNode(new OpcDaNode(Client, browseNodeChild));
            }
        }
    }

    /// <summary>
    /// 获取迭代器
    /// </summary>
    /// <returns></returns>
    public IEnumerator<IOpcNode> GetEnumerator()
    {
        return Nodes.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// 添加到分组
    /// </summary>
    /// <param name="group">分组名称</param>
    public void AppendToGroup(string group)
    {
        Client.AppendToGroup(ItemName, group);
    }

    /// <summary>
    /// 添加子节点
    /// </summary>
    /// <param name="node"></param>
    public void AddChildNode(IOpcNode node)
    {
        Nodes.Add(node);
    }

    /// <summary>
    /// 移除子节点
    /// </summary>
    /// <param name="node"></param>
    public void RemoveChildNode(IOpcNode node)
    {
        Nodes.Remove(node);
    }

    /// <summary>
    /// 读取值
    /// </summary>
    /// <param name="group"></param>
    /// <returns></returns>
    public TagValue ReadValue(string group = "default")
    {
        return !IsLeaf ? default : Client.ReadNode(group, ItemName);
    }

    /// <summary>
    /// 读取节点值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="group"></param>
    /// <returns></returns>
    public T ReadValue<T>(string group = "default")
    {
        return !IsLeaf ? default : Client.ReadNode<T>(group, ItemName);
    }

    /// <summary>
    /// 写入值
    /// </summary>
    /// <param name="value"></param>
    /// <param name="group"></param>
    public void WriteValue(object value, string group = "default")
    {
        if (IsLeaf)
        {
            Client.WriteNode(group, ItemName, value);
        }
    }

    /// <summary>
    /// 异步读取值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="group"></param>
    /// <returns></returns>
    public Task<T> ReadValueAsync<T>(string group = "default")
    {
        return !IsLeaf ? default : Client.ReadNodeAsync<T>(group, ItemName);
    }

    /// <summary>
    /// 异步写入值
    /// </summary>
    /// <param name="value"></param>
    /// <param name="group"></param>
    /// <returns></returns>
    public Task WriteValueAsync(object value, string group = "default")
    {
        return !IsLeaf ? default : Client.WriteNodeAsync(group, ItemName, value);
    }

    /// <summary>
    /// 订阅节点
    /// </summary>
    /// <param name="group"></param>
    /// <param name="updateRate"></param>
    public void Subscription(string group = "default", int updateRate = 100)
    {
        _isSubscription = true;
        Client.AddGroup(group, true, updateRate);
        Client.Subscription(group, ItemName);
        Client.OnDataChange += (g, itemName, oldValue, newValue) =>
        {
            if (IsSubscription)
            {
                if (group == null || g == group && itemName == ItemName)
                {
                    OnDataChange?.Invoke(group, oldValue, newValue);
                }
            }
        };
    }

    /// <summary>
    /// 取消订阅
    /// </summary>
    /// <param name="group"></param>
    public void UnSubscription(string group = "default")
    {
        _isSubscription = false;
        Client.UnSubscription(group, ItemName);
    }
}