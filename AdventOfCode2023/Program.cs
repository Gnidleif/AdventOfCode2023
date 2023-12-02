using AdventOfCode2023.Days;
using AdventOfCode2023.Days._2023;
using System.Diagnostics;
using System.Text;

var example = false;
var solutions = new List<DayBase>
{
  new Day1(2023, 1),
  new Day2(2023, 2),

}.Select(async day =>
{
  var sb = new StringBuilder();

  var stopWatch = new Stopwatch();
  stopWatch.Start();
  var result = await day.Solution1(example);
  stopWatch.Stop();
  sb.Append($"Solution 1: '{result}' ({stopWatch.ElapsedMilliseconds}ms)\t");

  stopWatch.Reset();
  stopWatch.Start();
  result = await day.Solution2(example);
  stopWatch.Stop();
  sb.Append($"Solution 2: '{result}' ({stopWatch.ElapsedMilliseconds}ms)");

  return sb.ToString();
});

foreach (var solution in solutions)
{
  Console.WriteLine(await solution);
}