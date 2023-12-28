
using BlApi;


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
            throw new BO.BlAlreadyExistsException($"task with ID={boTask.Id} already exists", ex);
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
            throw new BO.BlDeletionImpossible($"task with ID={id} already exists", ex);
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
            Status=BO.Status.Unscheduled//נדרש חישוב
            
        };
    }

    public BO.Task? Read(Func<BO.Task, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.Task?> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Task item)
    {
        throw new NotImplementedException();
    }
}
