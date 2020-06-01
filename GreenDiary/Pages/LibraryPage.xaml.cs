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
using System.Threading.Tasks;
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

        //private readonly ObservableCollection<Diary> diarys = new ObservableCollection<Diary>();
        private IncrementalLoadingCollection<Diary> diarys;

        private readonly int limit = 30;
        private int page = 0;

        public LibraryPage()
        {
            this.InitializeComponent();
            // 缓存页面，在页面关闭之前保留缓存，比如后退到这个页面或被重新打开
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
            this.Loaded += LibraryPage_Loaded;
        }

        private void LibraryPage_Loaded(object sender, RoutedEventArgs e)
        {
            diarys = new IncrementalLoadingCollection<Diary>(count =>
            {
                page++;
                var data = GetData();
                return Task.Run(() => Tuple.Create(data.Result, true));
            });
            diarys.OnLoadMoreCompleted += Diarys_OnLoadMoreCompleted;
            GridView_Library.ItemsSource = diarys;
        }

        private void Diarys_OnLoadMoreCompleted(int count)
        {
            Trace.WriteLine(page);
        }

        private async Task<List<Diary>> GetData()
        {
            List<Diary> result = new List<Diary>();

            var data = await ViewModel.GetLibraryList(limit);
            if (data.Successfully())
            {
                foreach (Diary diary in data.GetTArray<Diary>())
                {
                    result.Add(diary);
                }
                return result;
            }
            else
            {
                new NotifyPopup(data.message).Show();
            }

            return result;
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
