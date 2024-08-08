using System;
using System.Linq;
using System.Reflection;

namespace HmExtension.Commons.utils;

/// <summary>
/// 反射工具类
/// </summary>
public class TypeHelper
{
    /// <summary>
    /// 获取类型的属性(包括父类)
    /// </summary>
    /// <param name="type">待获取属性的类型</param>
    /// <param name="isPrivate">是否包含私有属性</param>
    /// <param name="isStatic">是否包含静态属性</param>
    /// <param name="bindingFlags">决定查找属性的标志</param>
    /// <returns></returns>
    public static PropertyInfo[] GetProperties(Type type, bool isPrivate = false,bool isStatic=false, BindingFlags bindingFlags = BindingFlags.Instance)
    {
        bindingFlags |= isPrivate ? BindingFlags.NonPublic : BindingFlags.Public;
        bindingFlags |= isStatic ? BindingFlags.Static : BindingFlags.Instance;

        var propertyInfos = type.GetProperties(bindingFlags).ToList();
        // 检查是否有父类
        if (type.BaseType != null)
        {
            propertyInfos.AddRange(GetProperties(type.BaseType, isPrivate));
        }

        return propertyInfos.ToArray();
    }

    /// <summary>
    /// 获取类型的字段(包括父类)
    /// </summary>
    /// <param name="type">待获取字段的类型</param>
    /// <param name="isPrivate">是否包含私有属性</param>
    /// <param name="isStatic">是否包含静态属性</param>
    /// <param name="bindingFlags">决定查找属性的标志</param>
    /// <returns></returns>
    public static FieldInfo[] GetFields(Type type, bool isPrivate = false, bool isStatic = false, BindingFlags bindingFlags = BindingFlags.Instance)
    {
        bindingFlags |= isPrivate ? BindingFlags.NonPublic : BindingFlags.Public;
        bindingFlags |= isStatic ? BindingFlags.Static : BindingFlags.Instance;
        var fieldInfos = type.GetFields(bindingFlags).ToList();
        // 检查是否有父类
        if (type.BaseType != null)
        {
            fieldInfos.AddRange(GetFields(type.BaseType, isPrivate));
        }

        return fieldInfos.ToArray();
    }

    /// <summary>
    /// 获取类型的属性
    /// </summary>
    /// <param name="type">待获取属性的类型</param>
    /// <param name="propertyName">属性名称</param>
    /// <returns></returns>
    public static PropertyInfo GetProperty(Type type, string propertyName)
    {
        return GetProperties(type).FirstOrDefault(x => x.Name == propertyName);
    }

    /// <summary>
    /// 获取类型的字段
    /// </summary>
    /// <param name="type">待获取字段的类型</param>
    /// <param name="fieldName">字段名称</param>
    /// <returns></returns>
    public static FieldInfo GetField(Type type, string fieldName)
    {
        return GetFields(type).FirstOrDefault(x => x.Name == fieldName);
    }
    /// <summary>
    /// 判断类型是否为基础类型
    /// </summary>
    /// <param name="type">待检查类型</param>
    /// <returns></returns>
    public static bool IsBaseType(Type type)
    {
        return type.IsPrimitive || type == typeof(string) || type == typeof(decimal) || type == typeof(DateTime);
    }
    /// <summary>
    /// 判断类型是否为基础类型
    /// </summary>
    /// <typeparam name="T">待检查类型</typeparam>
    /// <returns></returns>
    public static bool IsBaseType<T>()
    {
        return IsBaseType(typeof(T));
    }
    /// <summary>
    /// 判断类型是否为可空类型
    /// </summary>
    /// <param name="type">待检查类型</param>
    /// <returns></returns>
    public static bool IsNullableType(Type type)
    {
        return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
    }
    /// <summary>
    /// 判断类型是否为可空类型
    /// </summary>
    /// <typeparam name="T">待检查类型</typeparam>
    /// <returns></returns>
    public static bool IsNullableType<T>()
    {
        return IsNullableType(typeof(T));
    }
    /// <summary>
    /// 判断类型是有指定属性
    /// </summary>
    /// <param name="type">待检查类型</param>
    /// <param name="propertyName">属性名称</param>
    /// <returns></returns>
    public static bool HasProperty(Type type, string propertyName)
    {
        return GetProperty(type, propertyName) != null;
    }
    /// <summary>
    /// 判断类型是有指定字段
    /// </summary>
    /// <param name="type">待检查类型</param>
    /// <param name="fieldName">字段名称</param>
    /// <returns></returns>
    public static bool HasField(Type type, string fieldName)
    {
        return GetField(type, fieldName) != null;
    }

    /// <summary>
    /// 获取对象的属性值
    /// </summary>
    /// <typeparam name="T">属性值类型</typeparam>
    /// <param name="obj">待获取属性值对象</param>
    /// <param name="propertyName">属性名称</param>
    /// <returns></returns>
    public static T GetPropertyValue<T>(object obj, string propertyName)
    {
        var property = GetProperty(obj.GetType(), propertyName);
        if (property == null)
        {
            return default;
        }
        var value = property.GetValue(obj);
        if (value == default) return default;
        // 检查value的类型是否与T兼容
        if (value is T t)
        {
            return t;
        }
        throw new InvalidCastException($"无法将{value.GetType()}转换为{typeof(T)}");
    }
    /// <summary>
    /// 获取对象的字段值
    /// </summary>
    /// <typeparam name="T">字段值类型</typeparam>
    /// <param name="obj">待获取字段值对象</param>
    /// <param name="fieldName">字段名称</param>
    /// <returns></returns>
    /// <exception cref="InvalidCastException"></exception>
    public static T GetFieldValue<T>(object obj, string fieldName)
    {
        var field = GetField(obj.GetType(), fieldName);
        if (field == null)
        {
            return default;
        }
        var value = field.GetValue(obj);
        if (value == default) return default;
        // 检查value的类型是否与T兼容
        if (value is T t)
        {
            return t;
        }
        throw new InvalidCastException($"无法将{value.GetType()}转换为{typeof(T)}");
    }
    /// <summary>
    /// 设置对象的属性值
    /// </summary>
    /// <param name="o">待设置对象</param>
    /// <param name="propertyName">属性名称</param>
    /// <param name="value">属性值</param>
    /// <exception cref="ArgumentException"></exception>
    public static void SetPropertyValue(object o, string propertyName, object value)
    {
        var property = GetProperty(o.GetType(), propertyName);
        if (property == null)
        {
            throw new ArgumentException($"对象中不存在{propertyName}属性");
        }
        property.SetValue(o, value);
    }

    /// <summary>
    /// 设置对象的字段值
    /// </summary>
    /// <param name="o">待设置对象</param>
    /// <param name="fieldName">字段名称</param>
    /// <param name="value">字段值</param>
    /// <exception cref="ArgumentException"></exception>
    public static void SetFieldValue(object o, string fieldName, object value)
    {
        var field = GetField(o.GetType(), fieldName);
        if (field == null)
        {
            throw new ArgumentException($"对象中不存在{fieldName}字段");
        }
        field.SetValue(o, value);
    }
}