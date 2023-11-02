namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;


public class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        int id = DataSource.Config.NextTaskId;
        Task copy = item with { Id = id };
        DataSource.Tasks.Add(copy);
        return id;

    }

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
            DataSource.Tasks.RemoveAt(id);
            //נגיד והיה לי משימה מספר 7 שתלויה במשימה 4 ו 5 
            //אז עכשיו לא רלוונטי לי במה היא תלויה אז צריך למחוק 
            //את הרשומה  מטבלת התלותיות
            //איך נדע את ה"איידי" כדי למחוק  
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
   
    public Task? Read(int id)
    {
        return DataSource.Tasks.Find(tk => tk.Id == id);

    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }

    public void Update(Task item)
    {
        if (Read(item.Id) is null)
            throw new Exception($"Task with ID={item.Id} doesn't exists");
        Delete(item.Id);
        DataSource.Tasks.Add(item);
    }
}
