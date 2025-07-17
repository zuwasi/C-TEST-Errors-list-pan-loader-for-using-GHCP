using System;
using System.ComponentModel.Design;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Parasoft_errors_list_loader
{
    internal sealed class LoadSarifCommand
    {
        private static readonly EventHandler ExecuteHandler = Execute;

        public static void Initialize(Package package)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            // Use IServiceProvider interface directly to avoid extension method conflicts
            IServiceProvider serviceProvider = package;
            object serviceObject = serviceProvider.GetService(typeof(IMenuCommandService));
            IMenuCommandService commandService = serviceObject as IMenuCommandService;

            if (commandService != null)
            {
                var cmdId = new CommandID(new Guid("c1af0903-1399-4dc0-b802-6efb2990b16d"), 0x0100);
                var menuCommand = new MenuCommand(ExecuteHandler, cmdId);
                commandService.AddCommand(menuCommand);
            }
        }

        private static void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var ofd = new OpenFileDialog
            {
                Filter = "SARIF files (*.sarif)|*.sarif|All files (*.*)|*.*",
                Title = "Select SARIF File"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var content = File.ReadAllText(ofd.FileName); // Placeholder for SARIF processing

                VsShellUtilities.ShowMessageBox(
                    ServiceProvider.GlobalProvider,
                    "SARIF file loaded: " + ofd.FileName,
                    "Parasoft",
                    OLEMSGICON.OLEMSGICON_INFO,
                    OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
            }
        }
    }
}