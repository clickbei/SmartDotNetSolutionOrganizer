using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using SmartSolutionOrganizer.Core;
using SmartSolutionOrganizer.Core.Services;
using SmartSolutionOrganizer.StandardBridge;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using Task = System.Threading.Tasks.Task;

namespace SmartSolutionOrganizer.VSExt
{
    internal sealed class OrganizeSolutionCommand
    {
        public const int CommandId = 0x0100;
        public static readonly Guid CommandSet = new Guid("d2f309ee-34be-4cc9-9c80-47a7701c7a7b");
        private readonly AsyncPackage package;

        private OrganizeSolutionCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package;

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        public static async Task InitializeAsync(AsyncPackage package)
        {
            var commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            new OrganizeSolutionCommand(package, commandService);
        }

        private async void Execute(object sender, EventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var dte = await package.GetServiceAsync(typeof(DTE)) as DTE;
            var solutionPath = dte?.Solution?.FullName;

            if (string.IsNullOrEmpty(solutionPath))
            {
                VsShellUtilities.ShowMessageBox(
                    this.package,
                    "No solution is loaded.",
                    "Smart Solution Organizer",
                    OLEMSGICON.OLEMSGICON_WARNING,
                    OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                return;
            }
            var core = new CoreBridge();

            var projects = core.GetProjects(solutionPath).ToList();

            var scanner = new SolutionScannerService();
            var structureIssues = scanner.ValidateStructure(projects).ToList();

            var bestPracticeIssues = new List<string>();
            foreach (var project in projects)
            {
                bestPracticeIssues.AddRange(BestPractices.Check(project));
            }

            var sb = new StringBuilder();
            sb.AppendLine($"\n✅ Found {projects.Count} project(s)\n");

            if (!structureIssues.Any() && !bestPracticeIssues.Any())
            {
                sb.AppendLine("🎉 Solution structure and naming follow best practices!");
            }
            else
            {
                if (structureIssues.Any())
                {
                    sb.AppendLine("⚠️ Structure Issues:");
                    structureIssues.ForEach(issue => sb.AppendLine(" - " + issue));
                }

                if (bestPracticeIssues.Any())
                {
                    sb.AppendLine("\n📌 Best Practice Warnings:");
                    bestPracticeIssues.ForEach(issue => sb.AppendLine(" - " + issue));
                }
            }

            ShowOutput(sb.ToString());
        }

        private void ShowOutput(string message)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var output = (IVsOutputWindow)ServiceProvider.GetService(typeof(SVsOutputWindow));
            var paneGuid = Guid.NewGuid();
            output.CreatePane(ref paneGuid, "Smart Organizer", 1, 1);
            output.GetPane(ref paneGuid, out IVsOutputWindowPane pane);
            pane.OutputString(message + "\n");
            pane.Activate();
        }

        private IServiceProvider ServiceProvider => this.package;
    }
}
