namespace Practice_CORE.Models;

internal class CountryInfo: PlaceInfo
{
    public IEnumerable<ProvinceInfo> ProvinceCounts { get; set; }
}