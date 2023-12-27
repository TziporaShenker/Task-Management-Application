
namespace BlApi;

public interface IMilestone
{
    int Create(BO.Milestone item); //Creates new entity object in DAL
    BO.Milestone? Read(int id); //Reads entity object by its ID 
    //BO.Milestone? Read(Func<BO.Milestone, bool> filter); // stage 2

    //List<T> ReadAll(); //stage 1 only, Reads all entity objects
    IEnumerable<BO.Milestone?> ReadAll(Func<BO.Milestone, bool>? filter = null); // stage 2
    void Update(BO.Milestone item); //Updates entity object
    void Delete(int id);//Deletes an object by its Id
}
