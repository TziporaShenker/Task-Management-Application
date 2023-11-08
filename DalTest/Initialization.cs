
namespace DalTest;
using DalApi;
using DO;
using System.Numerics;
using System.Security.Cryptography;

public static class Initialization
{
    private static IDependency? s_dalDependency; 
    private static IEngineer? s_dalEngineer;
    private static ITask? s_dalTask;
    private static readonly Random s_rand = new();

    private static void createEngineers()
    {
        const int MIN_ID = 200000000;
        const int MAX_ID = 400000000;

        string[] engineerNames =
        {
         "Dani Levi" ,"Eli Amar", "Yair Cohen",
        "Ariela Levin", "Dina Klein", "Shira Israelof"
        };

        foreach (var _name in engineerNames)
        {
            int _id;
            string _email;
            EngineerExperience _level;
            double _cost;
            do
                _id = s_rand.Next(MIN_ID, MAX_ID);
            while (s_dalEngineer!.Read(_id) != null);

            int x = s_rand.Next(0, Enum.GetNames<EngineerExperience>().Count());
            _level  = (EngineerExperience)x;
            _cost = s_rand.Next(200, 500);
            _email = _name + "@gmail.com" ;

            Engineer newEng = new(_id, _name, _email, _level, _cost);

            s_dalEngineer!.Create(newEng);
        }
    }
    private static void createTasks()
    {
        
        string[] tasksDescription =
        {
         "Dani Levi" ,"Eli Amar", "Yair Cohen",
        "Ariela Levin", "Dina Klein", "Shira Israelof"
        };

        foreach (var _description in tasksDescription)
        {
            string _alias="dd";
            bool _milestone = false;
            DateTime _createdAt = DateTime.Now.AddDays(-30);
            DateTime? _start = null;
            DateTime? _scheduledDate= null;
            DateTime? _deadLine= null;
            DateTime? _complete= null;
            string? _productDescription=null;
            string? _remarks=null;
            int? _engineerId;
            EngineerExperience _copmlexityLevel;


            List<Engineer> engineersList= s_dalEngineer.ReadAll();
            int x = s_rand.Next(0, engineersList.Count());
            _engineerId= engineersList[x].Id;

            x = s_rand.Next(0, Enum.GetNames<EngineerExperience>().Count());
            _copmlexityLevel = (EngineerExperience)x;

            Task newTsk = new (0,_description, _alias, _milestone, _createdAt, _start, _scheduledDate, _deadLine, _complete, _productDescription, _remarks, _engineerId, _copmlexityLevel);

            s_dalTask.Create(newTsk);
        }
    }
    private static void createDependencies()
    {

        int? _dependentTask;
        int? _dependsOnTask; 
        int x;
        List<Task> tasksList = s_dalTask.ReadAll(); ;

        for (int i = 0; i < tasksList.Count(); i++)
        {
            _dependentTask= tasksList[i].Id;
            
            do
                x = s_rand.Next(0, tasksList.Count());
            while (tasksList[i].Id == x);

            _dependsOnTask = tasksList[x].Id;

            Dependency newDep = new(0, _dependentTask, _dependsOnTask);
            s_dalDependency!.Create(newDep);

        }
    }
    public static void Do(IDependency? s_dalDependency, IEngineer? s_dalEngineer, ITask? s_dalTask)
    {
        IDependency? dalDependency;
        IEngineer? dalEngineer;
        ITask? dalTask;
        //מה הסיפור פה????????????
        //????????????????????????????????????????????????
        //????????????????????????????????????????????????
        //????????????????????????????????????????????????
        //????????????????????????????????????????????????
        //????????????????????????????????????????????????
        //????????????????????????????????????????????????
        //
        s_dalDependency = dalDependency ?? throw new NullReferenceException("DAL can not be null!");
        dalEngineer=  s_dalEngineer?? throw new NullReferenceException("DAL can not be null!");
        s_dalTask=  dalTask?? throw new NullReferenceException("DAL can not be null!");
        createEngineers();
        createTasks();
        createDependencies();
    }
}  


