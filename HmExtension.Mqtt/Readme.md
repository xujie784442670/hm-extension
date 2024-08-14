# MQTT扩展模块
本模块基于MQTTnet进行扩展,提供了简易的使用方法

# 简单使用
```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmExtension.Mqtt;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MqttTest();
            Console.ReadKey();
        }

        private static async Task MqttTest()
        {
            // 1. 创建客户端对象
            var client = new HmMqttClient()
            {
                IsPrintLogger = true // 开启消息日志
            };
            // 2. 连接MQTT服务器
            // 请自行替换对应参数
            await client.ConnectAsync([Host], [Username], [Password], [Port],[ClientId]);
            // 3. 订阅主题
            var topic = await client.SubscribeAsync("/test");

            topic.OnApplicationMessageReceived += e =>
            {
                // 接收消息
                var content = Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment.Array);
                Console.WriteLine("接收到消息: "+content);
            };
            // 发送消息
            topic.Push("消息内容");
        }
    }
}

```
结果如下:
```
[发送] 2024-08-14 11:58:04 [1c558c0a-f460-47ef-aa71-11d2b299b607]       [/test] [ContentType=] [Payload=消息内容]
[接收] 2024-08-14 11:58:04 [1c558c0a-f460-47ef-aa71-11d2b299b607]       [/test] [ContentType=] [Payload=消息内容]
接收到消息: 消息内容
[接收] 2024-08-14 11:58:06 [1c558c0a-f460-47ef-aa71-11d2b299b607]       [/test] [ContentType=] [Payload=内容测试]
接收到消息: 内容测试
```