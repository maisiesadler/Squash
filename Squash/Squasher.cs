namespace Squash
{
    public class Squasher
    {
        public static void Squash(SquashConfiguration configuration)
        {
            var squashFeatureDirectory = new SquashFeatureDirectory(configuration.InputDirectory);

            var template = new PageTemplate(squashFeatureDirectory, configuration.OutputDirectory, configuration.Title);
            var htmlGenerator = new HtmlGenerator(template, configuration.OutputDirectory, squashFeatureDirectory);
        }

        //public static void Squash(string directory, string outputDirectory, string menuTitle = "")
        //{
        //    var squashFeatureDirectory = new SquashFeatureDirectory(directory);

        //    var template = new PageTemplate(squashFeatureDirectory, outputDirectory, menuTitle);
        //    var htmlGenerator = new HtmlGenerator(template, outputDirectory, squashFeatureDirectory);
        //}
    }
}