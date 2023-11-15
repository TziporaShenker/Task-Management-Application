namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
{
    /// <summary>
    /// Adding a new object of type Dependency to a database, (to the list of objects of type Dependency).
    /// </summary>
    public int Create(Dependency item)
    {
        int id = DataSource.Config.NextDependencyId;
        Dependency copy = item with { Id = id };
        DataSource.Dependencies.Add(copy);
        return id;
    }

    /// <summary>
    /// Deletion of an existing object with a certain ID, from the list of objects of type Dependency.
    /// </summary>
    public void Delete(int id)
    {
        if (Read(id) is not null)
        {
            DataSource.Dependencies.RemoveAll(item => item.Id == id);
        }
        else 
        { 
            throw new Exception($"Dependency with ID={id} does Not exist");
        }
    }

    /// <summary>
    /// Returning a reference to a single object of type Dependency with a certain ID, if it exists in a database (in a list of data of type Dependency), or null if the object does not exist.
    /// </summary>
    public Dependency? Read(int id)
    {
        return DataSource.Dependencies.Find(dy => dy.Id == id);
    }

    /// <summary>
    /// Return a copy of the list of references to all objects of type Dependency
    /// </summary>
    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencies);
    }

    /// <summary>
    /// Update of an existing object. The update will consist of deleting the existing object with the same ID number and replacing it with a new object with the same ID number and updated fields.
    /// </summary>
    public void Update(Dependency item)
    {
        if (Read(item.Id) is null)
            throw new Exception($"Dependency with ID={item.Id} doesn't exists");
        Delete(item.Id);
        DataSource.Dependencies.Add(item);
    }
}
