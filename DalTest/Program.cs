using Dal;
using DalApi;
using DO;
using System.Diagnostics;


namespace DalTest
{
    internal class Program
    {
        private static IDependency? s_dalDependency = new DependencyImplementation();
        private static IEngineer? s_dalEngineer = new EngineerImplementation();
        private static ITask? s_dalTask = new TaskImplementation();
        private static void EngineerMenu()
        {
            int chooseSubMenu;
            do
            {
                Console.WriteLine("for exit press 0\n" +
                          "for add an order press 1\n" +
                          "for read an order press 2\n" +
                          "for read all orders press 3\n" +
                          "for update an order press 4\n" +
                          "for delete an order press 5\n");
                chooseSubMenu = int.Parse(Console.ReadLine());

                switch (chooseSubMenu)
                {
                    case 1:
                        Console.WriteLine("Enter id, name, email, cost and a number to choose experience");
                        int idEngineer, currentNum;
                        string nameEngineer, emailEngineer;
                        EngineerExperience levelEngineer;
                        double costEngineer;
                        idEngineer = int.Parse(Console.ReadLine());
                        nameEngineer = (Console.ReadLine());
                        emailEngineer = Console.ReadLine();
                        costEngineer = double.Parse(Console.ReadLine());
                        currentNum = int.Parse(Console.ReadLine());
                        switch (currentNum)
                        {
                            case 1: levelEngineer = EngineerExperience.Expert; break;
                            case 2: levelEngineer = EngineerExperience.Competent; break;
                            case 3: levelEngineer = EngineerExperience.Novice; break;
                            default: levelEngineer = EngineerExperience.Expert; break;
                        }
                        s_dalEngineer = new EngineerImplementation();
                        Engineer newEngineer = new(idEngineer, nameEngineer, emailEngineer, levelEngineer, costEngineer);
                        s_dalEngineer!.Create(newEngineer);
                        break;
                    case 2:
                        int id;
                        Console.WriteLine("Enter id for reading");
                        id = int.Parse(Console.ReadLine());
                        if (s_dalEngineer!.Read(id) is null)
                            Console.WriteLine("no engineer found");
                        Console.WriteLine(s_dalEngineer!.Read(id).ToString());
                        break;
                    case 3:
                        foreach (var engineer in s_dalEngineer!.ReadAll())
                            Console.WriteLine(engineer.ToString());
                        break;
                    case 4:
                        int idEngineerUpdate, currentNumUpdate;
                        string nameEngineerUpdate, emailEngineerUpdate;
                        EngineerExperience levelEngineerUpdate;
                        double costEngineerUpdate;
                        Console.WriteLine("Enter id for reading");
                        idEngineerUpdate = int.Parse(Console.ReadLine());
                        Console.WriteLine(s_dalEngineer!.Read(idEngineerUpdate).ToString());
                        Console.WriteLine("Enter details to update");//if null to put the same details
                        nameEngineerUpdate = (Console.ReadLine());
                        emailEngineerUpdate = Console.ReadLine();
                        costEngineerUpdate = double.Parse(Console.ReadLine());
                        currentNumUpdate = int.Parse(Console.ReadLine());
                        switch (currentNumUpdate)
                        {
                            case 1: levelEngineerUpdate = EngineerExperience.Expert; break;
                            case 2: levelEngineerUpdate = EngineerExperience.Competent; break;
                            case 3: levelEngineerUpdate = EngineerExperience.Novice; break;
                            default: levelEngineerUpdate = EngineerExperience.Expert; break;
                        }
                        Engineer newEngineerUpdate = new(idEngineerUpdate, nameEngineerUpdate, emailEngineerUpdate, levelEngineerUpdate, costEngineerUpdate);
                        s_dalEngineer!.Update(newEngineerUpdate);
                        break;
                    case 5:
                        int idDelete;
                        Console.WriteLine("Enter id for deleting");
                        idDelete = int.Parse(Console.ReadLine());
                        try
                        {
                            s_dalEngineer!.Delete(idDelete);

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(  ex.Message);
                        }
                        break;
                    default: return;
                }
            } while (chooseSubMenu > 0 && chooseSubMenu < 6);
        }


