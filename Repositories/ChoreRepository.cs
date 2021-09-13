using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roommates.Models;
using Microsoft.Data.SqlClient;

namespace Roommates.Repositories
{
    // class responsible for interacting with chore data and inherits from the BaseRepository class so that it can be use the BaseRepository's Connection property
    public class ChoreRepository : BaseRepository
    {
        // when new ChoreRepository is invoked, it passes the connection string along to the BaseRepository
       public ChoreRepository(string connectionString) : base(connectionString) { }

        // gets a list of all chores in the database
       public List<Chore> GetAllChores()
        {
            // class below represents the connection between the console application and the SQL Server Database:
            using (SqlConnection choreConn = Connection)
            {
                // opens the connection
                choreConn.Open();

                using (SqlCommand cmd = choreConn.CreateCommand())
                {
                    // sets up the command with the sql queries in the c# code and executes them against the database
                    cmd.CommandText = "SELECT Id, Name FROM Chore";

                    // executes the sql in the database and gets a "reader" that will give access to the data:
                    // class below parses out the data that comes back from the database so that it can be converted to the c# objects:
                    SqlDataReader reader = cmd.ExecuteReader();

                    // a list to hold the chores retrieved from the database: 
                    List<Chore> chores = new List<Chore>();

                    // Read() will return true if there's more data to read: 
                    while (reader.Read())
                    {
                        // the "ordinal" is the numeric position of the column in the query results 
                        // for this query, "Id" has an ordinal value of 0 and "Name" is 1.
                        int choreIdColumnPosition = reader.GetOrdinal("Id");

                        // use the reader's GetXXX methods to get the value for a particular ordinal:
                        int choreValue = reader.GetInt32(choreIdColumnPosition);


                        int choreNameColumnPosition = reader.GetOrdinal("Name");
                        string choreNameValue = reader.GetString(choreNameColumnPosition);

                        // create a new room object via object initializer using the data from the database: 
                        Chore chore = new chore
                        {
                            Id = choreIdValue,
                            Name = choreNameValue
                        }
                    };

                        // add chore object to chores list:
                        chores.Add(chore);
                }

                    reader.Close();

                    // return the list of chores who whomever called this method: 
                    return chores;
            }
        }
    }


                // returns a single chore with the given id
                public Chore GetChoreById(int id)
                {
                    using (SqlConnection choreConn = Connection)
                    {
                        cmd.CommandText = "SELECT Name FROM Chore WHERE Id = @id";
                        cmd.Parameters.AddWithValue("@id", id);


                        Chore chore = null; // creating a local chore variable with initial value of null

                        // if we only expect a single row back from the database, we don't need a while loop
                        if (read.Read())
                        {
                            // create a new chore object via object initializer using the data from the database:
                            chore = new Chore
                            {
                                Id = id,
                                Name = reader.GetString(reader.GetOrdinal("Name"))
                            };
                        }

                        reader.Close();
                        return chore;
                    }
                }
            

            // add a new chore to the database:
            public void InsertChore (Chore chore)
            {
                using (SqlConnection choreConn = Connection)
                {
                    choreConn.Open();
                    using (SqlCommad cmd = choreConn.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO Chore (Name)
                                        OUTPUT INSERTED.Id
                                        VALUES (@Name)";
                        cmd.Parameters.AddWithValues("@Name", chore.Name);
                        int choreId = (int)cmd.ExecuteScalar();

                        chore.Id = choreId;
                    }
                }
            }
        }