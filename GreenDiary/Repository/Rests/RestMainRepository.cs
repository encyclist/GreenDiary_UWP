using GreenDiary.Helpers;
using GreenDiary.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Storage;

namespace GreenDiary.Repository.Rests
{
    internal class RestMainRepository : IMainRepository
    {
        private ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        private readonly HttpHelper _http;

        public RestMainRepository(string baseUrl)
        {
            _http = new HttpHelper(baseUrl);
        }

        public async Task<BaseResultModel> Mark()
        {
            return await _http.PostAsync<SortedList<string, string>, BaseResultModel>("user/mark", SignHelper.GenerateSign());
        }
    }
}