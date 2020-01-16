// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

/* EditTemplateViewModel.cs
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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Caliburn.Micro;
using PRSPKT_ProjectManager.Model;

#endregion

namespace PRSPKT_ProjectManager.ViewModels
{
    public class EditTemplateViewModel : Screen
    {
        private Point startpoint;
        private FolderNodeModel _selectedItem;
        private FolderNodeModel _editableItem;
        private string savedName;

        public FolderNodeModel SelectedItem
        {
            get => this._selectedItem;
            set => this._selectedItem = value;
        }

        public void Add()
        {
            var node = new FolderNodeModel(9, "Пусто");
            node.TreeList = this;
            node.ParentNode = null;
            Nodes.Add(node);
            NotifyOfPropertyChange(() => Nodes);
        }

        public static dynamic DefaultWindowSettings
        {
            get
            {
                dynamic settings = new ExpandoObject();
                settings.Height = 350;
                settings.Width = 400;
                settings.Title = "Редактор шаблона";
                settings.SizeToContent = System.Windows.SizeToContent.Manual;
                return settings;
            }
        }

        public void Remove()
        {
            RemoveNode(SelectedItem);
        }

        public void Row_SelectedItemChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this._editableItem != null)
            {
                _editableItem.InEditMode = false;
            }
            SelectedItem = ((TreeView)sender).SelectedItem as FolderNodeModel;
        }

        public EditTemplateViewModel()
        {
            Nodes = new BindableCollection<FolderNodeModel>();
            PopulateTree();
        }

        public BindableCollection<FolderNodeModel> Nodes { get; set; }

        public void ReadOnlyText_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((e.LeftButton == MouseButtonState.Pressed) && (e.ClickCount == 1))
            {
                TextBlock item = sender as TextBlock;

                if ((item.Tag as FolderNodeModel) == SelectedItem)
                {
                    _editableItem = (FolderNodeModel)SelectedItem;
                    _editableItem.InEditMode = true;
                    e.Handled = true;
                    savedName = this._editableItem.Name;
                }
            }
            NotifyOfPropertyChange(() => Nodes);
        }

        public void NodePreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e == null || sender == null) return;
            startpoint = e.GetPosition(null);
        }

        public void EditableText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if ((sender is TextBox) && (((TextBox) sender).Tag is FolderNodeModel))
                {
                    FolderNodeModel item = ((TextBox)sender).Tag as FolderNodeModel;
                    item.InEditMode = false;
                    NotifyOfPropertyChange(() => Nodes);
                }
            }

            if (e.Key == Key.Escape)
            {
                if ((sender is TextBox) && (((TextBox)sender).Tag is FolderNodeModel))
                {
                    FolderNodeModel item = ((TextBox)sender).Tag as FolderNodeModel;
                    if (item != null)
                    {
                        item.Name = this.savedName;
                        item.InEditMode = false;
                    }
                }
            }
        }

        public void NodeDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(FolderNodeModel)))
            {
                var node = e.Data.GetData(typeof(FolderNodeModel)) as FolderNodeModel;
                var treeViewItem = FindAnchestor<TreeViewItem>((DependencyObject) e.OriginalSource);

                if (treeViewItem != null)
                {
                    var dropTarget = treeViewItem.Header as FolderNodeModel;
                    if ((node == dropTarget) || dropTarget == null || node == null) return;
                    if (node.ParentNode == null) RemoveNode(node);
                    node.ParentNode = dropTarget;
                }
            }
        }

        public void RemoveNode(FolderNodeModel node)
        {
            if (node.ParentNode != null)
            {
                node.ParentNode.SubFolders.Remove(node);
            }
            else
            {
                Nodes.Remove(node);
                NotifyOfPropertyChange(() => Nodes);
            }
        }

        //public void AddNode(FolderNodeModel node)
        //{
        //    node.SubFolders.Add();
        //}

        public void NodeDragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(FolderNodeModel)) || sender != e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        public void NodeMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var mousePos = e.GetPosition(null);
                var diff = this.startpoint - mousePos;

                if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance
                    || Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    var treeView = sender as TreeView;
                    var treeViewItem = FindAnchestor<TreeViewItem>((DependencyObject)e.OriginalSource);

                    if (treeView == null || treeViewItem == null)
                        return;

                    var node = treeView.SelectedItem as FolderNodeModel;
                    if (node == null)
                        return;
                    if (node.InEditMode) return;
                    var dragData = new DataObject(node);
                    DragDrop.DoDragDrop(treeViewItem, dragData, DragDropEffects.Move);
                }
            }
        }

        private static T FindAnchestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T) current;
                }

                current = VisualTreeHelper.GetParent(current);
            } while (current != null);
            return null;
        }

        private void PopulateTree()
        {
            //var collection = new BindableCollection<FolderNodeModel>();
            var root1 = new FolderNodeModel(1, "01_ОБЩИЕ");
            (new FolderNodeModel(2, "01_Техническое задание")).ParentNode = root1;
            (new FolderNodeModel(3, "02_План реализации проекта")).ParentNode = root1;
            (new FolderNodeModel(4, "03_Исходные данные")).ParentNode = root1;
            (new FolderNodeModel(5, "04_Задания")).ParentNode = root1;
            root1.SubFolders.ToList().ForEach(x => x.TreeList = this);
            root1.TreeList = this;
            Nodes.Add(root1);

            var root2 = new FolderNodeModel(6, "02_ТЕХНИЧЕСКАЯ ДОКУМЕНТАЦИЯ");
            (new FolderNodeModel(7, "10_Концепция")).ParentNode = root2;
            root2.SubFolders.ToList().ForEach(x => x.TreeList = this);
            root2.TreeList = this;
            Nodes.Add(root2);
            //FolderNodeModel parent = null;
            //FolderNodeModel child = null;
            //List<string> keks = new List<string>()
            //{
            //    "01_ОБЩИЕ",
            //    "02_ТЕХНИЧЕСКАЯ ДОКУМЕНТАЦИЯ",
            //    "03_КОРРЕСПОНДЕНЦИЯ",
            //    "04_РЕЗУЛЬТАТЫ РАБОТ",
            //    "05_ДОГОВОР"
            //};
            //foreach (string kek in keks)
            //{
            //    parent = new FolderNodeModel(kek);
            //    collection.Add(parent);
            //}

            //this.Nodes = collection;
            //NotifyOfPropertyChange(() => Nodes);
        }

        public void Save()
        {
            TryClose(true);
        }

        public void Close()
        {
            TryClose(false);
        }
    }
}