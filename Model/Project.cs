// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

/* Project.cs
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

#endregion

namespace PRSPKT_ProjectManager.Model
{
    public class Project
    {
        public bool IsSelected { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }

        public Project(string name)
        {
            Name = name;
            Comment = "";
        }

        public Project(string name, string comment)
        {
            Name = name;
            Comment = comment;
        }
    }
}