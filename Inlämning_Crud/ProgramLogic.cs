using System;
using System.Collections.Generic;
using System.Data;

namespace Inlämning_Crud
{
    internal class ProgramLogic
    {
        internal void Run()
        {
            var db = new SQLDatabase();
            db.CreateDatabase();
            Menu();
        }

        /// <summary>
        /// StartMeny
        /// </summary>
        private void Menu()
        {
            while (true)
            {
                Console.WriteLine("1. Add Person");
                Console.WriteLine("2. Show people");
                Console.WriteLine("3. Update person");
                Console.WriteLine("4. Delete person");
                Console.WriteLine("5. Exit application");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        AddPerson();
                        break;

                    case "2":
                        ListPeople();
                        break;

                    case "3":
                        UpdatePerson();
                        break;

                    case "4":
                        DeletePerson();
                        break;

                    case "5":
                        Environment.Exit(0);
                        break;
                }
            }
        }

        /// <summary>
        /// Raderar person
        /// </summary>
        private void DeletePerson()
        {
            var db = new SQLDatabase();
            Console.WriteLine("Enter name of the person you wanna update");
            var name = Console.ReadLine();
            var people = db.GetPersons(name);
            PrintList(people);
            if (people.Count > 0)
            {
                Console.WriteLine("Choose an id of an person you would like to delete!");
                var userId = Convert.ToInt32(Console.ReadLine());
                db.Delete(userId);
            }
            else
            {
                Console.WriteLine("No match!");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Hämtar personer och uppdaterar dess data
        /// </summary>
        private void UpdatePerson()
        {
            var db = new SQLDatabase();
            Console.WriteLine("Enter name of the person you wanna update");
            var name = Console.ReadLine();
            var people = db.GetPersons(name);
            if (people.Count > 0)
            {
                PrintList(people);
                Console.WriteLine("Choose an id of an person you would like to update!");
                var userId = Convert.ToInt32(Console.ReadLine());
                var person = db.GetPersons(userId);
                bool isRunning = true;
                while (isRunning)
                {
                    Console.Clear();
                    PrintPerson(person);
                    Console.WriteLine("What do you wanna change?");
                    Console.WriteLine("1. First name: ");
                    Console.WriteLine("2. Last Name ");
                    Console.WriteLine("3. Date of birth ");
                    Console.WriteLine("4. Date of death ");
                    Console.WriteLine("5. Mother");
                    Console.WriteLine("6. Father");
                    Console.WriteLine("7. Exit to main Menu");

                    var input = Console.ReadLine();
                    if (int.TryParse(input, out int choice))
                    {
                        switch (choice)
                        {
                            case 1:
                                Console.Write("Enter first name: ");
                                person.FirstName = Console.ReadLine();
                                break;

                            case 2:
                                Console.Write("Enter first name: ");
                                person.LastName = Console.ReadLine();
                                break;

                            case 3:
                                Console.Write("Enter Date of birth: ");
                                person.Born = Convert.ToInt32(Console.ReadLine());
                                break;

                            case 4:
                                Console.Write("Enter Date of birth: ");
                                person.Died = Convert.ToInt32(Console.ReadLine());
                                break;

                            case 5:
                                var mother = GetParent("mother");
                                if (mother != null)
                                {
                                    person.Mother = mother.Id;
                                }
                                break;

                            case 6:
                                var father = GetParent("father");
                                if (father != null)
                                {
                                    person.Father = father.Id;
                                }
                                break;

                            case 7:
                                isRunning = false;
                                break;
                        }
                        db.Update(person);
                    }
                }
            }
        }

        /// <summary>
        /// Hämtar föräldrar
        /// </summary>
        /// <param name="type">om det är mor eller far</param>
        /// <returns>Förhoppningsvis en person annars kommer alternativet att skapa upp dem</returns>
        private Person GetParent(string type)
        {
            var db = new SQLDatabase();
            Console.Write($"Enter your {type}´s name: ");
            var name = Console.ReadLine();
            var persons = db.GetPersons(name);
            var ctr = 1;
            foreach (var person in persons)
            {
                var info = $"{ctr++}. {person.FirstName} {person.LastName} Born: {person.Born}";
                Console.WriteLine(info);
            }
            Console.WriteLine("0. None of the above");

            var option = ChoosePerson(persons.Count);
            if (option > 0)
            {
                return persons[option - 1];
            }
            else
            {
                Console.WriteLine($"{name} doesn't seem to exist in the database!");
                Console.Write($"Do you want to create {name}(y/n)? ");
                var choice = Console.ReadLine();
                if (string.Equals(choice, "y", StringComparison.OrdinalIgnoreCase))
                {
                    AddPerson();
                }
            }

            return null;
        }

        /// <summary>
        /// Listar personer från databasen beroende på vad vi vill visa
        /// </summary>
        private void ListPeople()
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
                        Parents();
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

        /// <summary>
        /// Visar syskon
        /// </summary>
        private void ShowSiblings()
        {
            var db = new SQLDatabase();
            Console.Write("Enter name: ");
            var name = Console.ReadLine();
            var persons = db.GetPersons(name);
            if (persons.Count > 0)
            {
                PrintList(persons);
                Console.Write("Enter person: ");
                var userId = Convert.ToInt32(Console.ReadLine());
                var person = db.GetPersons(userId);
                var siblings = db.GetSiblings(person);
                PrintList(siblings);
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("No match, do you wanna add person(y/n)");
                var choice = Console.ReadLine();
                if (string.Equals(choice, "y", StringComparison.OrdinalIgnoreCase))
                {

                }
            }
           
        }

        //TODO: Ändra
        /// <summary>
        /// Visar föräldrar
        /// </summary>        
        private void Parents()
        {
            var db = new SQLDatabase();
            Console.Write("Enter name: ");
            var name = Console.ReadLine();
            var persons = db.GetPersons(name);
            PrintList(persons);
            Console.Write("Enter person: ");
            var userId = Convert.ToInt32(Console.ReadLine());

            var databas = new SQLDatabase();
            var dt = databas.Read(userId);
            DataRow rad = dt;
            var fname = rad["FirstName"].ToString();
            var lname = rad["LastName"].ToString();
            var mother = (int)rad["motherId"];
            var father = (int)rad["FatherId"];
            Console.WriteLine($"{fname} {lname}");
            if (mother > 0)
            {
                dt = databas.Read(mother);
                rad = dt;
                fname = rad["FirstName"].ToString();
                lname = rad["LastName"].ToString();
                Console.WriteLine($"Mother: {fname} {lname}");
            }
            if (father > 0)
            {
                dt = databas.Read(father);
                rad = dt;
                fname = rad["FirstName"].ToString();
                lname = rad["LastName"].ToString();
                System.Console.WriteLine($"Father: {fname} {lname}");
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Visar Personer som saknar data i databasen
        /// </summary>
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

        /// <summary>
        /// Listar personer efter år
        /// </summary>
        private void ShowWhenPeopleBorn()
        {
            try
            {
                var db = new SQLDatabase();
                Console.Write("Enter year: ");
                var year = Convert.ToInt32(Console.ReadLine());
                var sql = "Where born = @born";

                PrintAllPerson(db.ShowAllFrom(sql, ("@born", $"{year}")));
            }
            catch
            {
                Console.WriteLine("Invalid Input, try again! ");
            }
        }

        /// <summary>
        /// Sök Personer som börjar på ....
        /// </summary>
        private void ShowPeopleByLetter()
        {
            try
            {
                var db = new SQLDatabase();
                Console.Write("Enter a letter: ");
                var letter = Console.ReadLine();
                var sql = "Where firstName LIKE @letter +'%'";
                PrintAllPerson(db.ShowAllFrom(sql, ("@letter", letter)));
            }
            catch
            {
                Console.WriteLine("Invalid input, try again");
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Lägger till Personer till Databasen
        /// </summary>
        private void AddPerson()
        {
            var db = new SQLDatabase();
            var person = db.CreatePerson();
            Console.WriteLine($"{person.FirstName} was added");
        }

        /// <summary>
        /// Skriver ut personer från en tabell
        /// </summary>
        /// <param name="dt"></param>
        private static void PrintAllPerson(DataTable dt)
        {
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    Console.Write(" " + dt.Columns[i].ColumnName + ": ");
                    Console.Write(dt.Rows[j].ItemArray[i]);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Skriver ut ett objekt
        /// </summary>
        /// <param name="person">skicka med ett namn</param>
        private static void PrintPerson(Person person)
        {
            Console.WriteLine($"Id: {person.Id} Name : {person.FirstName} {person.LastName} " +
                      $"Born: {person.Born}  Died: {person.Died}  " +
                      $"MotherId: {person.Mother}" +
                      $"FatherId :{person.Father}");
        }

        /// <summary>
        /// Skriver ut en lista med personer
        /// </summary>
        /// <param name="people"></param>
        private void PrintList(List<Person> people)
        {
            foreach (var person in people)
            {
                Console.WriteLine($"Id: {person.Id} Name : {person.FirstName} {person.LastName} " +
                    $"Born: {person.Born}  Died: {person.Died}  " +
                    $"MotherId: {person.Mother}" +
                    $"FatherId :{person.Father}");
            }
        }

        //TODO: Refakturera bort!!!
        /// <summary>
        /// Väljer en person
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private int ChoosePerson(int count)
        {
            while (true)
            {
                Console.Write("Which person do you want to choose? ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice > 0 && choice >= count)
                    {
                        return choice;
                    }
                    else if (choice == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. try again!");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice. try again!");
                }
            }
        }
        
    }
}