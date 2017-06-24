using System.Collections.Generic;
using System.Linq;

namespace Squash
{
    internal class SquashStepTableRow
    {
        private SquashStepTableRow()
        {
        }

        public SquashStepTableRow(string row)
        {
            Cells = row.Split('|').Select(r => r.Trim()).Where(r => !string.IsNullOrWhiteSpace(r)).ToList();
        }

        public List<string> Cells { get; set; }
    }
}