using System.Collections.Generic;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;

namespace HmExtension.Netty;
public class ModbusTcpDecoder: ByteToMessageDecoder
{
    /// <summary>
    /// Modbus TCP header length
    /// </summary>
    public const int HeaderLength = 6;

    protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
    {
        if (input.ReadableBytes < HeaderLength) return;
        var length = input.GetUnsignedShort(4) + HeaderLength;
        if (input.ReadableBytes < length) return;
        // 读取所有数据
        var buffer = input.ReadBytes(length);
        // 去除协议头
        buffer.SkipBytes(HeaderLength);
        // 转换为byte数组
        var bytes = new byte[buffer.ReadableBytes];
        buffer.ReadBytes(bytes);
        output.Add(bytes);
    }
}