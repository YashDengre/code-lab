using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptRevision.Questions
{
    public class LinqPlayGround
    {
        public static void Run()
        {
            LinqPlayGround.FindEmployeeNamesIfOlderThan30();
            LinqPlayGround.GroupEmployeesByDept();
            LinqPlayGround.InnerJoinEmployeeDepartment();
            LinqPlayGround.FindHighestSalaryInDepartment();
            LinqPlayGround.GetTop3CustomersByPurchaseAmount();
            LinqPlayGround.GetDistinctCharacterInOrder();
        }

        private static List<Employee> employees =
            [
            new Employee { Id = 1, Name = "Amit", Age = 28, Department = 2, Salary = 50000 },
            new Employee { Id = 2, Name = "Yash", Age = 32, Department = 3, Salary = 45000 },
            new Employee { Id = 3, Name = "Neha", Age = 35, Department = 2, Salary = 70000 },
            new Employee { Id = 4, Name = "John", Age = 25, Department = 1, Salary = 60000 },
            new Employee { Id = 5, Name = "Carl", Age = 31, Department = 3, Salary = 52000 },
            new Employee { Id = 6, Name = "Nyro", Age = 29, Department = 1, Salary = 90000 },

            ];

        private static List<Department> departments =
            [
            new Department { Id=1, Name="Finance" },
            new Department { Id=2, Name="IT" },
            new Department { Id=3, Name="HR" }
            ];
        public static void Play() { }

        public static void FindEmployeeNamesIfOlderThan30()
        {
            //Get names of all employees older than 30.
            var EmployeesOlderThan30 = employees.Where(e => e.Age > 30);
            var stringNames = EmployeesOlderThan30.Select(e => e.Name).ToList();
            foreach (var name in stringNames)
            {
                Console.WriteLine(name);
            }
        }
        public static void GroupEmployeesByDept()
        {
            //Group employees by department and return department name + count.
            var group = employees.GroupBy(e => e.Department);
            foreach (var employee in group)
            {
                Console.WriteLine($"{employee.Key}: {employee.Count()}");
            }
        }
        public static void InnerJoinEmployeeDepartment()
        {
            //Perform an inner join on Employee and Department and return:
            //EmployeeName, DepartmentName
            var empJoinDept = employees.Join(departments,
                employee => employee.Department, department => department.Id,
                (employee, department) => new { EmployeeName = employee.Name, DepartmentName = department.Name });

            foreach (var data in empJoinDept)
            {
                Console.WriteLine($"{data.EmployeeName} - {data.DepartmentName}");
            }
        }

        public static void FindHighestSalaryInDepartment()
        {
            //Find the highest salary in the IT department.
            var group = employees.GroupBy(e => e.Department);
            foreach (var employee in group)
            {
                Console.WriteLine($"{departments.FirstOrDefault(x => x.Id == employee.Key)?.Name}: {employee.Max(x => x.Salary)}");
            }
        }

        public static void GetTop3CustomersByPurchaseAmount()
        {
            //From a list of orders, get the top 3 customers by total purchase amount.

            var orders = new List<Order>() {
                new Order { CustomerId=1, Amount=2000 },
                new Order { CustomerId=2, Amount=5000 },
                new Order { CustomerId=1, Amount=3000 },
                new Order { CustomerId=3, Amount=7000 },
                new Order { CustomerId=2, Amount=1000 },
                new Order { CustomerId=2, Amount=1000 },
                new Order { CustomerId=4, Amount=11000 },
            };

            var top3 = orders.GroupBy(o => o.CustomerId).Select(g => new
            {
                CustomerId = g.Key,
                TotalAmount = g.Sum(x => x.Amount)
            }).OrderByDescending(x => x.TotalAmount).Take(3).ToList();

            foreach (var c in top3)
            {
                Console.WriteLine($"CustomerId: {c.CustomerId}, TotalAmount: {c.TotalAmount}");
            }

            var top3Query = (from o in orders
                             group o by o.CustomerId into g
                             select new
                             {
                                 CustomerId = g.Key,
                                 TotalAmount = g.Sum(x => x.Amount)
                             })
                             .OrderByDescending(x => x.TotalAmount).Take(3).ToList();


        }

        public static void GetDistinctCharacterInOrder()
        {
            //Return distinct characters from a string in alphabetical order
            string text = "yashdengre";
            var distinct = text.GroupBy(x => x).Select(g => g.Key).OrderBy(x => x);

            foreach (var c in distinct)
            {
                Console.WriteLine(c);
            }
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; } = 0;
        public int Department { get; set; }
        public int Salary
        {
            get; set;
        }
    }
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Order
    {
        public int CustomerId { get; set; }
        public int Amount { get; set; }
    }
}

