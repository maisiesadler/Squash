using System.Collections.Generic;
using System.Linq;

namespace Squash
{
    public class SquashScenarios
    {
        public SquashScenarios(string name)
        {
            Name = name;

            Rows = new List<List<string>>();
        }

        public void AddRow(string row)
        {
            var newRow = row.Split('|').Select(c => c.Trim()).Where(c => !string.IsNullOrWhiteSpace(c)).ToList();
            Rows.Add(newRow);
        }

        public string Name { get; set; }
        public List<List<string>> Rows { get; set; }
    }
}