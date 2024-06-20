using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using HmExtension;

namespace HmExtensionTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            "123".ToMd5().Println("md5: ", "");
            "123".ToBase64().Println("base64: ", "");
            "MTIz".FromBase64().Println("from base64: ", "");
            "user name".ToCamelCase().Println("UpperCamelCase: ","");
            "user name".ToCamelCase(false).Println("LowerCamelCase: ", "");
            "".IsEmpty().Println("IsEmpty: ");
            " ".IsEmptyOrWhiteSpace().Println("IsEmptyOrWhiteSpace: ");
            "123abc".ReplaceRegex(@"\d","*").Println("ReplaceRegex: 123abc=>");
            byte[] arr = {0x01, 0x02, 0x03, 25};
            arr.ToHexString("-").Println("ToHex: ");
            "name:{0}".Format("张三").Println("format: ");
            Console.ReadKey();
        }
    }
}
