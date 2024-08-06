# 虹猫信息扩展包-人脸识别模块
## 1. 介绍
虹猫信息扩展包-人脸识别模块是虹猫信息扩展包的一个子模块，主要用于人脸识别功能的实现。该模块基于虹软人脸识别SDK，实现了人脸识别功能，支持人脸检测、人脸特征提取、人脸比对等功能。

## 2. 功能
- 人脸检测
- 标记关键点
- 人脸特征提取
- 人脸比对
- 人脸注册
- 人脸删除
- 人脸搜索

## 3. 使用
### 3.1. 人脸检测
```C#
using HmExtension.Face;

public class FaceDetectExample
{
	public static async void Main(string[] args){
		var bitmap = new Bitmap("test.jpg");
        var faceInfos =await FaceHelper.Detect(bitmap);
		foreach (var faceInfo in faceInfos)
		{
			Console.WriteLine($"人脸位置: {faceInfo.Location}");
		}
	}
}
```

### 3.2. 标记关键点
```C#
using HmExtension.Face;

public class FaceLandmarkExample
{
	public static async void Main(string[] args){
		var bitmap = new Bitmap("test.jpg");
		var faceInfos =await FaceHelper.Detect(bitmap);
		foreach (var faceInfo in faceInfos)
		{
			var faceMarkPoints = await FaceHelper.FaceMark(bitmap, faceInfo);
			foreach (var faceMarkPoint in faceMarkPoints)
			{
				Console.WriteLine($"关键点: {faceMarkPoint}");
			}
		}
	}
}
```

### 3.3. 人脸特征提取
```C#
using HmExtension.Face;
public class FaceFeatureExample
{
	public static async void Main(string[] args){
		var bitmap = new Bitmap("test.jpg");
		var faceInfos =await FaceHelper.Detect(bitmap);
		foreach (var faceInfo in faceInfos)
		{
			var faceMarkPoints = await FaceHelper.FaceMark(bitmap, faceInfo);
			var extract = FaceHelper.Extract(bitmap, faceMarkPoints);
			Console.WriteLine($"人脸特征: {extract}");
		}
	}
}
```

### 3.4. 人脸比对
```C#
using HmExtension.Face;

public class FaceCompareExample
{
	
	public static async void Main(string[] args){
		var bitmap1 = new Bitmap("test1.jpg");
		var bitmap2 = new Bitmap("test2.jpg");
		// 方法1
		Compare1(bitmap1,bitmap2);
		// 方法2
		Compare2(bitmap1,bitmap2);
		// 方法3
		Compare3(bitmap1,bitmap2);
		// 方法4
		Compare4(bitmap1,bitmap2);
	}

	// 方法1
	public void Compare1(Bitmap bitmap1,Bitmap bitmap2){
		var faceInfos1 =await FaceHelper.Detect(bitmap1);
		var faceInfos2 =await FaceHelper.Detect(bitmap2);
		// 标记关键点
		var faceMarkPoints1 = await FaceHelper.FaceMark(bitmap1, faceInfo1[0]);
		var faceMarkPoints2 = await FaceHelper.FaceMark(bitmap1, faceInfo2[0]);
		// 提取特征
		var extract1 = FaceHelper.Extract(bitmap1, faceMarkPoints1);
		var extract2 = FaceHelper.Extract(bitmap2, faceMarkPoints2);
		var score = FaceHelper.Compare(extract1, extract2);
		Console.WriteLine($"人脸相似度: {score}");
	}

	// 方法2
	public void Compare2(Bitmap bitmap1,Bitmap bitmap2){
		var score = FaceHelper.Compare(bitmap1, bitmap2);
		Console.WriteLine($"人脸相似度: {score}");
	}

	// 方法3
	public void Compare3(Bitmap bitmap1,Bitmap bitmap2){
		var faceInfos1 =await FaceHelper.Detect(bitmap1);
		var faceInfos2 =await FaceHelper.Detect(bitmap2);
		// 标记关键点
		var faceMarkPoints1 = await FaceHelper.FaceMark(bitmap1, faceInfo1[0]);
		var faceMarkPoints2 = await FaceHelper.FaceMark(bitmap1, faceInfo2[0]);
		// 提取特征
		var extract1 = FaceHelper.Extract(bitmap1, faceMarkPoints1);
		var extract2 = FaceHelper.Extract(bitmap2, faceMarkPoints2);
		// 检查是否是同一个人
		var isSamePerson =await FaceHelper.IsSelf(extract1, extract2);
	}

	// 方法4
	public void Compare4(Bitmap bitmap1,Bitmap bitmap2){
		var isSamePerson = FaceHelper.IsSelf(bitmap1, bitmap2);
	}
}
```

### 3.5. 人脸注册
```C#
using HmExtension.Face;

public class FaceRegisterExample
{
	public static async void Main(string[] args){
		var bitmap = new Bitmap("test.jpg");
		var key = await RegisterFace(bitmap, "test");
	}
}
```

### 3.6. 人脸删除
```C#
using HmExtension.Face;

public class FaceDeleteExample
{
	public static async void Main(string[] args){
		var bitmap = new Bitmap("test.jpg");
		var key = await RegisterFace(bitmap, "test");
		FaceHelper.RemoveFace(key);
	}
}
```


### 3.7. 人脸搜索
```C#

using HmExtension.Face;

public class FaceSearchExample
{
	public static async void Main(string[] args){
		var bitmap = new Bitmap("test.jpg");
		var key = await RegisterFace(bitmap, "test");
		var result =await FaceHelper.Match(bitmap);
		Console.WriteLine($"人脸搜索结果: {result}");
	}
}
```