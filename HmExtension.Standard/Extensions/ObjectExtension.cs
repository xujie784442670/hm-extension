using System;
using HmExtension.Standard.utils;
using Newtonsoft.Json;

namespace HmExtension.Standard.Extensions;

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
        Print(value, "", "");
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
        Println(value, "", "");
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
    public static string ToJson(this object value, Formatting formatting = Formatting.None,
        params JsonConverter[] converters)
    {
        return JsonConvert.SerializeObject(value, formatting, converters);
    }

    /// <summary>
    /// 将当前对象作为参数,并使用指定的格式化字符串格式化
    /// <example>
    /// <code>
    /// // 第一种使用方式: 将当前对象作为参数,并使用指定的格式化字符串格式化
    /// var obj = 10;
    /// obj.FormatPatten("value:{}").Println(); // value:10
    /// obj.FormatPatten("value:{} - {}").Println(); // value:10 - 10
    /// obj.FormatPatten("{} value: {}").Println(); // 10 value: 10
    /// // 第二种使用方式: 将当前对象中的字段或属性作为参数,并使用指定的格式化字符串格式化
    /// class Student{
    ///     public string Name { get; set; }
    ///     public int Age;
    /// }
    /// var stu = new Student{Name="张三",Age=20};
    /// stu.FormatPatten("name:{Name} age:{Age}").Println(); // name:张三
    /// 
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">当前对象</param>
    /// <param name="fromat">字符串格式</param>
    /// <returns>格式化字符串</returns>
    public static string FormatPatten(this object value, string fromat)
    {
        var result = fromat.Replace("{}", value.ToString());
        // 使用正则表达式搜索当前对象中的字段或属性
        var matches = System.Text.RegularExpressions.Regex.Matches(fromat, @"\{(\w+)\}");
        foreach (System.Text.RegularExpressions.Match match in matches)
        {
            var key = match.Groups[1].Value;
            // 使用反射获取属性值,并替换格式化字符串中的占位符
            if (TypeUtil.HasProperty(value.GetType(), key))
            {
                result = result.Replace($"{{{key}}}", TypeUtil.GetPropertyValue<object>(value, key)?.ToString());
            }
            else if (TypeUtil.HasField(value.GetType(), key))
            {
                result = result.Replace($"{{{key}}}", TypeUtil.GetFieldValue<object>(value, key).ToString());
            }
            else
            {
                throw new ArgumentException($"对象中不存在{key}属性或字段");
            }
        }

        return result;
    }

    /// <summary>
    /// 获取对象的属性值
    /// <example>
    /// <code>
    /// class Student{
    ///     public string Name { get; set; }
    ///     public int Age { get; set; }
    /// }
    ///
    /// var stu = new Student{Name="张三",Age=20};
    /// stu.GetPropertyValue&lt;string&gt;("Name").Println(); // 控制台输出: 张三
    /// stu.GetPropertyValue&lt;int&gt;("Age").Println(); // 控制台输出: 20
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="obj">当前对象</param>
    /// <param name="propertyName">属性名称</param>
    /// <typeparam name="T">属性值类型</typeparam>
    /// <returns></returns>
    public static T GetPropertyValue<T>(this object obj, string propertyName)
    {
        return TypeUtil.GetPropertyValue<T>(obj, propertyName);
    }

    /// <summary>
    /// 获取对象的字段值
    /// <example>
    /// <code>
    /// class Student{
    ///     public string Name ;
    ///     public int Age ;
    /// }
    ///
    /// var stu = new Student{Name="张三",Age=20};
    /// stu.GetFieldValue&lt;string&gt;("Name").Println(); // 控制台输出: 张三
    /// stu.GetFieldValue&lt;int&gt;("Age").Println(); // 控制台输出: 20
    /// </code>
    /// </example>
    /// </summary>
    /// <typeparam name="T">字段值类型</typeparam>
    /// <param name="obj">当前对象</param>
    /// <param name="fieldName">字段名称</param>
    /// <returns></returns>
    public static T GetFieldValue<T>(this object obj, string fieldName)
    {
        return TypeUtil.GetFieldValue<T>(obj, fieldName);
    }

    /// <summary>
    /// 获取对象的属性或字段值
    /// <example>
    /// <code>
    /// class Student{
    ///     public string Name {get;set;}
    ///     public int Age ;
    /// }
    ///
    /// var stu = new Student{Name="张三",Age=20};
    /// stu.GetPropertyOrFieldValue&lt;string&gt;("Name").Println(); // 控制台输出: 张三
    /// stu.GetPropertyOrFieldValue&lt;int&gt;("Age").Println(); // 控制台输出: 20
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="obj">当前对象</param>
    /// <param name="name">字段或属性名称</param>
    /// <typeparam name="T">值类型</typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException">字段或属性不存在时</exception>
    public static T GetPropertyOrFieldValue<T>(this object obj, string name)
    {
        if (TypeUtil.HasField(obj.GetType(), name))
        {
            return obj.GetFieldValue<T>(name);
        }

        if (TypeUtil.HasProperty(obj.GetType(), name))
        {
            return obj.GetPropertyValue<T>(name);
        }

        throw new ArgumentException($"对象中不存在{name}属性或字段");
    }

    /// <summary>
    /// 设置对象的属性值
    /// </summary>
    /// <param name="obj">当前对象</param>
    /// <param name="propertyName">属性名称</param>
    /// <param name="value">属性值</param>
    public static void SetPropertyValue(this object obj, string propertyName, object value)
    {
        TypeUtil.SetPropertyValue(obj, propertyName, value);
    }

    /// <summary>
    /// 设置对象的字段值
    /// </summary>
    /// <param name="obj">当前对象</param>
    /// <param name="fieldName">字段名称</param>
    /// <param name="value">字段值</param>
    public static void SetFieldValue(this object obj, string fieldName, object value)
    {
        TypeUtil.SetFieldValue(obj, fieldName, value);
    }

    /// <summary>
    /// 设置对象的属性或字段值
    /// </summary>
    /// <param name="obj">当前对象</param>
    /// <param name="name">字段或属性名称</param>
    /// <param name="value">字段或属性值</param>
    /// <exception cref="ArgumentException"></exception>
    public static void SetPropertyOrFieldValue(this object obj, string name, object value)
    {
        if (TypeUtil.HasField(obj.GetType(), name))
        {
            obj.SetFieldValue(name, value);
        }
        else if (TypeUtil.HasProperty(obj.GetType(), name))
        {
            obj.SetPropertyValue(name, value);
        }
        else
        {
            throw new ArgumentException($"对象中不存在{name}属性或字段");
        }
    }
}