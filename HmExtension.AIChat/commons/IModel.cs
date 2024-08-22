using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;

namespace HmExtension.AIChat.commons;
/// <summary>
/// 模型信息接口
/// </summary>
/// <typeparam name="TV"></typeparam>
public interface IModel<TV>
{
    /// <summary>
    /// 模型名称
    /// </summary>
    /// <returns></returns>
    string Name { get; }
    /// <summary>
    /// 模型描述
    /// </summary>
    /// <returns></returns>
    string Description { get; }
    /// <summary>
    /// 模型值
    /// </summary>
    /// <returns></returns>
    TV Value { get; }
    /// <summary>
    /// 获取所有模型信息
    /// </summary>
    /// <returns></returns>
    List<IModel<TV>> GetModels();
    /// <summary>
    /// 根据模型名称获取模型信息
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IModel<TV> ValueOf(string name);

}

public interface IModel : IModel<string>
{
}

public abstract class BaseModel(string name, string description, string value) : BaseModel<string>(name, description, value)
{
}

public abstract class BaseModel<TV>(string name, string description, TV value) : IModel<TV>
{
    public string Name { get; } = name;

    public string Description { get; } = description;

    public TV Value { get; } = value;

    private static readonly Dictionary<string, List<object>> _models=new();

    public static IModel AddModel(string key, IModel model)
    {
        if (!_models.TryGetValue(key, out var models))
        {
            _models[key] = models = new();
        }
        models.Add(model);
        return model;
    }

    public static T AddModel<T>(string key,T model)
    {
        if (!_models.TryGetValue(key, out var models))
        {
            _models[key] = models = new();
        }
        models.Add(model);
        return model;
    }

    public abstract List<IModel<TV>> GetModels();

    public abstract IModel<TV> ValueOf(string name);


    public static List<IModel<TV>> GetModels(string key)
    {
        if (_models.TryGetValue(key, out var model))
        {
            return [.. model.Cast<IModel<TV>>()];
        }
        return [];
    }

    public static IModel<TV> ValueOf(Type type,object value)
    {
        return GetModels(type.Name).FirstOrDefault(m => m.Value.Equals(value));
    }
}