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
        internal async void GetCode(string phone)
        {
            var data = await App.Repository.Logins.GetCode(phone);
        }
    }
}
