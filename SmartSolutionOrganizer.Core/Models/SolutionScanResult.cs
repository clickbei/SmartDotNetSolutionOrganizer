using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSolutionOrganizer.Core.Models
{
    public class SolutionScanResult
    {
        public string SolutionPath { get; set; }
        public IEnumerable<ProjectInfo> Projects { get; set; }
        public int ProjectCount { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; }
    }
}
