using Aop.Api;

namespace HmExtension.Pay;
/// <summary>
/// 支付宝响应扩展
/// </summary>
public static class AopResponseExtension
{
    /// <summary>
    /// 是否成功
    /// </summary>
    /// <param name="response">响应对象</param>
    /// <returns>true or false</returns>
    public static bool IsSuccess(this AopResponse response)
    {
        return !response.IsError;
    }
    
}