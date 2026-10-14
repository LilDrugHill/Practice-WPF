namespace Practice_CORE.Models.Decanat;

internal class Group
{
    public string Name { get; set; }
    public ICollection<Student> Students { get; set; }
}