using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GodSharp.Opc.Da;
using GodSharp.Opc.Da.Extensions;
using GodSharp.Opc.Da.Options;

namespace HmExtension.Opc.da;

/// <summary>
/// OpcDa客户端封装
/// </summary>
public class HmOpcDaClient : IClient, IDisposable
{
    /// <summary>
    /// OpcDa客户端对象
    /// </summary>
    private readonly OpcAutomationClient _client;

    /// <summary>
    /// 主机地址
    /// </summary>
    public string Host { get; set; }
    /// <summary>
    /// 服务名称
    /// </summary>
    public string Server { get; set; }

    private bool isConnected
    {
        get => _client.Connected;
        set => OnConnectChanged?.Invoke(value);// 触发事件
    }

    /// <summary>
    /// 连接状态
    /// </summary>
    public bool IsConnected
    {
        get => isConnected;
        set
        {
        }
    }
    /// <summary>
    /// 连接状态变化事件
    /// </summary>
    public event Action<bool> OnConnectChanged;
    /// <summary>
    /// 数据变化事件
    /// <para>
    ///     Action&lt;组名, 节点名, 旧值, 新值&gt;
    /// </para>
    /// </summary>
    public event Action<string, string, TagValue, TagValue> OnDataChange;

    /// <summary>
    /// TagValue缓存
    /// </summary>
    private readonly Dictionary<string, TagValue> _tagValues = new();

    /// <summary>
    /// 创建OpcDa客户端
    /// </summary>
    public HmOpcDaClient() : this(options => { })
    {
    }

    /// <summary>
    /// 创建OpcDa客户端
    /// </summary>
    /// <param name="initHandler">初始化设置项委托</param>
    public HmOpcDaClient(Action<DaClientOptions> initHandler)
    {
        _client = (OpcAutomationClient)DaClientFactory.Instance.CreateOpcAutomationClient(options =>
        {
            // 监听数据变化
            options.OnDataChangedHandler = OnDataChangeHandler;
            // 创建服务数据与默认组
            options.Data = new ServerData
            {
                Groups =
                [
                    new GroupData()
                    {
                        Name = "default",
                        IsSubscribed = true,
                        UpdateRate = 100,
                        Tags = new()
                    }
                ]
            };
            // 初始化设置
            initHandler?.Invoke(options);
        });
    }

    

    /// <summary>
    /// 得到服务列表
    /// </summary>
    /// <param name="host">主机地址</param>
    /// <returns></returns>
    public string[] GetServers(string host = "localhost")
    {
        var discovery = DaClientFactory.Instance.CreateOpcAutomationServerDiscovery();
        return discovery.GetServers(host: host);
    }

  

    /// <summary>
    /// 连接
    /// </summary>
    /// <returns></returns>
    public Task Connect()
    {
        return Task.Run(() =>
        {
            _client.Server.Host = Host;
            _client.Server.ProgId = Server;
            isConnected = _client.Connect();
        });
    }
    /// <summary>
    /// 将节点添加到分组
    /// </summary>
    /// <param name="itemName">节点名称</param>
    /// <param name="group">分组名称</param>
    public void AppendToGroup(string itemName, string group = "default")
    {
        if (!_client.Groups.TryGetValue(group,out var g))
        {
            AddGroup(group);
            g = _client.Change(group);
        }
        if(g.Values.ContainsKey(itemName)) return;
        g.Add(new Tag(itemName, g.Values.Count + 1));
    }

    /// <summary>
    /// 浏览节点
    /// </summary>
    /// <param name="itemName">节点名称</param>
    /// <param name="includeChild">是否包含子节点</param>
    /// <param name="isLeaf">是否是叶子节点</param>
    /// <returns></returns>
    public List<IOpcNode> Browses(string itemName = null, bool includeChild = true, bool isLeaf = false)
    {
        var browseNodes = _client.BrowseNodes(itemName, true);
        var nodes = new List<IOpcNode>();
        foreach (var browseNode in browseNodes)
        {
            if (isLeaf && !browseNode.IsLeaf) continue;
            if (includeChild)
            {
                nodes.Add(new OpcDaNode(this, browseNode));
                continue;
            }

            var full = browseNode.Full;
            // 1. ItemName为空
            if (string.IsNullOrWhiteSpace(itemName))
            {
                // 则只获取顶级节点
                if (full.IndexOf(".", StringComparison.Ordinal) == -1)
                {
                    nodes.Add(new OpcDaNode(this, browseNode));
                }
            }
            // 2. ItemName不为空
            else if (full.StartsWith(itemName) && full != itemName)
            {
                full = full.Replace(itemName, "");
                if (full.IndexOf(".", StringComparison.Ordinal) == full.LastIndexOf(".", StringComparison.Ordinal))
                {
                    // 则获取所有节点
                    nodes.Add(new OpcDaNode(this, browseNode));
                }
            }
        }

        return nodes;
    }

    /// <summary>
    /// 浏览节点树
    /// </summary>
    /// <param name="itemName">节点名称</param>
    /// <returns></returns>
    public List<IOpcNode> BrowseTrees(string itemName = null)
    {
        var browseNodes = _client.BrowseNodeTree(itemName);
        var nodes = new List<IOpcNode>();
        foreach (var browseNode in browseNodes)
        {
            nodes.Add(new OpcDaNode(this, browseNode));
        }
        return nodes;
    }

