namespace Practice_CONSOLE;

static class Program
{
    static async Task Main(params string[] args)
    {
        const string dataUrl =
            "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/refs/heads/master/" +
            "csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        using var client = new HttpClient();
        using var response = await client.GetAsync(dataUrl, HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();

        await using var httpStream = await response.Content.ReadAsStreamAsync();
        await using var fileStream = new FileStream("csv", FileMode.Create, FileAccess.Write, FileShare.None, 81920, useAsync: true);


        await httpStream.CopyToAsync(fileStream);
        Console.WriteLine(csvStr);
    }
}