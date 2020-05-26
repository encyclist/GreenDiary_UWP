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

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace GreenDiary
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static ContentDialog CurrentDialog { get; set; } = null;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Navi_Setting_Click(object sender, TappedRoutedEventArgs e)
        {
            string s = ((NavigationViewItem)sender).Content.ToString();
            Trace.WriteLine(s);
        }

        private async void Navi_About_Click(object sender, TappedRoutedEventArgs e)
        {
            string s = ((NavigationViewItem)sender).Content.ToString();
            Trace.WriteLine(s);

            if (CurrentDialog != null)
            {
                CurrentDialog.Hide();
            }
            CurrentDialog = null;

            try
            {
                CurrentDialog = new AboutDialog();
                ContentDialogResult result = await CurrentDialog.ShowAsync();
            }
            catch (Exception)
            {
                // The dialog didn't open, probably because another dialog is already open.
            }
        }

        private void Navi_SelectChange(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var s = ((NavigationViewItem)sender.SelectedItem).Content.ToString();
            Trace.WriteLine(s);
            Navi.Header = s;
        }
    }
}
