namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        int id = DataSource.Config.NextDependencyId;
        Dependency copy = item with { Id = id };
        DataSource.Dependencies.Add(copy);
        return id;
    }

    public void Delete(int id)
    {
        if (Read(id) is not null)
        {
            DataSource.Dependencies.RemoveAt(id);
        }
        throw new Exception($"Dependency with ID={id} does Not exist");
    }

    public Dependency? Read(int id)
    {
        return DataSource.Dependencies.Find(dy => dy.Id == id);
    }

    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencies);
    }

    public void Update(Dependency item)
    {
        if (Read(item.Id) is null)
            throw new Exception($"Dependency with ID={item.Id} doesn't exists");
        Delete(item.Id);
        DataSource.Dependencies.Add(item);
    }
}
