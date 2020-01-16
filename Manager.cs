// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

/* Manager.cs
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
using PRSPKT_ProjectManager.Model;

#endregion

namespace PRSPKT_ProjectManager
{
    public class Manager
    {
        public List<Project> Projects { get; set; }

        public void AddProject(string name)
        {
            this.Projects.Add(new Project(name));
        }

        public void AddProject(string name, string comment)
        {
            this.Projects.Add(new Project(name, comment));
        }

        public Manager(List<Project> projectsList)
        {
            this.Projects = projectsList;
        }

        public void RemoveProject(Project prj)
        {
            this.Projects.Remove(prj);
        }

        //public Manager(List<Project> projectsList)
        //{
        //    //this.projects = projectsList;
        //}
    }
}