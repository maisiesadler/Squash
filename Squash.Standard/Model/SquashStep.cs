using System;
using System.Collections.Generic;
using System.Linq;

namespace Squash
{
    internal class SquashStep
    {
        public SquashStepDefinitionAction Action { get; set; }
        public string Statement { get; set; }
        public List<SquashStepTableRow> Table { get; set; }

        private SquashStep()
        {
        }

        public SquashStep(string row)
        {
            Table = new List<SquashStepTableRow>();

            var parts = row.Trim().Split(' ');
            Action = (SquashStepDefinitionAction)Enum.Parse(typeof(SquashStepDefinitionAction), parts[0]);
            Statement = string.Join(" ", parts.Skip(1));
        }
    }
}