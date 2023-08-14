using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace Banking_system_project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string Language;
            Console.WriteLine("\n\n\n\n");
            Banking_system.displaytitle("Welcome To Our ATM Machine");
            Console.Write("\n\t\t\t\t   select Language: English  Or French : ");
            Language = Console.ReadLine().Trim().ToUpper().Substring(0,1);
            if (Language == "E")
            {
                Console.Clear();
                Banking_system.start();
            }
            else if (Language =="F")
            {
                Console.Clear();
                BankingSystemFrench.start();
            }
            else
            {
                Console.WriteLine("Enter A Valid information");
            }
        }
    }
}
