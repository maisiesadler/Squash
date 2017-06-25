using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Squash
{
    internal class SquashFeature
    {
        private Cabinet _cabinet;

        public SquashFeature(string location)
        {
            Scenarios = new List<SquashScenario>();
            _cabinet = new Cabinet(location);

            Create();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public List<SquashScenario> Scenarios { get; set; }

        private void Create()
        {
            SetFeatureTitle();
            SetFeatureDescription();

            var scenario = new SquashScenario();
            while (_cabinet.PeekNextLine() != null)
            {
                var row = _cabinet.PeekNextLine();
                if (string.IsNullOrWhiteSpace(row))
                    continue;

                //tags
                if (row.StartsWith("@"))
                {
                    var newscenario = CreateScenarioWithTags();
                    SquashLogger.Debug($"Feature {Name} has scenario {newscenario.Name} with tags {string.Join(",",newscenario.Tags)}");
                    Scenarios.Add(newscenario);
                    continue;
                }

                //scenario
                if (row.StartsWith("Scenario:"))
                {
                    var newscenario = CreateScenario();
					SquashLogger.Debug($"Feature {Name} has scenario {newscenario.Name}");
					Scenarios.Add(newscenario);
                    continue;
                }

                //scenario
                if (row.StartsWith("Scenario Outline:"))
                {
                    var newscenario = CreateScenarioOutline();
                    SquashLogger.Debug($"Feature {Name} has scenario outline {newscenario.Name} with scenarios {string.Join(",", newscenario.Scenarios.Select(s => s.Name))}");
					Scenarios.Add(newscenario);
                    continue;
                }
            }
        }

        private void SetFeatureTitle()
        {
            Name = _cabinet.GetCurrentLine().Split(':')[1].Trim();
        }

        private void SetFeatureDescription()
        {
            while (_cabinet.PeekNextLine() != null && !IsStartOfSection(_cabinet.PeekNextLine()))
            {
                //start of the file has title and description
                var row = _cabinet.GetNextLine();
                if (string.IsNullOrWhiteSpace(row))
                    continue;

                Description += row + "<br/>";
            }
        }

        private SquashScenario CreateScenarioWithTags()
        {
            var row = _cabinet.GetNextLine();
            var tags = row.Split('@').Where(t => !string.IsNullOrWhiteSpace(t));

            //_cabinet.GetNextLine();

            SquashScenario scenario;
            if (_cabinet.PeekNextLine().StartsWith("Scenario:"))
            {
                scenario = CreateScenario();
            }
            else
            {
                scenario = CreateScenarioOutline();
            }

            foreach (var tag in tags)
            {
                scenario.Tags.Add(tag.Trim());
            }

            return scenario;
        }

        private SquashScenario CreateScenario()
        {
            var scenario = new SquashScenario();
            scenario.Name = _cabinet.GetNextLine().Split(':')[1];

            while (_cabinet.PeekNextLine() != null && !IsStartOfSection(_cabinet.PeekNextLine()))
            {
                var step = CreateStep();

                //if (step == null)
                //{
                //    currentLine--;
                //    break;
                //}

                scenario.StepDefinitions.Add(step);
            }

            return scenario;
        }

        private SquashScenarioOutline CreateScenarioOutline()
        {
            var scenario = new SquashScenarioOutline();
            scenario.Name = _cabinet.GetNextLine().Split(':')[1];

            while (_cabinet.PeekNextLine() != null && !IsStartOfSection(_cabinet.PeekNextLine()))
            {
                if (_cabinet.PeekNextLine().StartsWith("Scenarios:"))
                {
                    scenario.Scenarios.Add(GetScenarios());
                }
                else
                {
                    var step = CreateStep();
                    scenario.StepDefinitions.Add(step);
                }
            }

            return scenario;
        }

        private SquashScenarios GetScenarios()
        {
            var scenarios = new SquashScenarios(_cabinet.GetNextLine().Split(':')[1]);

            while (_cabinet.PeekNextLine() != null && _cabinet.PeekNextLine().StartsWith("|"))
            {
                scenarios.AddRow(_cabinet.GetNextLine());
            }

            return scenarios;
        }

        private SquashStep CreateStep()
        {
            var step = new SquashStep(_cabinet.GetNextLine());

            while (_cabinet.PeekNextLine() != null && _cabinet.PeekNextLine().StartsWith("|"))
            {
                step.Table.Add(new SquashStepTableRow(_cabinet.GetNextLine()));
            }

            return step;
        }

        private static bool IsStartOfSection(string row)
        {
            return row.StartsWith("@") || row.StartsWith("Scenario:") || row.StartsWith("Scenario Outline:");
        }
    }
}