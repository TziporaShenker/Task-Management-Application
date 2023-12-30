
using BlApi;
using BO;
using DO;

namespace BlImplementation;
internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public int Create(BO.Task boTask)
    {
        DO.Task doTask = new DO.Task
                       (boTask.Id, boTask.Alias, boTask.Description, boTask.CreatedAtDate, boTask.RequiredEffortTime,false, boTask.StartDate, boTask.ScheduledDate, boTask.DeadlineDate, boTask.CompleteDate, boTask.Deliverables, boTask.Remarks,null, (DO.EngineerExperience?)boTask.Copmlexity);
        try
        {
            int idTask = _dal.Task.Create(doTask);
            return idTask;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException(ex.Message, ex);
        }
    }

    public void Delete(int id)
    {
        try
        {
            _dal.Task.Delete(id);

        }
        catch (DO.DalDeletionImpossible ex)
        {
            throw new BO.BlDeletionImpossible(ex.Message, ex);
        }
    }

    public BO.Task? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");

        return new BO.Task()
        {
            Id = id,
            Alias = doTask.Alias,
            Description = doTask.Description,
            CreatedAtDate = doTask.CreatedAtDate,
            Status=BO.Status.Unscheduled,//נדרש חישוב
            Dependencies= ReadDependencies(id),//נדרש חישוב
            Milestone =null,//נדרש חישוב
            RequiredEffortTime=doTask.RequiredEffortTime,
            StartDate=doTask.StartDate,
            ScheduledDate=doTask.ScheduledDate,
            ForecastDate= doTask.StartDate+ doTask.RequiredEffortTime,
            DeadlineDate=doTask.DeadLineDate,
            CompleteDate=doTask.CompleteDate,
            Deliverables=doTask.Deliverables,
            Remarks=doTask.Remarks,
            Engineer= ReadEngineerInTask(doTask.EngineerId),
            Copmlexity= (BO.EngineerExperience?)doTask.Copmlexity
        };
    }

    public BO.Task? Read(Func<BO.Task, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.Task?> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        return (IEnumerable<BO.Task?>)(
            from DO.Task doTask in _dal.Task.ReadAll((Func<DO.Task, bool>?)filter)
            select Read(doTask.Id));
    }

    public void Update(BO.Task boTask)
    {
        if (Read(boTask.Id) is null)
            throw new BO.BlDoesNotExistException($"Task with ID={boTask.Id} does Not exist");

        DO.Task doTask = new DO.Task
                (boTask.Id, boTask.Alias, boTask.Description, boTask.CreatedAtDate, boTask.RequiredEffortTime, false, boTask.StartDate, boTask.ScheduledDate, boTask.DeadlineDate, boTask.CompleteDate, boTask.Deliverables, boTask.Remarks, null, (DO.EngineerExperience?)boTask.Copmlexity);
        try
        {
            _dal.Task.Update(doTask);
        }
        catch (Exception ex)
        {

            throw new BO.BlDoesNotExistException(ex.Message ,ex);
        }

    }
    public Tuple<int, string>? ReadEngineerInTask(int? engineerId)
    {

        return (Tuple<int, string>?)(
            from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
            where (doEngineer.Id == engineerId)
            select new BO.EngineerInTask()
            {
                Id = doEngineer.Id,
                Name = doEngineer.Name,
            });
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
                Alias= dependentTask.Alias,
                Status=null//נדרש חישוב
            }).ToList() ;
    }
}
