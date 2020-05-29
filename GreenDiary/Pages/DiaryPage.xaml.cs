using GreenDiary.Dialogs;
using GreenDiary.Models;
using GreenDiary.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class DiaryPage : Page
    {
        private MainViewModel ViewModel => App.MainViewModel;

        private readonly int limit = 20;
        private int page = 0;
        private bool loading = false;

        public DiaryPage()
        {
            this.InitializeComponent();
            GetData();
        }

        private async void GetData()
        {
            if (loading)
            {
                return;
            }
            loading = true;
            page++;

            var data = await ViewModel.GetDiaryList(page,limit);
            if (data.Successfully())
            {
                loading = false;
                var diarys = data.GetTArray<Diary>();
                // TODO
            }
            else
            {
                new NotifyPopup(data.message).Show();
                loading = false;
            }
        }
        private void GetNewData()
        {
            if (loading)
            {
                return;
            }
            page = 0;
            GetData();
        }
    }
}
