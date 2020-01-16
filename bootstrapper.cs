using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Caliburn.Micro;

namespace PRSPKT_ProjectManager
{
    class bootstrapper : BootstrapperBase
    {
        public bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
