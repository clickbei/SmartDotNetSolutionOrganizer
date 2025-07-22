using SmartSolutionOrganizer.Core;
using SmartSolutionOrganizer.Core.Interface;
using SmartSolutionOrganizer.Core.Models;
using SmartSolutionOrganizer.Core.Services;
using System.Collections.Generic;

namespace SmartSolutionOrganizer.StandardBridge
{
    public class CoreBridge
    {
        private readonly ISolutionScannerService solutionScanner;
      
        public CoreBridge()
        {
            solutionScanner = new SolutionScannerService(); // Initialize your .NET 8 class
        }

        public IEnumerable<ProjectInfo> GetProjects(string solutionPath)
        {
            return solutionScanner.GetProjects(solutionPath); // Example method from your .NET 8 project
        }
    }
}
