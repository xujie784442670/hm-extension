# 虹猫通用扩展包

## 1. 介绍
该扩展包是虹猫系统的通用扩展包，提供了一些常用类的扩展方法，以及一些常用的工具类。

主要包括以下内容：
- 扩展类

| 类名 | 说明 |
| --- | --- |
| ObjectExtension | Object扩展类 |
| StringExtension | String扩展类 |
| CollectionExtension | 集合扩展类 |
|BaseTypeExtension|基础类型扩展类|
|ByteArrayExtension|Byte数组扩展类|
|StreamExtension|流扩展类(v1.0.0.3加入)|

## 2. 安装
```shell
dotnet add package hongmao.HmExtension --version 1.0.0.2
```

## 3. 更新日志

### v1.0.0.3
1. ObjectExtension类新增FormatPatten方法
2. StringExtension类新增IsDataUrl方法
3. StringExtension类新增FromBase64ToBytes方法
4. StringExtension类新增FromBitmap方法
5. ByteArrayExtension类新增ToMd5方法
6. ByteArrayExtension类新增ToBase64方法
7. ByteArrayExtension类新增ToBitmap方法
8. 新增StreamExtension类
9. 新增BitmapExtension类

### v1.0.0.2
- 修复了一些bug
- 优化了一些代码
- StringExtension类新增FromJson方法
- StringExtension类新增ToQRCode方法
- ObjectExtension类新增ToJson方法

### v1.0.0.1
- 初始版本
- 提供了Object、String、Collection、BaseType、ByteArray等类的扩展方法