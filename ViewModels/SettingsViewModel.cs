// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

/* SettingsViewModel.cs
 * prspkt.ru
 * © PRSPKT Architects, 2016-2020
 *
 * This updater is used to create an updater capable of reacting
 * to changes in the Revit model.
 */

#region Namespaces

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Caliburn.Micro;
using Microsoft.Win32;
using PRSPKT_ProjectManager.Service;

#endregion

namespace PRSPKT_ProjectManager.ViewModels
{
    public class SettingsViewModel : Screen
    {
        private string _path;

        public static dynamic DefaultWindowSettings
        {
            get
            {
                dynamic settings = new ExpandoObject();
                settings.Height = 250;
                settings.Width = 500;
                settings.Title = "Настройки";
                settings.SizeToContent = System.Windows.SizeToContent.Manual;
                return settings;
            }
        }

        public void Browse()
        {
            var dialogService = new BasicDialogService();
            var result = dialogService.ShowDirectorySelectionDialog(Path, out string newPath);
            if (result.HasValue && result.Value)
            {
                Path = newPath;
            }
        }

        public SettingsViewModel(string path)
        {
            this._path = path;
        }

        public void OK()
        {
            TryClose(true);
        }

        public void Cancel()
        {
            TryClose(false);
        }

        public string Path
        {
            get => this._path;
            set
            {
                if (value != this._path)
                {
                    this._path = value;
                    NotifyOfPropertyChange(() => Path);
                }
            }
        }
    }
}