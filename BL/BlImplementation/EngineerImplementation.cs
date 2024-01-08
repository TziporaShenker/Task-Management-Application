using BlApi;
using DO;
using System.Collections.Generic;

namespace BlImplementation;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Engineer boEngineer)
    {
        DO.Engineer doEngineer = new DO.Engineer
                (boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience)boEngineer.Level, boEngineer.Cost);
        try
        {
            int idEngineer = _dal.Engineer.Create(doEngineer);
            return idEngineer;
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
            _dal.Engineer.Delete(id);

        }
        catch (DO.DalDeletionImpossible ex)
        {
            throw new BO.BlDeletionImpossible(ex.Message, ex);
        }
    }

    public BO.Engineer? Read(int id)
    {

        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");

        return new BO.Engineer()
        {
            Id = id,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Level = (BO.EngineerExperience)doEngineer.Level,
            Cost = doEngineer.Cost,
            Task = /*ReadTaskInEngineer(id)*/null,
        };
    }

    public BO.Engineer? Read(Func<BO.Engineer, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.Engineer?> ReadAll(Func<BO.Engineer, bool>? filter = null)
    {
        return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll((Func<DO.Engineer, bool>?)filter)
                select new BO.Engineer()
                {
                    Id = doEngineer.Id,
                    Name = doEngineer.Name,
                    Email = doEngineer.Email,
                    Level = (BO.EngineerExperience)doEngineer.Level,
                    Cost = doEngineer.Cost,
                    Task =/* ReadTaskInEngineer(doEngineer.Id)*/null,
                });
    }

    public void Update(BO.Engineer boEngineer)
    {
        if (Read(boEngineer.Id) is null)
            throw new BO.BlDoesNotExistException($"Task with ID={boEngineer.Id} does Not exist");

        DO.Engineer doEngineer = new DO.Engineer
                (boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience)boEngineer.Level, boEngineer.Cost);
        try
        {
            _dal.Engineer.Update(doEngineer);
        }
        catch (Exception ex)
        {
            throw new BO.BlDoesNotExistException(ex.Message, ex);
        }
    }
    public Tuple<int, string>? ReadTaskInEngineer(int id)
    {

        return (Tuple<int, string>?)(
            from DO.Task doTask in _dal.Task.ReadAll()
            where (doTask.Id == id&& doTask.StartDate!=null&& doTask.CompleteDate!=null&&doTask.StartDate<=DateTime.Today&& doTask.CompleteDate >= DateTime.Today)
            select new BO.TaskInEngineer()
            {
                Id= doTask.Id,
                Alias= doTask.Alias,
            });
    }
}
