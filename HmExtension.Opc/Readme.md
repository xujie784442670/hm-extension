# 简介
本项目旨在简化OPC的使用，提供了一个简单的API，使得用户可以通过简单的代码实现OPC的读写操作。

# 使用方法
## 1. 安装依赖
```shell
dotnet add package hongmao.HmExtension.Opc
```

## 2. 创建OPC客户端
```csharp
var client = HmOpcClientFactory.CreateOpcDaClient();
```

## 3. 获取OPC服务器列表
```csharp
var servers = client.GetServers(); // 获取本地OPC服务器列表
// var servers = client.GetServers("127.0.0.1")
```

## 4. 连接OPC服务器
```csharp
client.Host = "127.0.0.1";
client.ServerName = "Kepware.KEPServerEX.V6";
client.Connect();
```

## 5. 浏览节点数据
```csharp
var nodes = client.Browses(); // 浏览节点列表
var treeNodes = client.BrowseTrees(); // 浏览节点树
var node = client.GetOpcNode("Channel1.Device1.Tag1"); // 获取节点信息
```

## 6. 读取数据
```csharp
var node = client.GetOpcNode("Channel1.Device1.Tag1"); // 获取节点信息
var value = client.ReadValue(node); // 读取节点数据
```

## 7. 写入数据
```csharp
var node = client.GetOpcNode("Channel1.Device1.Tag1"); // 获取节点信息
client.WriteValue(100); // 写入节点数据
```

## 8. 监听数据变化
```csharp
var node = client.GetOpcNode("Channel1.Device1.Tag1"); // 获取节点信息
client.OnDataChange += (groupName,oldValue,newValue)=>{
	Console.WriteLine($"数据变化：{groupName} {oldValue} -> {newValue}");
}
```

## 9. 断开连接
```csharp
client.Disconnect();
```