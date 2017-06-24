using Newtonsoft.Json;
using System;
using System.IO;

namespace Squash
{
    class Program
    {
        static void Main(string[] args)
        {
            var configFileLocation = "squash.json";
            
            var config = new SquashConfiguration(configFileLocation);
            if (!string.IsNullOrEmpty(config.ErrorMessage))
            {
                if (config.AutoGenerateConfig)
                {
                    var shouldGenerateConfig = Output(config.ErrorMessage + ", auto generate? Y/N", true);
                    if (shouldGenerateConfig == 'y' || shouldGenerateConfig == 'Y')
                    {
                        GenerateConfig(configFileLocation, config);
                    }
                }
                else
                {
                    Output(config.ErrorMessage, true);
                }
            }
            else
            {
                Output("Config is valid, squashing files.");
                Squasher.Squash(config);
            }
        }

        private static void GenerateConfig(string location, SquashConfiguration configuration)
        {
            SquashConfiguration blankConfig = new SquashConfiguration();
            blankConfig.InputDirectory = configuration == null || string.IsNullOrWhiteSpace(configuration.InputDirectory) ? "[[Enter input directory]]" : configuration.InputDirectory;
            blankConfig.OutputDirectory = configuration == null || string.IsNullOrWhiteSpace(configuration.OutputDirectory) ? "[[Enter output directory]]" : configuration.OutputDirectory;
            blankConfig.Title = configuration == null || string.IsNullOrWhiteSpace(configuration.Title) ? "[[optional title]]" : configuration.Title;

            var config = JsonConvert.SerializeObject(blankConfig, Formatting.Indented);
            File.WriteAllText(location, config);
        }

        private static char Output(string message, bool waitForReturn = false)
        {
            Console.WriteLine(message);

            if (waitForReturn)
            {
                return Console.ReadKey().KeyChar;
            }

            return '-';
        }
    }
}