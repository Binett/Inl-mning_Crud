using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Net.Http.Headers;
using System.Text;

namespace Inlämning_Crud
{
    class ProgramLogic
    {
        internal void Run()
        {
            var db = new SQLDatabase();
            db.CreateDatabase();
            Menu();
        }
        private void Menu()
        {
            while (true)
            {
                Console.WriteLine("1. Add Person");
                Console.WriteLine("2. Show people");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        AddPerson();
                        break;
                    case "2":
                        Read();
                        break;
                }
            }
        }
        private void Read()
        {
            while (true)
            {
                var db = new SQLDatabase();
                Console.WriteLine("What do you wanna search for? ");
                Console.WriteLine("1. People starting with a certain letter");
                Console.WriteLine("2. People born a certain year");
                Console.WriteLine("3. People misssing data");
                Console.WriteLine("4. Show parents to a certain person");
                Console.WriteLine("5. Show siblings to a certain person");
                Console.WriteLine("6. Exit to main menu");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        ShowPeopleByLetter();
                        break;
                    case "2":
                        ShowWhenPeopleBorn();
                        break;
                    case "3":
                        ShowPeopleMissingData();
                        break;
                    case "4":
                        ShowParents();
                        break;
                    case "5":
                        ShowSiblings();
                        break;
                    case "6":
                        Menu();
                        break;
                } 
            }

        }

        private void ShowSiblings()
        {
           
        }

        private void ShowParents()
        {
            var db = new SQLDatabase();
            Console.Write("Enter name:");
            var getPerson = Console.ReadLine();
            PrintAllPerson(db.ShowAllFrom(getPerson));
            Console.Write("Choose ID to show parents: ")
                ;
            Console.ReadLine();
        }

        private void ShowPeopleMissingData()
        {
            while (true)
            {
                var db = new SQLDatabase();
                Console.WriteLine("Missing Infos Menu");
                Console.WriteLine("1. Show persons missing date of birth");
                Console.WriteLine("2. Show persons missing date of death");
                Console.WriteLine("3. Show persons missing MotherId");
                Console.WriteLine("4. Show persons missing FatherID");
                Console.WriteLine("5. Exit to main menu");
                var sql = "";
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                         sql = "Where born=0";
                        break;
                    case "2":
                         sql = "Where died=0";
                        break;
                    case "3":
                         sql = "Where motherId=0";
                        break;
                    case "4":
                         sql = "Where fatherId=0";
                        break;
                    case "5":
                        Menu();
                        break;
                }
                PrintAllPerson(db.ShowAllFrom(sql));
            }
        }
        private void ShowWhenPeopleBorn()
        {
            var db = new SQLDatabase();
            Console.Write("Enter year: ");
            var year = Convert.ToInt32(Console.ReadLine());
            var sql = "Where born = @born";

            PrintAllPerson(db.ShowAllFrom(sql, ("@born", $"{year}")));   
        }
        private void ShowPeopleByLetter()
        {
            var db = new SQLDatabase();
            Console.Write("Enter a letter: ");
            var letter = Console.ReadLine();
            var sql = "Where firstName LIKE '%'+ @letter +'%'";
            PrintAllPerson(db.ShowAllFrom(sql, ("@letter", letter)));
         
            Console.ReadLine();
        }
        private void AddPerson()
        {
            var db = new SQLDatabase();
            var person = db.CreatePerson();
            Console.WriteLine($"{person.FirstName} was added");
        }
        public static void PrintAllPerson(DataTable dt)
        {
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    Console.Write(" " + dt.Columns[i].ColumnName + ": " );
                    Console.Write(dt.Rows[j].ItemArray[i]);
                }
                Console.WriteLine();
            } 
        }
    }
}
