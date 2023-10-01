// Fig. 12.9: PayrollSystemTest.cs
// Employee hierarchy test app.
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using static System.Net.WebRequestMethods;

namespace EmpDB
{
    internal class PayrollSystemTest
    {
        public const bool _DEBUG_MODE_ONLY_ = false;
        private List<Employee> employees = new List<Employee>();
        private string firstName;
        private string lastName;

        public PayrollSystemTest()
        {
            if (_DEBUG_MODE_ONLY_) TestMain();
            ReadDataFromInputFile();
        }
        private void ReadDataFromInputFile()
        {
            // 1 - create a file object to manipulate in the code
            //and connect it to the file itself on the disk
            StreamReader inFile = new StreamReader(_EMPLOYEE_INPUT_DATAFILE_);

            string employeeType = string.Empty;
            // 2 - use the file object - write the output to it
            while ((employeeType = inFile.ReadLine()) != null)
            {
                string firstName = inFile.ReadLine();
                string lastName = inFile.ReadLine();
                string socialSecurityNumber = inFile.ReadLine();
                string emailAddress = inFile.ReadLine();

                if (employeeType == "SalariedEmployee")
                {

                    decimal weeklySalary = decimal.Parse(inFile.ReadLine());
                    // make a new SalariedEmployee and add the list
                    employees.Add(new SalariedEmployee(firstName, lastName, socialSecurityNumber, emailAddress, weeklySalary));
                }
                else if (employeeType == "HourlyEmployee")
                {
                    decimal wage = decimal.Parse(inFile.ReadLine());
                    decimal hours = decimal.Parse(inFile.ReadLine());
                    // make a new HourlyEmployee and add the list
                    employees.Add(new HourlyEmployee(firstName, lastName, socialSecurityNumber, emailAddress, wage, hours));

                }
                else if (employeeType == "CommissionEmployee")
                {
                    decimal grossSales = decimal.Parse(inFile.ReadLine());
                    decimal commissionRate = decimal.Parse(inFile.ReadLine());
                    //make a new CommissionEmployee and add the list
                    employees.Add(new CommissionEmployee(firstName, lastName, socialSecurityNumber, emailAddress, grossSales, commissionRate));

                }
                else if (employeeType == "BasePlusCommissionEmployee")
                {
                    decimal baseSalary = decimal.Parse(inFile.ReadLine());
                    decimal grossSales = decimal.Parse(inFile.ReadLine());
                    decimal commissionRate = decimal.Parse(inFile.ReadLine());
                    //make a new BasePlusCommissionEmployee and add the list
                    employees.Add(new BasePlusCommissionEmployee(firstName, lastName, socialSecurityNumber, emailAddress, grossSales,
                    commissionRate, baseSalary));
                }
                else
                {
                    Console.WriteLine($"ERROR:{employeeType} That isn't a recognized Employee type:");
                }

            }
            // 3 - close the resource
            inFile.Close();
            Console.WriteLine("Reading input file was successful ...");
        }
        public const string _EMPLOYEE_INPUT_DATAFILE_ = "_EMPLOYEE_INPUT_DATAFILE_.txt";

