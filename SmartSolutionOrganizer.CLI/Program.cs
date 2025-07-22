using SmartSolutionOrganizer.Core;
using SmartSolutionOrganizer.Core.Services;

namespace SmartSolutionOrganizer.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            args= new string[1];

            args[0] = Console.ReadLine() ?? string.Empty;
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: smartorganizer <solution.sln>");
                return;
            }

            var solutionPath = args[0];

            if (!File.Exists(solutionPath))
            {
                Console.WriteLine($"Solution file not found: {solutionPath}");
                return;
            }

            var scanner = new SolutionScannerService();
            var projects = scanner.GetProjects(solutionPath).ToList();

            Console.WriteLine($"\n✅ Found {projects.Count} project(s) in solution\n");

            var issues = scanner.ValidateStructure(projects).ToList();

            var bestPracticeIssues = new List<string>();

            foreach (var project in projects)
            {
                bestPracticeIssues.AddRange(BestPractices.Check(project));
            }

            if(bestPracticeIssues.Any())
                issues.AddRange(bestPracticeIssues);

            if (issues.Count == 0)
            {
                Console.WriteLine("🎉 Solution folder structure looks good!");
            }
            else
            {
                Console.WriteLine("⚠️ Issues found:");
                foreach (var issue in issues)
                {
                    Console.WriteLine(" - " + issue);
                }
            }

        }
    }
}
