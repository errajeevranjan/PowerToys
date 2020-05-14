﻿
using System;
using Windows.UI.Xaml.Controls;
using PowerToys_Settings_Sandbox.ViewModels;
using Windows.UI.Xaml.Navigation;
using Microsoft.Toolkit.Uwp.Helpers;
using Windows.UI.Notifications;

namespace PowerToys_Settings_Sandbox.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();
        
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is string x)
            {
                if (x == "FirstOpen")
                {
                    PowerOnLaunchDialog();
                    ToastNotificationManager.History.Clear();
                }
                else if (x == "NewUpdateOpen")
                {
                    DisplayUpdateDialog();
                    ToastNotificationManager.History.Clear();
                }
            }
        }

        public async void PowerOnLaunchDialog()
        {
            onLaunchContentDialog dialog = new onLaunchContentDialog();
            dialog.PrimaryButtonClick += Dialog_PrimaryButtonClick;
            await dialog.ShowAsync();
        }

        /// <summary>
        ///     This is currently called when the "Check for Updates" button is clicked (as a placeholder trigger for now)
        ///     For the future: customize to calling only once after update is installed, etc.
        /// </summary>
        private async void DisplayUpdateDialog()
        {
            ContentDialog updateDialog = new UpdateContentDialog();
            await updateDialog.ShowAsync();
        }
        

        private void Dialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            OpenFirstGeneralSettingsTip();
        }

        // This method opens the first teaching tip on the General Settings page
        // Should open automatically only on initial install after user starts tutorial
        public void BeginSettingsTips()
        {
            OpenFirstGeneralSettingsTip();
        }

        private void OpenFirstGeneralSettingsTip()
        {
            GeneralSettingsTip.IsOpen = true;
        }

        // This method opens the second teaching tip
        private void OpenRunAsUserTip()
        {
            GeneralSettingsTip.IsOpen = false;
            RunAsUserTip.IsOpen = true;
        }

        // This method opens the last teaching tip
        private void OpenFinalGeneralSettingsTip()
        {
            RunAsUserTip.IsOpen = false;
            FinalGeneralSettingsTip.IsOpen = true;
        }

        // This method closes all teaching tips
        private void CloseTeachingTips()
        {
            GeneralSettingsTip.IsOpen = false;
            RunAsUserTip.IsOpen = false;
            FinalGeneralSettingsTip.IsOpen = false;
        }
    }
}
