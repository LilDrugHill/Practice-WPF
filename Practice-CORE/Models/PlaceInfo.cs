using System.Windows;

namespace Practice_CORE.Models;

internal class PlaceInfo
{
    public string Name { get; set; }
    public Point Location { get; set; }
    public IEnumerable<ConfirmedCount> Counts { get; set; }
}