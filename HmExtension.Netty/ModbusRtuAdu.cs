namespace HmExtension.Netty;
/// <summary>
/// Modbus RTU 包装类
/// </summary>
public class ModbusRtuAdu
{
    /// <summary>
    /// 从站地址
    /// </summary>
    public ushort Slave { get; set; }
    /// <summary>
    /// 功能码
    /// </summary>
    public byte FunctionCode { get; set; }
    /// <summary>
    /// 数据
    /// </summary>
    public byte[] Data { get; set; }
}