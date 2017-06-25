using System;

namespace Squash
{
    public class Squasher
    {
        public Squasher(ILogger logger)
        {
            SquashLogger.Logger = logger;
        }

        public Squasher()
        {
            SquashLogger.Logger = new NoOpLogger();
        }

        public void Squash(SquashConfiguration configuration)
        {
            var squashFeatureDirectory = new SquashFeatureDirectory(configuration.InputDirectory);

            var template = new PageTemplate(squashFeatureDirectory, configuration.OutputDirectory, configuration.Title);
            var htmlGenerator = new HtmlGenerator(template, configuration.OutputDirectory, squashFeatureDirectory);
            htmlGenerator.ClearOutputDirectory();
            htmlGenerator.GenerateFiles();
            SquashLogger.Info("Done!");
        }
    }

    internal static class SquashLogger
    {
        internal static ILogger Logger { get; set; }

        public static void Error(string message)
        {
            Logger?.Error(message);
		}

		public static void Info(string message)
		{
			Logger?.Info(message);
		}

		public static void Debug(string message)
		{
            Logger?.Debug(message);
		}
    }
}