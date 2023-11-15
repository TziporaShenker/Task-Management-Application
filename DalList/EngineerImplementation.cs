namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Threading.Tasks;

public class EngineerImplementation : IEngineer
{
    /// <summary>
    /// Adding a new object of type Engineer to a database, (to the list of objects of type Engineer).
    /// </summary>
    public int Create(Engineer item)
    {
        if (Read(item.Id) is not null)
            throw new Exception($"Engineer with ID={item.Id} already exists");
        DataSource.Engineers.Add(item);
        return item.Id;

    }

    /// <summary>
    /// Deletion of an existing object with a certain ID, from the list of objects of type Engineer.
    /// </summary>
    public void Delete(int id)
    {
        if (Read(id) is not null)
        {
            for (int i = 0; i < DataSource.Tasks.Count; i++) 
            { 
                if (DataSource.Tasks[i].EngineerId == id)
                {
                    throw new Exception($"A task is depends on engineer with ID={id}");
                }
            }
            DataSource.Engineers.RemoveAll(item => item.Id == id);
        }
        else
        {
            throw new Exception($"Engineer with ID={id} does Not exist");
        }
    }

    /// <summary>
    /// Returning a reference to a single object of type Engineer with a certain ID, if it exists in a database (in a list of data of type Engineer), or null if the object does not exist.
    /// </summary>
    public Engineer? Read(int id)
    {
        return DataSource.Engineers.Find(er => er.Id == id);
    }

    /// <summary>
    /// Return a copy of the list of references to all objects of type Engineer
    /// </summary>
    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }

    /// <summary>
    /// Update of an existing object. The update will consist of deleting the existing object with the same ID number and replacing it with a new object with the same ID number and updated fields.
    /// </summary>
    public void Update(Engineer item)
    {
        if (Read(item.Id) is null)
            throw new Exception($"Engineer with ID={item.Id} doesn't exists");
        int id= item.Id;
        DataSource.Engineers.RemoveAll(item => item.Id == id);
        DataSource.Engineers.Add(item);
    }
}
