using EmployeesDB.Data.Models;

namespace EmployeesDB
{
    public abstract class AbstractTasksPool
    {
        public EmployeesContext Context { get; } = new EmployeesContext();

        public abstract string GetEmployeesInformation();
        public abstract void TestTask3();
        public abstract void Task1();
        public abstract void Task2();
        public abstract void Task3();
        public abstract void Task4();
        public abstract void Task5();
        public abstract void Task6();
        public abstract void Task7();
        public abstract void Task8();
    }
}