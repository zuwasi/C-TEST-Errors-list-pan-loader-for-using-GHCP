using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace Parasoft_errors_list_loader
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [Guid("c1af0903-1399-4dc0-b802-6efb2990b16d")]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    public sealed class VSPackage : Package
    {
        protected override void Initialize()
        {
            base.Initialize();
            ThreadHelper.ThrowIfNotOnUIThread();
            LoadSarifCommand.Initialize(this);
        }
    }
}