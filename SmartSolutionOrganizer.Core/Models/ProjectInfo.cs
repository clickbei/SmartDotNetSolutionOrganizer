using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSolutionOrganizer.Core.Models
{
    public class ProjectInfo
    {
        public string Name { get; set; }
        public string RelativePath { get; set; }
        public string FullPath => Path.GetFullPath(RelativePath);
    }
}
