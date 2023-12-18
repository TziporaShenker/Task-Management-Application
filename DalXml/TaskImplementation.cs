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
        // הגדרת אוביקט= מכונה שיודעת להמיר אוביקטים מ ואל מחרוזת
        XmlSerializer serializer = new XmlSerializer(typeof(List<DO.Task>));
        // מצביע לקובץ שיודע לקרוא
        TextReader textReader = new StringReader(tasksFile);
        List<DO.Task> lst = (List<Task>?)serializer.Deserialize(textReader) ?? throw new Exception();
        lst.Add(item);

        using (TextWriter writer = new StreamWriter(tasksFile))
        {
            serializer.Serialize(writer, lst);
        }

        return item.Id;
    }

    public void Delete(int id)
    {
        XmlSerializer serializerTasks = new XmlSerializer(typeof(List<DO.Task>));
        TextReader textReaderTasks = new StringReader(tasksFile); 
        List<DO.Task> lstTasks = (List<Task>?)serializerTasks.Deserialize(textReaderTasks) ?? throw new Exception(); 
        XmlSerializer serializerDependencies = new XmlSerializer(typeof(List<DO.Dependency>));
        TextReader textReaderDependencies = new StringReader(dependenciesFile);
        List<DO.Dependency> lstDependencies = (List<Dependency>?)serializerDependencies.Deserialize(textReaderDependencies) ?? throw new Exception();

        if (Read(id) is not null)
        {
            if (lstDependencies.Any(dependency => dependency.DependsOnTask == id))
            {
                throw new DalDeletionImpossible($"Another task depends on task with ID={id}");
            }

            lstTasks.RemoveAll(item => item.Id == id);

            lstDependencies.RemoveAll(dependency => dependency.DependentTask == id);
        }
        else
        {
            throw new DalDoesNotExistException($"Task with ID={id} doesn't exists");
        }

        using (TextWriter writer = new StreamWriter(tasksFile))
        {
            serializerTasks.Serialize(writer, lstTasks);
        } 

        using (TextWriter writer = new StreamWriter(dependenciesFile))
        {
            serializerDependencies.Serialize(writer, lstDependencies);
        }
    }

    public Task? Read(int id)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<DO.Task>));
        TextReader textReader = new StringReader(tasksFile);
        List<DO.Task> lst = (List<Task>?)serializer.Deserialize(textReader) ?? throw new Exception();
        return lst.Find(tk => tk.Id == id);

    }

    public Task? Read(Func<Task, bool> filter)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<DO.Task>));
        TextReader textReader = new StringReader(tasksFile);
        List<DO.Task> lst = (List<Task>?)serializer.Deserialize(textReader) ?? throw new Exception();
        if (filter == null)
            return (Task?)lst.Select(item => item);
        else
            return (Task?)lst.Where(filter);
    }

    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    public void Update(Task item)
    {
        throw new NotImplementedException();
    }
}
