using System;

namespace Squash
{
    class Program
    {
        static void Main(string[] args)
        {
            Squasher squasher = new Squasher();

            var inputDirectory = "";
            var outputDirectory = "";

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
				Console.WriteLine(message);
			}

            public void Info(string message)
            {
                Console.WriteLine(message);
            }

			public void Error(string message)
			{
				Console.WriteLine(message);
			}
        }
    }
}