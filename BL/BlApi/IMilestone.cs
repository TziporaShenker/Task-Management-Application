
namespace BlApi;

public interface IMilestone
{
    
    BO.Milestone? Read(int id); //Reads entity object by its ID 
    void Update(BO.Milestone item); //Updates entity object
}
