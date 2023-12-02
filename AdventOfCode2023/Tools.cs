using System.Net;

namespace AdventOfCode2023
{
  internal static class Tools
  {
    private static Uri AoCUrl { get; } = new Uri("https://adventofcode.com/");
    // Will make sure the files end up in the main project directory
    private static string ProjectDirectory => Directory.GetParent(path: Environment.CurrentDirectory)?.Parent?.Parent?.FullName ?? throw new FileNotFoundException();

    /// <summary>
    /// Attempts to read the input from file given a year and a day, example is set when running test runs using the provided example for each part
    /// </summary>
    /// <param name="year">2023 for this year</param>
    /// <param name="day">2 for day 2</param>
    /// <param name="example">1 for example of part 1, 2 for example of part 2</param>
    /// <returns>All provided input as a single string</returns>
    /// <exception cref="FileNotFoundException"></exception>
    public static async Task<string> ReadInput(int year, int day, int? example)
    {
      var yearAsString = year.ToString();
      // The examples has to be added manually with the name following the following format: {day}_{example}.txt
      // So for Day 1 part 2 the file would be called 1_2.txt
      var filePath = Path.Combine(ProjectDirectory, "Inputs", yearAsString, string.Format("{0}{1}.txt", day, example.HasValue ? $"_{example}" : string.Empty));

      // If the file doesn't exist we need to request and create it from the server
      if (!File.Exists(filePath))
      {
        // If the file doesn't exist and example is set we throw an exception to remind you to create it
        if (example.HasValue)
        {
          throw new FileNotFoundException(filePath);
        }
        var dirPath = Path.GetDirectoryName(filePath) ?? throw new DirectoryNotFoundException(Path.GetDirectoryName(filePath));
        if (!Directory.Exists(dirPath))
        {
          Directory.CreateDirectory(dirPath);
        }
        await RequestInput(year, day, filePath);
      }

      return await File.ReadAllTextAsync(filePath);
    }

    /// <summary>
    /// Makes a request using your session cookie to get the given input and store it in a file for future runs
    /// </summary>
    /// <param name="year">2023 for this year</param>
    /// <param name="day">2 for day 2</param>
    /// <param name="path">Path of the file we want to create</param>
    private static async Task RequestInput(int year, int day, string path)
    {
      // The session_cookie.txt file has to be placed in your project directory
      // Make sure to add the file to .gitignore so you won't share it with the world
      var session = await File.ReadAllTextAsync(Path.Combine(ProjectDirectory, "session_cookie.txt"));
      var cookies = new CookieContainer();
      cookies.Add(AoCUrl, new Cookie("session", session));

      using var file = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
      using var handler = new HttpClientHandler { CookieContainer = cookies };
      using var client = new HttpClient(handler) { BaseAddress = AoCUrl };
      using var response = await client.GetAsync($"{year}/day/{day}/input");
      using var stream = await response.Content.ReadAsStreamAsync();
      await stream.CopyToAsync(file);
    }
  }
}
