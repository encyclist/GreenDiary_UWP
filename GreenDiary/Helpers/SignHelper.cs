using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace GreenDiary.Helpers
{
    class SignHelper
    {
        private static ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        public static string Sign(SortedList<string, string> data,string time,string token)
        {
            StringBuilder sb = new StringBuilder();

            // 添加所有字典数据
            foreach (string key in data.Keys) 
            {
                sb.Append(key).Append("=").Append(data[key]).Append("&");
            }
            // 添加时间戳
            sb.Append("timestamp").Append("=").Append(time);
            // 添加token
            if (!String.IsNullOrEmpty(token)) 
            {
                sb.Append("&").Append("token").Append("=").Append(token);
            }
            // 添加密钥
            sb.Append("&").Append("key").Append("=").Append(App.sign);

            string SignStr = sb.ToString();
            Trace.WriteLine("签名使用："+SignStr);
            return Md5helper.GenerateMD5(SignStr).ToUpper();
        }

        public static SortedList<string, string> GenerateSign()
        {
            return GenerateSign(new SortedList<string, string>());
        }
        public static SortedList<string, string> GenerateSign(SortedList<string, string> data)
        {
            string time = TimeHelper.GetTimestamp().ToString();
            string token = localSettings.Values.ContainsKey("token") ? localSettings.Values["token"].ToString() : "";

            data.Add("sign", Sign(data, time, token));
            data.Add("timestamp", time);
            if (!String.IsNullOrEmpty(token))
            {
                data.Add("token", token);
            }

            return data;
        }
    }
}
