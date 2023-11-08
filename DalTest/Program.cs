using Dal;

using DalApi;


namespace DalTest
{
    internal class Program
    {
        private static IDependency? s_dalDependency = new DependencyImplementation();
        private static IEngineer? s_dalEngineer = new EngineerImplementation();
        private static ITask? s_dalTask = new TaskImplementation();

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            
        }

    } 
}
