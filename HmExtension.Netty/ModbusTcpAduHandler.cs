using System;
using System.Linq;
using DotNetty.Transport.Channels;
using HmExtension.Commons.Extensions;

namespace HmExtension.Netty;

public class ModbusTcpAduHandler : SimpleChannelInboundHandler<byte[]>
{
    public event Action<ModbusRtuAdu> OnReceive;

    protected override void ChannelRead0(IChannelHandlerContext ctx, byte[] msg)
    {
        var adu = new ModbusRtuAdu
        {
            Slave = msg.ToUShort(0,true),
            FunctionCode = msg[2],
            Data = msg.Skip(3).ToArray()
        };
        OnReceive?.Invoke(adu);
    }
}