

using System;

namespace BO;

public class Task
{
    int Id { get; init; }
    string Description { get; set; }
    string Alias { get; set; }
    DateTime CreatedAtDate { get; set; }
    Status? Status { get; set; }
    TaskInList? Dependencies { get; set; }
    MilestoneInTask? Milestone { get; set; }
    TimeSpan? RequiredEffortTime { get; set; }
    DateTime? StartDate { get; set; }
    DateTime? ScheduledDate { get; set; }
    DateTime? ForecastDate { get; set; }
    DateTime? DeadlineDate { get; set; }
    DateTime? CompleteDate { get; set; }
    string? Deliverables { get; set; }
    string? Remarks { get; set; }
    EngineerInTask? Engineer { get; set; }
    EngineerExperience? Copmlexity { get; set; }
}
