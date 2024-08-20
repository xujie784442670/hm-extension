using GodSharp.Opc.Da;
using System;
using HmExtension.Opc.da;

namespace HmExtension.Opc;
/// <summary>
/// Opc客户端工厂
/// </summary>
public static class HmOpcClientFactory
{
    /// <summary>
    /// 创建OpcDa客户端
    /// </summary>
    /// <param name="initHandler"></param>
    /// <returns></returns>
    public static IClient CreateOpcDaClient(Action<DaClientOptions> initHandler = null)
    {
        return new HmOpcDaClient(initHandler ?? (options => { }));
    }
}