using GreenDiary.Dialogs;
using GreenDiary.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace GreenDiary.Pages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        private MainViewModel ViewModel => App.MainViewModel;
        public static ContentDialog CurrentDialog { get; set; } = null;

        public MainPage()
        {
            this.InitializeComponent();
            // 默认第一个被选中的效果
            Navi.SelectedItem = Navi_Item_Diary;
            frame.Navigate(typeof(DiaryPage));

            Mark();
        }

        private async void Navi_About_Click(object sender, TappedRoutedEventArgs e)
        {
            string s = ((NavigationViewItem)sender).Content.ToString();
            Trace.WriteLine(s);

            if (CurrentDialog != null)
            {
                CurrentDialog.Hide();
            }
            CurrentDialog = new AboutDialog();
            await CurrentDialog.ShowAsync();
        }

        /// <summary>
        /// 导航到所选项目对应的页面。
        /// </summary>
        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var s = ((NavigationViewItem)sender.SelectedItem).Content.ToString();
            Trace.WriteLine(s);
            Navi.Header = s;

            if (args.IsSettingsInvoked)
            {
                frame.Navigate(typeof(SettingPage));
            }
            else if (Navi.SelectedItem == Navi_Item_Diary)
            {
                frame.Navigate(typeof(DiaryPage));
            }
            else if (Navi.SelectedItem == Navi_Item_Library)
            {
                if (localSettings.Values.ContainsKey("LibraryUseWaterfall"))
                {
                    bool libraryUseWaterfall = (bool)localSettings.Values["LibraryUseWaterfall"];
                    if (libraryUseWaterfall)
                    {
                        frame.Navigate(typeof(WaterfallLibraryPage));
                    }
                    else
                    {
                        frame.Navigate(typeof(LibraryPage));
                    }
                }
                else
                {
                    frame.Navigate(typeof(LibraryPage));
                }
            }
            else if (Navi.SelectedItem == Navi_Item_Album)
            {
                frame.Navigate(typeof(AlbumPage));
            }
            else if (Navi.SelectedItem == Navi_Item_Notify)
            {
                frame.Navigate(typeof(NotifyPage));
            }
            else if (Navi.SelectedItem == Navi_Item_My)
            {
                frame.Navigate(typeof(MyPage));
            }
        }

        // 每日签到
        private async void Mark()
        {
            var data = await ViewModel.Mark();
            if (data.Successfully())
            {
                new NotifyPopup("签到成功，积分+1").Show();
            }
        }
    }
}
