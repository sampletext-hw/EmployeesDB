using System;
using System.Collections.Generic;

namespace EmployeesDB.Data.Models
{
    public partial class Employees
    {
        public Employees()
        {
            Departments = new HashSet<Departments>();
            EmployeesProjects = new HashSet<EmployeesProjects>();
            InverseManager = new HashSet<Employees>();
        }

        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string JobTitle { get; set; }
        public int DepartmentId { get; set; }
        public int? ManagerId { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        public int? AddressId { get; set; }

        public virtual Addresses Address { get; set; }
        public virtual Departments Department { get; set; }
        public virtual Employees Manager { get; set; }
        public virtual ICollection<Departments> Departments { get; set; }
        public virtual ICollection<EmployeesProjects> EmployeesProjects { get; set; }
        public virtual ICollection<Employees> InverseManager { get; set; }

        public override string ToString()
        {
            return
                $"Employees:\n" +
                $"{{\n" +
                $"\t{nameof(EmployeeId)}: {EmployeeId},\n" +
                $"\t{nameof(FirstName)}: {FirstName},\n" +
                $"\t{nameof(LastName)}: {LastName},\n" +
                $"\t{nameof(MiddleName)}: {MiddleName},\n" +
                $"\t{nameof(JobTitle)}: {JobTitle},\n" +
                $"\t{nameof(DepartmentId)}: {DepartmentId},\n" +
                $"\t{nameof(ManagerId)}: {ManagerId},\n" +
                $"\t{nameof(HireDate)}: {HireDate},\n" +
                $"\t{nameof(Salary)}: {Salary},\n" +
                $"\t{nameof(AddressId)}: {AddressId},\n" +
                $"\t{nameof(Address)}: {Address},\n" +
                $"\t{nameof(Department)}: {Department},\n" +
                $"\t{nameof(Manager)}: {Manager},\n" +
                $"\t{nameof(Departments)}: {Departments},\n" +
                $"\t{nameof(EmployeesProjects)}: {EmployeesProjects},\n" +
                $"\t{nameof(InverseManager)}: {InverseManager}\n" +
                $"}}";
        }
    }
}