namespace EmployeesDB
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            AbstractTasksPool pool = new TasksExtensionsLINQ();
            pool.Task8();
        }
    }
}