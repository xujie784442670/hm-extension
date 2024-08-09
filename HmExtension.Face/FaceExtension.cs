using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ViewFaceCore.Model;

namespace HmExtension.Face;

public static class FaceExtension
{
    /// <summary>
    /// 注册人脸
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static Task<string> RegisterFace(this Image bitmap, string key = null)
    {
        return FaceHelper.RegisterFace(bitmap, key);
    }

    /// <summary>
    /// 查找人脸信息
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="faceType"></param>
    /// <param name="threshold"></param>
    /// <returns></returns>
    public static Task<string> Match(this Image bitmap, FaceType faceType = FaceType.Normal,
        float threshold = 0)
    {
        return FaceHelper.Match(bitmap, faceType, threshold);
    }

    /// <summary>
    /// 检测人脸位置
    /// </summary>
    /// <param name="bitmap"></param>
    /// <returns></returns>
    public static Task<FaceInfo[]> Detect(this Image bitmap)
    {
        return FaceHelper.Detect(bitmap);
    }

    /// <summary>
    /// 人脸关键点标记
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="faceInfo"></param>
    /// <returns></returns>
    public static FaceMarkPoint[] FaceMark(this Image bitmap, FaceInfo faceInfo, MarkType markType = MarkType.Normal)
    {
        return FaceHelper.FaceMark(bitmap, faceInfo, markType);
    }

    /// <summary>
    /// 人脸特征提取
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="markPoints"></param>
    /// <param name="faceType"></param>
    /// <param name="threshold"></param>
    /// <returns></returns>
    public static float[] Extract(this Image bitmap, FaceMarkPoint[] markPoints, FaceType faceType = FaceType.Normal,
        float threshold = 0)
    {
        return FaceHelper.Extract(bitmap, markPoints, faceType, threshold);
    }

    /// <summary>
    /// 计算俩张图片的相似度
    /// </summary>
    /// <param name="face1"></param>
    /// <param name="face2"></param>
    /// <returns></returns>
    public static Task<float> Compare(this Bitmap face1, Bitmap face2)
    {
        return FaceHelper.Compare(face1, face2);
    }

    /// <summary>
    /// 判断是否为同一个人
    /// </summary>
    /// <param name="face1"></param>
    /// <param name="face2"></param>
    /// <param name="faceType"></param>
    /// <param name="threshold"></param>
    /// <returns></returns>
    public static Task<bool> IsSelf(Image face1, Image face2, FaceType faceType = FaceType.Normal,
        float threshold = 0)
    {
        return FaceHelper.IsSelf(face1, face2, faceType, threshold);
    }

    /// <summary>
    /// 活体检测
    /// </summary>
    /// <param name="bitmap">待检测图片</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static Task<AntiSpoofingResult> LiveDetection(Bitmap bitmap)
    {
        return FaceHelper.LiveDetection(bitmap);
    }
}