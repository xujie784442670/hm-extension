using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ViewFaceCore;
using ViewFaceCore.Configs;
using ViewFaceCore.Core;
using ViewFaceCore.Model;

namespace HmExtension.Face;

public class FaceHelper
{
    public static IFacePersistence Persistence { get; set; } = new LocalFacePersistence();

    public static bool Debug { get; set; } = false;

    static FaceHelper()
    {
    }

    /// <summary>
    /// 注册人脸
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="key"></param>
    /// <param name="fdc">人脸检测配置</param>
    /// <param name="flc">人脸标记配置</param>
    /// <param name="frc">特征提取配置</param>
    /// <returns></returns>
    public static async Task<string> RegisterFace(Image bitmap,
        string key = null, FaceDetectConfig fdc = null,
        FaceLandmarkConfig flc = null, FaceRecognizeConfig frc = null)
    {
        key ??= Guid.NewGuid().ToString();
        var faceInfo = await Detect(bitmap, fdc);
        if (faceInfo == null) throw new Exception("未检测到人脸");
        if (faceInfo.Length > 1) throw new Exception("检测到多张人脸");
        var markPoints = FaceMark(bitmap, faceInfo[0], flc);
        var features = Extract(bitmap, markPoints, frc);
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
    /// <param name="fdc">人脸检测配置</param>
    /// <param name="flc">人脸标记配置</param>
    /// <param name="frc">特征提取配置</param>
    /// <returns></returns>
    public static async Task<string> Match(Image bitmap,
        FaceDetectConfig fdc=null,FaceLandmarkConfig flc=null,FaceRecognizeConfig frc=null)
    {
        var faceInfo = await Detect(bitmap, fdc);
        if (faceInfo.Length > 0)
        {
            var markPoints = FaceMark(bitmap, faceInfo[0], flc);
            var features = Extract(bitmap, markPoints, frc);
            return Persistence.Match(features, fdc,flc,frc);
        }

        return null;
    }


    /// <summary>
    /// 检测人脸位置
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static async Task<FaceInfo[]> Detect(Image bitmap, FaceDetectConfig config = null)
    {
        CheckModel("ViewFaceCore.model.face_detector");
        config ??= new FaceDetectConfig();
        using var face = new FaceDetector(config);
        return await face.DetectAsync(bitmap);
    }

    /// <summary>
    /// 人脸关键点标记
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="faceInfo"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static FaceMarkPoint[] FaceMark(Image bitmap, FaceInfo faceInfo, FaceLandmarkConfig config = null)
    {
        config ??= new FaceLandmarkConfig();
        CheckModel(config.MarkType switch
        {
            MarkType.Normal => "ViewFaceCore.model.face_landmarker_pts68",
            MarkType.Mask => "ViewFaceCore.model.face_landmarker_mask_pts5",
            _ => "ViewFaceCore.model.face_landmarker_pts5"
        });
        using var faceMark = new FaceLandmarker(config);
        return faceMark.Mark(bitmap, faceInfo);
    }

    /// <summary>
    /// 人脸特征提取
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="markPoints"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static float[] Extract(Image bitmap, FaceMarkPoint[] markPoints, FaceRecognizeConfig config = null)
    {
        config ??= new();
        CheckModel(config.FaceType switch
        {
            FaceType.Normal => "ViewFaceCore.model.face_recognizer",
            FaceType.Mask => "ViewFaceCore.model.face_recognizer_mask",
            _ => "ViewFaceCore.model.face_recognizer_light"
        });
        using var recognizer = new FaceRecognizer(config);
        return recognizer.Extract(bitmap, markPoints);
    }

    /// <summary>
    /// 计算俩张图片的相似度
    /// </summary>
    /// <param name="face1"></param>
    /// <param name="face2"></param>
    /// <param name="fdc">人脸检测配置</param>
    /// <param name="flc">关键点标记配置</param>
    /// <param name="frc">特征提取配置</param>
    /// <returns></returns>
    public static async Task<float> Compare(Bitmap face1, Bitmap face2,FaceDetectConfig fdc=null, FaceLandmarkConfig flc = null,
        FaceRecognizeConfig frc = null)
    {
        var detect = await Detect(face1,fdc);
        var detect2 = await Detect(face2, fdc);
        if (detect.Length > 0 && detect2.Length > 0)
        {
            var mark1 = FaceMark(face1, detect[0], flc);
            var mark2 = FaceMark(face2, detect2[0], flc);
            var extract1 = Extract(face1, mark1, frc);
            var extract2 = Extract(face2, mark2, frc);
            return Compare(extract1, extract2,frc);
        }

        return 0;
    }

    /// <summary>
    /// 计算人脸相似度
    /// </summary>
    /// <param name="face1"></param>
    /// <param name="face2"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static float Compare(float[] face1, float[] face2,
        FaceRecognizeConfig config=null)
    {

        config ??= new FaceRecognizeConfig();
        using var recognizer = new FaceRecognizer(config);
        return recognizer.Compare(face1, face2);
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
    public static async Task<bool> IsSelf(Image face1, Image face2, FaceDetectConfig fdc = null, FaceLandmarkConfig flc = null,
        FaceRecognizeConfig frc = null)
    {
        var detect = await Detect(face1,fdc);
        var detect2 = await Detect(face2, fdc);
        if (detect.Length > 0 && detect2.Length > 0)
        {
            var mark1 = FaceMark(face1, detect[0],flc);
            var mark2 = FaceMark(face2, detect2[0], flc);
            var extract1 = Extract(face1, mark1,frc);
            var extract2 = Extract(face2, mark2, frc);
            return IsSelf(extract1, extract2,frc);
        }

        return false;
    }

    /// <summary>
    /// 判断是否为同一个人
    /// </summary>
    /// <param name="face1"></param>
    /// <param name="face2"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static bool IsSelf(float[] face1, float[] face2,
        FaceRecognizeConfig config=null)
    {
        config ??= new FaceRecognizeConfig();
        CheckModel(config.FaceType switch
        {
            FaceType.Normal => "ViewFaceCore.model.face_recognizer",
            FaceType.Mask => "ViewFaceCore.model.face_recognizer_mask",
            _ => "ViewFaceCore.model.face_recognizer_light"
        });
        using var recognizer = new FaceRecognizer(config);
        return recognizer.IsSelf(face1, face2);
    }