    /// <summary>
    /// 得到Opc节点
    /// </summary>
    /// <param name="itemName"></param>
    /// <returns></returns>
    public IOpcNode GetOpcNode(string itemName)
    {
        return Browses(itemName, false).FirstOrDefault();
    }

    /// <summary>
    /// 断开连接
    /// </summary>
    public void Disconnect()
    {
        _client?.Disconnect();
        isConnected = false;
    }

    /// <summary>
    /// 读取节点
    /// </summary>
    /// <param name="group">分组名称</param>
    /// <param name="itemName">节点名称</param>
    /// <returns></returns>
    public TagValue ReadNode(string group, string itemName)
    {
        AppendToGroup(itemName,group);
        return _client.Read(group, itemName)?.Result;
    }

    /// <summary>
    /// 读取节点
    /// </summary>
    /// <param name="group">分组名称</param>
    /// <param name="itemName">节点名称</param>
    /// <returns></returns>
    public Task<TagValue> ReadNodeAsync(string group, string itemName)
    {
        return Task.Run(() =>
        {
            AppendToGroup(itemName, group);
            ActionResult<Tag> rs = _client.ReadAsync(group, itemName);
            return (TagValue)rs?.Result;
        });
    }

    /// <summary>
    /// 读取节点
    /// </summary>
    /// <typeparam name="T">结果类型</typeparam>
    /// <param name="group">分组名称</param>
    /// <param name="itemName">节点名称</param>
    /// <returns></returns>
    public T ReadNode<T>(string group, string itemName)
    {
        AppendToGroup(itemName, group);
        var actionResult = _client.Read(group, itemName);
        return (T)actionResult?.Result?.Value ?? default;
    }

   

    /// <summary>
    /// 异步读取节点
    /// </summary>
    /// <typeparam name="T">结果类型</typeparam>
    /// <param name="group">分组名称</param>
    /// <param name="itemName">节点名称</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<T> ReadNodeAsync<T>(string group, string itemName)
    {
        return Task.Run(() =>
        {
            AppendToGroup(itemName, group);
            ActionResult<Tag> rs = _client.ReadAsync(group, itemName);
            if (rs?.Result is TagValue tv)
            {
                return (T)tv.Value;
            }

            return default;
        });
    }

    /// <summary>
    /// 写入节点
    /// </summary>
    /// <param name="group">分组名称</param>
    /// <param name="itemName">节点名称</param>
    /// <param name="value">节点值</param>
    public void WriteNode(string group, string itemName, object value)
    {
        AppendToGroup(itemName, group);
        _client.Write(group, itemName, value);
    }

    /// <summary>
    /// 异步写入节点
    /// </summary>
    /// <param name="group">分组名称</param>
    /// <param name="itemName">节点名称</param>
    /// <param name="value">节点值</param>
    /// <returns></returns>
    public Task WriteNodeAsync(string group, string itemName, object value)
    {
        AppendToGroup(itemName, group);
        return Task.Run(() => { _client.WriteAsync(group, itemName, value); });
    }

    /// <summary>
    /// 数据变化处理
    /// </summary>
    /// <param name="output"></param>
    private void OnDataChangeHandler(DataChangedOutput output)
    {
        var itemName = output.Data.ItemName;
        _tagValues.TryGetValue(itemName, out var oldValue);
        OnDataChange?.Invoke(output.Group.Name, itemName, oldValue, output.Data);
        _tagValues[itemName] = output.Data;
    }

    /// <summary>
    /// 添加组
    /// </summary>
    /// <param name="groupName">组名称</param>
    /// <param name="isSubscribed">是否订阅组</param>
    /// <param name="updateRate">更新频率</param>
    public void AddGroup(string groupName, bool isSubscribed = false, int updateRate = 100)
    {
        AddGroup(new Group()
        {
            Name = groupName,
            IsSubscribed = isSubscribed,
            UpdateRate = updateRate,
        });
    }

    /// <summary>
    /// 添加组
    /// </summary>
    /// <param name="group"></param>
    public void AddGroup(Group group)
    {
        // 检查是否存在
        if (_client.Groups.ContainsKey(group.Name))
        {
            return;
        }

        _client.Add(group);
        group.DataChangedHandler += OnDataChangeHandler;
    }

    /// <summary>
    /// 移除组
    /// </summary>
    /// <param name="groupName"></param>
    public void RemoveGroup(string groupName)
    {
        var gs = _client.Change(groupName);
        if (gs == null) return;
        gs.Group.DataChangedHandler -= OnDataChangeHandler;
        _client.Remove(groupName);
    }

    /// <summary>
    /// 订阅
    /// </summary>
    /// <param name="group"></param>
    /// <param name="itemNames"></param>
    /// <returns></returns>
    public bool Subscription(string group, params string[] itemNames)
    {
        var gs = _client.Change(group);
        if (gs == null)
        {
            return false;
        }

        foreach (var itemName in itemNames)
        {
            // 检查是否存在
            if (gs.Values.ContainsKey(itemName))
            {
                continue;
            }

            gs.Add(new Tag(itemName, gs.Values.Count + 1));
        }

        return true;
    }

    /// <summary>
    /// 取消订阅
    /// </summary>
    /// <param name="group"></param>
    /// <param name="itemNames"></param>
    public void UnSubscription(string group, params string[] itemNames)
    {
        var gs = _client.Change(group);
        gs?.Remove(itemNames);
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose()
    {
        Disconnect();
    }
}