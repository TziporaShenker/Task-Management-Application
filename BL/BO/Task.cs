

using System;

namespace BO;

public class Task
{
    public int Id { get; init; }
    public string Alias { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAtDate { get; set; }
    public Status? Status { get; set; }
    List<TaskInList>? Dependencies { get; set; }
    public MilestoneInTask? Milestone { get; set; }
    public TimeSpan? RequiredEffortTime { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? ScheduledDate { get; set; }
    public DateTime? ForecastDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public DateTime? CompleteDate { get; set; }
    public string? Deliverables { get; set; }
    public string? Remarks { get; set; }
    public EngineerInTask? Engineer { get; set; }
    public EngineerExperience? Copmlexity { get; set; }
}
