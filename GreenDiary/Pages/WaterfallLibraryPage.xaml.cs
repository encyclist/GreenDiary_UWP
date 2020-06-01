using GreenDiary.Dialogs;
using GreenDiary.Models;
using GreenDiary.ViewModels;
using GreenDiary.Views;
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
    public sealed partial class WaterfallLibraryPage : Page
    {
        private MainViewModel ViewModel => App.MainViewModel;

        private IncrementalLoadingCollection<Diary> diarys;

        private readonly int limit = 30;

        public WaterfallLibraryPage()
        {
            this.InitializeComponent();
            this.Loaded += LibraryPage_Loaded;
        }

        private void LibraryPage_Loaded(object sender, RoutedEventArgs e)
        {
            diarys = new IncrementalLoadingCollection<Diary>(count =>
            {
                var data = GetData();
                return Task.Run(() => Tuple.Create(data.Result, true));
            });
            GridView_Library.ItemsSource = diarys;
            GetData2();
        }

        private async Task<List<Diary>> GetData()
        {
            var data = await ViewModel.GetLibraryList(limit);
            if (data.Successfully())
            {
                return data.GetTArray<Diary>();
            }
            else
            {
                new NotifyPopup(data.message).Show();
            }

            return new List<Diary>(0);
        }

        private async void GetData2()
        {
            var data = await ViewModel.GetLibraryList(limit);
            if (data.Successfully())
            {
                foreach (Diary diary in data.GetTArray<Diary>())
                {
                    diarys.Add(diary);
                }
            }
            else
            {
                new NotifyPopup(data.message).Show();
            }
        }

        private void PageSizeChanged(object sender, SizeChangedEventArgs e)
        {
            int colunm = (int)(e.NewSize.Width / (200 + 10 + 4));
            (GridView_Library.ItemsPanelRoot as WaterfallPanel).NumberOfColumnsOrRows = colunm;
        }

        private void scrollRoot_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (scrollRoot.VerticalOffset <= scrollRoot.ScrollableHeight - 500)
                return;

            GetData2();
        }
    }
}
