using SmartSolutionOrganizer.Core.Helper;
using SmartSolutionOrganizer.Core.Interface;
using SmartSolutionOrganizer.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text.RegularExpressions;

namespace SmartSolutionOrganizer.Core.Services
{
    public class SolutionScannerService : ISolutionScannerService
    {
        private readonly BestPracticesConfig bestPracticesConfig;
        private readonly string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "organizer.config.json");

        public SolutionScannerService()
        {
            bestPracticesConfig = BestPracticesConfigLoader.Load(path);
        }
        public IEnumerable<ProjectInfo> GetProjects(string solutionPath)
        {
            var baseDir = Path.GetDirectoryName(solutionPath);
            var lines = File.ReadAllLines(solutionPath);

            const string regexPattern = @"Project\("".*?""\) = ""(.*?)"", ""(.*?)"", ""(.*?)""";

            foreach (var line in lines)
            {
                if (line.StartsWith("Project(", StringComparison.Ordinal))
                {
                    var match = Regex.Match(line, regexPattern);
                    if (!match.Success)
                        continue;
                    var name = match.Groups[1].Value.Trim();
                    var path = match.Groups[2].Value.Replace("\\", Path.DirectorySeparatorChar.ToString());

                    yield return new ProjectInfo
                    {
                        Name = name,
                        RelativePath = path
                    };
                }
            }
        }

        public IEnumerable<string> ValidateStructure(IEnumerable<ProjectInfo> projects)
        {
            foreach (var proj in projects)
            {
                var expectedFolder = ExtractLogicalFolder(proj.Name); // e.g., MyApp.Data => /Data/
                if (!proj.RelativePath.Contains(expectedFolder))
                {
                    yield return $"Project \"{proj.Name}\" should be in folder /{expectedFolder}";
                }
            }
        }
        private string ExtractLogicalFolder(string projectName)
        {
            // Simple rule: MyApp.Core => Core
            var parts = projectName.Split('.');
            return parts.Length > 1 ? parts[1] : parts[0];
        }

        public SolutionScanResult GetScanResult(string solutionPath)
        {
            var result = new SolutionScanResult();

            var projects = GetProjects(solutionPath).ToList();

            Console.WriteLine($"\n✅ Found {projects.Count} project(s) in solution\n");

            var issues = ValidateStructure(projects).ToList();

            var bestPracticeIssues = new List<string>();

            foreach (var project in projects)
            {
                bestPracticeIssues.AddRange(BestPractices.Check(project));
            }

            if (bestPracticeIssues.Any())
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


            return result;
        }
    }
}