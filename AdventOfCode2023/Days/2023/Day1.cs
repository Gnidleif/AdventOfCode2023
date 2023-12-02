using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Days._2023
{
  internal class Day1 : DayBase
  {
    public Day1(int year, int day) : base(year, day)
    {
    }

    public override async Task<int> Solution1(bool example)
    {
      var input = await GetInput(example ? 1 : null);

      var rgxString = @"\d";
      return input.Select(line =>
      {
        var firstNumber = Regex.Match(line, rgxString)?.Value ?? string.Empty;
        var lastNumber = Regex.Match(line, rgxString, RegexOptions.RightToLeft)?.Value ?? string.Empty;
        var combined = $"{firstNumber}{lastNumber}";

        return !string.IsNullOrEmpty(combined) ? int.Parse(combined) : 0;
      }).Sum();
    }

    public override async Task<int> Solution2(bool example)
    {
      var input = await GetInput(example ? 2 : null);

      var numberDict = new Dictionary<string, string>
      {
        { "zero", "0" },
        { "one", "1" },
        { "two", "2" },
        { "three", "3" },
        { "four", "4" },
        { "five", "5" },
        { "six", "6" },
        { "seven", "7" },
        { "eight", "8" },
        { "nine", "9" },
      };

      var sb = new StringBuilder(@"\d");
      foreach (var key in numberDict.Keys)
      {
        sb.Append($"|{key}");
      }
      var rgxString = sb.ToString();

      return input.Select(line =>
      {
        var firstNumberStr = Regex.Match(line, rgxString)?.Value ?? string.Empty;
        if (!string.IsNullOrEmpty(firstNumberStr) && !int.TryParse(firstNumberStr, out int _))
        {
          firstNumberStr = numberDict.GetValueOrDefault(firstNumberStr);
        }

        var lastNumberStr = Regex.Match(line, rgxString, RegexOptions.RightToLeft)?.Value ?? string.Empty;
        if (!string.IsNullOrEmpty(lastNumberStr) && !int.TryParse(lastNumberStr, out int _))
        {
          lastNumberStr = numberDict.GetValueOrDefault(lastNumberStr);
        }

        var combined = $"{firstNumberStr}{lastNumberStr}";

        return !string.IsNullOrEmpty(combined) ? int.Parse(combined) : 0;
      }).Sum();
    }
  }
}
