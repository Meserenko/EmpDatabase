﻿// Fig. 12.5: SalariedEmployee.cs
// SalariedEmployee class that extends Employee.
using System;

namespace EmpDB
{
    public class SalariedEmployee : Employee
    {
        public decimal weeklySalary { get; set; }


        // four-parameter constructor
        public SalariedEmployee(string firstName, string lastName,
           string socialSecurityNumber, string emailAddress, decimal weeklySalary)
           : base(firstName, lastName, socialSecurityNumber, emailAddress)
        {
            WeeklySalary = weeklySalary; // validate salary via property
        }

        // property that gets and sets salaried employee's salary
        public decimal WeeklySalary
        {
            get
            {
                return weeklySalary;
            }
            set
            {
                if (value < 0) // validation
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                       value, $"{nameof(WeeklySalary)} must be >= 0");
                }

                weeklySalary = value;
            }
        }

        // calculate earnings; override abstract method Earnings in Employee
        public override decimal Earnings() => WeeklySalary;
        //this codes are the same
        //  public decimal Earning2()
        //  {
        //    return WeeklySalary;
        //  }

        // return string representation of SalariedEmployee object
        public override string ToString() =>
           $"salaried employee: {base.ToString()}\n" +
           $"weekly salary: {WeeklySalary:C}";
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