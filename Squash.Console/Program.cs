using System;

namespace Squash
{
    class Program
    {
        static void Main(string[] args)
        {
            Squasher squasher = new Squasher(new Logger());

			var inputDirectory = "/Users/maisiesadler/Downloads/input/";
			var outputDirectory = "/Users/maisiesadler/Downloads/output";

            var squashConfiguration = new SquashConfiguration(title: "Title",
                                                inputDirectory: inputDirectory,
                                                outputDirectory: outputDirectory);
            
            if (squashConfiguration.Validate())
            {
                squasher.Squash(squashConfiguration);
            }
        }

        public class Logger : ILogger
		{
			public void Debug(string message)
			{
                Console.ForegroundColor = ConsoleColor.Gray;
				Console.WriteLine(message);
			}

            public void Info(string message)
			{
				Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(message);
            }

			public void Error(string message)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(message);
			}
        }
    }
}