using System.Collections.Generic;

namespace Squash
{
    public class SquashScenarioOutline : SquashScenario
    {
        public SquashScenarioOutline() : base()
        {
            Scenarios = new List<SquashScenarios>();
        }

        public List<SquashScenarios> Scenarios { get; set; }
    }
}