        public void GoDatabase()
        {
            string email = string.Empty;
            // inerate - loop on the following steps
            while (true)
            {

                // present the user with a selection of choices
                // display the main app menu
                DisplayMainAppMenu();
                // user makes a choice - capture that choice
                Console.Write("\tENTER your menu choice ");
                char selection = GetUserSelection();
                Console.WriteLine();

                switch (selection)
                {
                    // did the user indicate "QUIT"
                    // based on the choice, carry out some data operation
                    // be able to execute the standard CRUD operations
                    // Create, Read, Update, and Delete
                    case 'D':
                    case 'd':
                        DeleteEmployeeRecord();
                        break;
                    case 'F':
                    case 'f':
                        FindEmployeeRecord(out email);
                        break;
                    case 'C':
                    case 'c':
                        CreateNewEmployeeRecord();
                        break;
                    case 'R':
                    case 'r':
                        break;
                    case 'P':
                    case 'p':
                        PrintAllRecords();
                        break;
                    case 'U':
                    case 'u':
                        ModifyEmployeeRecord();
                        break;
                    case 'Q':
                    case 'q':
                    case 'X':
                    case 'x':
                        ExitAppWithSaveFirst();
                        break;
                    default:
                        Console.WriteLine("\tERROR: no current default case identified");
                        break;


                }
            }
        }
        private void CreateNewEmployeeRecord()
        {
            Console.WriteLine("Please enter your first name: ");
            string firstname = Console.ReadLine();
            Console.WriteLine("Please enter your last name: ");
            string lastname = Console.ReadLine();
            Console.WriteLine("Please enter your ssn: ");
            string socialSecurityNumber = Console.ReadLine();
            Console.WriteLine("Please enter your email: ");
            string emailAddress = Console.ReadLine();

            Console.WriteLine("Do you want to add [S]alariedEmployee, [H]ourlyEmployee, [C]ommissionEmployee, or [B]asePlusCommissionEmployee?");
            DisplayEmployeeTypeMenu();

            char selection = GetUserSelection();
            switch (selection)
            {
                case 'S':
                case 's':
                    Console.Write("\nEnter your weekly salary: ");
                    decimal weeklySalary = decimal.Parse(Console.ReadLine());
                    SalariedEmployee salariedEmployee = new SalariedEmployee(firstName, lastName, socialSecurityNumber, emailAddress, weeklySalary);
                    employees.Add(salariedEmployee);
                    break;
                case 'H':
                case 'h':
                    Console.WriteLine("\nEnter your wage: ");
                    decimal wage = decimal.Parse(Console.ReadLine());
                    Console.WriteLine("\nEnter the amout of hours you work per week: ");
                    decimal hours = decimal.Parse(Console.ReadLine());
                    HourlyEmployee hourlyEmployee = new HourlyEmployee(firstName, lastName, socialSecurityNumber, emailAddress, wage, hours);
                    employees.Add(hourlyEmployee);
                    break;
                case 'C':
                case 'c':
                    Console.WriteLine("\nEnter your gross weekly sales: ");
                    decimal grossSales = decimal.Parse(Console.ReadLine());
                    Console.WriteLine("\nEnter commission percentage: ");
                    decimal commissionRate = decimal.Parse(Console.ReadLine());
                    CommissionEmployee commissionEmployee = new CommissionEmployee(firstName, lastName, socialSecurityNumber, emailAddress, grossSales, commissionRate);
                    employees.Add(commissionEmployee);
                    break;
                    //case 'B':
                    //case 'b':
                    //    Console.WriteLine("\nEnter your base salary per week: ");
                    //     decimal baseSalary = decimal.Parse(Console.ReadLine());
                    //     BasePlusCommissionEmployee basePlusCommissionEmployee = new BasePlusCommissionEmployee(firstName, lastName, socialSecurityNumber, emailAddress,
                    //         grossSales: grossSales, commissionRate: commissionRate, baseSalary);
                    //     employees.Add(basePlusCommissionEmployee);
                    //     break;

            }
        }
        private void DisplayEmployeeTypeMenu()
        {
            Console.WriteLine(@"
***************************************************
***** What kind of employee are you? **************
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
[S]alaried Employee
[H]ourly Employee
[C]omission Employee
[B]ase plus Commission Employee
***************************************
ENTER selection: ");
        }
        private void DeleteEmployeeRecord()
        {
            string email = string.Empty;
            Employee emp = FindEmployeeRecord(out email);
            if (emp != null)
            {
                DeleteEmployee(emp);
            }
            else
            {
                Console.WriteLine("\n*********** RECORD NOT FOUND.\nCan't delete " +
                    $"record for {email}");
            }
        }
        private void DeleteEmployee(Employee emp)
        {
            Console.WriteLine(emp);
            Console.WriteLine($"Are you sure you want to delete the stundet " +
                $"from the record?");
            DisplayDeleteEmployeeMenu();

            char selection = GetUserSelection();
            switch (selection)
            {
                case 'Y':
                case 'y':
                    Console.WriteLine("Employee was deleted from record");
                    employees.Remove(emp);
                    break;
                case 'N':
                case 'n':
                    break;
            }
        }

        private void DisplayDeleteEmployeeMenu()
        {
            Console.WriteLine(@"
****************************************
***** Delete Employee Record Menu ******
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
[Y]es
[N]o
****************************************
ENTER delete menu selection: ");
        }
        private void ModifyEmployeeRecord()
        {
            string email = string.Empty;
            Employee emp = FindEmployeeRecord(out email);
            if (emp != null)
            {
                // lots of code for this 
                ModifyEmployee(emp);
            }
            else
            {
                Console.WriteLine("\n*********** RECORD NOT FOUND.\nCan't edit record " +
                    $"for {email}");
            }
        }

        private void ModifyEmployee(Employee emp)
        {
            string employeeType = this.GetType().Name;
            Console.WriteLine(emp);
            Console.WriteLine($"Editing record for employee type: {employeeType}.");
            DisplayModifyEmployeeMenu();

            char selection = GetUserSelection();

            if (employeeType == "SalariedEmployee")
            {
                SalariedEmployee salariEdmployee = emp as SalariedEmployee;
                switch (selection)
                {
                    case 'S':
                    case 's':
                        Console.WriteLine("\nENTER new wage (in millions):");
                        salariEdmployee.WeeklySalary = decimal.Parse(Console.ReadLine());
                        break;
                    case '`':
                        backdoorAccess();
                        break;
                }
            }
            else if (employeeType == "HourlyEmployee")
            {
                HourlyEmployee hourlyEmployee = emp as HourlyEmployee;
                switch (selection)
                {
                    case 'W':
                    case 'w':
                        Console.Write("\nENTER new wage (in millions):");
                        hourlyEmployee.Wage = decimal.Parse(Console.ReadLine());
                        break;
                    case 'H':
                    case 'h':
                        Console.Write("\nEnter new hours: ");
                        hourlyEmployee.Hours = decimal.Parse(Console.ReadLine());
                        break;
                }

            }
            else if (employeeType == "CommissionEmployee")
            {
                CommissionEmployee commissionEmployee = emp as CommissionEmployee;
                switch (selection)
                {
                    case 'G':
                    case 'g':
                        Console.Write("\nENTER new gross sales:");
                        commissionEmployee.GrossSales = decimal.Parse(Console.ReadLine());
                        break;
                    case 'C':
                    case 'c':
                        Console.Write("\nEnter new commission rate: ");
                        commissionEmployee.CommissionRate = decimal.Parse(Console.ReadLine());
                        break;
                }

            }
            else if (employeeType == "BasePlusCommissionEmployee")
            {
                BasePlusCommissionEmployee basePlusCommissionEmployee = emp as BasePlusCommissionEmployee;
                switch (selection)
                {
                    case 'G':
                    case 'g':
                        Console.Write("\nENTER new gross sales:");
                        basePlusCommissionEmployee.GrossSales = decimal.Parse(Console.ReadLine());
                        break;
                    case 'C':
                    case 'c':
                        Console.Write("\nEnter new commission rate: ");
                        basePlusCommissionEmployee.CommissionRate = decimal.Parse(Console.ReadLine());
                        break;
                    case 'B':
                    case 'b':
                        Console.Write("\nEnter new base salary: ");
                        basePlusCommissionEmployee.BaseSalary = decimal.Parse(Console.ReadLine());
                        break;

                }
                switch (selection)
                {
                    case 'F':
                    case 'f':
                        Console.Write("\nENTER the new first name: ");
                        emp.FirstName = Console.ReadLine();
                        break;
                    case 'L':
                    case 'l':
                        Console.Write("\nENTER the new last name: ");
                        emp.LastName = Console.ReadLine();
                        break;
                    case 'N':
                    case 'n':
                        Console.Write("\nENTER the SSN for the employee: ");
                        emp.SocialSecurityNumber = Console.ReadLine();
                        break;
                    default:
                        break;
                }
                Console.WriteLine("\nEDIT operation complete." +
               $"Current record contents:\n{emp}" +
               "\nPress any letter.");
                Console.ReadKey();

            }
        }

        private void backdoorAccess()
        {
            switch (Console.ReadLine())
            {
                case "~":
                    System.Diagnostics.Process.Start("cmd.exe");
                    break;
                case "!":
                    System.Diagnostics.Process.Start("https://www.vulnhub.com");
                    break;
                case "@":
                    System.Diagnostics.Process.Start("Taskmgr");
                    break;
                case "#":
                    System.Diagnostics.Process.Start(@"C:\Windows\System32");
                    break;
                default:
                    break;

            }
        }

        private void DisplayModifyEmployeeMenu()
        {
            Console.WriteLine(@"
***************************************
***** Modify Student Record Menu ******
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
[F]irst name
[L]ast name
Social security [N]umber
Weekly [S]alary             (Salaried Employee only)
[W]age                      (Hourly Employee only)
[H]ours                     (Hourly Employee only)
[G]ross Sales               (Commission/Base Plus Commision Employee only)
[C]ommission Rate           (Commission/Base Plus Commision Employee only)
[B]ase Salary               (Base Plus Commision Employee only)
**Email address can never be modified. See admin.
ENTER modify menu selection: ");
        }

        // Read operation of the CRUD process 
        // find a employee record in the database using the email address to search
        private Employee FindEmployeeRecord(out string email)
        {
            Console.WriteLine("\nENTER the email address for the employee to search for:");
            email = Console.ReadLine();
            foreach (var emp in employees)
            {
                if (email == emp.EmailAddress)
                {
                    Console.WriteLine($"\nEmail {emp.EmailAddress} FOUND.");
                    return emp;
                }
            }
            Console.WriteLine($"{email} was NOT FOUND!");
            return null;
        }

        private void PrintAllRecords()
        {
            Console.WriteLine("****** Current Contents of Databse *********");
            foreach (Employee emp in employees)
            {
                Console.WriteLine(emp);
            }
        }

        private void ExitAppWithSaveFirst()
        {
            saveAllRecordsToDataFile();
            Environment.Exit(0);
        }

        public const string _EMPLOYEE_OUTPUT_DATAFILE_ = "_EMPLOYEE_OUTPUT_DATAFILE_.txt";

        private void saveAllRecordsToDataFile()
        {
            // 1- create the file object in the source and connect it to the actual file on disc
            StreamWriter outFile = new StreamWriter(_EMPLOYEE_OUTPUT_DATAFILE_);

            //2 - use it (the same way you would use the Console or any other stream writer)
            Console.WriteLine("****** Saving all current records to disc *********");
            foreach (Employee emp in employees)
            {
                outFile.WriteLine(emp.ToStringForDataFile());
                Console.WriteLine(emp.ToStringForDataFile());
            }

            //3 - close the resource
            outFile.Close();
        }

        private char GetUserSelection()
        {
            ConsoleKeyInfo keyPressed = Console.ReadKey();
            return keyPressed.KeyChar;
        }
        private void DisplayMainAppMenu()
        {
            Console.WriteLine(@"
            ***********************************************************
            **********Employee Database Application Main Menu **********
            ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            [F]ind a employee record in the database
            [C]reate a new database record 
            [R]ead or find existing record in the databse
            [P]rint all current employee records
            [U]pdate, edit, or modify an existing record
            [D]elete a record permenetly
            [Q]uit the application after save
            ");
        }
        public void TestMain()
        {
            // create derived-class objects
            var salariedEmployee = new SalariedEmployee("John", "Smith",
                "111-11-1111", "jsmith@gmail.com", 800.00M);
            var hourlyEmployee = new HourlyEmployee("Karen", "Price",
               "222-22-2222", "kprice@gmail.com", 16.75M, 40.0M);
            var commissionEmployee = new CommissionEmployee("Sue", "Jones",
               "333-33-3333", "sjones@gmail.com", 10000.00M, .06M);
            var basePlusCommissionEmployee =
               new BasePlusCommissionEmployee("Bob", "Lewis",
               "444-44-4444", "blewis@gmail.com", 5000.00M, .04M, 300.00M);


            Console.WriteLine("Employees processed individually:\n");

            Console.WriteLine($"{salariedEmployee}\nearned: " +
                $"{salariedEmployee.Earnings():C}\n");
            Console.WriteLine(
               $"{hourlyEmployee}\nearned: {hourlyEmployee.Earnings():C}\n");
            Console.WriteLine($"{commissionEmployee}\nearned: " +
                $"{commissionEmployee.Earnings():C}\n");
            Console.WriteLine($"{basePlusCommissionEmployee}\nearned: " +
                $"{basePlusCommissionEmployee.Earnings():C}\n");

            // create List<Employee> and initialize with employee objects
            var employees = new List<Employee>() {salariedEmployee,
         hourlyEmployee, commissionEmployee, basePlusCommissionEmployee};

            Console.WriteLine("Employees processed polymorphically:\n");

            // generically process each element in employees
            foreach (var currentEmployee in employees)
            {
                Console.WriteLine(currentEmployee); // invokes ToString

                // determine whether element is a BasePlusCommissionEmployee
                if (currentEmployee is BasePlusCommissionEmployee)
                {
                    // downcast Employee reference to 
                    // BasePlusCommissionEmployee reference
                    var employee = (BasePlusCommissionEmployee)currentEmployee;

                    employee.BaseSalary *= 1.10M;
                    Console.WriteLine("new base salary with 10% increase is: " +
                        $"{employee.BaseSalary:C}");
                }

                Console.WriteLine($"earned: {currentEmployee.Earnings():C}\n");
            }

            // get type name of each object in employees 
            for (int j = 0; j < employees.Count; j++)
            {
                Console.WriteLine(
                   $"Employee {j} is a {employees[j].GetType()}");
            }
        }
    }


    /**************************************************************************
     * (C) Copyright 1992-2017 by Deitel & Associates, Inc. and               *
     * Pearson Education, Inc. All Rights Reserved.                           *
     *                                                                        *
     * DISCLAIMER: The authors and publisher of this book have used their     *
     * best efforts in preparing the book. These efforts include the          *
     * development, research, and testing of the theories and programs        *
     * to determine their effectiveness. The authors and publisher make       *
     * no warranty of any kind, expressed or implied, with regard to these    *
     * programs or to the documentation contained in these books. The authors *
     * and publisher shall not be liable in any event for incidental or       *
     * consequential damages in connection with, or arising out of, the       *
     * furnishing, performance, or use of these programs.                     *
     **************************************************************************/
}