        private static void DependencyMenu()
        {
            int chooseSubMenu;

            do
            {
                Console.WriteLine("for exit press 0\n" +
                          "for add an order press 1\n" +
                          "for read an order press 2\n" +
                          "for read all orders press 3\n" +
                          "for update an order press 4\n" +
                          "for delete an order press 5\n");
                chooseSubMenu = int.Parse(Console.ReadLine());

                switch (chooseSubMenu)
                {
                    case 1:
                        Console.WriteLine("Enter details for all the characteristics");
                        int dependentTask, dependsOnTask;
                        dependentTask = int.Parse(Console.ReadLine());
                        dependsOnTask = int.Parse(Console.ReadLine());
                        s_dalDependency = new DependencyImplementation();
                        Dependency newDependency = new(0, dependentTask, dependsOnTask);
                        s_dalDependency!.Create(newDependency);
                        break;
                    case 2:
                        int id;
                        Console.WriteLine("Enter id for reading");
                        id = int.Parse(Console.ReadLine());
                        if (s_dalDependency!.Read(id) is null)
                            Console.WriteLine("no dependency found");
                        Console.WriteLine(s_dalDependency!.Read(id).ToString());
                        break;
                    case 3:
                        foreach (var dependency in s_dalDependency!.ReadAll())
                            Console.WriteLine(dependency.ToString());
                        break;
                    case 4:
                        int idUpdate, dependentTaskUpdate, dependsOnTaskUpdate;
                        Console.WriteLine("Enter id for reading");
                        idUpdate = int.Parse(Console.ReadLine());
                        Console.WriteLine(s_dalDependency!.Read(idUpdate).ToString());
                        Console.WriteLine("Enter details to update");
                        dependentTaskUpdate = int.Parse(Console.ReadLine());
                        dependsOnTaskUpdate = int.Parse(Console.ReadLine());
                        Dependency newDependencyUpdate = new(idUpdate, dependentTaskUpdate, dependsOnTaskUpdate);
                        try
                        {
                            s_dalDependency!.Update(newDependencyUpdate);

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(  ex.Message);
                        }
                        break;
                    case 5:
                        int idDelete;
                        Console.WriteLine("Enter id for deleting");
                        idDelete = int.Parse(Console.ReadLine());
                        try
                        {
                            s_dalDependency!.Delete(idDelete);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(  ex.Message);
                        }
                        break;
                    default: return;
                }
            } while (chooseSubMenu > 0 && chooseSubMenu < 6);
        }

