using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GodSharp.Opc.Da;

namespace HmExtension.Opc;

public interface IOpcNode:IEnumerable<IOpcNode>
{
    public event Action<string,TagValue,TagValue> OnDataChange;

    public string ItemName { get; set; }

    public string Name { get; set; }

    public bool IsSubscription { get; set; }

    public bool IsLeaf { get; set; }

    public void AddChildNode(IOpcNode node);

    public void RemoveChildNode(IOpcNode node);

    public T ReadValue<T>(string group= "default");

    public void WriteValue<T>(T value,string group= "default");

    public Task<T> ReadValueAsync<T>(string group = "default");

    public Task WriteValueAsync<T>(T value, string group = "default");

    public void Subscription(string group = "default",int updateRate=100);

    public void UnSubscription(string group = "default");
}