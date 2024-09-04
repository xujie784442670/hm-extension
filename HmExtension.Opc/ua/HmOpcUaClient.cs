using System;
using System.Collections.Generic;
using System.Linq;
using Opc.Ua;
using OpcUaHelper;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using GodSharp.Opc.Da;
using Opc.Ua.Client;

namespace HmExtension.Opc.ua;

public class HmOpcUaClient:IClient
{
    #region 事件

    /// <summary>
    /// 连接状态变更事件
    /// </summary>
    public event Action<bool> OnConnectStateChanged;

    // public event Action<string, MonitoredItem, MonitoredItemNotificationEventArgs> OnDataChange;

    #endregion

    private readonly Dictionary<string, Action<string, MonitoredItem, MonitoredItemNotificationEventArgs>>
        subscriptionHandler =
            new();


    public OpcUaClient Client { get; private set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public bool IsAnonymous { get; set; } = false;

    public string CertificatePath { get; set; }

    public string CertificatePassword { get; set; }

    public bool IsConnected => Client.Connected;

    /// <summary>
    /// 是否使用证书连接
    /// </summary>
    public bool IsCertificate { get; set; } = false;

    public X509KeyStorageFlags CertificateType { get; set; } =
        X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable;

    public HmOpcUaClient()
    {
        Client = new OpcUaClient();
        // 连接完成
        Client.ConnectComplete += (sender, e) => { OnConnectStateChanged?.Invoke(IsConnected); };
        // 重连
        Client.ReconnectComplete += (sender, e) => { OnConnectStateChanged?.Invoke(IsConnected); };
    }

    private UserIdentity GetUserIdentity()
    {
        // 匿名
        if (IsAnonymous)
        {
            return new UserIdentity(new AnonymousIdentityToken());
        }

        // 证书
        if (IsCertificate)
        {
            return new UserIdentity(new X509Certificate2(CertificatePath, CertificatePassword, CertificateType));
        }

        // 用户名密码
        return new UserIdentity(Username, Password);
    }

    public Task Connect(string url)
    {
        // 设置用户身份
        Client.UserIdentity = GetUserIdentity();

        return Client.ConnectServer(url);
    }

    public async Task<OpcNode> LoadChild(OpcNode opcNode)
    {
        var nodes = Browses(opcNode);
        foreach (var node in nodes)
        {
            opcNode.AddChildNode(await LoadChild(node));
        }

        return opcNode;
    }

    /// <summary>
    /// 浏览节点
    /// </summary>
    /// <param name="parentNode">父节点</param>
    /// <returns></returns>
    public List<OpcNode> Browses(OpcNode parentNode)
    {
        // if (parentNode == null) throw new NullReferenceException("父节点不能为null");
        // return Browses(parentNode.NodeId);
        return null;
    }

    /// <summary>
    /// 浏览节点
    /// </summary>
    /// <param name="nodeId"></param>
    /// <param name="isAwaitChild">是否等待子节点加载完成</param>
    /// <returns></returns>
    public List<IOpcNode> Browses(NodeId nodeId = null)
    {
        // nodeId ??= ObjectIds.ObjectsFolder;
        // // 查询所有节点
        // // find all of the components of the node.
        // BrowseDescription nodeToBrowse1 = new BrowseDescription
        // {
        //     NodeId = nodeId,
        //     BrowseDirection = BrowseDirection.Forward,
        //     ReferenceTypeId = ReferenceTypeIds.Aggregates,
        //     IncludeSubtypes = true,
        //     NodeClassMask = (uint)(NodeClass.Object | NodeClass.Variable | NodeClass.Method |
        //                            NodeClass.ReferenceType | NodeClass.ObjectType | NodeClass.View |
        //                            NodeClass.VariableType | NodeClass.DataType),
        //     ResultMask = (uint)BrowseResultMask.All
        // };
        //
        // // find all nodes organized by the node.
        // BrowseDescription nodeToBrowse2 = new BrowseDescription
        // {
        //     NodeId = nodeId,
        //     BrowseDirection = BrowseDirection.Forward,
        //     ReferenceTypeId = ReferenceTypeIds.Organizes,
        //     IncludeSubtypes = true,
        //     NodeClassMask = (uint)(NodeClass.Object | NodeClass.Variable | NodeClass.Method | NodeClass.View |
        //                            NodeClass.ReferenceType | NodeClass.ObjectType | NodeClass.VariableType |
        //                            NodeClass.DataType),
        //     ResultMask = (uint)BrowseResultMask.All
        // };
        //
        // BrowseDescriptionCollection nodesToBrowse =
        // [
        //     nodeToBrowse1,
        //     nodeToBrowse2
        //     // fetch references from the server.
        // ];
        // // fetch references from the server.
        // var descriptionCollection = FormUtils.Browse(Client.Session, nodesToBrowse, false);
        // var nodes = new List<OpcNode>();
        // foreach (var description in descriptionCollection)
        // {
        //     var opcNode = new OpcNode(this, description);
        //     nodes.Add(opcNode);
        // }

        // return nodes;
        return null;
    }

    /// <summary>
    /// 读取节点
    /// </summary>
    /// <param name="opcNode"></param>
    /// <returns></returns>
    public DataValue ReadNode(OpcNode opcNode)
    {
        // return Client.ReadNode(opcNode.NodeId);
        return null;
    }

    /// <summary>
    /// 读取节点
    /// </summary>
    /// <param name="nodeId"></param>
    /// <returns></returns>
    public DataValue ReadNode(NodeId nodeId)
    {
        return Client.ReadNode(nodeId);
    }

    /// <summary>
    /// 读取节点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="opcNode"></param>
    /// <returns></returns>
    public T ReadNode<T>(OpcNode opcNode)
    {
        return ReadNode<T>(opcNode.Tag);
    }

    /// <summary>
    /// 读取节点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="tag"></param>
    /// <returns></returns>
    public T ReadNode<T>(string tag)
    {
        return Client.ReadNode<T>(tag);
    }

    /// <summary>
    /// 异步读取节点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="opcNode"></param>
    /// <returns></returns>
    public Task<T> ReadNodeAsync<T>(OpcNode opcNode)
    {
        return ReadNodeAsync<T>(opcNode.Tag);
    }

    /// <summary>
    /// 异步读取节点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="tag"></param>
    /// <returns></returns>
    public Task<T> ReadNodeAsync<T>(string tag)
    {
        return Client.ReadNodeAsync<T>(tag);
    }

    /// <summary>
    /// 写入节点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="opcNode"></param>
    /// <param name="value"></param>
    public void WriteNode<T>(OpcNode opcNode, T value)
    {
        WriteNode<T>(opcNode.Tag, value);
    }

    /// <summary>
    /// 写入节点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="tag"></param>
    /// <param name="value"></param>
    public void WriteNode<T>(string tag, T value)
    {
        Client.WriteNode(tag, value);
    }

    /// <summary>
    /// 异步写入节点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="opcNode"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public Task WriteNodeAsync<T>(OpcNode opcNode, T value)
    {
        return WriteNodeAsync<T>(opcNode.Tag, value);
    }

    /// <summary>
    /// 异步写入节点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="tag"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public Task WriteNodeAsync<T>(string tag, T value)
    {
        return Client.WriteNodeAsync(tag, value);
    }

    /// <summary>
    /// 写入多个节点
    /// </summary>
    /// <param name="tags"></param>
    /// <param name="values"></param>
    public void WriteNodes(string[] tags, object[] values)
    {
        Client.WriteNodes(tags, values);
    }

    private void OnDataChangeHandler(string groupName, MonitoredItem monitoredItem,
        MonitoredItemNotificationEventArgs e)
    {
        // OnDataChange?.Invoke(groupName, monitoredItem, e);
        // if (subscriptionHandler.TryGetValue(groupName, out var handler))
        // {
        //     handler?.Invoke(groupName, monitoredItem, e);
        // }
    }

    public void AddSubscription(string groupName,
        Action<string, MonitoredItem, MonitoredItemNotificationEventArgs> callback, params OpcNode[] nodes)
    {
        subscriptionHandler.Add(groupName, callback);
        Client.AddSubscription(groupName, nodes.Select(node => node.Tag).ToArray(), OnDataChangeHandler);
    }

    public void RemoveSubscription(string groupName)
    {
        subscriptionHandler.Remove(groupName);
        Client.RemoveSubscription(groupName);
    }

    public string Host { get; set; }
    public string Server { get; set; }
    bool IClient.IsConnected { get; set; }

    public event Action<bool> OnConnectChanged;
    public event Action<string, string, bool> OnSubscriptionChanged;

    public string[] GetServers(string host = "localhost")
    {
        throw new NotImplementedException();
    }

    public bool CheckSubscription(string group, string itemName)
    {
        throw new NotImplementedException();
    }

    public event Action<string, string, TagValue, TagValue> OnDataChange;
    public Task Connect()
    {
        throw new NotImplementedException();
    }

    public void AppendToGroup(string itemName, string group = "default")
    {
        throw new NotImplementedException();
    }

    public List<IOpcNode> Browses(string itemName, bool includeChild = true, bool isLeaf = false,
        bool includeParent = false)
    {
        throw new NotImplementedException();
    }

    public List<IOpcNode> BrowseTrees(string itemName)
    {
        throw new NotImplementedException();
    }

    public IOpcNode GetOpcNode(string itemName)
    {
        throw new NotImplementedException();
    }

    public void Disconnect()
    {
        throw new NotImplementedException();
    }

    public T ReadNode<T>(string group, string itemName)
    {
        throw new NotImplementedException();
    }

    public Task<T> ReadNodeAsync<T>(string group, string itemName)
    {
        throw new NotImplementedException();
    }

    public void WriteNode(string group, string itemName, object value)
    {
        throw new NotImplementedException();
    }

    public Task WriteNodeAsync(string group, string itemName, object value)
    {
        throw new NotImplementedException();
    }

    public void WriteNode<T>(string group, string itemName, T value)
    {
        throw new NotImplementedException();
    }

    public Task WriteNodeAsync<T>(string group, string itemName, T value)
    {
        throw new NotImplementedException();
    }

    public void AddGroup(string groupName, bool isSubscribed = false, int updateRate = 100)
    {
        throw new NotImplementedException();
    }

    public void AddGroup(Group group)
    {
        throw new NotImplementedException();
    }

    public bool Subscription(string group, params string[] itemNames)
    {
        throw new NotImplementedException();
    }

    public void UnSubscription(string group, params string[] itemNames)
    {
        throw new NotImplementedException();
    }
}