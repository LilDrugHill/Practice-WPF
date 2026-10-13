using System.Globalization;

namespace Practice_CONSOLE;

static class Program
{
    private const string DataUrl =
            "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/refs/heads/master/" +
            "csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

    static async Task Main(params string[] args)
    {


        // foreach(var line in await GetDates()) Console.WriteLine(line.ToShortDateString());

        // var russia = await GetData().FirstAsync(r => r.Country.Equals("Russia", StringComparison.OrdinalIgnoreCase));

        await foreach (var data in GetData())
        {
            Console.WriteLine($"{data.Country}, {data.Counts.Aggregate(0, (start, next) =>  start + next)}");

        }
        // await using var fileStream = new FileStream(
        //     @"C:\Users\timur\Downloads\csv", FileMode.Create, FileAccess.Write,
        //     FileShare.None, 81920, useAsync: true
        // );


        // await httpStream.CopyToAsync(fileStream);
    }

    private static async IAsyncEnumerable<(string Country, string Province, int[] Counts)> GetData()
    {
        await foreach (var line in GetDataLines().Skip(1))
        {
            int[]? counts = null;
            var row = line.Split(',');
            try
            {
                counts = row.Skip(4).Select(int.Parse).ToArray();
            }
            catch (FormatException e)
            {
                Console.WriteLine($"Bad CSV line: {row[1].Trim(' ', '"')}");
                continue;
            }
            yield return (row[1].Trim(' ', '"'), row[0].Trim(), counts);

        }
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
            yield return line.Replace("Korea,", "Korea -");
        }

    }

}