namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

internal class EngineerImplementation : IEngineer
{
    const string engineersFile = @"..\xml\engineers.xml";
    const string tasksFile = @"..\xml\tasks.xml";

    public int Create(Engineer item)
    {
        // הגדרת אוביקט= מכונה שיודעת להמיר אוביקטים מ ואל מחרוזת
        XmlSerializer serializer = new XmlSerializer(typeof(List<DO.Engineer>));
        // מצביע לקובץ שיודע לקרוא
        TextReader textReader = new StringReader(engineersFile);
        // 
        List<DO.Engineer> lst = (List<Engineer>?)serializer.Deserialize(textReader) ?? throw new Exception();
        // הוספת הפריט החדש
        lst.Add(item);

        using (TextWriter writer = new StreamWriter(engineersFile))
        {
            serializer.Serialize(writer, lst);
        }

        return item.Id;
    }

    public void Delete(int id)
    { 
        XmlSerializer serializerEngineers = new XmlSerializer(typeof(List<DO.Engineer>));
        TextReader textReaderEngineers = new StringReader(engineersFile);
        List<DO.Engineer> lstEngineers = (List<Engineer>?)serializerEngineers.Deserialize(textReaderEngineers) ?? throw new Exception();

        XmlSerializer serializerTasks = new XmlSerializer(typeof(List<DO.Task>));
        TextReader textReaderTasks = new StringReader(tasksFile);
        List<DO.Task> lstTasks = (List<Task>?)serializerTasks.Deserialize(textReaderTasks) ?? throw new Exception();

        if (Read(id) is not null)
        {
            if (lstTasks.Any(task => task.EngineerId == id))
            {
                throw new DalDeletionImpossible($"A task is depends on engineer with ID={id}");
            }
            lstEngineers.RemoveAll(item => item.Id == id);
        }
        else
        {
            throw new DalDoesNotExistException($"Engineer with ID={id} does Not exist");
        }

        using (TextWriter writer = new StreamWriter(engineersFile))
        {
            serializerEngineers.Serialize(writer, lstEngineers);
        }
    }

    public Engineer? Read(int id)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<DO.Engineer>));
        TextReader textReader = new StringReader(engineersFile);
        List<DO.Engineer> lst = (List<Engineer>?)serializer.Deserialize(textReader) ?? throw new Exception();
        return lst.Find(er => er.Id == id);
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<DO.Engineer>));
        TextReader textReader = new StringReader(engineersFile);
        List<DO.Engineer> lst = (List<Engineer>?)serializer.Deserialize(textReader) ?? throw new Exception();
        if (filter == null)
            return (Engineer?)lst.Select(item => item);
        else
        {

            if (filter == null)
                return (Engineer?)lst.Select(item => item);
            else
                return (Engineer?)lst.Where(filter);
        }

    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        throw new NotImplementedException();
    }
}
