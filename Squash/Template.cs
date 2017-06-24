using System.IO;

namespace Squash
{
    internal class Template
    {
        private string _template;

        internal Template(string path)
        {
            _template = File.ReadAllText("Templates/" + path);
        }

        public void Replace(string propertyName, string propertyValue)
        {
            _template = _template.Replace("{{" + propertyName + "}}", propertyValue);
        }

        public string TemporaryReplace(string propertyName, string propertyValue)
        {
            return _template.Replace("{{" + propertyName + "}}", propertyValue);
        }

        public string Get()
        {
            return _template;
        }
    }
}