using Newtonsoft.Json;
using System;
using System.IO;

namespace Squash
{
    public class SquashConfiguration
    {
        public SquashConfiguration()
        {

        }

        public SquashConfiguration(string configLocation)
        {
            if (!File.Exists(configLocation))
            {
                ErrorMessage = "Configuration file does not exist";
                AutoGenerateConfig = true;
                return;
            }

            var file = File.ReadAllText(configLocation);
            if (string.IsNullOrWhiteSpace(file))
            {
                ErrorMessage = "File is empty";
                AutoGenerateConfig = true;
                return;
            }

            SquashConfiguration config = null;

            try
            {
                config = JsonConvert.DeserializeObject<SquashConfiguration>(file);
            }
            catch (Exception ex)
            {
                ErrorMessage = "Error parsing config file: " + ex.Message;
            }

            if (config == null)
            {
                ErrorMessage = "Config file is not in the correct format";
                AutoGenerateConfig = true;
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
                ErrorMessage = "Input directory does not exist: " + InputDirectory;
                return false;
            }

            if (!Directory.Exists(OutputDirectory))
            {
                ErrorMessage = "Output directory does not exist: " + OutputDirectory;
                return false;
            }

            return true;
        }

        [JsonIgnore]
        public string ErrorMessage { get; private set; }

        [JsonIgnore]
        public bool AutoGenerateConfig { get; } = false;

        public string Title { get; set; }
        public string InputDirectory { get; set; }
        public string OutputDirectory { get; set; }
    }
}