        private static void TaskMenu()
        {
            int chooseSubMenu;
            do
            {
                Console.WriteLine("for exit press 0\n" +
                          "for add an order press 1\n" +
                          "for read an order press 2\n" +
                          "for read all orders press 3\n" +
                          "for update an order press 4\n" +
                          "for delete an order press 5\n");
                chooseSubMenu = int.Parse(Console.ReadLine());
                switch (chooseSubMenu)
                {
                    case 1:
                        Console.WriteLine("Enter description, alias, deriverables, remarks, milestone,engineerId, dates and task's level");
                        int taskEngineerId, currentTaskNum;
                        string taskDescription, taskAlias, taskDeliverables, taskRemarks;
                        bool taskMilestone;
                        DateTime taskCreateAt, taskStart, taskScheduledDate,taskForecastDate, taskDeadline, taskComplete;
                        EngineerExperience taskLevel;
                        taskDescription = Console.ReadLine();
                        taskAlias = Console.ReadLine();
                        taskMilestone = bool.Parse(Console.ReadLine());
                        taskCreateAt = DateTime.Parse(Console.ReadLine());
                        taskStart = DateTime.Parse(Console.ReadLine());
                        taskScheduledDate = DateTime.Parse(Console.ReadLine());
                        taskForecastDate = DateTime.Parse(Console.ReadLine());
                        taskDeadline = DateTime.Parse(Console.ReadLine());
                        taskComplete = DateTime.Parse(Console.ReadLine());
                        taskDeliverables = Console.ReadLine();
                        taskRemarks = Console.ReadLine();
                        taskEngineerId = int.Parse(Console.ReadLine());
                        currentTaskNum = int.Parse(Console.ReadLine());
                        switch (currentTaskNum)
                        {
                            case 1: taskLevel = EngineerExperience.Expert; break;
                            case 2: taskLevel = EngineerExperience.Competent; break;
                            case 3: taskLevel = EngineerExperience.Novice; break;
                            default: taskLevel = EngineerExperience.Expert; break;
                        }
                        s_dalTask = new TaskImplementation();
                        DO.Task newTask = new(0, taskDescription, taskAlias, taskMilestone, taskCreateAt, taskStart, taskScheduledDate, taskForecastDate, taskDeadline, taskComplete, taskDeliverables, taskRemarks, taskEngineerId, taskLevel);
                        s_dalTask!.Create(newTask);
                        break;
                    case 2:
                        int id;
                        Console.WriteLine("Enter id for reading");
                        id = int.Parse(Console.ReadLine());
                        if (s_dalTask!.Read(id) is null)
                            Console.WriteLine("no task found");
                        Console.WriteLine(s_dalTask!.Read(id).ToString());
                        break;
                    case 3:
                        foreach (var task in s_dalTask!.ReadAll())
                            Console.WriteLine(task.ToString());
                        break;
                    case 4:
                        int idTaskUpdate, currentTaskNumUpdate, taskEngineerIdUpdate;
                        string taskDescriptionUpdate, taskAliasUpdate, taskDeliverablesUpdate, taskRemarksUpdate;
                        bool taskMilestoneUpdate;
                        DateTime taskCreateAtUpdate, taskStartUpdate, taskScheduledDateUpdate, taskForecastDateUpdate, taskDeadlineUpdate, taskCompleteUpdate;
                        EngineerExperience taskLevelUpdate;
                        Console.WriteLine("Enter id for reading");
                        idTaskUpdate = int.Parse(Console.ReadLine());
                        Console.WriteLine(s_dalTask!.Read(idTaskUpdate).ToString());
                        Console.WriteLine("Enter details to update");//if null to put the same details
                        taskDescriptionUpdate = Console.ReadLine();
                        taskAliasUpdate = Console.ReadLine();
                        taskMilestoneUpdate = bool.Parse(Console.ReadLine());
                        taskCreateAtUpdate = DateTime.Parse(Console.ReadLine());
                        taskStartUpdate = DateTime.Parse(Console.ReadLine());
                        taskScheduledDateUpdate = DateTime.Parse(Console.ReadLine());
                        taskForecastDateUpdate = DateTime.Parse(Console.ReadLine());
                        taskDeadlineUpdate = DateTime.Parse(Console.ReadLine());
                        taskCompleteUpdate = DateTime.Parse(Console.ReadLine());
                        taskDeliverablesUpdate = Console.ReadLine();
                        taskRemarksUpdate = Console.ReadLine();
                        taskEngineerIdUpdate = int.Parse(Console.ReadLine());
                        currentTaskNumUpdate = int.Parse(Console.ReadLine());
                        switch (currentTaskNumUpdate)
                        {
                            case 1: taskLevelUpdate = EngineerExperience.Expert; break;
                            case 2: taskLevelUpdate = EngineerExperience.Competent; break;
                            case 3: taskLevelUpdate = EngineerExperience.Novice; break;
                            default: taskLevelUpdate = EngineerExperience.Expert

; break;
                        }
                        DO.Task newTaskUpdate = new(idTaskUpdate, taskDescriptionUpdate, taskAliasUpdate, taskMilestoneUpdate, taskCreateAtUpdate, taskStartUpdate, taskScheduledDateUpdate, taskForecastDateUpdate, taskDeadlineUpdate, taskCompleteUpdate, taskDeliverablesUpdate, taskRemarksUpdate, taskEngineerIdUpdate, taskLevelUpdate);
                        s_dalTask!.Update(newTaskUpdate);
                        break;
                    case 5:
                        int idDelete;
                        Console.WriteLine("Enter id for deleting");
                        idDelete = int.Parse(Console.ReadLine());
                        try
                        {
                            s_dalTask!.Delete(idDelete);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    default: return;
                }
            } while (chooseSubMenu > 0 && chooseSubMenu < 6);
        }


        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello, World!");
                Initialization.Do(s_dalDependency, s_dalEngineer, s_dalTask);
                int chooseEntity;
                do
                {
                    Console.WriteLine("for task press 1\nfor engineer press 2\nfor dependency press 3\nfor exit press 0\n");
                    chooseEntity = int.Parse(Console.ReadLine());
                    switch (chooseEntity)
                    {
                        case 1:
                            TaskMenu();//doing this function 
                            break;
                        case 2:

                            EngineerMenu();//doing this function 
                            break;
                        case 3:

                            DependencyMenu();//doing this function 
                            break;
                        default:
                            break;
                    }
                }
                while (chooseEntity != 0);
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }
    } 
}
