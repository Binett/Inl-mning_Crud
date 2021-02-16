using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace Inlämning_Crud
{
    class SQLDatabase
    {
        public string ConnectionString { get; set; } = @"Data Source = .\SQLExpress; Integrated Security = true; database = {0}";
        public string DatabaseName { get; set; } = "Binett";

        public bool CreateDatabase()
        {
            try
            {
                DatabaseName = "master";
                ExecuteSQL("CREATE DATABASE Binett");
                DatabaseName = "Binett";
                CreateTable("Family");
                return true;
            }
            catch
            {
                DatabaseName = "Binett";
                Debug.WriteLine("Database already exist");
                return false;
            }
        }
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
        internal Person CreatePerson()
        {
            var person = new Person();
            Console.Write("Enter first name: ");
            person.FirstName = Console.ReadLine();
            Console.Write("Enter last name: ");
            person.LastName = Console.ReadLine();
            Console.Write("Enter year of birth: ");
            person.Born = int.Parse(Console.ReadLine());

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
        public Person Read(string name)
        {
            string sql = "SELECT TOP 1 * from Persons Where firstName LIKE @name OR lastName LIKE @name";
            var parameter = new (string, string)[]
            {
                ("name",name),
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
                Born = (int)row["birthDate"],
                Died = (int)row["deathDate"],
                Mother = (int)row["mother"],
                Father = (int)row["father"]
            };
        }        
        public DataTable ShowAllFrom(string filter = null, params(string,string)[] parameters)
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
    }
}
