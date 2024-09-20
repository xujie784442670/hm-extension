using System;
using DotNetty.Transport.Channels;

namespace HmExtension.Netty;
/// <summary>
/// Modbus TCP byte handler
/// </summary>
public class ModbusTcpByteHandler : SimpleChannelInboundHandler<byte[]>
{
    public event Action<byte[]> OnReceive;

    protected override void ChannelRead0(IChannelHandlerContext ctx, byte[] msg)
    {
        OnReceive?.Invoke(msg);
    }
}