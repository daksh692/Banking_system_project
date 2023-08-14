using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking_system_project
{
    public static class BankingSystemFrench
    {
        public struct Information
        {
            public string AccountNum;
            public string PinNum;
            public string Name;
            public Single Balance;
        }
        //acc
        static int outout, Nb_Details, maxAcc = 200;
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
            if (size > maxAcc)
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
            bool found = false;

            for (int i = 0; i < maxAcc; i++)
            {
                if (Detail[i].AccountNum == accountnum)
                {
                    outout = i;
                    found = true;

                    checkpin(outout);
                }
            }
            if (found == false)
            {
                Console.Write("Oups !!! Aucune correspondance trouvée \n Pour réessayer, appuyez sur n'importe quelle touche pour saisir à nouveau");
                string temp = Console.ReadLine();

                pause_login();
            }


            return accountnum;
        }

        private static void checkpin(int outout)
        {
            Console.Clear();
            displaytitle("BANQUE ROYALE");
            displaysubtitle("Entrez votre NIP : ");
            Console.WriteLine("\t\t\tBienvenue " + Detail[outout].Name + "\n\n");
            Console.Write("entrez votre NIP : ");
            string pin = Console.ReadLine();

            if (Detail[outout].PinNum.ToUpper() == pin.Trim().ToUpper())
            {
                Console.WriteLine("Correspondance trouvée");
                diplaymenue();

            }
            else
            {
                Console.Write("Ouf !!! correspondance introuvable\n appuyez sur n'importe quelle touche pour entrer à nouveau");
                string temp = Console.ReadLine();
                checkpin(outout);

            }
        }

        private static void diplaymenue()
        {
            int choice;
            Console.Clear();
            displaytitle("BANQUE ROYALE");
            displaysubtitle("Sélectionnez l'action que vous souhaitez effectuer : ");

            Console.WriteLine("Entrez 1 pour déposer de l'argent ");
            Console.WriteLine("Entrez 2 pour retirer de l'argent ");
            Console.WriteLine("Entrez 3 pour consulter");
            Console.Write("Entrez votre choix : ");
            choice = Convert.ToInt32(Console.ReadLine());
            while (choice < 1 || choice > 3)
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
            displaysubtitle("Votre solde actuel est le suivant :");

            Console.WriteLine("\nVotre numéro de compte est :" + Detail[outout].AccountNum + "\n Votre NIP est : "
                + Detail[outout].PinNum + "\n Ton nom est :" + Detail[outout].Name + "\nVotre solde actuel est : $"
                + Detail[outout].Balance);
            Console.WriteLine("Merci d'utiliser notre système " + Detail[outout].Name + "\nAppuyez sur n'importe quelle touche pour vous déconnecter ");
            string temp = Console.ReadLine();
            WriteArray2File();
            Environment.Exit(0);

        }

        private static void removemoney()
        {
            Single amount;
            Console.Clear();
            displaytitle("BANQUE ROYALE");
            displaysubtitle("Combien d'argent voulez-vous retirer ? ");
            Console.Write("Entrez le montant que vous souhaitez retirer : ");
            amount = Single.Parse(Console.ReadLine());
            if (amount > 19 && amount < 501)
            {
                if (amount % 20 == 0)
                {
                    if (amount < Detail[outout].Balance)
                    {
                        Detail[outout].Balance = Detail[outout].Balance - amount;
                        Console.WriteLine("Votre argent a été déduit \votre solde actuel est de : $" + Detail[outout].Balance);
                        Console.Write("Appuyez sur n'importe quelle touche pour revenir au menu");
                        string temp = Console.ReadLine();
                        displayBalance();

                    }
                    else
                    {
                        Console.Write("Oups !! Solde insuffisant\n Appuyez sur n'importe quelle touche Retapez le montant");
                        string temp = Console.ReadLine();
                        removemoney();

                    }
                }
                else
                {
                    Console.Write("Entrez un montant valide qui est un multiple de 20 \n Appuyez sur n'importe quelle touche Entrez à nouveau le montant ");
                    string temp = Console.ReadLine();
                    removemoney();
                }


            }
            else
            {
                Console.WriteLine(" Entrez un montant valide entre 20 et 500 qui est un multiple de 20");
                string temp = Console.ReadLine();
                removemoney();

            }


        }

        private static void AddMoney()
        {
            Single amount = 0;

            Console.Clear();
            displaytitle("BANQUE ROYALE");
            displaysubtitle("Combien d'argent voulez-vous déposer ?");
            Console.Write("Entrez le montant que vous souhaitez déposer : $");
            amount = Single.Parse(Console.ReadLine());


            if (amount > 1 && amount < 20001)
            {
                //to Add amount in his account
                Detail[outout].Balance = Detail[outout].Balance + amount;
                Console.WriteLine("Processus terminé \n Votre nouveau solde est : $" + Detail[outout].Balance);
                Console.Write("Appuyez sur n'importe quelle touche pour continuer");
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
            Console.WriteLine("\n\n\n\n\n\n\n\n\t\t\t\t\t\t BIENVENUE À \n");
            Console.WriteLine("\t\t\t\t\t\t" + name);
            Console.Write("\t\t\t\t\t      ");
            for (int i = 0; i < name.Length + 4; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("\n\n");

            Console.Write("\t\t\t\t");
            Console.Write("Appuyez sur n'importe quelle touche pour continuer: ");
            string temp = Console.ReadLine();
            pause_login();

            return name;
        }

        private static void pause_login()
        {

            Console.Clear();
            displaytitle("BANQUE ROYALE");
            displaysubtitle("Guichet Autonatique Bancaire");
            Console.Write("Entrez votre numéro de compte ATM : ");
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
                Console.Write("Ouf !!! Nombre d'entrées incorrect, vérifiez le nombre d'entrées et réessayez\nAppuyez sur n'importe quelle touche pour réessayer");
                string temp = Console.ReadLine();
                pause_login();

            }

            return account_number;


        }

        private static void displaysubtitle(string subtitle)
        {
            Console.WriteLine("\t\t\t\t" + subtitle);
            Console.Write("\t\t\t      ");
            for (int i = 0; i < subtitle.Length + 4; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("");

        }

        private static void displaytitle(string title)
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
