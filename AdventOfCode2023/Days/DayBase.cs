namespace AdventOfCode2023.Days
{
  public abstract class DayBase
  {
    protected int Year { get; set; }
    protected int Day { get; set; }

    public DayBase(int year, int day)
    {
      Year = year;
      Day = day;
    }
    public abstract Task<int> Solution1(bool example);
    public abstract Task<int> Solution2(bool example);
    protected async Task<List<string>> GetInput(int? example)
      => (await Tools.ReadInput(Year, Day, example)).Split(new string[] { "\r", "\n" }, StringSplitOptions.TrimEntries).ToList();
  }
}
