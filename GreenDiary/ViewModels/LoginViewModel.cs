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
                return await App.Repository.Logins.GetCode(phone);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
                return new Models.BaseResultModel(-1,"网络错误");
            }
        }
    }
}
