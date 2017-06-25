using Squash.Standard.Templates;
using System.IO;

namespace Squash
{
    internal class PageTemplate
    {
        private Template _template;
        private string minifiedCss = "body{font-family:\"Palatino Linotype\",\"Book Antiqua\",Palatino,serif;}body .menu,body .content{display:inline-block;}body .menu{width:300px;vertical-align:top;}body .menu h3,body .menu h4{margin:5px 0;color:#1c3d72;}body .menu ul{-webkit-padding-start:25px;}body .so-scenarios{font-weight:bold;color:#1c3d72;}body .content{width:calc(100% - 308px);}body .content h1{margin-left:25px;color:#1c3d72;}body .content h3{margin-bottom:5px;}body .content .description{margin:10px;}body .content .scenarios{margin:10px;}body .content .scenarios .tag{padding:0 8px;background-color:#b8cbd8;border-radius:3px;}body .content .scenarios th{color:#1c3d72;text-align:left;}body .content .scenarios .action{font-weight:bold;}";
        private string minifiedJs = "$('.menu li').on('click', function (a,b,c) { var loc = $(this).attr('loc'); window.location = loc; });";

        internal PageTemplate(SquashFeatureDirectory squashFeatureDirectory, string outputDirectory, string menuHeader = "")
        {
            var menu = new Menu(squashFeatureDirectory, outputDirectory, menuHeader);

           // string style = File.ReadAllText("style.css");
            //string script = File.ReadAllText("script.js");

            var template = new Template(Templates.Template);
            
            template.Replace("menu", menu);
            template.Replace("templateStyle", minifiedCss);
            template.Replace("templateScript", minifiedJs);

            _template = template;
            SquashLogger.Info("Got page template");
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