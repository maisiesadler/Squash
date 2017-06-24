using System.IO;

namespace Squash
{
    internal class PageTemplate
    {
        private Template _template;

        internal PageTemplate(SquashFeatureDirectory squashFeatureDirectory, string outputDirectory, string menuHeader = "")
        {
            var menu = new Menu(squashFeatureDirectory, outputDirectory, menuHeader);

            string style = File.ReadAllText("style.css");
            string script = File.ReadAllText("script.js");

            var template = new Template("template.html");
            
            template.Replace("menu", menu);
            template.Replace("templateStyle", style);
            template.Replace("templateScript", script);

            _template = template;
        }

        public string GetPage(string content)
        {
            return _template.TemporaryReplace("content", content);
        }

        public override string ToString()
        {
            return _template.Get();
        }
    }
}