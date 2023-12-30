
using BO;

namespace BlApi;

public interface IMilestone
{
    List<TaskInList>? Create(List<TaskInList>? item); //Creates new entity object in DAL
    BO.Milestone? Read(int id); //Reads entity object by its ID 
    void Update(BO.Milestone item); //Updates entity object
}
