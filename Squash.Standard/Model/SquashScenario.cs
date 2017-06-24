using System;
using System.Collections.Generic;
using System.Linq;

namespace Squash
{
    internal class SquashScenario
    {
        public string Name { get; set; }
        public List<SquashStep> StepDefinitions { get; set; }
        public List<string> Tags { get; set; }

        public SquashScenario()
        {
            StepDefinitions = new List<SquashStep>();
            Tags = new List<string>();
        }
    }
}