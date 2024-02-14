using BlApi;
using BO;
using DO;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        return doEngineer == null
            ? throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist")
            : new BO.Engineer()
            {
                Id = id,
                Name = doEngineer.Name,
                Email = doEngineer.Email,
                Level = (BO.EngineerExperience)doEngineer.Level,
                Cost = doEngineer.Cost,
                Task = ReadTaskInEngineer(doEngineer.Id),
            };
    }

    public BO.Engineer? Read(Func<BO.Engineer, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.Engineer?> ReadAll(Func<BO.Engineer, bool>? filter = null)
    {
        Func<BO.Engineer, bool> filter1 = filter != null ? filter! : item => true;
        return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                select new BO.Engineer()
                {
                    Id = doEngineer.Id,
                    Name = doEngineer.Name,
                    Email = doEngineer.Email,
                    Level = (BO.EngineerExperience)doEngineer.Level,
                    Cost = doEngineer.Cost,
                    Task = ReadTaskInEngineer(doEngineer.Id),
                }).Where(filter1);
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
    public TaskInEngineer? ReadTaskInEngineer(int id)
    {
        if (id == null)
            return null;

        var doTask = _dal.Task.ReadAll()
            .FirstOrDefault(doTask => doTask.EngineerId == id);

        if (doTask == null)
            return null;

        return new TaskInEngineer
        {
            Id = doTask.Id,
            Alias = doTask.Alias
        };
    }
}
