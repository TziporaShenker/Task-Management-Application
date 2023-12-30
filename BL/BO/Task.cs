﻿

using System;

namespace BO;

public class Task
{
    public int Id { get; init; }
    public string Alias { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAtDate { get; set; }
    public Status? Status { get; set; }
    public List<TaskInList>? Dependencies { get; set; }
    public MilestoneInTask? Milestone { get; set; }
    public TimeSpan? RequiredEffortTime { get; set; }
    public DateTime? StartDate { get; set; }//התחלה בפועל
    public DateTime? ScheduledDate { get; set; }//התחלה משוער
    public DateTime? ForecastDate { get; set; }//סיום משוער
    public DateTime? DeadlineDate { get; set; }//יום אחרון לסיום
    public DateTime? CompleteDate { get; set; }//סיום בפועל
    public string? Deliverables { get; set; } 
    public string? Remarks { get; set; } 
    public Tuple<int, string>? Engineer { get; set; }
    public EngineerExperience? Copmlexity { get; set; }
}
