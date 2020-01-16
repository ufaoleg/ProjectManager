// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

/* IDialogService.cs
 * prspkt.ru
 * © PRSPKT Architects, 2016-2020
 *
 * This updater is used to create an updater capable of reacting
 * to changes in the Revit model.
 */

namespace PRSPKT_ProjectManager.Service
{
    public interface IDialogService
    {
        bool? ShowDirectorySelectionDialog(string defaultDir, out string dirPath);
    }
}