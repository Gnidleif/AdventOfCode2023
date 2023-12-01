using System.Net;

namespace AdventOfCode2023
{
  internal static class Tools
  {
    private static Uri AoCUrl { get; } = new Uri("https://adventofcode.com/");
    private static string ProjectDirectory => Directory.GetParent(path: Environment.CurrentDirectory)?.Parent?.Parent?.FullName ?? throw new FileNotFoundException();

    public static async Task<string> ReadInput(int year, int day)
    {
      var yearAsString = year.ToString();
      var filePath = Path.Combine(ProjectDirectory, "Inputs", yearAsString, $"{day}.txt");

      if (!File.Exists(filePath))
      {
        var dirPath = Path.GetDirectoryName(filePath) ?? throw new FileNotFoundException();
        if (!Directory.Exists(dirPath))
        {
          Directory.CreateDirectory(dirPath);
        }
        _ = await RequestInput(year, day, filePath);
      }

      return await File.ReadAllTextAsync(filePath);
    }

    private static async Task<bool> RequestInput(int year, int day, string path)
    {
      var session = await File.ReadAllTextAsync(Path.Combine(ProjectDirectory, "session_cookie.txt"));
      var cookies = new CookieContainer();
      cookies.Add(AoCUrl, new Cookie("session", session));

      using var file = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
      using var handler = new HttpClientHandler { CookieContainer = cookies };
      using var client = new HttpClient(handler) { BaseAddress = AoCUrl };
      using var response = await client.GetAsync($"{year}/day/{day}/input");
      using var stream = await response.Content.ReadAsStreamAsync();
      await stream.CopyToAsync(file);

      return true;
    }
  }
}
