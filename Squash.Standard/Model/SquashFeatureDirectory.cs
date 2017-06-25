using System.Collections.Generic;
using System.IO;

namespace Squash
{
    internal class SquashFeatureDirectory
    {
        public SquashFeatureDirectory(string directoryName, string directoryExtension = "")
        {
            DirectoryName = directoryName;
            DirectoryExtension = directoryExtension;

            FeatureFiles = new List<SquashFeature>();
            Directories = new List<SquashFeatureDirectory>();

            GetFeatureFiles();
            GetDirectories();
        }

        private void GetFeatureFiles()
        {
            var featureFiles = Directory.GetFiles(DirectoryName, "*.feature");

            SquashLogger.Info($"Got {featureFiles.Length} feature files for {DirectoryName}");

            foreach (var file in featureFiles)
            {
                var feature = new SquashFeature(file);
                FeatureFiles.Add(feature);
            }
        }

        private void GetDirectories()
        {
            var directories = Directory.GetDirectories(DirectoryName);
            SquashLogger.Info($"Got {directories.Length} directories for {DirectoryName}");
            foreach (var dir in directories)
            {
                var e = dir.Split('\\');
                var ext = e[e.Length - 1] + "\\";
                var f = new SquashFeatureDirectory(dir, DirectoryExtension + ext);
                if (f.Directories.Count > 0 || f.FeatureFiles.Count > 0)
                {
                    Directories.Add(f);
                }
            }
        }

        public string DirectoryExtension { get; set; }
        public string DirectoryName { get; set; }
        public List<SquashFeature> FeatureFiles { get; set; } //location, content
        public List<SquashFeatureDirectory> Directories { get; set; }
    }
}