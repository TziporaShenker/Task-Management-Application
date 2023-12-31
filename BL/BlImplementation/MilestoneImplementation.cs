
using BlApi;
using BO;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public List<BO.TaskInList>? Create(int taskId)
    {
        var groupedDependencies =
        from dependency in _dal.Dependency.ReadAll()
        group dependency by dependency.DependentTask into newGroup
        orderby newGroup.Key
        select newGroup;

        var newGroupedDependencies =groupedDependencies.Distinct();

        int id = 0;
        var mileStone =
        from dependency in newGroupedDependencies
        let task = _dal.Task.Read(dependency.Key)
        select new BO.Milestone()
        {
            Id = id++,
            Alias = "M" + id,
            Description = task.Description,
            CreatedAtDate = task.CreatedAtDate,
            Status = Status.Scheduled,//נדרש חישוב
            StartDate = task.StartDate,
            ForecastDate = null,//נדרש חישוב
            CompleteDate = task.CompleteDate,
            DeadlineDate = task.DeadlineDate,
            CompletionPercentage = 0,
            Remarks = task.Remarks,
            Dependencies = ReadDependencies(id)

        };


        //return (
        //        from DO.Dependency doDependency in _dal.Dependency.ReadAll()
        //        where (doDependency.DependentTask == taskId)
        //        let dependentTask = Read(doDependency.DependsOnTask)
        //        select new BO.TaskInList()
        //        {
        //            Id = doDependency.DependsOnTask,
        //            Description = dependentTask.Description,
        //            Alias = dependentTask.Alias,
        //            Status = null,
        //        }).ToList();
    }

    public Milestone? Read(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(Milestone item)
    {
        throw new NotImplementedException();
    }
    public List<TaskInList>? ReadDependencies(int taskId)
    {
        return (
            from DO.Dependency doDependency in _dal.Dependency.ReadAll()
            where (doDependency.DependentTask == taskId)
            let dependentTask = Read(doDependency.DependsOnTask)
            select new BO.TaskInList()
            {
                Id = doDependency.DependsOnTask,
                Description = dependentTask.Description,
                Alias = dependentTask.Alias,
                Status = ReadStatus(doDependency.DependsOnTask),
            }).ToList();
    }

    public BO.Status ReadStatus(int taskId)
    {
        DO.Task? doTask = _dal.Task.Read(taskId) ;

        if (doTask.CompleteDate != null && doTask.CompleteDate <= DateTime.Today)
        {
            return Status.Done;
        }
        else if (doTask.DeadlineDate != null && (DateTime)doTask.DeadlineDate >= DateTime.Today.AddDays(-7))
        {
            return Status.InJeopardy;
        }
        else if (doTask.StartDate != null && doTask.StartDate <= DateTime.Today)
        {
            return Status.OnTrack;
        }
        else if (doTask.ScheduledDate != null)
        {
            return Status.Scheduled;
        }
        else { return Status.Unscheduled; }
    }

}