    /// <summary>
    /// 活体检测
    /// </summary>
    /// <param name="bitmap">待检测图片</param>
    /// <param name="config"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static async Task<AntiSpoofingResult> LiveDetection(Bitmap bitmap,FaceDetectConfig fdc=null,FaceLandmarkConfig flc=null, FaceAntiSpoofingConfig fasc = null)
    {
        var faceInfo = await Detect(bitmap, fdc);
        // 检查是否有人脸
        if (faceInfo == null || !faceInfo.Any()) throw new Exception("未检测到人脸");
        // 检查是否有多张人脸
        if (faceInfo.Length > 1) throw new Exception("检测到多张人脸");
        // 人脸关键点标记
        var markPoints = FaceMark(bitmap, faceInfo[0],flc);
        // 活体检查
        fasc ??= new FaceAntiSpoofingConfig();
        // 活体检测识别器可以加载一个局部检测模型或者局部检测模型+全局检测模型，使用参数Global来区分，默认为True。
        // 当使用局部检测模型时，需要安装模型ViewFaceCore.model.fas_second。 当使用局部检测模型+全局检测模型时，需要安装模型ViewFaceCore.model.fas_first和ViewFaceCore.model.fas_second。
        CheckModel("ViewFaceCore.model.fas_second");
        if (fasc.Global)
        {
            CheckModel("ViewFaceCore.model.fas_first");
        }
        using FaceAntiSpoofing faceAntiSpoofing = new FaceAntiSpoofing(fasc);
        var result = faceAntiSpoofing.AntiSpoofing(bitmap, faceInfo[0], markPoints);
        return result;
    }

    /// <summary>
    /// 人脸追踪
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static FaceTrackInfo[] FaceTracker(Bitmap bitmap, FaceTrackerConfig config = null)
    {
        using var faceImage = bitmap.ToFaceImage();
        config ??= new FaceTrackerConfig(faceImage.Width, faceImage.Height);
        using FaceTracker faceTrack = new FaceTracker(config);
        return faceTrack.Track(faceImage);
    }

