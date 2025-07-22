using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSolutionOrganizer.Core.Models
{
    public class BestPracticesConfig
    {
        public List<string> ValidSuffixes { get; set; }
        public bool EnforcePascalCase { get; set; } = true;
        public bool EnforceMatchingProjectFileName { get; set; } = true;
    }
}
