using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GodSharp.Opc.Da;
using HmExtension.Opc.da;
using HmExtension.Opc.ua;
using Opc.Ua;

namespace HmExtension.Opc;

/// <summary>
/// Opc节点
/// </summary>
public class OpcNode : IEnumerable<OpcNode>
{
    public event Action<OpcNode> OnChildNodeAdded;

    public event Action<OpcNode> OnChildNodeRemoved;


    public event Action<string, TagValue,TagValue> OnDataChange;

    public string Name { get; set; }

    public string Tag => Client is HmOpcUaClient ? $"{NodeId}" : BrowseNode.Name;
    public NodeId NodeId { get; set; }

    public OpcNode ParentNode { get; private set; }

    public string ItemName
    {
        get
        {
            if (Client is HmOpcUaClient)
            {
                return NodeId.ToString();
            }
            else if (Client is HmOpcDaClient)
            {
                return BrowseNode.Full;
            }

            return null;
        }
    }

    public string FullPath
    {
        get
        {
            if (Client is HmOpcUaClient)
            {
                return ParentNode == null ? Name : $"{ParentNode.FullPath}/{Name}";
            }
            else if (Client is HmOpcDaClient)
            {
                return BrowseNode.Full;
            }

            return null;
        }
    }


    private readonly List<OpcNode> ChildNodes = new();

    public readonly BrowseNode BrowseNode;

    public readonly IClient Client;

    private readonly List<string> groups = new();

    public List<string> Groups => groups.ToArray().ToList();

    public OpcNode(IClient daClient, BrowseNode browseNode)
    {
        Client = daClient;
        BrowseNode = browseNode;
        Loader();
    }

    private void Loader()
    {
        if (Client is HmOpcUaClient)
        {
            // NodeId = (NodeId)ReferenceDescription.NodeId;
            // Name = ReferenceDescription.DisplayName.Text;
            // NodeClass = ReferenceDescription.NodeClass;
        }
        else if (Client is HmOpcDaClient)
        {
            Name = BrowseNode.Name;
            var browseNodeChildren = BrowseNode.Childs;
            if (browseNodeChildren == null) return;
            foreach (var browseNodeChild in browseNodeChildren)
            {
                AddChildNode(new OpcNode(Client, browseNodeChild));
            }
        }
        Client.OnDataChange += (group,itemName, oldValue, newValue) =>
        {
            if (Groups.Contains(group) && itemName == ItemName)
            {
                OnDataChange?.Invoke(group, oldValue, newValue);
            }
        };
    }

    public void AddChildNode(OpcNode opcNode)
    {
        opcNode.ParentNode = this;
        ChildNodes.Add(opcNode);
        OnChildNodeAdded?.Invoke(opcNode);
    }

    public void RemoveChildNode(OpcNode opcNode)
    {
        ChildNodes.Remove(opcNode);
        opcNode.ParentNode = null;
        OnChildNodeRemoved?.Invoke(opcNode);
    }

    public IEnumerator<OpcNode> GetEnumerator()
    {
        return ChildNodes.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public T ReadValue<T>(string group = null)
    {
        return Client.ReadNode<T>(Tag, ItemName);
    }

    public Task<T> ReadNodeAsync<T>(string group = null)
    {
        return Client.ReadNodeAsync<T>(group, ItemName);
    }

    public void WriteValue<T>(T value, string group = null)
    {
        Client.WriteNode(group,ItemName, value);
    }

    public Task WriteNodeAsync<T>(T value, string group = null)
    {
        return Client.WriteNodeAsync(group, ItemName, value);
    }

    public void Subscription(string group = null)
    {
        Client.Subscription(group,ItemName);
        groups.Add(group);
    }

    public void UnSubscription(string group = null)
    {
        Client.UnSubscription(group, ItemName);
        groups.Remove(group);
    }
}