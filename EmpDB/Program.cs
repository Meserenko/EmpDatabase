using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpDB

{
    internal class Program
    {
        static void Main(string[] args)
        {
            //PayrollSystemTest test = new PayrollSystemTest();
            //test.TestMain();

            PayrollSystemTest app = new PayrollSystemTest();
            app.GoDatabase();

        }
    }
}