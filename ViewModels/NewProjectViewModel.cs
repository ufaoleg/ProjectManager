// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

/* NewProjectViewModel.cs
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

#endregion

namespace PRSPKT_ProjectManager.ViewModels
{
    public class NewProjectViewModel :Screen
    {
        private string name;
        private string _comments;


        public static dynamic DefaultWindowSettings
        {
            get
            {
                dynamic settings = new ExpandoObject();
                settings.Height = 300;
                settings.Width = 350;
                settings.SizeToContent = System.Windows.SizeToContent.Manual;
                return settings;
            }
        }

        public string Name
        {
            get => this.name;
            set
            {
                if (value != this.name)
                {
                    this.name = value;
                    NotifyOfPropertyChange(() => Name);
                }
            }
        }

        public void OK()
        {
            TryClose(true);
        }

        public void Cancel()
        {
            TryClose(false);
        }

        public string Comments
        {
            get => this._comments;
            set
            {
                if (value != this._comments)
                {
                    this._comments = value;
                    NotifyOfPropertyChange(() => Comments);
                }
            }
        }
    }
}