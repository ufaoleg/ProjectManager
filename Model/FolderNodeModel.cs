// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

/* FolderNodeModel.cs
 * prspkt.ru
 * © PRSPKT Architects, 2016-2020
 *
 * This updater is used to create an updater capable of reacting
 * to changes in the Revit model.
 */

#region Namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using PRSPKT_ProjectManager.ViewModels;

#endregion

namespace PRSPKT_ProjectManager.Model
{
    public class FolderNodeModel : PropertyChangedBase
    {
        private FolderNodeModel parentNode;
        public int Id { get; set; }

        public string Name
        {
            get => this._name;
            set
            {
                this._name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        public bool InEditMode
        {
            get => this._inEditMode;
            set
            {
                this._inEditMode = value;
                NotifyOfPropertyChange(nameof(InEditMode));
            }
        }

        public BindableCollection<FolderNodeModel> SubFolders { get; set; }
        public EditTemplateViewModel TreeList;
        private bool _inEditMode;
        private string _name;

        public FolderNodeModel ParentNode
        {
            get { return this.parentNode;}
            set
            {
                if (value != this.parentNode)
                {
                    FolderNodeModel oldParent = this.parentNode;
                    this.parentNode = value;
                    if (oldParent != null)
                    {
                        oldParent.SubFolders.Remove(this);
                    }
                    this.parentNode.SubFolders.Add(this);
                }
            }
        }

        public FolderNodeModel(int id, string name)
        {
            Id = id;
            Name = name;
            SubFolders = new BindableCollection<FolderNodeModel>();
        }

        public void Add(FolderNodeModel subFolder)
        {
            if (this.SubFolders == null)
            {
                SubFolders = new BindableCollection<FolderNodeModel>();
            }

            subFolder.parentNode = this;
            this.SubFolders.Add(subFolder);
        }

        public void Traverse(Action<FolderNodeModel> action)
        {
            action(this);
            if (this.SubFolders != null)
            {
                foreach (FolderNodeModel subFolder in this.SubFolders)
                {
                    subFolder.Traverse(action);
                }
            }
        }
    }
}