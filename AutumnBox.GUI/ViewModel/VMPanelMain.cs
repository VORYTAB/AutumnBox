﻿/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/18 19:08:39 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.ManagedAdb;
using AutumnBox.GUI.Model;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Properties;
using AutumnBox.GUI.Util.OS;
using AutumnBox.GUI.View.DialogContent;
using AutumnBox.GUI.View.LeafContent;
using AutumnBox.GUI.View.Windows;
using MaterialDesignThemes.Wpf;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace AutumnBox.GUI.ViewModel
{
    class VMPanelMain : ViewModelBase
    {
        public ICommand StartShell { get; private set; }
        public ICommand ShowSettingsDialog { get; private set; }
        public int TabSelectedIndex
        {
            get => tabSelectedIndex;
            set
            {
                tabSelectedIndex = value;
                RaisePropertyChanged();
            }
        }
        private int tabSelectedIndex;
        public VMPanelMain()
        {
            StartShell = new FlexiableCommand(_StartShell);
            ShowSettingsDialog = new FlexiableCommand(_ShowSettingsDialog);
            Util.Bus.DeviceSelectionObserver.Instance.SelectedNoDevice += NoDevice;
            Util.Bus.DeviceSelectionObserver.Instance.SelectedDevice += Instance_SelectedDevice;
        }
        private void _ShowSettingsDialog()
        {
            (App.Current.MainWindow as MainWindow).DialogHost.ShowDialog(new ContentSettings());
        }
        private void _StartShell()
        {
            ProcessStartInfo info = new ProcessStartInfo
            {
                WorkingDirectory = Adb.AdbToolsDir.FullName,
                FileName = "cmd",
                UseShellExecute = false,
                Verb = "runas",
            };
            info.EnvironmentVariables["ANDROID_ADB_SERVER_PORT"] = Adb.Server.Port.ToString();
            if (Settings.Default.EnvVarCmdWindow)
            {
                var pathEnv = info.EnvironmentVariables["path"];
                info.EnvironmentVariables["path"] = $"{Adb.AdbToolsDir.FullName};" + pathEnv;
            }
            if (Settings.Default.StartCmdAtDesktop)
            {
                info.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }
            if (OSInfo.IsWindows10)
            {
                var view = new ChoiceView(
                    content: App.Current.Resources["msgShellChoiceTip"].ToString(),
                    btnYes: "PowerShell",
                    btnNo: "CMD",
                   btnCancel: null);
                var task = (App.Current.MainWindow as MainWindow).DialogHost.ShowDialog(view, closingEventHandler: (s, e) =>
                {
                    if (e.Parameter == null) return;
                    if ((bool)e.Parameter == true)
                    {
                        info.FileName = "powershell.exe";
                    }
                    Process.Start(info);
                });
            }
            else
            {
                Process.Start(info);
            }

        }
        private void Instance_SelectedDevice(object sender, EventArgs e)
        {
            TabSelectedIndex = 1;
        }

        private void NoDevice(object sender, EventArgs e)
        {
            TabSelectedIndex = 0;
        }
    }
}
