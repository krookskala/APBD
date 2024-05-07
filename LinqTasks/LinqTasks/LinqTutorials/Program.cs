using System;

namespace LinqTutorials
{
    class Program
    {
        static void Main(string[] args)
        {
            // Task 1: Backend Programmers
            Console.WriteLine("Task 1: Backend Programmers");
            foreach (var emp in LinqTasks.Task1())
            {
                Console.WriteLine($"{emp.Ename} - {emp.Job}");
            }
            Console.WriteLine();

            // Task 2: Frontend Programmers with salary > 1000
            Console.WriteLine("Task 2: Frontend Programmers with Salary > 1000");
            foreach (var emp in LinqTasks.Task2())
            {
                Console.WriteLine($"{emp.Ename} - {emp.Salary}");
            }
            Console.WriteLine();

            // Task 3: Maximum Salary
            Console.WriteLine("Task 3: Maximum Salary");
            Console.WriteLine(LinqTasks.Task3());
            Console.WriteLine();

            // Task 4: Employees with the Maximum Salary
            Console.WriteLine("Task 4: Employees with the Maximum Salary");
            foreach (var emp in LinqTasks.Task4())
            {
                Console.WriteLine($"{emp.Ename} - {emp.Salary}");
            }
            Console.WriteLine();

            // Task 5: Select ename and job
            Console.WriteLine("Task 5: Select Ename and Job");
            foreach (dynamic item in LinqTasks.Task5())
            {
                Console.WriteLine($"{item.Nazwisko} - {item.Praca}");
            }
            Console.WriteLine();

            // Task 6: Join Emps and Depts
            Console.WriteLine("Task 6: Join Emps and Depts");
            foreach (dynamic item in LinqTasks.Task6())
            {
                Console.WriteLine($"{item.Ename} - {item.Job} - {item.DeptName}");
            }
            Console.WriteLine();

            // Task 7: Count Employees by Job
            Console.WriteLine("Task 7: Count Employees by Job");
            foreach (dynamic item in LinqTasks.Task7())
            {
                Console.WriteLine($"{item.Praca} - {item.LiczbaPracownikow}");
            }
            Console.WriteLine();

            // Task 8: Any Backend Programmer?
            Console.WriteLine("Task 8: Is there any Backend Programmer?");
            Console.WriteLine(LinqTasks.Task8());
            Console.WriteLine();

            // Task 9: Most Recently Hired Frontend Programmer
            Console.WriteLine("Task 9: Most Recently Hired Frontend Programmer");
            var resultTask9 = LinqTasks.Task9();
            if (resultTask9 != null)
            {
                Console.WriteLine($"{resultTask9.Ename} - {resultTask9.HireDate}");
            }
            else
            {
                Console.WriteLine("No Frontend Programmer found.");
            }
            Console.WriteLine();

            // Task 10: Select and Union
            Console.WriteLine("Task 10: Select and Union Operation");
            foreach (dynamic item in LinqTasks.Task10())
            {
                Console.WriteLine($"{item.Ename} - {item.Job} - {item.HireDate}");
            }
            Console.WriteLine();

            // Task 11: Group Employees by Dept
            Console.WriteLine("Task 11: Group Employees by Department");
            foreach (dynamic group in LinqTasks.Task11())
            {
                Console.WriteLine($"{group.Name} - {group.NumOfEmployees}");
            }
            Console.WriteLine();

            // Task 12: Employees with Subordinates
            Console.WriteLine("Task 12: Employees with Subordinates");
            foreach (var emp in LinqTasks.Task12())
            {
                Console.WriteLine($"{emp.Ename} - {emp.Salary}");
            }
            Console.WriteLine();

            // Task 13: Number appearing odd times in an array
            Console.WriteLine("Task 13: Number appearing odd times in an array");
            int[] array = new[] { 1, 1, 1, 1, 1, 1, 10, 1, 1, 1, 1 };
            Console.WriteLine(LinqTasks.Task13(array));
            Console.WriteLine();

            // Task 14: Departments with exactly 5 employees or none
            Console.WriteLine("Task 14: Departments with exactly 5 employees or none");
            foreach (var dept in LinqTasks.Task14())
            {
                Console.WriteLine($"{dept.Dname} - {dept.Deptno}");
            }
            Console.WriteLine();

            // Task 15: Jobs containing 'A' with count > 2
            Console.WriteLine("Task 15: Jobs containing 'A' with count > 2");
            foreach (dynamic item in LinqTasks.Task15())
            {
                Console.WriteLine($"{item.Job} - {item.Count}");
            }
            Console.WriteLine();

            // Task 16: Select all from Emps and Depts
            Console.WriteLine("Task 16: Select all from Emps and Depts");
            foreach (dynamic item in LinqTasks.Task16())
            {
                Console.WriteLine($"{item.Ename} - {item.Job} - {item.Dname}");
            }
            Console.WriteLine();
        }
    }
}
