using System;
using System.Reflection;
using V8.Net;

namespace HmExtension.Commons.utils;

/// <summary>
/// Js帮助类
/// </summary>
public class JsHelper
{
    private static readonly object LockV8 = new object();//V8 需要加锁

    /// <summary>
    /// 执行js Eval方法 支持多线程
    /// </summary>
    /// <param name="reString">Js代码</param>
    /// <param name="para">方法名称</param>
    /// <param name="methodName">参数字符串(使用逗号分隔)</param>
    public string EvalMethod(string reString, string methodName, string para = "")
    {
        try
        {
            Type obj = Type.GetTypeFromProgID("ScriptControl");
            if (obj == null) return "";
            object scriptControl = Activator.CreateInstance(obj);
            obj.InvokeMember("Language", BindingFlags.SetProperty, null, scriptControl, new object[] { "JavaScript" });
            obj.InvokeMember("AddCode", BindingFlags.InvokeMethod, null, scriptControl, new object[] { reString });
            return obj.InvokeMember("Eval", BindingFlags.InvokeMethod, null, scriptControl, new object[] { string.Format("{0}({1})", methodName, para) }).ToString();//执行结果

        }
        catch (Exception ex)
        {
            string errorInfo =  $"执行JS出现错误:   \r\n 错误描述: {ex.Message} \r\n 错误原因: {ex.InnerException.Message} \r\n 错误来源:{ex.InnerException.Source}";//异常信息
            return errorInfo; //  ErrorInfo  异常信息
        }
    }

    /// <summary>
    /// V8 执行js 不支持多线程 所以得加锁(内部已加锁无需重复加)
    /// </summary>
    /// <param name="reString">加载的js文本</param>
    /// <param name="methodName">方法名称</param>
    /// <param name="para">参数字符串(使用逗号分隔)</param>
    /// <returns></returns>
    public static string V8Method(string reString, string methodName, string para)
    {
        lock (LockV8)
        {
            V8Engine engine = new V8Engine();//创建V8对象
            var script = engine.Compile(reString);//编译
            try
            {
                engine.Execute(script);//将编译的脚本加载到V8引擎中
                return engine.Execute(string.Format("{0}({1})", methodName, para)).ToString();//执行结果
            }
            catch (Exception ex)
            {
                return ex.Message;//异常信息
            }
        }


    }
}