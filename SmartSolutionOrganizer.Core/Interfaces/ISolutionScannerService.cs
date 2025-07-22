using SmartSolutionOrganizer.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSolutionOrganizer.Core.Interface
{
    public  interface ISolutionScannerService
    {
        IEnumerable<string> ValidateStructure(IEnumerable<ProjectInfo> projects);
        IEnumerable<ProjectInfo> GetProjects(string solutionPath);
    }
}
