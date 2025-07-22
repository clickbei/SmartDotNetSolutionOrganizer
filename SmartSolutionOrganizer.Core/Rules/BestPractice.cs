using SmartSolutionOrganizer.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SmartSolutionOrganizer.Core
{
    public static class BestPractices
    {
        public static IEnumerable<string> Check(ProjectInfo project)
        {
            var issues = new List<string>();

            // Rule 1: Project name should follow PascalCase
            if (!char.IsUpper(project.Name[0]))
            {
                issues.Add($"Project '{project.Name}' should use PascalCase.");
            }

            // Rule 2: Check for meaningful suffixes
            var validSuffixes = new[] { "Core", "Web", "Data", "Api", "Common", "Services" };
            if (!validSuffixes.Any(suffix => project.Name.EndsWith(suffix)))
            {
                issues.Add($"Project '{project.Name}' should end with a valid layer suffix (e.g., Core, Web, Data).");
            }

            // Rule 3: .csproj file should match project name
            var fileName = Path.GetFileNameWithoutExtension(project.FullPath);
            if (!string.Equals(fileName, project.Name, StringComparison.OrdinalIgnoreCase))
            {
                issues.Add($"Project file name '{fileName}' does not match project name '{project.Name}'.");
            }

            return issues;
        }
    }

}
