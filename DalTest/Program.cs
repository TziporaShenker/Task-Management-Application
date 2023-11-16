using Dal;
using DalApi;
using DO;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Xml.Linq;


namespace DalTest
{
    internal class Program
    {
        //private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1
        //private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
        //private static ITask? s_dalTask = new TaskImplementation(); //stage 1
        static readonly IDal s_dal = new Dal.DalList(); //stage 2

        /// <summary>
        /// A function that allows operations to be performed on the engineer entity
        /// </summary>
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
                            case 1: levelEngineer = EngineerExperience.Novice; break;
                            case 2: levelEngineer = EngineerExperience.AdvancedBeginner; break;
                            case 3: levelEngineer = EngineerExperience.Competent; break;
                            case 4:levelEngineer = EngineerExperience.Proficient; break;
                            case 5:levelEngineer = EngineerExperience.Expert; break;
                            default: levelEngineer = EngineerExperience.Expert; break;
                        }
                        s_dal.Engineer.Create(new Engineer(idEngineer, nameEngineer, emailEngineer, levelEngineer, costEngineer));
                        break;
                    case 2:
                        int id;
                        Console.WriteLine("Enter id for reading");
                        id = int.Parse(Console.ReadLine());
                        if (s_dal.Engineer!.Read(id) is null)
                            Console.WriteLine("no engineer found");
                        Console.WriteLine(s_dal.Engineer!.Read(id).ToString());
                        break;
                    case 3:
                        foreach (var engineer in s_dal.Engineer!.ReadAll())
                            Console.WriteLine(engineer.ToString());
                        break;
                    case 4:
                        int idEngineerUpdate, currentNumUpdate;
                        string nameEngineerUpdate, emailEngineerUpdate;
                        EngineerExperience levelEngineerUpdate;
                        double costEngineerUpdate;
                        Console.WriteLine("Enter id for reading");
                        idEngineerUpdate = int.Parse(Console.ReadLine());
                        Console.WriteLine(s_dal.Engineer!.Read(idEngineerUpdate).ToString());
                        Console.WriteLine("Enter details to update");//if null to put the same details
                        nameEngineerUpdate = (Console.ReadLine());
                        emailEngineerUpdate = Console.ReadLine();
                        costEngineerUpdate = double.Parse(Console.ReadLine());
                        currentNumUpdate = int.Parse(Console.ReadLine());
                        switch (currentNumUpdate)
                        {
                            case 1: levelEngineerUpdate = EngineerExperience.Novice; break;
                            case 2: levelEngineerUpdate = EngineerExperience.AdvancedBeginner; break;
                            case 3: levelEngineerUpdate = EngineerExperience.Competent; break;
                            case 4: levelEngineerUpdate = EngineerExperience.Proficient; break;
                            case 5: levelEngineerUpdate = EngineerExperience.Expert; break;
                            default: levelEngineerUpdate = EngineerExperience.Expert; break;
                        }
                        Engineer newEngineerUpdate = new(idEngineerUpdate, nameEngineerUpdate, emailEngineerUpdate, levelEngineerUpdate, costEngineerUpdate);
                        s_dal.Engineer!.Update(newEngineerUpdate);
                        break;
                    case 5:
                        int idDelete;
                        Console.WriteLine("Enter id for deleting");
                        idDelete = int.Parse(Console.ReadLine());
                        try
                        {
                            s_dal.Engineer!.Delete(idDelete);

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

        /// <summary>
        /// A function that allows operations to be performed on the dependency entity
        /// </summary>
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
                        s_dal.Dependency!.Create(new Dependency(0, dependentTask, dependsOnTask));
                        break;
                    case 2:
                        int id;
                        Console.WriteLine("Enter id for reading");
                        id = int.Parse(Console.ReadLine());
                        if (s_dal.Dependency!.Read(id) is null)
                            Console.WriteLine("no dependency found");
                        Console.WriteLine(s_dal.Dependency!.Read(id).ToString());
                        break;
                    case 3:
                        foreach (var dependency in s_dal.Dependency!.ReadAll())
                            Console.WriteLine(dependency.ToString());
                        break;
                    case 4:
                        int idUpdate, dependentTaskUpdate, dependsOnTaskUpdate;
                        Console.WriteLine("Enter id for reading");
                        idUpdate = int.Parse(Console.ReadLine());
                        Console.WriteLine(s_dal.Dependency!.Read(idUpdate).ToString());
                        Console.WriteLine("Enter details to update");
                        dependentTaskUpdate = int.Parse(Console.ReadLine());
                        dependsOnTaskUpdate = int.Parse(Console.ReadLine());
                        Dependency newDependencyUpdate = new(idUpdate, dependentTaskUpdate, dependsOnTaskUpdate);
                        try
                        {
                            s_dal.Dependency!.Update(newDependencyUpdate);

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
                            s_dal.Dependency!.Delete(idDelete);
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
        /// <summary>
        /// A function that allows operations to be performed on the task entity
        /// </summary>
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
                            case 2: taskLevel = EngineerExperience.Proficient; break;
                            case 3: taskLevel = EngineerExperience.Competent; break;
                            case 4: taskLevel = EngineerExperience.AdvancedBeginner; break;
                            case 5: taskLevel = EngineerExperience.Novice; break;
                            default: taskLevel = EngineerExperience.Expert; break;
                        }
                        s_dal.Task.Create(new DO.Task(0, taskDescription, taskAlias, taskMilestone, taskCreateAt, taskStart, taskScheduledDate, taskForecastDate, taskDeadline, taskComplete, taskDeliverables, taskRemarks, taskEngineerId, taskLevel));
                        break;
                    case 2:
                        int id;
                        Console.WriteLine("Enter id for reading");
                        id = int.Parse(Console.ReadLine());
                        if (s_dal.Task!.Read(id) is null)
                            Console.WriteLine("no task found");
                        Console.WriteLine(s_dal.Task!.Read(id).ToString());
                        break;
                    case 3:
                        foreach (var task in s_dal.Task!.ReadAll())
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
                        Console.WriteLine(s_dal.Task!.Read(idTaskUpdate).ToString());
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
                            case 1: taskLevelUpdate = EngineerExperience.Novice; break;
                            case 2: taskLevelUpdate = EngineerExperience.AdvancedBeginner; break;
                            case 3: taskLevelUpdate = EngineerExperience.Competent; break;
                            case 4: taskLevelUpdate = EngineerExperience.Proficient; break;
                            case 5: taskLevelUpdate = EngineerExperience.Expert; break;
                            default: taskLevelUpdate = EngineerExperience.Expert; break;
                        }
                        DO.Task newTaskUpdate = new(idTaskUpdate, taskDescriptionUpdate, taskAliasUpdate, taskMilestoneUpdate, taskCreateAtUpdate, taskStartUpdate, taskScheduledDateUpdate, taskForecastDateUpdate, taskDeadlineUpdate, taskCompleteUpdate, taskDeliverablesUpdate, taskRemarksUpdate, taskEngineerIdUpdate, taskLevelUpdate);
                        s_dal.Task!.Update(newTaskUpdate);
                        break;
                    case 5:
                        int idDelete;
                        Console.WriteLine("Enter id for deleting");
                        idDelete = int.Parse(Console.ReadLine());
                        try
                        {
                            s_dal.Task!.Delete(idDelete);
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

        /// <summary>
        /// Main plan for testing a data layer
        /// The main program calls a function depending on the selected entity
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                //Initialization.Do(s_dalDependency, s_dalEngineer, s_dalTask); //stage 1
                Initialization.Do(s_dal); //stage 2
                int chooseEntity;
                do
                {
                    Console.WriteLine("for task press 1\nfor engineer press 2\nfor dependency press 3\nfor exit press 0\n");
                    chooseEntity = int.Parse(Console.ReadLine());
                    switch (chooseEntity)
                    {
                        case 1:
                            TaskMenu();
                            break;
                        case 2:

                            EngineerMenu();
                            break;
                        case 3:

                            DependencyMenu();
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
