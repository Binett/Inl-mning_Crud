using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

namespace Inlämning_Crud
{
    internal class SQLDatabase
    {
        public string ConnectionString { get; set; } = @"Data Source = .\SQLExpress; Integrated Security = true; database = {0}";
        public string DatabaseName { get; set; } = "Binett";

        /// <summary>
        /// Skapar databasen om databasen inte existerar skapas tabell och lägger till mockdata
        /// </summary>
        /// <returns></returns>
        public bool CreateDatabase()
        {
            try
            {
                DatabaseName = "master";
                ExecuteSQL("CREATE DATABASE Binett");
                DatabaseName = "Binett";
                CreateTable("Family");
                MockData.FillDataTable();
                return true;
            }
            catch
            {
                DatabaseName = "Binett";
                Debug.WriteLine("Database already exist");
                return false;
            }
        }

        /// <summary>
        /// SQL query för att skapa tabellen
        /// </summary>
        /// <param name="name"></param>
        internal void CreateTable(string name)
        {
            string sql = @$"CREATE TABLE {name}
                    (id int PRIMARY KEY IDENTITY (1,1) NOT NULL,
                    firstName nvarchar(50) NOT NULL,
                    lastName nvarchar(50) NOT NULL,
                    born int NULL,
                    died int NULL,
                    motherId int NULL,
                    fatherId int NULL)";
            ExecuteSQL(sql);        
        } 
        
