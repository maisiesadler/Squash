using System;

namespace Squash.Console
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
    }
}