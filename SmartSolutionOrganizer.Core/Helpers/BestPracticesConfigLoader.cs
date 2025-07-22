using System.IO;
using Newtonsoft.Json;
using SmartSolutionOrganizer.Core.Models;

namespace SmartSolutionOrganizer.Core.Helper
{
    public static class BestPracticesConfigLoader
    {
        public static BestPracticesConfig Load(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("Best practices config file not found.", path);

            var json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<BestPracticesConfig>(json)
                   ?? new BestPracticesConfig(); // Fallback to default if deserialization fails
        }
    }
}
