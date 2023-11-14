﻿namespace DO;

/// <summary>
/// Task Entity represents a task with all its props
/// </summary>
/// <param name="Id">unique ID (created automatically)</param>
/// <param name="Description">Description (optional) - text detailing the task</param>
/// <param name="Alias">Nickname - a short name to characterize the task (doesn't have to be unique, but preferably)</param>
/// <param name="Milestone">Milestone</param>
/// <param name="CreatedAt">indicates the time when the task was created by the administrator</param>
/// <param name="Start">Planned date for the start of work</param>
/// <param name="ScheduledDate">Date of commencement of work on the task</param>
/// <param name="ForecastDate">The amount of time required to perform the task</param>
/// <param name="DeadLine">Last possible end date (deadline)</param>
/// <param name="Complete">Actual end date</param>
/// <param name="Deriverables">a string describing the results or items provided at the end of the task.</param>
/// <param name="Remarks">Remarks</param>
/// <param name="EngineerId">The engineer ID assigned to the task</param>
/// <param name="CopmlexityLevel">defines the minimum engineer level that can work on it.</param>
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

