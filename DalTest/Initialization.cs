
namespace DalTest;
using DalApi;
using DO;
using System.Diagnostics;
using System.Net;
using System.Numerics;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Principal;

/// <summary>
/// A class responsible for initializing the data
/// </summary>
public static class Initialization
{
    //private static IDependency? s_dalDependency; //stage 1 
    //private static IEngineer? s_dalEngineer; //stage 1
    //private static ITask? s_dalTask; //stage 1

    private static IDal? s_dal; //stage 2

    private static readonly Random s_rand = new();
    /// <summary>
    /// A function that initializes the engineer entity list with data
    /// </summary>
    private static void createEngineers()
    {
        const int MIN_ID = 200000000;
        const int MAX_ID = 400000000;

        string[] engineerNames =
        {
         "Dani Levi" ,"Eli Amar", "Yair Cohen",
        "Ariela Levin", "Dina Klein"
        };

        //foreach (var _name in engineerNames)
        //{
        //    int _id;
        //    string _email;
        //    EngineerExperience _level;
        //    double _cost;
        //    do
        //        _id = s_rand.Next(MIN_ID, MAX_ID);
        //    while (s_dal.Engineer!.Read(_id) != null);

        //    int x = s_rand.Next(0, Enum.GetNames<EngineerExperience>().Count());
        //    _level  = (EngineerExperience)x;
        //    _cost = s_rand.Next(200, 500);
        //    _email = _name + "@gmail.com" ;

        //    Engineer newEng = new(_id, _name, _email, _level, _cost);

        //    s_dal.Engineer!.Create(newEng);
        //}
        var engineers = 
            engineerNames.Select(_name =>
        {
            int _id;
            string _email;
            EngineerExperience _level;
            double _cost;

            do
                _id = s_rand.Next(MIN_ID, MAX_ID);
            while (s_dal.Engineer!.Read(_id) != null);

            int x = s_rand.Next(0, Enum.GetNames<EngineerExperience>().Length);
            _level = (EngineerExperience)x;
            _cost = s_rand.Next(200, 500);
            _email = _name + "@gmail.com";

            return new Engineer(_id, _name, _email, _level, _cost);
        });
        engineers.ToList().ForEach(engineer => s_dal.Engineer!.Create(engineer));

    }
    /// <summary>
    /// A function that initializes the task entity list with data
    /// </summary>
    private static void createTasks()
    {
        
        string[] tasksDescription =
        {
            "Designing and developing new products or systems.",
            "Conducting research and analysis to solve engineering problems.",
            "Creating and interpreting technical drawings and specifications.",
            "Testing and evaluating prototypes and systems for performance and safety.",
            "Collaborating with cross - functional teams to identify and address design issues.",
            "Developing and implementing engineering solutions to optimize manufacturing processes.",
            "Conducting feasibility studies to assess the viability of engineering projects.",
            "Providing technical support and guidance to colleagues and clients.",
            "Managing and overseeing engineering projects from start to finish.",
            "Ensuring compliance with industry standards and regulations.",
            "Conducting risk assessments and implementing appropriate safety measures.",
            "Troubleshooting and resolving technical issues and malfunctions.",
            "Conducting quality control and assurance checks on products and systems.",
            "Developing and implementing preventive maintenance strategies.",
            "Keeping up - to - date with industry trends and advancements in engineering technology.",
            "Collaborating with suppliers and vendors to source and procure materials and equipment.",
            "Training and mentoring junior engineers and technicians.",
            "Conducting cost analysis and budgeting for engineering projects.",
            "Participating in continuous improvement initiatives to enhance efficiency and productivity.",
            "Documenting and communicating engineering processes and outcomes effectively."
        };

        //foreach (var _description in tasksDescription)
        //{
        //    string _alias="dd";
        //    bool _milestone = false;
        //    DateTime _createdAt = DateTime.Now.AddDays(-30);
        //    DateTime? _start = null;
        //    DateTime? _scheduledDate= null;
        //    DateTime? _forecastDate = null;
        //    DateTime? _deadLine= null;
        //    DateTime? _complete= null;
        //    string? _deriverables = null;
        //    string? _remarks=null;
        //    int? _engineerId;
        //    EngineerExperience _copmlexityLevel;


        //    IEnumerable<Engineer?> engineersList= s_dal.Engineer.ReadAll();
        //    int x = s_rand.Next(0, engineersList.Count());
        //    _engineerId= engineersList.ElementAt(x).Id;

        //    _engineerId = null;

        //    x = s_rand.Next(0, Enum.GetNames<EngineerExperience>().Count());
        //    _copmlexityLevel = (EngineerExperience)x;

        //    Task newTsk = new (0,_description, _alias, _milestone, _createdAt, _start, _scheduledDate, _forecastDate ,_deadLine, _complete, _deriverables, _remarks, _engineerId, _copmlexityLevel);

        //    s_dal.Task.Create(newTsk);
        //}
        var tasks =
           tasksDescription.Select(_description =>
           {
               string _alias = "dd";
               bool _milestone = false;
               DateTime _createdAt = DateTime.Now.AddDays(-30);
               DateTime? _start = null;
               DateTime? _scheduledDate = null;
               DateTime? _forecastDate = null;
               DateTime? _deadLine = null;
               DateTime? _complete = null;
               string? _deriverables = null;
               string? _remarks = null;
               int? _engineerId;
               EngineerExperience _copmlexityLevel;


               IEnumerable<Engineer?> engineersList = s_dal.Engineer.ReadAll();
               int x = s_rand.Next(0, engineersList.Count());
               _engineerId = engineersList.ElementAt(x).Id;

               _engineerId = null;

               x = s_rand.Next(0, Enum.GetNames<EngineerExperience>().Count());
               _copmlexityLevel = (EngineerExperience)x;

               return new Task(0, _description, _alias, _milestone, _createdAt, _start, _scheduledDate, _forecastDate, _deadLine, _complete, _deriverables, _remarks, _engineerId, _copmlexityLevel);

           });
        tasks.ToList().ForEach(task => s_dal.Task!.Create(task));
    }
    /// <summary>
    /// A function that initializes the dependency entity list with data
    /// </summary>
    private static void createDependencies()
    {
        int? _dependentTask;
        int? _dependsOnTask;
        int x;
        IEnumerable<Task?> tasksList = s_dal.Task.ReadAll();

        int[] dependecies40 = new int[40];
        var randomOrderedTasks = from dependency in dependecies40
                                 select s_rand.Next(0, tasksList.Count());
        var dependecies =
            randomOrderedTasks
            .Select(_dependentTask =>
            {
            do
                x = s_rand.Next(0, tasksList.Count());
            while (tasksList.ElementAt(_dependentTask).Id == x);
            _dependsOnTask = tasksList.ElementAt(x).Id;
            return new Dependency(0, _dependentTask, _dependsOnTask);

            });
        dependecies.ToList().ForEach(dependency => s_dal.Dependency!.Create(dependency));


        //for (int i = 0; i < 40; i++)
        //{
        //    int a = s_rand.Next(0, tasksList.Count());
        //    _dependentTask = tasksList.ElementAt(a).Id;
        //    do
        //        x = s_rand.Next(0, tasksList.Count());
        //    while (tasksList.ElementAt(a).Id == x);

        //    _dependsOnTask = tasksList.ElementAt(x).Id;
        //    Dependency newDep = new(0, _dependentTask, _dependsOnTask);
        //    s_dal.Dependency!.Create(newDep);
        //}
    }

    /// <summary>
    /// A public method, which will schedule the private methods we prepared and trigger the initialization of the lists.
    /// </summary>
    /// <param name="dalDependency">An access variable to a list of dependencies (the interface parameter we defined), which comes already initialized from the layer above.</param>
    /// <param name="dalEngineer">An access variable to a list of engineers (the interface parameter we defined), which comes already initialized from the layer above.</param>
    /// <param name="dalTask">An access variable to a list of tasks (the interface parameter we defined), which comes already initialized from the layer above.</param>
    /// <exception cref="NullReferenceException"></exception>

    //public static void Do(IDependency? dalDependency, IEngineer? dalEngineer, ITask? dalTask) //stage 1
    public static void Do(IDal dal) //stage 2
    {

        //s_dalDependency  = dalDependency ?? throw new NullReferenceException("DAL can not be null!"); //stage 1
        //s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!"); //stage 1
        //s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!"); //stage 1

        s_dal = dal ?? throw new NullReferenceException("DAL object can not be null!"); //stage 2

        createEngineers();
        createTasks();
        createDependencies();
    }
}  


