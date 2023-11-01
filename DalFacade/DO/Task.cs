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
    DateTime? DeadLine,
    DateTime? Complete,
    string? ProductDescription,
    string? Remarks,
    int? EngineerId,
    EngineerExperience? CopmlexityLevel
);

