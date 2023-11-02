namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        if (Read(item.Id) is not null)
            throw new Exception($"Engineer with ID={item.Id} already exists");
        DataSource.Engineers.Add(item);
        return item.Id;

    }

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
            DataSource.Engineers.RemoveAt(id);
        }
        else
        {
            throw new Exception($"Engineer with ID={id} does Not exist");
        }
    }
    public Engineer? Read(int id)
    {
        return DataSource.Engineers.Find(er => er.Id == id);
    }

    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }

    public void Update(Engineer item)
    {
        if (Read(item.Id) is null)
            throw new Exception($"Engineer with ID={item.Id} doesn't exists");
        Delete(item.Id);
        DataSource.Engineers.Add(item);
    }
}
