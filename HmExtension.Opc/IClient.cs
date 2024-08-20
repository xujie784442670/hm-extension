using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GodSharp.Opc.Da;
using Opc.Ua;

namespace HmExtension.Opc;

public interface IClient
{

    public string Host { get; set; }

    public string Server { get; set; }

    public string[] GetServers(string host= "localhost");

    public event Action<string,string,TagValue,TagValue> OnDataChange;

    public Task Connect();

    public List<IOpcNode> Browses(string itemName=null, bool includeChild = true,bool isLeaf=false);

    public List<IOpcNode> BrowseTrees(string itemName = null);

    public IOpcNode GetOpcNode(string itemName);

    public void Disconnect();

    public T ReadNode<T>(string group, string itemName);

    public Task<T> ReadNodeAsync<T>(string group, string itemName);

    public void WriteNode(string group, string itemName, object value);

    public Task WriteNodeAsync(string group, string itemName, object value);

    public void AddGroup(string groupName, bool isSubscribed = false, int updateRate = 100);

    public void AddGroup(Group group);

    public bool Subscription(string group, params string[] itemNames);

    public void UnSubscription(string group, params string[] itemNames);
}