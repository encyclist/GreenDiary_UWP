using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenDiary.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        internal async Task<Models.BaseResultModel> GetCode(string phone)
        {
            try
            {
                var data = await App.Repository.Logins.GetCode(phone);
                if (data == null)
                {
                    return new Models.BaseResultModel(-1, "请求被拒绝，可能是恶意攻击");
                }
                return data;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
                return new Models.BaseResultModel(-1,"网络错误");
            }
        }

        internal async Task<Models.BaseResultModel> Login(string phone, string code)
        {
            try
            {
                var data = await App.Repository.Logins.Login(phone, code);
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
