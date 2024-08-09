using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ViewFaceCore.Configs;
using ViewFaceCore.Model;

namespace HmExtension.Face;

public static class FaceExtension
{
    /// <summary>
    /// 注册人脸
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="key"></param>
    /// <param name="fdc">人脸检测配置</param>
    /// <param name="flc">人脸标记配置</param>
    /// <param name="frc">特征提取配置</param>
    /// <returns></returns>
    public static Task<string> RegisterFace(this Image bitmap, string key = null, FaceDetectConfig fdc = null, FaceLandmarkConfig flc = null,
        FaceRecognizeConfig frc = null)
    {
        return FaceHelper.RegisterFace(bitmap, key,fdc,flc,frc);
    }

    /// <summary>
    /// 查找人脸信息
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="fdc">人脸检测配置</param>
    /// <param name="flc">人脸标记配置</param>
    /// <param name="frc">特征提取配置</param>
    /// <returns></returns>
    public static Task<string> Match(this Image bitmap, FaceDetectConfig fdc = null, FaceLandmarkConfig flc = null,
        FaceRecognizeConfig frc = null)
    {
        return FaceHelper.Match(bitmap, fdc, flc,frc);
    }

    /// <summary>
    /// 检测人脸位置
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static Task<FaceInfo[]> Detect(this Image bitmap,FaceDetectConfig config=null)
    {
        return FaceHelper.Detect(bitmap, config);
    }

    /// <summary>
    /// 人脸关键点标记
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="faceInfo"></param>
    /// <param name="config">人脸标记配置</param>
    /// <returns></returns>
    public static FaceMarkPoint[] FaceMark(this Image bitmap, FaceInfo faceInfo, FaceLandmarkConfig config=null)
    {
        return FaceHelper.FaceMark(bitmap, faceInfo, config);
    }

    /// <summary>
    /// 人脸关键点标记
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="fdc">人脸检测配置</param>
    /// <param name="config">人脸标记配置</param>
    /// <returns></returns>
    public static async Task<FaceMarkPoint[]> FaceMark(this Image bitmap, FaceDetectConfig fdc, FaceLandmarkConfig config=null)
    {
        var faceInfos =await FaceHelper.Detect(bitmap, fdc);
        if(faceInfos == null || faceInfos.Length == 0)
        {
            throw new Exception("未检测到人脸");
        }
        if(faceInfos.Length > 1)
        {
            throw new Exception("检测到多张人脸");
        }
        return FaceHelper.FaceMark(bitmap, faceInfos.First(), config);
    }

    /// <summary>
    /// 人脸特征提取
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="markPoints"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static float[] Extract(this Image bitmap, FaceMarkPoint[] markPoints, FaceRecognizeConfig config=null)
    {
        return FaceHelper.Extract(bitmap, markPoints, config);
    }

    /// <summary>
    /// 人脸特征提取
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="flc">人脸标记配置</param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static async Task<float[]> Extract(this Image bitmap,FaceLandmarkConfig flc, FaceRecognizeConfig config=null)
    {
        var faceInfos =await FaceHelper.Detect(bitmap);
        if(faceInfos == null || faceInfos.Length == 0)
        {
            throw new Exception("未检测到人脸");
        }
        if(faceInfos.Length > 1)
        {
            throw new Exception("检测到多张人脸");
        }
        var markPoints = FaceHelper.FaceMark(bitmap, faceInfos.First(), flc);
        return FaceHelper.Extract(bitmap, markPoints, config);
    }

    /// <summary>
    /// 计算俩张图片的相似度
    /// </summary>
    /// <param name="face1"></param>
    /// <param name="face2"></param>
    /// <param name="fdc">人脸检测配置</param>
    /// <param name="flc">人脸标记配置</param>
    /// <param name="frc">特征提取配置</param>
    /// <returns></returns>
    public static Task<float> Compare(this Bitmap face1, Bitmap face2, FaceDetectConfig fdc = null, FaceLandmarkConfig flc = null,
        FaceRecognizeConfig frc = null)
    {
        return FaceHelper.Compare(face1, face2,fdc,flc,frc);
    }

    /// <summary>
    /// 判断是否为同一个人
    /// </summary>
    /// <param name="face1"></param>
    /// <param name="face2"></param>
    /// <param name="fdc">人脸检测配置</param>
    /// <param name="flc">人脸标记配置</param>
    /// <param name="frc">特征提取配置</param>
    /// <returns></returns>
    public static Task<bool> IsSelf(this Image face1, Image face2, FaceDetectConfig fdc = null, FaceLandmarkConfig flc = null,
        FaceRecognizeConfig frc = null)
    {
        return FaceHelper.IsSelf(face1, face2,fdc,flc,frc);
    }

    /// <summary>
    /// 活体检测
    /// </summary>
    /// <param name="bitmap">待检测图片</param>
    /// <param name="fdc">人脸检测配置</param>
    /// <param name="flc">人脸标记配置</param>
    /// <param name="fasc">活体检测配置</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static Task<AntiSpoofingResult> LiveDetection(this Bitmap bitmap, FaceDetectConfig fdc = null, FaceLandmarkConfig flc = null,
        FaceAntiSpoofingConfig fasc=null)
    {
        return FaceHelper.LiveDetection(bitmap,fdc,flc,fasc);
    }

    /// <summary>
    /// 人脸追踪
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static FaceTrackInfo[] FaceTracker(this Bitmap bitmap, FaceTrackerConfig config = null)
    {
        return FaceHelper.FaceTracker(bitmap, config);
    }

    /// <summary>
    /// 口罩检测
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="fdc">人脸检测配置</param>
    /// <param name="config"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static async Task<PlotMaskResult> MaskDetector(this Bitmap bitmap,FaceDetectConfig fdc=null, MaskDetectConfig config = null)
    {
        return await FaceHelper.MaskDetector(bitmap,fdc, config);
    }

    /// <summary>
    /// 人脸质量评估
    /// </summary>
    /// <param name="bitmap">待评估图片</param>
    /// <param name="type">评估类型</param>
    /// <param name="fdc">人脸检测配置</param>
    /// <param name="flc">人脸标记配置</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static async Task<QualityResult> FaceQuality(this Bitmap bitmap, QualityType type, FaceDetectConfig fdc = null, FaceLandmarkConfig flc = null)
    {
        return await FaceHelper.FaceQuality(bitmap, type,fdc,flc);
    }


    /// <summary>
    /// 年龄预测
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="fdc">人脸检测配置</param>
    /// <param name="flc">人脸标记配置</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static async Task<int> AgePredictor(this Bitmap bitmap, FaceDetectConfig fdc = null, FaceLandmarkConfig flc = null)
    {
        return await FaceHelper.AgePredictor(bitmap,fdc,flc);
    }

    /// <summary>
    /// 眼睛状态检测
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="fdc">人脸检测配置</param>
    /// <param name="flc">人脸标记配置</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static async Task<EyeStateResult> EyeStateDetector(this Bitmap bitmap, FaceDetectConfig fdc = null, FaceLandmarkConfig flc = null)
    {
        return await FaceHelper.EyeStateDetector(bitmap,fdc,flc);
    }
}