    /// <summary>
    /// 口罩检测
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static async Task<PlotMaskResult> MaskDetector(Bitmap bitmap,FaceDetectConfig fdc=null, MaskDetectConfig config = null)
    {
        config ??= new MaskDetectConfig();
        CheckModel("ViewFaceCore.model.mask_detector");
        using MaskDetector md = new MaskDetector(config);
        var faceInfos = await bitmap.Detect(fdc);
        if (faceInfos == null || !faceInfos.Any()) throw new Exception("未检测到人脸");
        if (faceInfos.Length > 1) throw new Exception("检测到多张人脸");
        return md.PlotMask(bitmap.ToFaceImage(), faceInfos[0]);
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
    public static async Task<QualityResult> FaceQuality(Bitmap bitmap, QualityType type,FaceDetectConfig fdc=null,FaceLandmarkConfig flc=null)
    {
        switch (type)
        {
            case QualityType.Pose:
            case QualityType.PoseEx:
            case QualityType.ClarityEx:
                CheckModel("ViewFaceCore.model.pose_estimation");
                break;
            default:
                CheckModel("ViewFaceCore.model.quality_lbn");
                break;
        }

        using var faceQuality = new FaceQuality();
        var faceInfos = await Detect(bitmap,fdc);
        if (faceInfos == null || !faceInfos.Any()) throw new Exception("未检测到人脸");
        if (faceInfos.Length > 1) throw new Exception("检测到多张人脸");
        var faceMarkPoints = bitmap.FaceMark(faceInfos[0],flc);
        return faceQuality.Detect(bitmap, faceInfos[0], faceMarkPoints, type);
    }

    /// <summary>
    /// 年龄预测
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="fdc">人脸检测配置</param>
    /// <param name="flc">人脸标记配置</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static async Task<int> AgePredictor(Bitmap bitmap, FaceDetectConfig fdc = null, FaceLandmarkConfig flc = null)
    {
        CheckModel("ViewFaceCore.model.age_predictor");
        using var ap = new AgePredictor();
        var faceInfos = await Detect(bitmap,fdc);
        if (faceInfos == null || !faceInfos.Any()) throw new Exception("未检测到人脸");
        if (faceInfos.Length > 1) throw new Exception("检测到多张人脸");
        var faceMarkPoints = bitmap.FaceMark(faceInfos[0],flc);
        return ap.PredictAge(bitmap.ToFaceImage(), faceMarkPoints);
    }

    /// <summary>
    /// 性别预测
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="fdc">人脸检测配置</param>
    /// <param name="flc">人脸标记配置</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static async Task<Gender> GenderPredictor(Bitmap bitmap, FaceDetectConfig fdc = null, FaceLandmarkConfig flc = null)
    {
        CheckModel("ViewFaceCore.model.gender_predictor");
        using var ap = new GenderPredictor();
        var faceInfos = await Detect(bitmap, fdc);
        if (faceInfos == null || !faceInfos.Any()) throw new Exception("未检测到人脸");
        if (faceInfos.Length > 1) throw new Exception("检测到多张人脸");
        var faceMarkPoints = bitmap.FaceMark(faceInfos[0], flc);
        return ap.PredictGender(bitmap.ToFaceImage(), faceMarkPoints);
    }

    /// <summary>
    /// 眼睛状态检测
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="fdc">人脸检测配置</param>
    /// <param name="flc">人脸标记配置</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static async Task<EyeStateResult> EyeStateDetector(Bitmap bitmap, FaceDetectConfig fdc = null, FaceLandmarkConfig flc = null)
    {
        CheckModel("ViewFaceCore.model.eye_state");
        using var ap = new EyeStateDetector();
        var faceInfos = await Detect(bitmap, fdc);
        if (faceInfos == null || !faceInfos.Any()) throw new Exception("未检测到人脸");
        if (faceInfos.Length > 1) throw new Exception("检测到多张人脸");
        var faceMarkPoints = bitmap.FaceMark(faceInfos[0], flc);
        return ap.Detect(bitmap.ToFaceImage(), faceMarkPoints);
    }

    public static readonly string Model_Path =
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "viewfacecore", "models");

    /// <summary>
    /// 检查模型是否安装
    /// </summary>
    /// <param name="modeName"></param>
    public static void CheckModel(string modeName)
    {
        // ViewFaceCore.model.face_recognizer
        var fileName = modeName.Split('.').Last() + ".csta";
        var modelPath = Path.Combine(Model_Path, fileName);
        if (!File.Exists(modelPath))
        {
            throw new Exception($"模型{modeName}未安装");
        }
    }
}