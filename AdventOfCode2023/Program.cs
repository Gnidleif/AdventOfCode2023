using AdventOfCode2023;
using AdventOfCode2023.Days._2023;

var example = true;
var days = new List<DayBase>
{
  new Day1(2023, 1),
}.Select(day =>
{
  var result1 = day.Solution1(example);
  var result2 = day.Solution2(example);

  return $"Solution 1: '{result1}'\tSolution2: '{result2}'";
});

foreach (var day in days)
{
  Console.WriteLine(day);
}