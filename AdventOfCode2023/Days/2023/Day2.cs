using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days._2023
{
  internal class Day2 : DayBase
  {
    public Day2(int year, int day) : base(year, day)
    {
    }

    public override async Task<int> Solution1(bool example)
    {
      var input = await GetInput(example ? 1 : null);

      var idPattern = new Regex(@"^Game\s(\d+)", RegexOptions.Compiled);
      var colorPattern = new Regex(@"(\d+)\s(blue|red|green)", RegexOptions.Compiled);
      var limits = new Dictionary<string, int>
      {
        { "red", 12 },
        { "green", 13 },
        { "blue", 14 },
      };

      return input.Select(line =>
      {
        var id = int.Parse(idPattern.Match(line)?.Groups[1]?.Value ?? throw new InvalidOperationException());
        var breaksLimits = line.Split(";")
          .Select(set => colorPattern.Matches(set))
          .Select(collection =>
          {
            foreach (Match match in collection)
            {
              var key = match.Groups[2].Value;
              var value = int.Parse(match.Groups[1].Value);

              if (limits.TryGetValue(key, out int compare) && value > compare)
              {
                return false;
              }
            }

            return true;
          })
          .Any(comparison => !comparison);

        return breaksLimits ? 0 : id;
      }).Sum();
    }

    public override async Task<int> Solution2(bool example)
    {
      var input = await GetInput(example ? 1 : null);

      await Task.CompletedTask;
      return 0;
    }
  }
}
