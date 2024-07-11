using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HmExtension.Commons.utils;

/// <summary>
/// Rsa密钥对
/// </summary>
public class RsaKey
{
    /// <summary>
    /// 公钥
    /// </summary>
    public string PublicKey;

    /// <summary>
    /// 私钥
    /// </summary>
    public string PrivateKey;
}

/// <summary>
/// Rsa加密解密及Rsa签名和验证
/// </summary>
public static class RsaHelper
{
    private static RsaKey RsaKey = GenerateRsaKeys();

    #region Rsa 加密解密

    #region Rsa 的密钥产生

    /// <summary>
    /// 生成 Rsa 公钥和私钥
    /// </summary>
    public static RsaKey GenerateRsaKeys()
    {
        using (var rsa = new RSACryptoServiceProvider())
        {
            return RsaKey ?? (RsaKey = new RsaKey
            {
                PrivateKey = rsa.ToXmlString(true),
                PublicKey = rsa.ToXmlString(false)
            });
        }
    }

    #endregion

    #region Rsa的加密函数

    /// <summary>
    /// Rsa的加密函数 string
    /// </summary>
    /// <param name="publicKey">公钥</param>
    /// <param name="mStrEncryptString">需要加密的字符串</param>
    /// <returns>加密后的内容</returns>
    /// <exception cref="CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
    public static string RsaEncrypt(this string mStrEncryptString, string publicKey)
    {
        var rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(publicKey);
        var plainTextBArray = new UnicodeEncoding().GetBytes(mStrEncryptString);
        var cypherTextBArray = rsa.Encrypt(plainTextBArray, false);
        return Convert.ToBase64String(cypherTextBArray);
    }

    /// <summary>
    /// Rsa的加密函数 string
    /// </summary>
    /// <param name="mStrEncryptString">需要加密的字符串</param>
    /// <returns>加密后的内容</returns>
    /// <exception cref="CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
    public static string RsaEncrypt(this string mStrEncryptString)
    {
        return RsaEncrypt(mStrEncryptString, RsaKey.PublicKey);
    }

    /// <summary>
    /// Rsa的加密函数 byte[]
    /// </summary>
    /// <param name="encryptString">需要加密的字节数组</param>
    /// <param name="publicKey">公钥</param>
    /// <returns>加密后的内容</returns>
    /// <exception cref="CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
    public static string RsaEncrypt(this byte[] encryptString, string publicKey)
    {
        var rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(publicKey);
        var cypherTextBArray = rsa.Encrypt(encryptString, false);
        return Convert.ToBase64String(cypherTextBArray);
    }

    /// <summary>
    /// Rsa的加密函数 byte[]
    /// </summary>
    /// <param name="encryptString">需要加密的字节数组</param>
    /// <returns>加密后的内容</returns>
    /// <exception cref="CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
    public static string RsaEncrypt(this byte[] encryptString)
    {
        return RsaEncrypt(encryptString, RsaKey.PublicKey);
    }

    #endregion

    #region Rsa的解密函数

    /// <summary>
    /// Rsa的解密函数  string
    /// </summary>
    /// <param name="mStrDecryptString">需要解密的字符串</param>
    /// <param name="privateKey">私钥</param>
    /// <returns>解密后的内容</returns>
    /// <exception cref="CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
    public static string RsaDecrypt(this string mStrDecryptString, string privateKey)
    {
        var rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(privateKey);
        var plainTextBArray = Convert.FromBase64String(mStrDecryptString);
        var dypherTextBArray = rsa.Decrypt(plainTextBArray, false);
        return new UnicodeEncoding().GetString(dypherTextBArray);
    }

    /// <summary>
    /// Rsa的解密函数  string
    /// </summary>
    /// <param name="mStrDecryptString">需要解密的字符串</param>
    /// <returns>解密后的内容</returns>
    /// <exception cref="CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
    public static string RsaDecrypt(this string mStrDecryptString)
    {
        return RsaDecrypt(mStrDecryptString, RsaKey.PrivateKey);
    }

    /// <summary>
    /// Rsa的解密函数  byte
    /// </summary>
    /// <param name="decryptString">需要解密的字符串</param>
    /// <param name="privateKey">私钥</param>
    /// <returns>解密后的内容</returns>
    /// <exception cref="CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
    public static string RsaDecrypt(this byte[] decryptString, string privateKey)
    {
        var rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(privateKey);
        var dypherTextBArray = rsa.Decrypt(decryptString, false);
        return new UnicodeEncoding().GetString(dypherTextBArray);
    }

