namespace BO;

public class Engineer
{
    public int Id { get; init; }
    public string Name { get; set; }
    public string Email { get; set; }
    public EngineerExperience Level { get; set; }
    public double Cost { get; set; }
    public Tuple<int, string>? Task { get; set; }

    public override string ToString()
    {
        return Tools.GenericToString(this);
    }
}
