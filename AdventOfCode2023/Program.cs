using AdventOfCode2023.Days;
using AdventOfCode2023.Days._2023;

var example = true;
var solutions = new List<DayBase>
{
  new Day1(2023, 1),
}.Select(async day =>
{
  var result1 = await day.Solution1(example);
  var result2 = await day.Solution2(example);

  return $"Solution 1: '{result1}'\tSolution2: '{result2}'";
});

foreach (var solution in solutions)
{
  Console.WriteLine(await solution);
}