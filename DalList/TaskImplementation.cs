namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;




public class TaskImplementation : ITask
{
    /// <summary>
    /// Adding a new object of type Task to a database, (to the list of objects of type Task).
    /// </summary>
    public int Create(Task item)
    {
        int id = DataSource.Config.NextTaskId;
        Task copy = item with { Id = id };
        DataSource.Tasks.Add(copy);
        return id;

    }

    /// <summary>
    /// Deletion of an existing object with a certain ID, from the list of objects of type Task.
    /// </summary>
    public void Delete(int id)
    {
        if (Read(id) is not null)
        {
            for (int i = 0; i < DataSource.Dependencies.Count; i++) 
            { 
                if (DataSource.Dependencies[i].DependsOnTask == id)
                {
                    throw new Exception($"Another task depends on task with ID={id}");
                }
            }
            DataSource.Tasks.RemoveAll(item => item.Id == id);
            for (int i = 0; i < DataSource.Dependencies.Count; i++)
            {
                if (DataSource.Dependencies[i].DependentTask == id)
                {
                    DataSource.Dependencies.RemoveAt(i);
                }
            }
        }
        else 
        { 
           throw new Exception($"Task with ID={id} doesn't exists");
        }
    }
    /// <summary>
    /// Returning a reference to a single object of type Task with a certain ID, if it exists in a database (in a list of data of type Task), or null if the object does not exist.
    /// </summary>
    public Task? Read(int id)
    {
        return DataSource.Tasks.Find(tk => tk.Id == id);
    }

    /// <summary>
    /// Return a copy of the list of references to all objects of type Task
    /// </summary>
    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }

    /// <summary>
    /// Update of an existing object. The update will consist of deleting the existing object with the same ID number and replacing it with a new object with the same ID number and updated fields.
    /// </summary>
    public void Update(Task item)
    {
        if (Read(item.Id) is null)
            throw new Exception($"Task with ID={item.Id} doesn't exists");
        //Delete(item.Id);
        int id = item.Id;
        DataSource.Tasks.RemoveAll(item => item.Id==id);
        Create(item);
    }
}