        internal void ExecuteSQL(string sql, params (string, string)[] parameters)
        {
            var connectionString = string.Format(ConnectionString, DatabaseName);
            using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new System.Data.SqlClient.SqlCommand(sql, connection))
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.AddWithValue(parameter.Item1, parameter.Item2);
                    }
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Hämtar en datatabell
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns>dataTable</returns>
        internal DataTable GetDataTable(string sql, params (string, string)[] parameters)
        {
            var dt = new DataTable();
            var connectionString = string.Format(ConnectionString, DatabaseName);
            var connection = new SqlConnection(connectionString);
            connection.Open();
            using (var command = new SqlCommand(sql, connection))
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.Item1, parameter.Item2);
                }
                using (var adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// Lägger till personer i databasen
        /// </summary>
        /// <returns></returns>
        internal Person CreatePerson()
        {
            var person = new Person();
            try
            {
                Console.Write("Enter first name: ");
                person.FirstName = Console.ReadLine();
                Console.Write("Enter last name: ");
                person.LastName = Console.ReadLine();
                Console.Write("Enter year of birth: ");
                person.Born = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Gör om, gör rätt PAPPSKALLE");
            }

            var sql = @"INSERT INTO Family (firstName, lastName, born, died, motherId,fatherId)
                       VALUES (@fName, @lName, @born, @died, @mId, @fId)";
            var parameter = new (string, string)[]
            {
                ("@fName",person.FirstName),
                ("@lName",person.LastName),
                ("@born",person.Born.ToString()),
                ("@died",person.Died.ToString()),
                ("@mId",person.Mother.ToString()),
                ("@fId",person.Father.ToString())
            };
            ExecuteSQL(sql, parameter);
            return person;
        }

        internal Person GetPersons(int id)
        {
            string sql = "SELECT TOP 1 * from Family Where id = @id";
            var parameter = new (string, string)[]
            {
                ("@id",id.ToString()),
            };

            var dt = GetDataTable(sql, parameter);

            if (dt.Rows.Count == 0)
            {
                return null;
            }

            var row = dt.Rows[0];

            return new Person
            {
                Id = (int)row["Id"],
                FirstName = row["firstName"].ToString(),
                LastName = row["lastName"].ToString(),
                Born = (int)row["born"],
                Died = (int)row["died"],
                Mother = (int)row["motherId"],
                Father = (int)row["fatherId"]
            };
        }

        /// <summary>
        /// Hämtar en lista med personer
        /// </summary>
        /// <param name="name">input</param>
        /// <returns>lista av namn</returns>
        internal List<Person> GetPersons(string name)
        {
            var list = new List<Person>();
            string sql = "SELECT * from Family Where firstName = @firstName OR lastName =@lastName";
            var parameter = new (string, string)[]
            {
                ("@firstName", name),
                ("@lastName",name)
            };

            var dt = GetDataTable(sql, parameter);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    list.Add(GetPerson(item));
                }
            }
            return list;
        }

        /// <summary>
        /// Tar in en rad och returnerar ett personobjekt
        /// </summary>
        /// <param name="row"></param>
        /// <returns>personobjekt</returns>
        private Person GetPerson(DataRow row)
        {
            var person = new Person()
            {
                Id = (int)row["id"],
                FirstName = row["firstName"].ToString(),
                LastName = row["lastName"].ToString(),
                Born = (int)row["born"],
                Died = (int)row["died"],
                Mother = (int)row["motherId"],
                Father = (int)row["fatherId"]
            };
            return person;
        }

        /// <summary>
        /// Skcikar en SQL query som hämtar alla som innehåller vissa påståenden
        /// </summary>
        /// <param name="filter">Optional</param>
        /// <param name="parameters"></param>
        /// <returns>DataTable</returns>
        internal DataTable ShowAllFrom(string filter = null, params (string, string)[] parameters)
        {
            var sql = "SELECT * FROM Family ";
            DataTable dt;
            if (filter != null)
            {
                sql += filter;
                dt = GetDataTable(sql, parameters);
            }
            else
            {
                dt = GetDataTable(sql);
            }
            if (dt.Rows.Count == 0)
                Console.WriteLine("No match! ");
            return dt;
        }

        /// <summary>
        /// Uppdaterar person vi tar in person från UpdatePerson metdoden i ProgramLogig
        /// </summary>
        /// <param name="person"></param>
        internal void Update(Person person)
        {
            string sql = @"Update Family SET firstName = @firstName, lastName = @lastName,
                         born= @born, died = @died, motherId= @motherId, fatherId = @fatherId
                         WHERE id = @id";
            var parameter = new (string, string)[]
            {
                ("@id",person.Id.ToString()),
                ("@firstName",person.FirstName),
                ("@lastName",person.LastName),
                ("@born",person.Born.ToString()),
                ("@died",person.Died.ToString()),
                ("@motherId",person.Mother.ToString()),
                ("@fatherId",person.Father.ToString())
            };
            ExecuteSQL(sql, parameter);
        }

        /// <summary>
        /// Hämtar person
        /// </summary>
        /// <param name="id">person id</param>
        /// <returns>En datarad baseras på id:t vi skickar in</returns>
        internal DataRow Read(int id)
        {
            var dt = GetDataTable($"SELECT TOP 1 * FROM Family WHERE Id={id};");
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            return dt.Rows[0];
        }

        /// <summary>
        /// Danger Raderar person baserat på vilket id vi skickar in
        /// </summary>
        /// <param name="id"></param>
        internal void Delete(int id)
        {
            ExecuteSQL("DELETE FROM Family Where id = @id", ("@id", id.ToString()));
        }

        public List<Person> GetSiblings(Person person)
        {           
            var siblings = new List<Person>();
            var dt = new DataTable();
            if(person.Mother > 0 && person.Father > 0)
            {
                var sql = "SELECT * FROM Family WHERE motherId = @mId OR fatherId = @fId ";
                dt = GetDataTable(sql, ("@mId", person.Mother.ToString()),
                    ("@fId", person.Father.ToString()));
                foreach (DataRow row in dt.Rows)
                {
                    siblings.Add(GetPerson(row));
                }
            }
            else
            {
                Console.WriteLine("No siblings found! ");
            }
            if (dt.Rows.Count == 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    siblings.Add(GetPerson(row));
                }
            }
            return siblings.Where(s => s.Id != person.Id).ToList();
        }
    }
}