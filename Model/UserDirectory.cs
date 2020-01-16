// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

/* Folder.cs
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

#endregion

namespace PRSPKT_ProjectManager.Model
{
    public class UserDirectory
    {
        public BindableCollection<UserDirectory> Subfolders { get; set; } = new BindableCollection<UserDirectory>();
        public string DirectoryPath { get; set; }
        public string Name { get; set; }
    }
}