namespace BO;

public class Engineer
{
    int Id { get; init; }
    string Name { get; set; }
    string Email { get; set; }
    EngineerExperience Level { get; set; }
    double Cost { get; set; }
    TaskInEngineer Task { get; set; }
}
