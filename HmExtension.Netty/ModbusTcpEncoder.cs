using System.Threading;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;

namespace HmExtension.Netty;

public class ModbusTcpEncoder : MessageToByteEncoder<byte[]>
{
    private static int _transactionId = 0;

    protected override void Encode(IChannelHandlerContext context, byte[] message, IByteBuffer output)
    {
        byte[] header = new byte[6];
        header[0] = (byte)(_transactionId >> 8);
        header[1] = (byte)_transactionId;
        header[4] = (byte)(message.Length >> 8);
        header[5] = (byte)message.Length;
        output.WriteBytes(header);
        output.WriteBytes(message);
        // 使用原子操作保证线程安全, 事务ID自增
        Interlocked.Increment(ref _transactionId);
        // 事务ID超过65535后重置
        if (_transactionId > 65535)
        {
            Interlocked.Exchange(ref _transactionId, 0);
        }
    }
}