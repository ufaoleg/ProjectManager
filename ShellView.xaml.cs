using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace PRSPKT_ProjectManager
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : MetroWindow
    {
        public ShellView()
        {
            InitializeComponent();
        }

        private void Projects_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.Projects.SelectedItems.Count == 1)
            {
                this.Projects.ScrollIntoView(this.Projects.SelectedItem);
            }
        }
    }
}
