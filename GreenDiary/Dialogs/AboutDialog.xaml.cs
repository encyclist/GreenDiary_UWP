using System;
using System.Collections.Generic;
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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“内容对话框”项模板

namespace GreenDiary.Dialogs
{
    public sealed partial class AboutDialog : ContentDialog
    {
        private string[] emojis = new string[] { "😜", "😍", "😗", "😈", "🤣", "😎", "🤔", "🙃", "🙄", "😫" };
        private Random random = new Random();

        public AboutDialog()
        {
            this.InitializeComponent();

            Text_About_Emoji.Text = emojis[random.Next(0, emojis.Length - 1)];
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void Erning_Click(object sender, TappedRoutedEventArgs e)
        {
            Text_About_Emoji.Text = emojis[random.Next(0, emojis.Length)];
        }
    }
}
