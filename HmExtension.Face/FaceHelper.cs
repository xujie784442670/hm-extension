using System;
using System.Drawing;
using System.Threading.Tasks;
using ViewFaceCore;
using ViewFaceCore.Configs;
using ViewFaceCore.Core;
using ViewFaceCore.Model;

namespace HmExtension.Face;

public class FaceHelper
{
    public static IFacePersistence Persistence { get; set; } = new LocalFacePersistence();

    /// <summary>
    /// 注册人脸
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static async Task<string> RegisterFace(Image bitmap, string key = null)
    {
        key ??= Guid.NewGuid().ToString();
        var faceInfo = await Detect(bitmap);
        if (faceInfo == null) throw new Exception("未检测到人脸");
        if (faceInfo.Length > 1) throw new Exception("检测到多张人脸");
        var markPoints = FaceMark(bitmap, faceInfo[0]);
        var features = Extract(bitmap, markPoints);
        Persistence.Save(key, features);
        return key;
    }

    /// <summary>
    /// 移除人脸
    /// </summary>
    /// <param name="key"></param>
    public static void RemoveFace(string key)
    {
        Persistence.Remove(key);
    }

    /// <summary>
    /// 查找人脸信息
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="faceType"></param>
    /// <param name="threshold"></param>
    /// <returns></returns>
    public static async Task<string> Match(Image bitmap, FaceType faceType = FaceType.Normal,
        float threshold = 0)
    {
        var faceInfo = await Detect(bitmap);
        if (faceInfo.Length > 0)
        {
            var markPoints = FaceMark(bitmap, faceInfo[0]);
            var features = Extract(bitmap, markPoints);
            return Persistence.Match(features, faceType, threshold);
        }

        return null;
    }


    /// <summary>
    /// 检测人脸位置
    /// </summary>
    /// <param name="bitmap"></param>
    /// <returns></returns>
    public static async Task<FaceInfo[]> Detect(Image bitmap)
    {
        using var face = new FaceDetector();
        return await face.DetectAsync(bitmap);
    }

    /// <summary>
    /// 人脸关键点标记
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="faceInfo"></param>
    /// <returns></returns>
    public static FaceMarkPoint[] FaceMark(Image bitmap, FaceInfo faceInfo, MarkType markType = MarkType.Normal)
    {
        using var faceMark = new FaceLandmarker(new FaceLandmarkConfig()
        {
            MarkType = markType
        });
        return faceMark.Mark(bitmap, faceInfo);
    }

    /// <summary>
    /// 人脸特征提取
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="markPoints"></param>
    /// <param name="faceType"></param>
    /// <param name="threshold"></param>
    /// <returns></returns>
    public static float[] Extract(Image bitmap, FaceMarkPoint[] markPoints, FaceType faceType = FaceType.Normal,
        float threshold = 0)
    {
        if (threshold == 0)
        {
            threshold = faceType switch
            {
                FaceType.Normal => 0.62F,
                FaceType.Mask => 0.4F,
                FaceType.Light => 0.55F,
                _ => 0.62F
            };
        }

        var faceRecognizeConfig = new FaceRecognizeConfig();
        faceRecognizeConfig.SetThreshold(faceType, threshold);
        using var recognizer = new FaceRecognizer(faceRecognizeConfig);
        return recognizer.Extract(bitmap, markPoints);
    }

    /// <summary>
    /// 计算俩张图片的相似度
    /// </summary>
    /// <param name="face1"></param>
    /// <param name="face2"></param>
    /// <returns></returns>
    public static async Task<float> Compare(Bitmap face1, Bitmap face2)
    {
        var detect = await Detect(face1);
        var detect2 = await Detect(face2);
        if (detect.Length > 0 && detect2.Length > 0)
        {
            var mark1 = FaceMark(face1, detect[0]);
            var mark2 = FaceMark(face2, detect2[0]);
            var extract1 = Extract(face1, mark1);
            var extract2 = Extract(face2, mark2);
            return Compare(extract1, extract2);
        }

        return 0;
    }

    /// <summary>
    /// 计算人脸相似度
    /// </summary>
    /// <param name="face1"></param>
    /// <param name="face2"></param>
    /// <param name="faceType"></param>
    /// <param name="threshold"></param>
    /// <returns></returns>
    public static float Compare(float[] face1, float[] face2,
        FaceType faceType = FaceType.Normal,
        float threshold = 0)
    {
        if (threshold == 0)
        {
            threshold = faceType switch
            {
                FaceType.Normal => 0.62F,
                FaceType.Mask => 0.4F,
                FaceType.Light => 0.55F,
                _ => 0.62F
            };
        }

        var faceRecognizeConfig = new FaceRecognizeConfig();
        faceRecognizeConfig.SetThreshold(faceType, threshold);
        using var recognizer = new FaceRecognizer(faceRecognizeConfig);
        return recognizer.Compare(face1, face2);
    }

    /// <summary>
    /// 判断是否为同一个人
    /// </summary>
    /// <param name="face1"></param>
    /// <param name="face2"></param>
    /// <param name="faceType"></param>
    /// <param name="threshold"></param>
    /// <returns></returns>
    public static async Task<bool> IsSelf(Image face1, Image face2, FaceType faceType = FaceType.Normal,
        float threshold = 0)
    {
        var detect = await Detect(face1);
        var detect2 = await Detect(face2);
        if (detect.Length > 0 && detect2.Length > 0)
        {
            var mark1 = FaceMark(face1, detect[0]);
            var mark2 = FaceMark(face2, detect2[0]);
            var extract1 = Extract(face1, mark1);
            var extract2 = Extract(face2, mark2);
            return IsSelf(extract1, extract2, faceType, threshold);
        }

        return false;
    }

    /// <summary>
    /// 判断是否为同一个人
    /// </summary>
    /// <param name="face1"></param>
    /// <param name="face2"></param>
    /// <param name="faceType"></param>
    /// <param name="threshold"></param>
    /// <returns></returns>
    public static bool IsSelf(float[] face1, float[] face2,
        FaceType faceType = FaceType.Normal,
        float threshold = 0)
    {
        if (threshold == 0)
        {
            threshold = faceType switch
            {
                FaceType.Normal => 0.62F,
                FaceType.Mask => 0.4F,
                FaceType.Light => 0.55F,
                _ => 0.62F
            };
        }

        var faceRecognizeConfig = new FaceRecognizeConfig();
        faceRecognizeConfig.SetThreshold(faceType, threshold);
        using var recognizer = new FaceRecognizer(faceRecognizeConfig);
        return recognizer.IsSelf(face1, face2);
    }
}