# AI模块
该模块用于简化各种大模型的调用过程

# 使用方式
后续会不定期添加支持的模型
## 星火

### 1. 创建AI客户端
```C#
_client = HmAiChatClientFactory.Create(new XingHuoChatOption()
            {
                ApiUrl = ApiUrl,
                ApiSecret = AppSecret
            });
```

## 2. 设置AI的身份(可选)
```C#
_client.SetSystemMessage("你是一个经验丰富的C#开发工程师");
```

## 3. 发送消息
```C#
try
{
    // 发送并接收消息
    var msg = await _client.SendMessageAsync(XinHuoModel.Lite, contentTb.Text);
}
catch (Exception ex)
{
    MessageBox.Show($"发生错误:{ex.Message}");
    Console.WriteLine(ex.StackTrace);
}
```