using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Documents;
using HmExtension.Commons.Extensions;

namespace HmExtension.Commons.utils;

/// <summary>
/// IP帮助类
/// </summary>
public class IpHelper
{
    /// <summary>
    /// 是否为ip
    /// </summary>
    /// <param name="ip">IP地址</param>
    /// <returns></returns>
    public static bool IsValidIp(string ip)
    {
        return Regex.IsMatch(ip, "[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}");
    }


    /// <summary>
    ///     获取本地外网 IP
    /// </summary>
    /// <returns></returns>
    public static string GetExternalIp()
    {
        var mc = Regex.Match(
            new System.Net.Http.HttpClient().GetStringAsync("http://www.net.cn/static/customercare/yourip.asp").Result,
            @"您的本地上网IP是：<h2>(\d+\.\d+\.\d+\.\d+)</h2>");
        if (mc.Success && mc.Groups.Count > 1)
        {
            return mc.Groups[1].Value;
        }

        throw new Exception("获取IP失败");
    }


    /// <summary>
    /// 校验IP地址的正确性，同时支持IPv4和IPv6
    /// </summary>
    /// <param name="s">源字符串</param>
    /// <returns>是否匹配成功</returns>
    public static bool MatchInetAddress(string s)
    {
        MatchInetAddress(s, out bool success);
        return success;
    }

    /// <summary>
    /// 校验IP地址的正确性，同时支持IPv4和IPv6
    /// </summary>
    /// <param name="s">源字符串</param>
    /// <param name="isMatch">是否匹配成功，若返回true，则会得到一个Match对象，否则为null</param>
    /// <returns>匹配对象</returns>
    public static Match MatchInetAddress(string s, out bool isMatch)
    {
        Match match;
        if (s.Contains(":"))
        {
            //IPv6
            match = Regex.Match(s, @"^([\da-fA-F]{0,4}:){1,7}[\da-fA-F]{1,4}$");
            isMatch = match.Success;
        }
        else
        {
            //IPv4
            match = Regex.Match(s, @"^(\d+)\.(\d+)\.(\d+)\.(\d+)$");
            isMatch = match.Success;
            foreach (Group m in match.Groups)
            {
                if (m.Value.ToInt() < 0 || m.Value.ToInt() > 255)
                {
                    isMatch = false;
                    break;
                }
            }
        }

        return isMatch ? match : null;
    }

    /// <summary>
    /// 将IP地址和端口号解析为DnsEndPoint对象
    /// </summary>
    /// <param name="hostname"></param>
    /// <param name="port"></param>
    /// <returns></returns>
    public static DnsEndPoint ParseDnsEndPoint(string hostname, int port)
    {
        return new DnsEndPoint(hostname, port);
    }

    /// <summary>
    /// 将IP地址和端口号解析为IPEndPoint对象
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="port"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IPEndPoint ParseIpEndPoint(string ip, int port)
    {
        var match = MatchInetAddress(ip, out var isMatch);
        if (!isMatch)
        {
            throw new ArgumentException("IP地址格式不正确");
        }

        return new IPEndPoint(IPAddress.Parse(ip), port);
    }

    /// <summary>
    /// 获取本地IP地址列表
    /// </summary>
    /// <returns></returns>
    public static List<IPAddress> GetLocalAddress()
    {
        var list = new List<IPAddress>();
        foreach (var adapter in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
        {
            foreach (var uni in adapter.GetIPProperties().UnicastAddresses)
            {
                if (uni.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    list.Add(uni.Address);
                }
            }
        }

        return list;
    }
}