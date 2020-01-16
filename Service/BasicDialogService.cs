// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

/* BasicDialogService.cs
 * prspkt.ru
 * © PRSPKT Architects, 2016-2020
 *
 * This updater is used to create an updater capable of reacting
 * to changes in the Revit model.
 */

#region Namespaces

using System.Windows.Forms;

#endregion

namespace PRSPKT_ProjectManager.Service
{
    public class BasicDialogService : IDialogService
    {
        public bool? ShowDirectorySelectionDialog(string defaultDir, out string dirPath)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    dirPath = dialog.SelectedPath;
                    return true;
                }
                dirPath = defaultDir;
                return false;
            }
        }
    }
}