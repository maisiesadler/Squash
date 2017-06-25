using System.Linq;

namespace Squash
{
    internal class Menu
    {
        private string _outputDirectory;
        private string _content;
        private string _header;

        internal Menu(SquashFeatureDirectory root, string outputDirectory, string menuHeader)
        {
            _outputDirectory = outputDirectory;
            SetHeader(menuHeader, root);
            _content = GetMenuFor(root);
            SquashLogger.Info("Generating page menu");
        }

        private void SetHeader(string header, SquashFeatureDirectory rootFeatureDirectory)
        {
            var h = string.IsNullOrWhiteSpace(header) ? rootFeatureDirectory.DirectoryName : header;
            _header = $"<h3>{h}</h3>";
        }

        private string GetMenuFor(SquashFeatureDirectory directory)
        {
            var menu = "<ul>";

            var location = directory.DirectoryExtension.Split(SquashConfiguration.Separator)
                                        .LastOrDefault(e => !string.IsNullOrWhiteSpace(e));

            menu += "<h4>" + location + "</h4>";

            foreach (var dir in directory.Directories)
            {
                menu += GetMenuFor(dir);
            }

            foreach (var feature in directory.FeatureFiles)
            {
                menu += $"<li loc='{_outputDirectory + directory.DirectoryExtension.Replace(SquashConfiguration.Separator,'-') + feature.Name + ".html"}'>{feature.Name}</li>";
            }

            menu += "</ul>";
            return menu;
        }

        public override string ToString()
        {
            return _header + _content;
        }

        public static implicit operator string(Menu menu)
        {
            return menu.ToString();
        }
    }

}