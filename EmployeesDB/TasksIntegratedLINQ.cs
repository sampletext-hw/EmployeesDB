using System;
using System.Linq;
using EmployeesDB.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeesDB
{
    public class TasksIntegratedLINQ : AbstractTasksPool
    {
        public override string GetEmployeesInformation()
        {
            var employees = (
                    from e in Context.Employees
                    orderby e.EmployeeId
                    select e
                )
                .Select(t => $"{t.EmployeeId} - {t.FirstName} - {t.LastName} - {t.MiddleName} - {t.JobTitle}")
                .ToList();

            return string.Join("\n", employees);
        }

        public override void TestTask3()
        {
            Console.WriteLine(GetEmployeesInformation());
        }

        public override void Task1()
        {
            var employees = (
                from e in Context.Employees
                orderby e.LastName
                where e.Salary > 48000
                select e
            ).ToList();

            Console.WriteLine(string.Join("\n", employees));
        }

        public override void Task2()
        {
            var town = new Towns() {Name = "Moscow"};
            Context.Towns.Add(town);
            Context.SaveChanges(); //call to get town ID

            var address = new Addresses() {AddressText = "27/1 Lubyanka", Town = town};
            Context.Addresses.Add(address);
            Context.SaveChanges(); //call to get address ID

            var browns = (
                from employee in Context.Employees
                where employee.LastName == "Brown"
                select employee
            ).ToList();

            browns.ForEach(t => t.Address = address);

            Context.SaveChanges(); //call to save addresses
        }

        public override void Task3()
        {
            var employees = (
                    from e in Context.Employees
                    where (
                        from ep in e.EmployeesProjects
                        select ep.ProjectId
                    ).Any(pid => (
                            from p in Context.Projects
                            where 2002 <= p.StartDate.Year && p.StartDate.Year <= 2005
                            select p.ProjectId
                        ).Contains(pid)
                    )
                    select e
                )
                .Include(e => e.EmployeesProjects)
                .ThenInclude(ep => ep.Project)
                .Include(e => e.Manager)
                .Take(5)
                .ToList();

            // как это работает?
            // 
            //  выбираем таких сотрудников, у которых
            //   в связях проектов
            //       из которых выбрали Id проектов
            //           хотя бы 1 такой проект,
            //           который содержится в выборке из проектов по году
            //           из которых выбрали Id проекта
            //  включая связи проектов
            //  для которых включаем сам проект
            //  включая менеджера сотрудника
            //  первые 5

            foreach (var employee in employees)
            {
                Console.WriteLine(
                    $"{employee.FirstName} {employee.LastName} - {employee.Manager.FirstName} {employee.Manager.LastName}");

                var projects = (
                    from ep in employee.EmployeesProjects
                    select ep.Project
                ).ToList();

                foreach (var project in projects)
                {
                    if (2002 <= project.StartDate.Year && project.StartDate.Year <= 2005)
                    {
                        Console.WriteLine("\t{0} - {1:dd.MM.yyyy} - {2}", project.Name, project.StartDate,
                            (project.EndDate.HasValue ? project.EndDate.Value.ToString("dd.MM.yyyy") : "Не завершён"));
                    }
                }
            }
        }

        public override void Task4()
        {
            int id = int.Parse(Console.ReadLine());

            var employee = (
                from e in Context.Employees
                where e.EmployeeId == id
                select e
            ).FirstOrDefault();

            if (employee == null)
            {
                Console.WriteLine("Сотрудник не найден");
                return;
            }

            Console.WriteLine($"{employee.LastName} {employee.FirstName} {employee.MiddleName} - {employee.JobTitle}");

            var projects = (
                from ep in Context.EmployeesProjects
                where ep.EmployeeId == id
                select ep.Project
            ).ToList();

            foreach (var project in projects)
            {
                Console.WriteLine("\t{0} - {1:dd.MM.yyyy} - {2}", project.Name, project.StartDate,
                    (project.EndDate.HasValue ? project.EndDate.Value.ToString("dd.MM.yyyy") : "Не завершён"));
            }
        }

        public override void Task5()
        {
            var names = (
                from department in Context.Departments
                where department.Employees.Count < 5
                select department.Name
            ).ToList();

            Console.WriteLine(string.Join("; ", names));
        }

        public override void Task6()
        {
            string name = Console.ReadLine();
            int percent = int.Parse(Console.ReadLine());

            var department = (
                from d in Context.Departments
                where d.Name == name
                select d
            ).First();

            foreach (var employee in department.Employees)
            {
                employee.Salary *= (decimal)((100 + percent) / 100f);
            }

            Context.SaveChanges();
        }

        public override void Task7()
        {
            int id = int.Parse(Console.ReadLine());

            var department = (
                from d in Context.Departments
                where d.DepartmentId == id
                select d
            ).First();

            if (department == null)
            {
                Console.WriteLine("Отдел не найден");
                return;
            }

            // THIS IS BROKEN BECAUSE OF DOUBLED FOREIGN KEY
            department.Employees.Clear();
            Context.SaveChanges();

            Context.Departments.Remove(department);
            Context.SaveChanges();
        }

        public override void Task8()
        {
            string name = Console.ReadLine();

            var town = (
                from t in Context.Towns
                where t.Name == name
                select t
            ).First();

            Context.Entry(town).Collection(t => t.Addresses).Load();

            Context.Towns.Remove(town);
            Context.SaveChanges();
        }
    }
}