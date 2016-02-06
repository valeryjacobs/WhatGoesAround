using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WhatGoesAround.Phone.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WhatGoesAround.Phone
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.ViewModel = new MainViewModel(this.MainCanvas);
        }



        public MainViewModel ViewModel { get; set; }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.HandleButtonClick(2);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.HandleButtonClick(1);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var settingsDialog = new SettingsDialog();
            var result = await settingsDialog.ShowAsync();

            if (result != ContentDialogResult.Primary)
                Application.Current.Exit();

            ViewModel.AppSettings = new AppSettingsViewModel();
            ViewModel.AppSettings.LoadFromSettings(Windows.Storage.ApplicationData.Current.LocalSettings.Values);

            await this.ViewModel.RegisterPlayer();
        }
    }
}
