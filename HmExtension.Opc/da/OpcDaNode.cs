using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GodSharp.Opc.Da;
using Opc.Ua.Server;

namespace HmExtension.Opc.da;

public class OpcDaNode : IOpcNode
{
    public readonly HmOpcDaClient Client;

    public readonly BrowseNode BrowseNode;

    public readonly List<IOpcNode> Nodes = [];

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


    public IEnumerator<IOpcNode> GetEnumerator()
    {
        return Nodes.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public event Action<string, TagValue, TagValue> OnDataChange;

    public string ItemName
    {
        get => BrowseNode.Full;
        set { }
    }

    public string Name
    {
        get => BrowseNode.Name;
        set { }
    }

    private bool _isSubscription;
    public bool IsSubscription
    {
        get => _isSubscription;
        set { }
    }

    public bool IsLeaf
    {
        get => BrowseNode.IsLeaf;
        set { }
    }

    public void AddChildNode(IOpcNode node)
    {
        Nodes.Add(node);
    }

    public void RemoveChildNode(IOpcNode node)
    {
        Nodes.Remove(node);
    }

    public T ReadValue<T>(string group = "default")
    {
        return !IsLeaf ? default : Client.ReadNode<T>(group, ItemName);
    }

    public void WriteValue<T>(T value, string group = "default")
    {
        if (IsLeaf)
        {
            Client.WriteNode(group, ItemName, value);
        }
    }

    public Task<T> ReadValueAsync<T>(string group = "default")
    {
        return !IsLeaf ? default : Client.ReadNodeAsync<T>(group, ItemName);
    }

    public Task WriteValueAsync<T>(T value, string group = "default")
    {
        return !IsLeaf ? default : Client.WriteNodeAsync(group, ItemName, value);
    }

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

    public void UnSubscription(string group = "default")
    {
        _isSubscription = false;
        Client.UnSubscription(group, ItemName);
    }
}