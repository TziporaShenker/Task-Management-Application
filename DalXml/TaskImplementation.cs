namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml.Linq;



internal class TaskImplementation : ITask
{
    const string tasksFile = @"..\xml\tasks.xml";
    const string dependenciesFile = @"..\xml\dependencies.xml";

    public int Create(Task item)
    {
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>(tasksFile);
        tasks.Add(item);
        XMLTools.SaveListToXMLSerializer<Task>(tasks, tasksFile);
        return item.Id;
    }

    public void Delete(int id)
    {
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>(tasksFile);
        List<Dependency> dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>(dependenciesFile);

        if (Read(id) is not null)
        {
            if (dependencies.Any(dependency => dependency.DependsOnTask == id))
            {
                throw new DalDeletionImpossible($"Another task depends on task with ID={id}");
            }

            tasks.RemoveAll(item => item.Id == id);

            dependencies.RemoveAll(dependency => dependency.DependentTask == id);
        }
        else
        {
            throw new DalDoesNotExistException($"Task with ID={id} doesn't exists");
        }
        XMLTools.SaveListToXMLSerializer<Task>(tasks, tasksFile);
        XMLTools.SaveListToXMLSerializer<Dependency>(dependencies, dependenciesFile);


    }

    public Task? Read(int id)
    {
       
        return XMLTools.LoadListFromXMLSerializer<Task>(tasksFile).Find(tk => tk.Id == id);

    }

    public Task? Read(Func<Task, bool> filter)
    {
        if (filter == null)
            return (Task?)XMLTools.LoadListFromXMLSerializer<Task>(tasksFile).Select(item => item);
        else
            return (Task?)XMLTools.LoadListFromXMLSerializer<Task>(tasksFile).Where(filter);
    }

    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        return XMLTools.LoadListFromXMLSerializer<Task>(tasksFile);
    }

    public void Reset()
    {
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>(tasksFile);
        tasks.Clear();
        XMLTools.SaveListToXMLSerializer<Task>(tasks, tasksFile);
    }

    public void Update(Task item)
    {
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>(tasksFile);
        if (Read(item.Id) is null)
            throw new DalDoesNotExistException($"Task with ID={item.Id} doesn't exists");
        int id = item.Id;
        tasks.RemoveAll(item => item.Id == id);
        tasks.Add(item);
        XMLTools.SaveListToXMLSerializer<Task>(tasks, tasksFile);
    }
}
