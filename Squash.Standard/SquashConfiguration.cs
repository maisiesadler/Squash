using Newtonsoft.Json;
using System;
using System.IO;

namespace Squash
{
    public class SquashConfiguration
    {
        internal SquashConfiguration()
        {

        }

        public SquashConfiguration(string title, string inputDirectory, string outputDirectory)
        {
            Title = title;
            InputDirectory = NormalizeEnding(inputDirectory);
            OutputDirectory = NormalizeEnding(outputDirectory);
        }

        public SquashConfiguration(string configLocation)
        {
            if (!File.Exists(configLocation))
            {
                SquashLogger.Error("Configuration file does not exist");
                return;
            }

            var file = File.ReadAllText(configLocation);
            if (string.IsNullOrWhiteSpace(file))
            {
                SquashLogger.Error("File is empty");
                return;
            }

            SquashConfiguration config = null;

            try
            {
                config = JsonConvert.DeserializeObject<SquashConfiguration>(file);
            }
            catch (Exception ex)
            {
                SquashLogger.Error("Error parsing config file: " + ex.Message);
            }

            if (config == null)
            {
                SquashLogger.Error("Config file is not in the correct format");
                return;
            }

            Title = config.Title;
            InputDirectory = config.InputDirectory;
            OutputDirectory = config.OutputDirectory;

            Validate();
        }

        public bool Validate()
        {
            if (!Directory.Exists(InputDirectory))
            {
                SquashLogger.Error("Input directory does not exist: \"" + InputDirectory + "\"");
                return false;
            }

            if (!Directory.Exists(OutputDirectory))
            {
                SquashLogger.Error("Output directory does not exist: \"" + OutputDirectory + "\"");
                return false;
            }

            return true;
        }

        public void GenerateBlankConfigFile(string configFileLocation)
        {
            SquashConfiguration blankConfig = new SquashConfiguration();
            blankConfig.InputDirectory = "[[Enter input directory]]";
            blankConfig.OutputDirectory = "[[Enter output directory]]";
            blankConfig.Title = "[[Title]]";

            var config = JsonConvert.SerializeObject(blankConfig, Formatting.Indented);
            File.WriteAllText(configFileLocation, config);
        }

        private string NormalizeEnding(string filePath)
        {
            if (filePath.EndsWith("\\"))
                return filePath;

            return filePath + "\\";
        }

        public string Title { get; set; }
        public string InputDirectory { get; set; }
        public string OutputDirectory { get; set; }

        public override string ToString()
        {
            return
                $"Title: {Title}, InputDirectory: {InputDirectory}, OutputDirectory: {OutputDirectory}";
        }
    }
}