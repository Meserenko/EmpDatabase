﻿// Fig. 12.4: Employee.cs
// Employee abstract base class.

using System.Net.Mail;

namespace EmpDB
{
    public abstract class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string EmailAddress { get; set; }



        // four-parameter constructor
        public Employee(string firstName, string lastName,
           string socialSecurityNumber, string emailAddress)
        {
            FirstName = firstName;
            LastName = lastName;
            SocialSecurityNumber = socialSecurityNumber;
            EmailAddress = emailAddress;
        }
        public virtual string ToStringForDataFile()
        {
            string str = $"First: {FirstName}\n";
            str += $"Last: {LastName}\n";
            str += $"SSN: {SocialSecurityNumber}\n";
            str += $"Email: {EmailAddress}\n";

            return str;
        }
        // return string representation of Employee object, using properties
        public override string ToString() => $"{FirstName} {LastName}\n" +
           $"social security number: {SocialSecurityNumber} email: {EmailAddress}";

        // abstract method overridden by derived classes
        public abstract decimal Earnings(); // no implementation here
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