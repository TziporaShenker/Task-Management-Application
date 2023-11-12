namespace DO;

public record Task
(
    int Id,
    string Description,
    string Alias,
    bool Milestone,
    DateTime CreatedAt,
    DateTime? Start,  
    DateTime? ScheduledDate,
    DateTime? ForecastDate,
    DateTime? DeadLine,
    DateTime? Complete,
    string? Deriverables,
    string? Remarks,
    int? EngineerId,
    EngineerExperience CopmlexityLevel
);

