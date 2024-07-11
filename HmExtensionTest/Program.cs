using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Security.Policy;
using System.Windows.Media;
using Gma.QrCodeNet.Encoding;
using HmExtension;
using HmExtension.Extensions;
using Color = System.Drawing.Color;

// using HmExtension.Pay;

namespace HmExtensionTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // "123".ToMd5().Println("md5: ", "");
            // "123".ToBase64().Println("base64: ", "");
            // "MTIz".FromBase64().Println("from base64: ", "");
            // "user name".ToCamelCase().Println("UpperCamelCase: ","");
            // "user name".ToCamelCase(false).Println("LowerCamelCase: ", "");
            // "".IsEmpty().Println("IsEmpty: ");
            // " ".IsEmptyOrWhiteSpace().Println("IsEmptyOrWhiteSpace: ");
            // "123abc".ReplaceRegex(@"\d","*").Println("ReplaceRegex: 123abc=>");
            // byte[] arr = {0x01, 0x02, 0x03, 25};
            // arr.ToHexString("-").Println("ToHex: ");
            // "name:{0}".Format("张三").Println("format: ");
            //
            //
            // List<string> list = new List<string> {"a", "b", "c"};
            // list.ToJson().Println("ToJson: ");
            //
            // "[1,2,3,4]".FromJson<List<int>>().Println("FromJson: ");
            // list.ToString().Println();
            //
            // var stu = new Student()
            // {
            //     Name = "张三",
            //     Age = 20
            // };
            //
            // stu.ToJson(converters: new StringEnumConverter()).Println();
            // // var logo = new Bitmap(@"E:\资料\虹猫\虹猫LOGO.png");
            // // var bitmap = "1234".ToQRCode(icon: logo,darkColor:Color.Red,lightColor:Color.Green);
            // // bitmap.Save("test.png");
            //
            // "1234".ToQRCode( System.Drawing.Color.Green, System.Drawing.Color.White).Save("test.png");
            // Console.ReadKey();
            // AlipayContext.Init(
            //     "https://openapi-sandbox.dl.alipaydev.com/gateway.do",
            //     "9021000126625851",
            //     "MIIEogIBAAKCAQEAghlsSlbNhTspVTEBXvQMVyiqgS4SNq8N5UFU59hFnrTvPHKO+8dAdxsHt+gKO8CPcm3guDIuQhugW3ttxUWFm336xYCJ1nV3eZIppHLrmIAr6mwK4tka1wuN998ZWHh5p1OFzBhua6UMRUWk5Z+pyOz9CGOAAlmXhMRaV+KFt7/iyte0ovBheCWRnI0qprc1hnODl8TM7FItrrDUSZFcQGAf/eH0644gPQLOy38AHZXRG69SdVK3N0+LQ2J2npvER/fte667hQ0nOMBj3mwb/JhonPpsu9M/JZqwN2kQzQjGUW/kwDYXU/fecqjEUvfEKF4zxXfXqenx8MmUiRV/DwIDAQABAoIBAAMg6WHARKD3kz7hNs85vzI5YCBLI1T9ULA6qfgJU2NFppslhhq35+z1UXIyukxFjS2LRuQnEMW2NFz+0tzzlwruNKGbPbVSjdT2ltnFibTZIDU68+gQoHybYoabbtiZzRMhBw6pZ75e8bSYwCZleIfEazFQczubO/GNZz7Fy8FnaxW65OzF7SuIEgQG7YIyzkfLOT4nUR5WS5hzrIsdINcFciI8Xpn/uD+JhKJazA9bHQnNfQIFI0Xjev19V+5JSTLwyATuU/MaYXpBOUPw0VWV+IlCB84hVDvRYyFTFv6j77P8y1ObaBbGelV3AjRfMSN9BYL4BiEs/U3Ix4GuHIECgYEAxpn5uG5ii+u4JGxW5pJY7Dd4TPmDIFRSA7LBSrBMmQWoZU2f2DqjqjYcpqu2hfjufIplj9i/tphqprDOvHhSvxUgwb/OFKNwyeWT+INmvYvCVx8t/rLAsaAlukNd97WKin6jCgjAZQi3ULA1cG7w+vBgZlxhz7KZ6kdRTDl1g7kCgYEAp7MlBqe+5jSCnedJgkWyrPJ5GIGTqROMMfwN6fVC0iY0SvvfNvIpxWqsTS4rfv3NmfEKRz1c3Qr0WBHfhEff2FROPkxjwfAtKeRHIwtp31mO5rvB3HdYV2f0e1xKuKX2DlqEMbeqYBJOqwHoVfDLaeuexuoi2P1NrYvklJTijQcCgYB5/+5yiITLFWOKJG+BtpcfjLe++Wu+uW2kTfQFcKWtPteCW5v81ZDip9kT3doDFLdUFCRtqCWlAp5JhcWJ65RRQ6ZavvvWm8xWikxK1lWPzMH2iPXVR3Ot6gYjFO0tlPzlNQPszxF5P1B1Jbm+NricPnvJhaikUT/RtjvPymBRKQKBgE7C+JTTm/kKmH4I7qFckrpfdzhnQy6Zm7KuKurC4gtJHi5JdWCdA7lQjHQDRzJxiWrPpoAm9cJrLq9eVZgPGbbEgasIvaGMZ+nQ6QgwiBz6Nv9vF3GwK7GBhXWatw1aBOJg6M1g8YgFkSUH1FLosOCiZQQPWyaGcUEc/tI5yt1pAoGAG0M5xbuJmVvPOgKaW85zmq+wqVjFcAHMj+VilLERmTFqrYmVEPsu49kAMfKqfKeAshLCv0xHTJRh8SfNGls12aLe9v7/k4+DHwGG2SmWhvyS4xpVECOuQHhxTdwqOEkV+CDHbMb0/Alb9Wy5no6czvrrRfi8x3DXKGI+06+YXVc=",
            //     "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAh8Sd5tdieTWMgZ+K52kEGHS4exwlD4aRwc82yi1P2HQpH4vhz3ddpBdaZMPMvSpN0uv2NJZo07g8MEYfCiQlWQHay8t6/8mIMERomMPcCOPBbUe49uTyVQt/9eMCXpgEzL9O7Kr4trQbSoAEggmi2jRfjEgB+3bN94B2BEt1dd0OwFHtouvVOYowDjfq9TMAv2/2+0UWBMSGizta/Q+Qq1jBhximX4pALnd+6iwvodbvEPZmCRrO8CwwDeFSgHk5+TdvNaclNbTf/W9l/3fmoYIqsI6zC19XQzfPh0Fedvn7+al8GBVT0NT/g6GxRcLMlUmHrDGul4fWcuZM4znASwIDAQAB");
            // PayInPersonTest();
            // ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
            // var collection = searcher.Get();
            // foreach (var o in collection)
            // {
            //     var properties = o.Properties;
            //     foreach (PropertyData pd in properties)
            //     {
            //         pd.FormatPatten("Name:{Name} Value: {Value} IsLocal: {IsLocal} IsArray:{IsArray}").Println();
            //     }
            // }
            // Console.ReadKey();


            // ushort a = 10;
            // var bytes = a.ToByte();
            // bytes.Join().Println();
            // bytes.Reverse().Reverse().ToUShort().Println();
            "1234".ToQRCode(darkColor:Color.Red,
                icon:new Bitmap("")).Save($"{Environment.CurrentDirectory}/1.jpg");
            Console.ReadKey();
        }




        private static void PayInPersonTest()
        {
            // // bb1eee5a-6cc6-4040-b5f9-b0f49dbfcec3
            // // 创建当面付API实例
            // PayInPersonApi payInPersonApi = new PayInPersonApi();
            // // 生成订单号
            // var outTradeNo = Guid.NewGuid().ToString();
            // // 执行API
            // var task = payInPersonApi.Precreate(outTradeNo,"预创建订单",1000);
            // task.ContinueWith(t =>
            // {
            //     // 获取返回的二维码内容
            //     if (t.Result.IsSuccess())
            //     {
            //         // 得到二维码内容
            //         var qrCode = t.Result.QrCode;
            //         // 将二维码内容转换为二维码图片并保存
            //         qrCode.ToQRCode().Save("test.png");
            //     }
            // });
        }


    }

    class Student
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}
