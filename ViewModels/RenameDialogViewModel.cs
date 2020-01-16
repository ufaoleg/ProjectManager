// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

/* RenameDialogViewModel.cs
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
using PRSPKT_ProjectManager.Model;

#endregion

namespace PRSPKT_ProjectManager.ViewModels
{
    public class RenameDialogViewModel : Screen
    {
        private string _name;

        public static dynamic DefaultWindowSettings
        {
            get
            {
                dynamic settings = new ExpandoObject();
                settings.Height = 100;
                settings.Width = 400;
                settings.Title = "Переименовать проект";
                settings.SizeToContent = System.Windows.SizeToContent.Manual;
                return settings;
            }
        }

        public Project Project { get; private set; }

        public string Name
        {
            get => this._name;
            set
            {
                this._name = value;
            }
        }

        public RenameDialogViewModel(Project project)
        {
            this.Project = project;
            this.Name = project.Name;
        }

        public void OK()
        {
            this.Project.Name = this.Name;
            TryClose(true);
        }

        public void Cancel()
        {
            TryClose(false);
        }
    }
}