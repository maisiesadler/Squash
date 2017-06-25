using Squash.Standard.Templates;
using System.IO;
using System.Linq;

namespace Squash
{
    internal class HtmlGenerator
    {
        private PageTemplate _pageTemplate;
        private string _outputDirectory;
        private SquashFeatureDirectory _root;

        internal HtmlGenerator(PageTemplate pageTemplate, string outputDirectory, SquashFeatureDirectory root)
        {
            _pageTemplate = pageTemplate;
            _outputDirectory = outputDirectory;
            _root = root;
        }

        public void GenerateFiles()
        {
            SquashLogger.Info("Generating files");
            GenerateFeatureFilesFor(_root);
        }

        public void ClearOutputDirectory()
        {
            SquashLogger.Info("Clearing output directory");
            DirectoryInfo di = new DirectoryInfo(_outputDirectory);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        private void GenerateFeatureFilesFor(SquashFeatureDirectory squashFeatureDirectory)
        {
            foreach (var feature in squashFeatureDirectory.FeatureFiles)
            {
                var content = FeatureToHtml(feature);
                var page = _pageTemplate.GetPage(content);
                var location = squashFeatureDirectory.DirectoryExtension.Replace(SquashConfiguration.Separator, '-');
                File.WriteAllText(_outputDirectory + location + feature.Name + ".html", page);
            }

            foreach(var dir in squashFeatureDirectory.Directories)
            {
                GenerateFeatureFilesFor(dir);
            }
        }
        
        public string FeatureToHtml(SquashFeature feature)
        {
            var template = new Template(Templates.Feature);

            template.Replace("name", feature.Name);
            template.Replace("description", feature.Description);

            var scenarios = "";
            foreach (var scenario in feature.Scenarios)
            {
                var t = new Template(Templates.Scenario);
                t.Replace("name", scenario.Name);
                t.Replace("tags", string.Join(" ", scenario.Tags.Select(tag => "<span class='tag'>" + tag + "</span>")));

                var steps = "";
                foreach (var stepDef in scenario.StepDefinitions)
                {
                    var t1 = new Template(Templates.StepDefinition);
                    t1.Replace("action", stepDef.Action.ToString());
                    t1.Replace("statement", stepDef.Statement);

                    if (stepDef.Table != null && stepDef.Table.Count > 0)
                    {
                        var header = $"<tr><th>{string.Join("</th><th>", stepDef.Table[0].Cells)}</th></tr>";
                        var table = stepDef.Table.Skip(1).Select(r => $"<tr><td>{string.Join("</td><td>", r.Cells)}</td></tr>");
                        t1.Replace("table", "<table>" + header + string.Join("", table) + "</table>");
                    }
                    else
                    {
                        t1.Replace("table", "");
                    }

                    steps += t1.Get();
                }

                var outline = scenario as SquashScenarioOutline;
                var scens = "";
                if (outline != null)
                {
                    foreach (var scenar in outline.Scenarios)
                    {
                        scens += $"<div class='so-scenarios'>{scenar.Name}</div>";
                        scens += "<table>";
                        foreach (var scen in scenar.Rows)
                        {
                            scens += "<tr><td>" + string.Join("</td><td>", scen) + "</td></tr>";  //$"<div>{scen}</div>";
                        }
                        scens += "</table>";
                    }

                }

                t.Replace("scenarios", scens);

                t.Replace("stepDefinitions", steps);

                scenarios += t.Get();
            }

            template.Replace("scenarios", scenarios);

            return template.Get();
        }
    }
}