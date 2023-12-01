namespace AdventOfCode2023
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

    public abstract int Solution1(bool example = false);
    public abstract int Solution2(bool example = false);
  }
}
