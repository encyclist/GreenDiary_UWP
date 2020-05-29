using GreenDiary.Dialogs;
using GreenDiary.Models;
using GreenDiary.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class LibraryPage : Page
    {
        private MainViewModel ViewModel => App.MainViewModel;

        private readonly ObservableCollection<Diary> diarys = new ObservableCollection<Diary>();

        private readonly int limit = 20;
        private bool loading = false;

        public LibraryPage()
        {
            this.InitializeComponent();

            GridView_Library.DataContext = diarys;
            GetData();
        }

        private async void GetData()
        {
            if (loading)
            {
                return;
            }
            loading = true;

            var data = await ViewModel.GetLibraryList(limit);
            if (data.Successfully())
            {
                loading = false;
                foreach (Diary diary in data.GetTArray<Diary>()) 
                {
                    diarys.Add(diary);
                }
            }
            else
            {
                new NotifyPopup(data.message).Show();
                loading = false;
            }
        }

        private void PageSizeChanged(object sender, SizeChangedEventArgs e)
        {
            int colunm = (int)(e.NewSize.Width / (200 + 10 + 4));
            var use = (colunm - 1) * 4 + colunm * (200 + 10);
            var remaininge = e.NewSize.Width - use;
            width.Width = (int)(200 + remaininge/colunm - 2);
        }

    }
}
