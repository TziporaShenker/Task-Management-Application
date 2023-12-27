

namespace BO;

public class MilestoneInList
{
    int Id { get; init; }
    string Description { get; set; }
    string Alias { get; set; }
    Status? Status { get; set; }
    double? CompletionPercentage { get; set; }
}
