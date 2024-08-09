using System.Collections.Generic;
using System.Drawing;
using System.IO;
using HmExtension.Commons.Extensions;
using ViewFaceCore.Configs;
using ViewFaceCore.Model;

namespace HmExtension.Face;

public interface IFacePersistence
{
    /// <summary>
    /// 保存人脸特征,如果已存在则覆盖
    /// </summary>
    /// <param name="key">人脸特征唯一标识</param>
    /// <param name="features">人脸特征</param>
    public void Save(string key, float[] features);

    /// <summary>
    /// 删除人脸特征
    /// </summary>
    /// <param name="key"></param>
    public void Remove(string key);

    /// <summary>
    /// 匹配人脸特征,返回匹配的人脸特征唯一标识
    /// </summary>
    /// <param name="features"></param>
    /// <param name="fdc">人脸检测配置</param>
    /// <param name="flc">人脸标记配置</param>
    /// <param name="frc">特征提取配置</param>
    /// <returns></returns>
    public string Match(float[] features, FaceDetectConfig fdc = null, FaceLandmarkConfig flc = null, FaceRecognizeConfig frc = null);
}

/// <summary>
/// 本地人脸特征持久化
/// </summary>
public class LocalFacePersistence : IFacePersistence
{
    public string Path { get; set; } = "faces.json";

    private Dictionary<string, float[]> _faces;

    public LocalFacePersistence()
    {
        if (!System.IO.File.Exists(Path))
        {
            System.IO.File.WriteAllText(Path, "{}");
        }
        _faces = File.ReadAllText(Path).FromJson<Dictionary<string, float[]>>();
    }

    private void Store()
    {
        // 写入新内容
        File.WriteAllText(Path, _faces.ToJson());
    }

    public void Save(string key, float[] features)
    {
        var json = features.ToJson();
        _faces[key] = features;
        Store();
    }

    public void Remove(string key)
    {
        _faces.Remove(key);
        Store();
    }

    public string Match(float[] features, FaceDetectConfig fdc = null, FaceLandmarkConfig flc = null,
        FaceRecognizeConfig frc = null)
    {
        foreach (var kv in _faces)
        {
            if (FaceHelper.IsSelf(kv.Value, features, frc))
            {
                return kv.Key;
            }
        }
        return null;
    }
}