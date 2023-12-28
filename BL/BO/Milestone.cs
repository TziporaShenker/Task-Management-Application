
using System;

namespace BO;

public class Milestone
{
    int Id { get; init; }
    string Description { get; set; }
    string Alias { get; set; }
    DateTime CreatedAtDate { get; set; }
    Status?  Status { get; set; }
    DateTime? ForecastDate { get; set; }
    DateTime? DeadlineDate { get; set; }
    DateTime? CompleteDate { get; set; }
    double? CompletionPercentage { get; set; }
    string? Remarks { get; set; }
    List<TaskInList>? Dependencies { get; set; }
}
