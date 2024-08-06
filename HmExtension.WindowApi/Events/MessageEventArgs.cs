using HmExtension.Commons.Commons;
using HmExtension.WindowApi;

namespace HmExtension.Commons.Events;
/// <summary>
/// 系统消息事件
/// </summary>
public class MessageEventArgs
{
    /// <summary>
    /// 指定是否已从队列中删除消息。 此参数的取值可为下列值之一
    /// </summary>
    public bool IsDelete;
    /// <summary>
    /// 其窗口过程接收消息的窗口的句柄。 当消息是线程消息时，此成员为 NULL 。
    /// </summary>
    public int Hwnd;
    /// <summary>
    /// 消息的标识符。 应用程序只能使用低字;高字由系统保留。
    /// </summary>
    public int Message;
    /// <summary>
    /// 消息的发布时间。
    /// </summary>
    public long Time;
    /// <summary>
    /// 发布消息时的光标位置（以屏幕坐标表示）。
    /// </summary>
    public Point<int> Point;
    /// <summary>
    /// 系统消息事件
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="isDelete"></param>
    public MessageEventArgs(WinApi.TagMSG msg,bool isDelete)
    {
        IsDelete = isDelete;
        Hwnd = msg.hwnd.ToInt32();
        Message = msg.message;
        Time = msg.time;
        Point = new Point<int>(msg.pt.X, msg.pt.Y);
    }

}