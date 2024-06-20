using System;
using Newtonsoft.Json;

namespace HmExtension;
/// <summary>
/// 对象扩展类
/// </summary>
public static class ObjectExtension
{
    /// <summary>
    /// 将对象打印到控制台
    /// <example>
    /// <code>
    /// object obj = new object();
    /// obj.Print("prefix: ","suffix"); // prefix: System.Objectsuffix
    /// obj.Print("prefix: "); // prefix: System.Object
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">当前对象</param>
    /// <param name="prefix">前缀字符串</param>
    /// <param name="suffix">后缀字符串</param>
    public static void Print(this object value, string prefix = "", string suffix = "")
    {
        Console.Write(prefix + value + suffix);
    }    
    /// <summary>
    /// 将对象打印到控制台
    /// <example>
    /// <code>
    /// object obj = new object();
    /// obj.Print(); // System.Object
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">当前对象</param>
    public static void Print(this object value)
    {
        Print(value,"","");
    }

    /// <summary>
    /// 将对象打印到控制台,并在末尾添加换行符
    /// <example>
    /// <code>
    /// object obj = new object();
    /// obj.Println("prefix: ","suffix"); // prefix: System.Objectsuffix\r\n
    /// obj.Println("prefix: "); // prefix: System.Object\r\n
    /// </code>
    /// </example>
    /// 
    /// </summary>
    /// <param name="value">当前对象</param>
    /// <param name="prefix">前缀字符串</param>
    /// <param name="suffix">后缀字符串</param>
    public static void Println(this object value, string prefix = "", string suffix = "")
    {
        Print(value, prefix, $"{suffix}{Environment.NewLine}");
    }
    /// <summary>
    /// 将对象打印到控制台,并在末尾添加换行符
    /// <example>
    /// <code>
    /// object obj = new object();
    /// obj.Print(); // System.Object\r\n
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">当前对象</param>
    public static void Println(this object value)
    {
        Println(value,"","");
    }

    /// <summary>
    /// 将对象转换为Json字符串
    /// <example>
    /// <code>
    /// class Student{
    ///     public string Name { get; set; }
    ///     public int age {get; set; }
    /// }
    ///
    /// var stu = new Student{
    ///     Name = "张三",
    ///     Age = 20
    /// }
    /// stu.ToJson().Println(); // {"Name":"张三","Age":20}
    /// stu.ToJson(Formatting.Indented).Println();
    /// /*
    /// {
    ///     "Name": "张三",
    ///     "Age": 20
    /// }
    /// */
    /// stu.ToJson(converters: new JsonConverter[]{new StringEnumConverter()}).Println();
    /// // {"Name":"张三","Age":20}
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">当前对象</param>
    /// <param name="formatting">格式化选项</param>
    /// <param name="converters">Json转换器</param>
    /// <returns>转换后的JSON字符串</returns>
    /// <seealso cref="Newtonsoft.Json.Formatting"/>
    /// <seealso cref="Newtonsoft.Json.JsonConverter"/>
    public static string ToJson(this object value, Formatting formatting= Formatting.None,params JsonConverter[] converters)
    {
        return JsonConvert.SerializeObject(value, formatting, converters);
    }
}