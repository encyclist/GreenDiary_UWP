using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenDiary.Helpers
{
    class SignHelper
    {
        public static string Sign(SortedList<string, string> data,string time)
        {
            StringBuilder sb = new StringBuilder();

            // 添加所有字典数据
            foreach (string key in data.Keys) 
            {
                sb.Append(key).Append("=").Append(data[key]).Append("&");
            }
            // 添加时间戳
            sb.Append("timestamp").Append("=").Append(time);
            // 添加密钥
            sb.Append("&").Append("key").Append("=").Append(App.sign);

            return Md5helper.GenerateMD5(sb.ToString()).ToUpper();
        }
    }
}
