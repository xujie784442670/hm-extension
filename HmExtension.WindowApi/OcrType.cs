namespace HmExtension.WindowApi;

public enum OcrType
{
    /// <summary>
    /// 普通选择
    /// </summary>
    OCR_NORMAL = 32512,
    /// <summary>
    /// 文本选择
    /// </summary>
    OCR_IBEAM = 32513,
    /// <summary>
    /// 忙碌
    /// </summary>
    OCR_WAIT = 32514,
    /// <summary>
    /// 精度选择
    /// </summary>
    OCR_CROSS = 32515,
    /// <summary>
    /// 备用选择
    /// </summary>
    OCR_UP = 32516,
    /// <summary>
    /// 对角调整大小 1
    /// </summary>
    OCR_SIZENWSE = 32642,
    /// <summary>
    /// 对角调整大小 2
    /// </summary>
    OCR_SIZENESW = 32643,
    /// <summary>
    /// 水平调整大小
    /// </summary>
    OCR_SIZEWE = 32644,
    /// <summary>
    /// 垂直调整大小
    /// </summary>
    OCR_SIZENS = 32645,
    /// <summary>
    /// 移动
    /// </summary>
    OCR_SIZEALL = 32646,
    /// <summary>
    /// 不可用
    /// </summary>
    OCR_NO = 32648,
    /// <summary>
    /// 链接选择
    /// </summary>
    OCR_HAND = 32649,
    /// <summary>
    /// 在后台工作
    /// </summary>
    OCR_APPSTARTING = 32650,
}