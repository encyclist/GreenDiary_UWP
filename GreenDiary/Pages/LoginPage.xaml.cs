using GreenDiary.Helpers;
using GreenDiary.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace GreenDiary.Pages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginViewModel ViewModel => App.LoginViewModel;

        public LoginPage()
        {
            this.InitializeComponent();
        }

        private void GetCode_Click(object sender, TappedRoutedEventArgs e)
        {
            string phone = Text_Login_Phone.Text;
            string code = Text_Login_Code.Text;
            //if (phone.Length != 11 || !phone.StartsWith("1") || !NumberHelper.IsNumeric(phone))
            //{
            //    return;
            //}
            //if (String.IsNullOrEmpty(phone))
            //{
            //    return;
            //}

            // 联网
            ViewModel.GetCode(phone);
        }
    }
}
