using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Banking_system_project
{
    public static class Banking_system
    {
        public struct Information
        {
            public string AccountNum;   
            public string PinNum;
            public string Name;
            public Single Balance;
        }
        //acc
        static int  outout, Nb_Details,maxAcc=200;
        static Information[] Detail;
        static string AccNum;




        public static void start()
        {
            
            ReadFile2Array();
            DisplayWelcomePage("BANQUE ROYALE");
            
            
        }

        private static void WriteArray2File()
        {
            StreamWriter myfile = new StreamWriter("Accounts.txt");
            for (int i = 0; i < Nb_Details; i++)
            {
                myfile.WriteLine(Detail[i].AccountNum);
                myfile.WriteLine(Detail[i].PinNum);
                myfile.WriteLine(Detail[i].Name);
                if (i != (Nb_Details - 1))
                {
                    myfile.WriteLine(Detail[i].Balance);
                }
                else
                {
                    myfile.Write(Detail[i].Balance);
                }

            }
            myfile.Close();

        }

        private static void ReadFile2Array()
        {

            //open file to read the number of line 
            int nblines = 0;
            StreamReader myfile = new StreamReader("Accounts.txt");
            while (myfile.EndOfStream == false)
            {
                string tmp = myfile.ReadLine();
                nblines++;
            }

            myfile.Close();
            Int16 size = Convert.ToInt16((nblines / 4) + 10);
            if(size > maxAcc) 
            {
                maxAcc = size;
            }

            // giving new size to array if 
            Detail = new Information[maxAcc];

            // opean file to store the data in array

             myfile = new StreamReader("Accounts.txt");
            int i = 0;
            while (myfile.EndOfStream == false)
            {

                Detail[i].AccountNum = myfile.ReadLine();
                Detail[i].PinNum = myfile.ReadLine();
                Detail[i].Name = myfile.ReadLine();
                Detail[i].Balance = Convert.ToSingle(myfile.ReadLine());

                i++;
            }

            Nb_Details = i;
            myfile.Close();

        }

        private static string CheckeTheAccNum(string accountnum)
        {
            bool found=false;
            
            for(int i = 0; i < maxAcc; i++)
            {
                if (Detail[i].AccountNum == accountnum)
                {
                    outout = i;
                    found =true;
                    
                    checkpin(outout);
                }
            }
            if (found == false)
            {
                Console.WriteLine("Opss !!! No Match found \n To try again press any key to re-enter");
                string temp = Console.ReadLine();

                pause_login();
            }


            return accountnum;
        }

        private static void checkpin(int outout)
        {
            Console.Clear();
            displaytitle("BANQUE ROYALE");
            displaysubtitle("Enter Your Pin Number: ");
            Console.WriteLine("\n\t\t\t\t  Welcome " + Detail[outout].Name+"\n\n");
            Console.Write("enter your Pin: ");
            string pin =Console.ReadLine();
            
            if (Detail[outout].PinNum.ToUpper() == pin.Trim().ToUpper())
            {
                Console.WriteLine("Match found");
                diplaymenue();

            }
            else
            {
                Console.Write("Opps!!! match not found\n press any key to reenter");
                string temp =Console.ReadLine();
                checkpin(outout);

            }
        }

        private static void diplaymenue()
        {
            int choice;
            Console.Clear ();
            displaytitle("BANQUE ROYALE");
            displaysubtitle("Select Which Action Do you want To Perform: ");
            
            Console.WriteLine("Enter 1 to Add money ");
            Console.WriteLine("Enter 2 to remove money ");
            Console.WriteLine("Enter 3 to to consult ");
            Console.Write("Enter your choice : ");
            choice=Convert.ToInt32(Console.ReadLine());
            while(choice <1||choice>3) 
            {
                diplaymenue();
            }

            switch (choice)
            {
                case 1:
                    AddMoney();
                    break; 

                case 2:
                    removemoney();
                    break;
                case 3:
                    Console.Clear();
                    displayBalance();
                    break;

            }


        }

        private static void displayBalance()
        {
            Console.Clear();
            displaytitle("BANQUE ROYALE");
            displaysubtitle("Your Current Balance is As follow:");
            
            Console.WriteLine("\nYour account number is :"+Detail[outout].AccountNum+"\nYour Pin Is : "
                + Detail[outout].PinNum+"\nYour name Is : "+ Detail[outout].Name+"\nYour current Balance is : $"
                + Detail[outout].Balance);
            Console.WriteLine("Thanks For using our system " + Detail[outout].Name+"\nPress any key to logout ");
            string temp=Console.ReadLine();
            WriteArray2File();
            Environment.Exit(0);

        }

        private static void removemoney()
        {
            Single amount;
            Console.Clear();
            displaytitle("BANQUE ROYALE");
            displaysubtitle("How Much Money Do You Want To Withdraw? ");
            Console.Write("Enter Amout You Want Withdraw : ");
            amount = Single.Parse(Console.ReadLine());
            if (amount > 19 && amount < 501)
            {
                if(amount % 20 == 0)
                {
                    if (amount < Detail[outout].Balance)
                    {
                        Detail[outout].Balance = Detail[outout].Balance - amount;
                        Console.WriteLine("Your money Has been deducted \nyour current balance is : $" + Detail[outout].Balance);
                        Console.Write("Press Any key to return to menue ");
                        string temp = Console.ReadLine();
                        displayBalance();

                    }
                    else
                    {
                        Console.Write("Opss!! Insufesent Balance\n Press any Key Re-Enter Amount ");
                        string temp = Console.ReadLine();
                        removemoney();

                    }
                }
                else
                {
                    Console.Write("Enter a valid amount Which is multiple of 20 \n Press any Key Re-Enter Amount ");
                    string temp = Console.ReadLine();
                    removemoney();
                }
               

            }
            else
            {
                Console.WriteLine(" Enter a valid Amount between 20-500 Which is multiple of 20");
                string temp = Console.ReadLine();
                removemoney();

            }
            
            
        }

        private static void AddMoney()
        {
            Single amount=0;
            
                Console.Clear();
            displaytitle("BANQUE ROYALE");
            displaysubtitle("How Much Money do you want to Deposite ?");
                Console.Write("Enter Amout You Want Deposite : $");
                amount = Single.Parse(Console.ReadLine());
                

            if (amount > 1 && amount < 20001)
            {
                //to Add amount in his account
                Detail[outout].Balance = Detail[outout].Balance + amount;
                Console.WriteLine("Proceess complected \n Your new balalance is : $" + Detail[outout].Balance);
                Console.Write("Press Any key to continue");
                string temp = Console.ReadLine();
                displayBalance();
            }
            else
            {
                
                AddMoney();
            }

        }

        private static string DisplayWelcomePage(string name)
        {
            Console.WriteLine("\n\n\n\n\n\n\n\n\t\t\t\t\t\t WELCOME TO \n");
            Console.WriteLine("\t\t\t\t\t\t" + name);
            Console.Write("\t\t\t\t\t      ");
            for (int i  = 0; i < name.Length+4; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("\n\n");

            Console.Write("\t\t\t\t\t");
            Console.Write("Press Any Key To continue: ");
            string temp = Console.ReadLine();
            pause_login();

            return name;   
        }

        private static void pause_login()
        {

            Console.Clear();
            displaytitle("BANQUE ROYALE");
            displaysubtitle("Automated teller machine");
            Console.Write("Enter your atm account Number : ");
            AccNum = Login_page(Console.ReadLine());
            Login_page(AccNum);
            CheckeTheAccNum(AccNum);

        }

        private static string Login_page(string account_number)
        {

    

            int lenght = account_number.Length;
            //calculating and cheking the pass word length it should be greter than 4 amd less than 16
            if (lenght > 16 || lenght < 4)
            {
                Console.Write("Opps!!!  Improper numbers  of input check the numbers of input and retry\nPress any key to Re-Try");
                string temp= Console.ReadLine();
                pause_login();

            }

            return account_number;


        }

        public static void displaysubtitle(string subtitle)
        {
            Console.WriteLine("\t\t\t\t" + subtitle);
            Console.Write("\t\t\t      ");
            for (int i = 0; i < subtitle.Length + 4; i++)
            {
                Console.Write("-");
            }
            Console.Write("");

        }

        public static void displaytitle(string title)
        {
            Console.WriteLine("\t\t\t\t\t" + title);
            Console.Write("\t\t\t\t      ");
            for (int i = 0; i < title.Length + 4; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("");

        }
    
        
    
    
    }
}
