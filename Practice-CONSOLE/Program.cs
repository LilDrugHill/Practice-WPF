using System.Globalization;

namespace Practice_CONSOLE;

static class Program
{
    private const string DataUrl =
            "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/refs/heads/master/" +
            "csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

    static async Task Main(params string[] args)
    {


        foreach(var line in await GetDates()) Console.WriteLine(line.ToShortDateString());


        // await using var fileStream = new FileStream(
        //     @"C:\Users\timur\Downloads\csv", FileMode.Create, FileAccess.Write,
        //     FileShare.None, 81920, useAsync: true
        // );


        // await httpStream.CopyToAsync(fileStream);
    }

    private static async Task<DateTime[]> GetDates()
    {
        var header = await GetDataLines().FirstAsync();
        return header
            .Split(',')
            .Skip(4)
            .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
            .ToArray();
    }

    private static async Task<Stream> GetDataStream()
    {
        using var client = new HttpClient();

        var response = await client.GetAsync(DataUrl, HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStreamAsync();
    }

    private static async IAsyncEnumerable<string> GetDataLines()
    {
        await using var dataStream = await GetDataStream();
        using var streamReader = new StreamReader(dataStream);

        string? line;
        while ((line = await streamReader.ReadLineAsync()) is not null)
        {
            if(string.IsNullOrWhiteSpace(line)) continue;
            yield return line;
        }

    }

}