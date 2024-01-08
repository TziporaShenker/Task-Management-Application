
using BlApi;
using BO;
using DalApi;
using DO;
using System.Threading.Tasks;

namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public void Create()
    {
        var groupedDependencies = _dal.Dependency.ReadAll()
        .OrderBy(dep => dep?.DependsOnTask)
        .GroupBy(dep => dep.DependentTask, dep => dep.DependsOnTask,
        (id, dependency) => new { TaskId = id, Dependencies = dependency })
        .ToList();

        var distinctDependencies = groupedDependencies
       //.Select(depGroup => new { TaskId = depGroup.TaskId, Dependencies = depGroup.Dependencies })
       .GroupBy(dep => dep.Dependencies,
        (key, group) => group.First())
       .ToList();

        int id = 1;
        var mileStones =
        from distinctDependency in distinctDependencies
        select new BO.Task()
        {
            Id = id++,
            Alias = "M" + id,
            Description = null,
            CreatedAtDate = DateTime.Today,
            Status = Status.Scheduled,//נדרש חישוב
            Dependencies = (from dep in distinctDependency.Dependencies
                            let task = _dal.Task.Read(dep)
                            select new BO.TaskInList()
                            {
                                Id = task.Id,
                                Description = task.Description,
                                Alias = task.Alias,
                                Status = null,
                            }).ToList(),

            StartDate = null,//נדרש חישוב
            ForecastDate = null,//נדרש חישוב
            CompleteDate = null,//נדרש חישוב,
            DeadlineDate = null,//נדרש חישוב
            Remarks = null,//נדרש חישוב
            Milestone = null,//נדרש חישוב
            RequiredEffortTime = null,//נדרש חישוב
            Deliverables = null,//נדרש חישוב
            Engineer = null,
            Copmlexity = null,
        };

        var dalMileStones =
       from mileStone in mileStones
       select new DO.Task()
       {
           Id = id++,
           Alias = mileStone.Alias,
           Description = mileStone.Description,
           CreatedAtDate = mileStone.CreatedAtDate,
           RequiredEffortTime = mileStone.RequiredEffortTime,
           IsMilestone = true,
           StartDate = mileStone.StartDate,//נדרש חישוב
           ScheduledDate = mileStone.ScheduledDate,//נדרש חישוב
           DeadlineDate = mileStone.DeadlineDate,//נדרש חישוב
           CompleteDate = mileStone.CompleteDate,//נדרש חישוב,
           Deliverables = mileStone.Deliverables,//נדרש חישוב
           Remarks = mileStone.Remarks,//נדרש חישוב
           EngineerId = null,
           Copmlexity = null,
       };

        dalMileStones.ToList().ForEach(dalMileStone => _dal.Task.Create(dalMileStone));
        var updatedDependencies = from mileStone in mileStones
                                  from dependency in mileStone.Dependencies
                                  from dalDependency in _dal.Dependency.ReadAll(dep => dep.DependsOnTask == dependency.Id)
                                  let dalMilestone = _dal.Task.Read(t => t.Alias == mileStone.Alias)
                                  select new Dependency(dalDependency.Id, dalDependency.DependentTask, dalMilestone.Id);

        updatedDependencies.ToList().ForEach(updatedDependency => _dal.Dependency.Update(updatedDependency));
  
        var dalStartMilestone = new DO.Task()
        {
            Id = id,
            Alias = "START",
            Description = null,
            CreatedAtDate = DateTime.Today,
            StartDate = null,//נדרש חישוב
            ScheduledDate = null,//נדרש חישוב
            CompleteDate = null,//נדרש חישוב,
            DeadlineDate = null,//נדרש חישוב
            Remarks = null,//נדרש חישוב
            IsMilestone = true,//נדרש חישוב
            RequiredEffortTime = null,//נדרש חישוב
            Deliverables = null,//נדרש חישוב
            EngineerId = null,
            Copmlexity = null,
        };
        _dal.Task.Create(dalStartMilestone);

        var dalBeginMilestone = _dal.Task.Read(t => t.Alias == dalStartMilestone.Alias);

        var startDependencies = from task in _dal.Task.ReadAll()
                              let dependency = _dal.Dependency.ReadAll(dep => dep.DependentTask == task.Id)
                              where dependency == null
                              select _dal.Dependency.Create(new Dependency(0, task.Id, dalBeginMilestone.Id));
     
        var dalEndMilestone = new DO.Task()
        {
            Id = id,
            Alias = "END",
            Description = null,
            CreatedAtDate = DateTime.Today,
            StartDate = null,//נדרש חישוב
            ScheduledDate = null,//נדרש חישוב
            CompleteDate = null,//נדרש חישוב,
            DeadlineDate = null,//נדרש חישוב
            Remarks = null,//נדרש חישוב
            IsMilestone = true,//נדרש חישוב
            RequiredEffortTime = null,//נדרש חישוב
            Deliverables = null,//נדרש חישוב
            EngineerId = null,
            Copmlexity = null,
        };

        _dal.Task.Create(dalEndMilestone);
        var endDependencies = (from task in _dal.Task.ReadAll()
                            let dependency = _dal.Dependency.ReadAll((dep => dep.DependsOnTask == task.Id))
                            where dependency == null
                            select new BO.TaskInList()
                            {
                                Id = task.Id,
                                Description = task.Description,
                                Alias = task.Alias,
                                Status = null,
                            }).ToList();

        var dalFinishMilestone = _dal.Task.Read(t => t.Alias == dalEndMilestone.Alias);
        foreach (var dependency in endDependencies)
        {
            _dal.Dependency.Create(new Dependency(0, dependency.Id, dalFinishMilestone.Id));
        }
    }

    public Milestone? Read(int id)
    {
        var milestone = _dal.Task.Read(id);
        var dependencies = _dal.Dependency.ReadAll(dep => dep.DependsOnTask == id);
        var dependenciesList = (from dependency in dependencies
                                let task = _dal.Task.Read(dependency.DependentTask)
                                select new BO.TaskInList()
                                {
                                    Id = task.Id,
                                    Description = task.Description,
                                    Alias = task.Alias,
                                    Status = null,
                                }).ToList();

        return new BO.Milestone()
        {
            Id = id,
            Description = milestone.Description,
            Alias = milestone.Alias,
            CreatedAtDate = milestone.CreatedAtDate,
            Status = Status.Scheduled,//נדרש חישוב
            StartDate = null,//נדרש חישוב
            ForecastDate = null,//נדרש חישוב          
            DeadlineDate = null,//נדרש חישוב
            CompleteDate = null,//נדרש חישוב,
            CompletionPercentage = dependenciesList.Count(dep => dep.Status == Status.Done) / dependenciesList.Count(),
            Remarks = null,//נדרש חישוב
            Dependencies = dependenciesList,
        };
    }
    public void Update(Milestone item)
    {
        if (Read(item.Id) is null)
            throw new BO.BlDoesNotExistException($"Milestone with ID={item.Id} does Not exist");

        DO.Task doMilestone = new DO.Task
                (item.Id, item.Alias, item.Description, item.CreatedAtDate,null, true, item.StartDate, null, item.DeadlineDate, item.CompleteDate, null, item.Remarks, null, null);
        try
        {
            _dal.Task.Update(doMilestone);
        }
        catch (Exception ex)
        {

            throw new BO.BlDoesNotExistException(ex.Message, ex);
        }
    }

}
