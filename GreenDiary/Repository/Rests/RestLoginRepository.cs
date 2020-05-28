using GreenDiary.Helpers;
using GreenDiary.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GreenDiary.Repository.Rests
{
    internal class RestLoginRepository : ILoginRepository
    {
        private readonly HttpHelper _http;

        public RestLoginRepository(string baseUrl)
        {
            _http = new HttpHelper(baseUrl);
        }

        public async Task<BaseResultModel> GetCode(string phone)
        {
            string time = TimeHelper.GetTimestamp().ToString();

            SortedList<string, string> data = new SortedList<string, string>();
            data.Add("phone", phone);
            data.Add("action", "login");
            data.Add("sign", SignHelper.Sign(data, time));
            data.Add("timestamp", time);
            return await _http.PostAsync<SortedList<string, string>, BaseResultModel>("base/sms", data);
        }

        public async Task<BaseResultModel> Login(string phone,string code)
        {
            string time = TimeHelper.GetTimestamp().ToString();

            SortedList<string, string> data = new SortedList<string, string>();
            data.Add("id", phone);
            data.Add("code", code);
            data.Add("nickname", phone.Substring(phone.Length-1));
            data.Add("sign", SignHelper.Sign(data, time));
            data.Add("timestamp", time);
            return await _http.PostAsync<SortedList<string, string>, BaseResultModel>("user/login", data);
        }
    }
}