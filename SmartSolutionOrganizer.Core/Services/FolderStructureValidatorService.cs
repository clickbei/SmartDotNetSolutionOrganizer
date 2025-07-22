//using SmartSolutionOrganizer.Core.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SmartSolutionOrganizer.Core
//{
//    public class FolderStructureValidatorService
//    {
//        public IEnumerable<string> ValidateStructure(IEnumerable<ProjectInfo> projects)
//        {
//            foreach (var proj in projects)
//            {
//                var expectedFolder = ExtractLogicalFolder(proj.Name); // e.g., MyApp.Data => /Data/
//                if (!proj.RelativePath.Contains(expectedFolder))
//                {
//                    yield return $"Project \"{proj.Name}\" should be in folder /{expectedFolder}";
//                }
//            }
//        }
//        private string ExtractLogicalFolder(string projectName)
//        {
//            // Simple rule: MyApp.Core => Core
//            var parts = projectName.Split('.');
//            return parts.Length > 1 ? parts[1] : parts[0];
//        }
//    }

//}