    /// <summary>
    /// Rsa的解密函数  byte
    /// </summary>
    /// <param name="decryptString">需要解密的字符串</param>
    /// <returns>解密后的内容</returns>
    /// <exception cref="CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
    public static string RsaDecrypt(this byte[] decryptString)
    {
        return RsaDecrypt(decryptString, RsaKey.PrivateKey);
    }

    #endregion

    #endregion

    #region Rsa数字签名

    #region 获取Hash描述表

    /// <summary>
    /// 获取Hash描述表
    /// </summary>
    /// <param name="mStrSource">源数据</param>
    /// <returns>Hash描述表</returns>
    public static byte[] GetHashBytes(this string mStrSource)
    {
        //从字符串中取得Hash描述
        HashAlgorithm md5 = HashAlgorithm.Create("MD5");
        var buffer = Encoding.UTF8.GetBytes(mStrSource);
        return md5?.ComputeHash(buffer);
    }

    /// <summary>
    /// 获取Hash描述表
    /// </summary>
    /// <param name="mStrSource">源数据</param>
    /// <returns>Hash描述表</returns>
    public static string GetHashString(this string mStrSource)
    {
        //从字符串中取得Hash描述
        HashAlgorithm md5 = HashAlgorithm.Create("MD5");
        var buffer = Encoding.UTF8.GetBytes(mStrSource);
        var hashData = md5?.ComputeHash(buffer);
        if (hashData != null) return Convert.ToBase64String(hashData);
        return null;
    }

    /// <summary>
    /// 从文件流获取Hash描述表
    /// </summary>
    /// <param name="objFile">源文件</param>
    /// <returns>Hash描述表</returns>
    public static byte[] GetHashBytes(this FileStream objFile)
    {
        //从文件中取得Hash描述
        using (objFile)
        {
            HashAlgorithm md5 = HashAlgorithm.Create("MD5");
            return md5?.ComputeHash(objFile);
        }
    }

    /// <summary>
    /// 从文件流获取Hash描述表
    /// </summary>
    /// <param name="objFile">源文件</param>
    /// <returns>Hash描述表</returns>
    public static string GetHashString(this FileStream objFile)
    {
        //从文件中取得Hash描述
        using (objFile)
        {
            HashAlgorithm md5 = HashAlgorithm.Create("MD5");
            var hashData = md5?.ComputeHash(objFile);
            if (hashData != null) return Convert.ToBase64String(hashData);
            return null;
        }
    }

    #endregion

    #region Rsa签名

    /// <summary>
    /// Rsa签名
    /// </summary>
    /// <param name="hashByteSignature">签名字节数据</param>
    /// <param name="privateKey">私钥</param>
    /// <returns>处理结果</returns>
    /// <exception cref="CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
    /// <exception cref="CryptographicUnexpectedOperationException">The key is null.-or- The hash algorithm is null. </exception>
    public static byte[] SignatureBytes(this byte[] hashByteSignature, string privateKey)
    {
        var rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(privateKey);
        var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
        //设置签名的算法为MD5
        rsaFormatter.SetHashAlgorithm("MD5");
        //执行签名
        return rsaFormatter.CreateSignature(hashByteSignature);
    }

    /// <summary>
    /// Rsa签名
    /// </summary>
    /// <param name="hashByteSignature">签名字节数据</param>
    /// <param name="privateKey">私钥</param>
    /// <returns>处理结果</returns>
    /// <exception cref="CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
    /// <exception cref="CryptographicUnexpectedOperationException">The key is null.-or- The hash algorithm is null. </exception>
    public static string SignatureString(this byte[] hashByteSignature, string privateKey)
    {
        var rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(privateKey);
        var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
        //设置签名的算法为MD5
        rsaFormatter.SetHashAlgorithm("MD5");
        //执行签名
        var encryptedSignatureData = rsaFormatter.CreateSignature(hashByteSignature);
        return Convert.ToBase64String(encryptedSignatureData);
    }

    /// <summary>
    /// Rsa签名
    /// </summary>
    /// <param name="mStrHashByteSignature">签名字符串数据</param>
    /// <param name="pStrKeyPrivate">私钥</param>
    /// <returns>处理结果</returns>
    /// <exception cref="CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
    /// <exception cref="CryptographicUnexpectedOperationException">The key is null.-or- The hash algorithm is null. </exception>
    public static byte[] SignatureBytes(this string mStrHashByteSignature, string pStrKeyPrivate)
    {
        byte[] hashByteSignature = Convert.FromBase64String(mStrHashByteSignature);
        var rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(pStrKeyPrivate);
        var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
        //设置签名的算法为MD5
        rsaFormatter.SetHashAlgorithm("MD5");
        //执行签名
        return rsaFormatter.CreateSignature(hashByteSignature);
    }

