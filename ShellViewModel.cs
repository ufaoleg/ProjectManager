using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using PRSPKT_ProjectManager.Model;
using PRSPKT_ProjectManager.Service;

namespace PRSPKT_ProjectManager
{
    class ShellViewModel : PropertyChangedBase
    {
        private BindableCollection<Project> projects = new BindableCollection<Project>();
        private Manager manager;
        //private bool isProjectSelected;
        private List<Project> _selectedProjects = new List<Project>();
        private Project _projectSelected;
        private string _labelName;
        private string _labelComments;
        private string _path;

        public BindableCollection<Project> Projects
        {
            get { return new BindableCollection<Project>(this.manager.Projects); }
            set
            {
                this.manager.Projects = value.ToList();
                NotifyOfPropertyChange(() => Projects);
            }
        }

        public ShellViewModel()
        {
            //DummyProjects();
            this.manager = new Manager(new List<Project>());
            this._path = Constants.DEFAULT_PATH;
            PopulateProjects();
        }

        private void PopulateProjects()
        {
            string[] getFiles = Directory.GetDirectories(this._path);
            this.manager.Projects.Clear();
            foreach (string s in getFiles)
            {
                if ((File.GetAttributes(s) & FileAttributes.Hidden) == FileAttributes.Hidden) continue;
                string dirName = new DirectoryInfo(s).Name;
                this.manager.Projects.Add(new Project(dirName));
            }
            NotifyOfPropertyChange(() => Projects);
        }

        private void DummyProjects()
        {
            this.projects = new BindableCollection<Project>
            {
                new Project("2019_ЗОРГЕ"),
                new Project("2016_Смарт парк"),
                new Project("2019_Белебей"),
                new Project("2019_БОТАНИКА")
            };
        }

        public void Row_SelectionChanged(object sender, SelectionChangedEventArgs obj)
        {
            List<Project> temp = ((DataGrid)sender).SelectedItems.Cast<Project>().ToList();
            ProjectSelected = (Project)(((DataGrid)sender).SelectedItem);
            SelectedProjects = temp;
        }

        public string LabelName
        {
            get => this._labelName;
            set
            {
                this._labelName = value;
                NotifyOfPropertyChange(() => Projects);
                NotifyOfPropertyChange(() => ProjectSelected);
                NotifyOfPropertyChange(() => SelectedProjects);
                NotifyOfPropertyChange(() => LabelName);
            }
        }

        public Project ProjectSelected
        {
            get => this._projectSelected;
            set
            {
                this._projectSelected = value;
                LabelName = this._projectSelected != null ? this._projectSelected.Name : "";
                LabelComments = this._projectSelected != null ? this._projectSelected.Comment : "";
                NotifyOfPropertyChange(() => ProjectSelected);
            }
        }

        public string LabelComments
        {
            get => this._labelComments;
            set
            {
                this._labelComments = value;
                NotifyOfPropertyChange(() => Projects);
                NotifyOfPropertyChange(() => ProjectSelected);
                NotifyOfPropertyChange(() => SelectedProjects);
                NotifyOfPropertyChange(() => LabelComments);
            }
        }

        public List<Project> SelectedProjects
        {
            get => this._selectedProjects;
            set
            {
                this._selectedProjects = value;
                NotifyOfPropertyChange(() => Projects);
                NotifyOfPropertyChange(() => SelectedProjects);
            }
        }

        public void CreateProject()
        {
            //manager.AddProject("2019_Новый проект");
            var vm = new ViewModels.NewProjectViewModel();
            var wm = new WindowManager();
            bool? result = wm.ShowDialog(vm, null, ViewModels.NewProjectViewModel.DefaultWindowSettings);
            if (result.HasValue && result.Value)
            {
                manager.AddProject(vm.Name, vm.Comments);
            }
            NotifyOfPropertyChange(() => Projects);
        }

        public void EditTemplate()
        {
            var vm = new ViewModels.EditTemplateViewModel();
            var wm = new WindowManager();
            bool? result = wm.ShowDialog(vm, null, ViewModels.EditTemplateViewModel.DefaultWindowSettings);
            if (result.HasValue && result.Value)
            {

            }
        }

        public void RenameProject()
        {
            var vm = new ViewModels.RenameDialogViewModel(ProjectSelected);
            var wm = new WindowManager();
            bool? result = wm.ShowDialog(vm, null, ViewModels.RenameDialogViewModel.DefaultWindowSettings);
            if (result.HasValue && result.Value)
            {
                this.ProjectSelected.Name = vm.Name;
                this.ProjectSelected = vm.Project;
                NotifyOfPropertyChange(() => Projects);
                NotifyOfPropertyChange(() => ProjectSelected);
                NotifyOfPropertyChange(() => LabelName);
            }
        }

        public void DeleteProject()
        {
            for (int i = 0; i < SelectedProjects.Count; i++)
            {
                manager.RemoveProject(this.manager.Projects.Find(e => e.Name == SelectedProjects[i].Name));
            }

            NotifyOfPropertyChange(() => Projects);
            NotifyOfPropertyChange(() => SelectedProjects);
        }

        public void Settings()
        {
            var vm = new ViewModels.SettingsViewModel(_path);
            var wm = new WindowManager();
            bool? result = wm.ShowDialog(vm, null, ViewModels.SettingsViewModel.DefaultWindowSettings);
            if (result.HasValue && result.Value)
            {
                this._path = vm.Path;
                PopulateProjects();
            }
        }
    }
}
