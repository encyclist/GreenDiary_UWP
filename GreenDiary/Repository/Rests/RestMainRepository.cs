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

        public async Task<BaseResultModel> GetDiaryList(int page, int limit)
        {
            SortedList<string, string> data = new SortedList<string, string>();
            data.Add("page", page.ToString());
            data.Add("limit", limit.ToString());
            return await _http.PostAsync<SortedList<string, string>, BaseResultModel>("diary/list", SignHelper.GenerateSign(data));
        }

        public async Task<BaseResultModel> GetLibraryList(int limit)
        {
            SortedList<string, string> data = new SortedList<string, string>();
            data.Add("limit", limit.ToString());
            return await _http.PostAsync<SortedList<string, string>, BaseResultModel>("diary/getLibraryList", SignHelper.GenerateSign(data));
        }
    }
}