    /// <summary>
    /// Rsa签名
    /// </summary>
    /// <param name="mStrHashByteSignature">签名字符串数据</param>
    /// <param name="pStrKeyPrivate">私钥</param>
    /// <returns>处理结果</returns>
    /// <exception cref="CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
    /// <exception cref="CryptographicUnexpectedOperationException">The key is null.-or- The hash algorithm is null. </exception>
    public static string SignatureString(this string mStrHashByteSignature, string pStrKeyPrivate)
    {
        var hashByteSignature = Convert.FromBase64String(mStrHashByteSignature);
        var rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(pStrKeyPrivate);
        var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
        //设置签名的算法为MD5
        rsaFormatter.SetHashAlgorithm("MD5");
        //执行签名
        var encryptedSignatureData = rsaFormatter.CreateSignature(hashByteSignature);
        return Convert.ToBase64String(encryptedSignatureData);
    }

    #endregion

    #region Rsa 签名验证

    /// <summary>
    /// Rsa 签名验证
    /// </summary>
    /// <param name="deFormatterData">反格式化字节数据</param>
    /// <param name="publicKey">公钥</param>
    /// <param name="pStrHashByteDeFormatter">哈希字符串数据</param>
    /// <returns>处理结果</returns>
    /// <exception cref="CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
    /// <exception cref="CryptographicUnexpectedOperationException">The key is null.-or- The hash algorithm is null. </exception>
    public static bool SignatureDeFormatter(this byte[] deFormatterData, string publicKey,
        string pStrHashByteDeFormatter)
    {
        byte[] hashsetDeformation = Convert.FromBase64String(pStrHashByteDeFormatter);
        var rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(publicKey);
        var rsaDeformation = new RSAPKCS1SignatureDeformatter(rsa);
        //指定解密的时候HASH算法为MD5
        rsaDeformation.SetHashAlgorithm("MD5");
        if (rsaDeformation.VerifySignature(hashsetDeformation, deFormatterData)) return true;
        return false;
    }

    /// <summary>
    /// Rsa 签名验证
    /// </summary>
    /// <param name="pStrDeFormatterData">反格式化字符串数据</param>
    /// <param name="publicKey">公钥</param>
    /// <param name="hashByteDeFormatter">哈希字节数据</param>
    /// <returns>处理结果</returns>
    /// <exception cref="CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
    /// <exception cref="CryptographicUnexpectedOperationException">The key is null.-or- The hash algorithm is null. </exception>
    public static bool SignatureDeFormatter(this string pStrDeFormatterData, string publicKey,
        byte[] hashByteDeFormatter)
    {
        var rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(publicKey);
        var rsaDeformation = new RSAPKCS1SignatureDeformatter(rsa);
        //指定解密的时候HASH算法为MD5
        rsaDeformation.SetHashAlgorithm("MD5");
        var deformationData = Convert.FromBase64String(pStrDeFormatterData);
        if (rsaDeformation.VerifySignature(hashByteDeFormatter, deformationData)) return true;
        return false;
    }

    /// <summary>
    /// Rsa 签名验证
    /// </summary>
    /// <param name="pStrDeFormatterData">格式字符串数据</param>
    /// <param name="publicKey">公钥</param>
    /// <param name="pStrHashByteDeFormatter">哈希字符串数据</param>
    /// <returns>处理结果</returns>
    /// <exception cref="CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
    /// <exception cref="CryptographicUnexpectedOperationException">The key is null.-or- The hash algorithm is null. </exception>
    public static bool SignatureDeFormatter(this string pStrDeFormatterData, string publicKey,
        string pStrHashByteDeFormatter)
    {
        byte[] hashsetDeformation = Convert.FromBase64String(pStrHashByteDeFormatter);
        var rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(publicKey);
        var rsaDeformation = new RSAPKCS1SignatureDeformatter(rsa);
        //指定解密的时候HASH算法为MD5
        rsaDeformation.SetHashAlgorithm("MD5");
        var deformatterData = Convert.FromBase64String(pStrDeFormatterData);
        if (rsaDeformation.VerifySignature(hashsetDeformation, deformatterData)) return true;
        return false;
    }
    #endregion

    #endregion
}

