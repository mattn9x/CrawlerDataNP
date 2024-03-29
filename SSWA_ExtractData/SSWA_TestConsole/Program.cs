﻿using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace SSWA_TestConsole
{
    internal class Program
    {
        public delegate void WriteData(People p, string path);

        //static void Main(string[] args)
        //{
        //    Console.OutputEncoding = Encoding.UTF8;
        //    string dateTime = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
        //    //string s = "http://vnexpress.net/rss/tin-moi-nhat.rss";
        //    //var ss = s.Replace("https://", "").Replace("http://", "").Split('/')[0];
        //    //Console.WriteLine(ss);
        //    WritePeople();
        //    Console.WriteLine(DateTime.Now);
        //    Console.ReadLine(); 
        //}

        /*Test đọc tập tin*/

        public static void TestReadText()
        {
            var path = Environment.CurrentDirectory + "/../../What/test.txt";
            var strContent = File.ReadAllText(path);
            Console.WriteLine(strContent);
        }

        private static void TestRegex()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.BufferHeight = 1000;
            var af =
                "<a href='http://www.24h.com.vn/tin-tuc-trong-ngay/da-nang-se-lap-dat-camera-an-ninh-toan-thanh-pho-c46a773684.html' title='Đà Nẵng: Sẽ lắp đặt camera an ninh toàn thành phố'><img width='130' height='100' src='http://24h-img.24hstatic.com:8008/upload/1-2016/images/2016-03-03/1457007383-thumbnail.jpg' alt='Đà Nẵng: Sẽ lắp đặt camera an ninh toàn thành phố' title='Đà Nẵng: Sẽ lắp đặt camera an ninh toàn thành phố' /></a><br />Việc đầu tư lắp đặt hệ thống camera này được cho là nhằm nâng cao chất lượng, hiệu quả hoạt động theo dõi, giám sát, điều hành công tác đảm bảo an ninh, phòng chống tội phạm...";
            var reg = new Regex("<.*>");

            //Regex reg = new Regex("href\\s*=\\s*[\"']([^\"']+)[\"']+");
            var m = reg.Match(af);
            if (m.Success)
                Console.WriteLine(reg.Replace(af, ""));
        }

        public static void ParseSingleWeb()
        {
            var wc = new WebClient();
            wc.Headers.Add("User-Agent",
                "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2403.157 Safari/537.36");
            wc.Encoding = Encoding.UTF8;
            var test = wc.DownloadString("http://www.24h.com.vn/upload/rss/tintuctrongngay.rss");
            var htmlDecode = WebUtility.HtmlDecode(test).Replace("&#", "")
                .Replace("34;", "").Replace("  ", "").Replace("\r", "").Replace("\n", "").Replace("\t", "");
            File.WriteAllText("data.txt", htmlDecode, Encoding.UTF8);
            Console.WriteLine(htmlDecode);
            var obj = JsonConvert.DeserializeObject(htmlDecode);
        }

        public static void Replace(string s)
        {
        }

        public static void WritePeople()
        {
            //var newPeople = new People
            //{
            //    Id = 1,
            //    Name = "AnhHai",
            //    Address = "ThaiBinh",
            //    Phone = "09780832323"
            //};
            //var strContent = JsonConvert.SerializeObject(newPeople);
            //var path = Environment.CurrentDirectory + "/../../What/test.txt";
            //File.WriteAllText(path, strContent, Encoding.UTF8);
            var newPeople = new People
            {
                Id = 1,
                Name = "anhhai",
                Address = "thaibinh",
                Phone = "09780832323"
            };
            var path = Environment.CurrentDirectory + "/../../what/test.txt";

            WriteData wrdata = (p, pathx) =>
            {
                var strcontent = JsonConvert.SerializeObject(p);
                File.WriteAllText(pathx, strcontent, Encoding.UTF8);
            };
            wrdata.Invoke(newPeople, path);

            Console.WriteLine("Successfully");
        }

        #region Test

        public static bool Test2(string Email)
        {
            var strEmail = "manhh.ipkhmt2@gmail.com";
            var partern = @"^\w+\.?\w+\@\w{3,5}\.\w{2,3}";
            var regex = new Regex(partern);
            var checkMailValid = regex.Match(strEmail);
            return checkMailValid.Success;
        }

        private static bool Test3()
        {
            string[] ss = { "1", "", "2", "3" };
            foreach (var item in ss)
            {
                if (item.Equals(""))
                    return true;
            }
            return false;
        }

        public void Test()
        {
            //var a = "NguyenManhxx";
            //SSWADataEncrypt ss = new SSWADataEncrypt();
            //var encode = ss.EncodeTwoWay(a);
            //Console.WriteLine(encode);

            //var decode = ss.DecodeTwoWay(encode);
            //Console.WriteLine("Giải mã");
            //Console.WriteLine(decode);
            //var strTemp = strInputFilter.Substring(input.Length - 9, input.Length);

            //System.IO.FileInfo fi = new System.IO.FileInfo(System.IO.Path.Combine(Application.StartupPath, "..\\What\\test.txt"));
            //var path = Application.StartupPath + @"..\..\..\What\test.txt";
            //var path2 = File.Exists("data.txt");
            //Console.WriteLine(path);
        }

        #endregion
    }

    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}