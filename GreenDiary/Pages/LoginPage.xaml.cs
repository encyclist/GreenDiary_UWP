﻿using GreenDiary.Dialogs;
using GreenDiary.Helpers;
using GreenDiary.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
        //获取当前应用的本地设置容器
        private readonly ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        private LoginViewModel ViewModel => App.LoginViewModel;

        public LoginPage()
        {
            this.InitializeComponent();
        }

        private async void GetCode_Click(object sender, TappedRoutedEventArgs e)
        {
            string phone = Text_Login_Phone.Text;
            if (phone.Length != 11 || !phone.StartsWith("1") || !NumberHelper.IsNumeric(phone))
            {
                return;
            }

            Progress_GetCode.IsActive = true;
            ((TextBlock)sender).IsTapEnabled = false;
            // 联网
            var data = await ViewModel.GetCode(phone);
            if (data.Successfully())
            {
                ((TextBlock)sender).Text = "验证码已发送";
                Progress_GetCode.IsActive = false;
            }
            else
            {
                new NotifyPopup(data.message).Show();
                Progress_GetCode.IsActive = false;
                ((TextBlock)sender).IsTapEnabled = true;
            }
        }

        private async void Text_Code_Change(object sender, TextChangedEventArgs e)
        {
            string code = Text_Login_Code.Text;
            string phone = Text_Login_Phone.Text;
            if (phone.Length != 11 || !phone.StartsWith("1") || !NumberHelper.IsNumeric(phone))
            {
                return;
            }
            if (code.Length != 6)
            {
                return;
            }

            var data = await ViewModel.Login(phone,code);
            if (data.Successfully())
            {
                var id = data.GetObject().Value<string>("id");
                var token = data.GetObject().Value<string>("token");
                localSettings.Values["id"] = id;
                localSettings.Values["token"] = token;
                this.Frame.Navigate(typeof(MainPage));
            }
            else
            {
                new NotifyPopup(data.message).Show();
            }
        }
    }
}
