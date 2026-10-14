namespace Practice_CORE.Models.Decanat;

internal class Student
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public DateTime Birthday { get; set; }
    public double Rating { get; set; }
    #region Description : string - DESCRIPTION

    /// <summary>DESCRIPTION</summary>
    private string _description;

    /// <summary>DESCRIPTION</summary>
    public string Description
    {
        get => _description;
        set => _description = value;
    }

    #endregion
}