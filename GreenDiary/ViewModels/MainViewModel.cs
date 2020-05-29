using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenDiary.ViewModels
{
    public class MainViewModel : BindableBase
    {
        internal async Task<Models.BaseResultModel> Mark()
        {
            try
            {
                var data = await App.Repository.Mains.Mark();
                if (data == null)
                {
                    return new Models.BaseResultModel(-1, "请求被拒绝，可能是恶意攻击");
                }
                return data;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
                return new Models.BaseResultModel(-1, "网络错误");
            }
        }

        internal async Task<Models.BaseResultModel> GetDiaryList(int page, int limit)
        {
            try
            {
                var data = await App.Repository.Mains.GetDiaryList(page, limit);
                if (data == null)
                {
                    return new Models.BaseResultModel(-1, "请求被拒绝，可能是恶意攻击");
                }
                return data;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
                return new Models.BaseResultModel(-1, "网络错误");
            }
        }

        internal async Task<Models.BaseResultModel> GetLibraryList(int limit)
        {
            try
            {
                var data = await App.Repository.Mains.GetLibraryList(limit);
                if (data == null)
                {
                    return new Models.BaseResultModel(-1, "请求被拒绝，可能是恶意攻击");
                }
                return data;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
                return new Models.BaseResultModel(-1, "网络错误");
            }
        }
    }
}
