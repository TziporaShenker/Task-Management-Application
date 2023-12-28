using BlApi;
using DO;

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
            throw new BO.BlAlreadyExistsException($"engineer with ID={boEngineer.Id} already exists", ex);
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
            throw new BO.BlDeletionImpossible($"engineer with ID={id} already exists", ex);
        }
    }

    public BO.Engineer? Read(int id)
    {

        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Student with ID={id} does Not exist");

        return new BO.Engineer()
        {
            Id = id,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Level = (BO.EngineerExperience)doEngineer.Level,
            Cost = doEngineer.Cost,
        };
    }

    public BO.Engineer? Read(Func<BO.Engineer, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.Engineer?> ReadAll(Func<BO.Engineer, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Engineer item)
    {
        throw new NotImplementedException();
    }
}
