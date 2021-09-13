using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Roommates.Models;


namespace Roommates.Repositories
{
    ///  Class responsible for interacting with Chore data and inherits from the BaseRepository class so that it can use
    ///  the BaseRepository's Connection property:
    public class ChoreRepository : BaseRepository
    {
        ///  When new ChoreRepository is instantiated, pass the connection string along to the BaseRepository
        public ChoreRepository(string connectionString) : base(connectionString) { }

        ///  Get a list of all Chores in the database
        public List<Chore> GetAllChores()
        {
            // Class below represents the connection between the console application and the SQL Server database:
            using (SqlConnection choreConn = Connection)
            {
                // Open() the connection:
                choreConn.Open();

                // Class below enables ability write sql queries in the C# code and execute them against the database:
                using (SqlCommand cmd = choreConn.CreateCommand())
                {
                    // Setup the command with the SQL to be executed before it is actually implemented:
                    cmd.CommandText = "SELECT Id, Name FROM Chore";

                    // Execute the SQL in the database and get a "reader" that will give access to the data:
                    // Class below parses out the data that comes back from the database so that it can be converted to C# objects:
                    SqlDataReader reader = cmd.ExecuteReader();

                    // A list to hold the chores retrieved from the database:
                    List<Chore> chores = new List<Chore>();

                    // Read() will return true if there's more data to read:
                    while (reader.Read())
                    {
                        // The "ordinal" is the numeric position of the column in the query results.
                        //  For this query, "Id" has an ordinal value of 0 and "Name" is 1.
                        int choreIdColumnPosition = reader.GetOrdinal("Id");

                        // Use the reader's GetXXX methods to get the value for a particular ordinal:
                        int choreIdValue = reader.GetInt32(choreIdColumnPosition);

                        int choreNameColumnPosition = reader.GetOrdinal("Name");
                        string choreNameValue = reader.GetString(choreNameColumnPosition);

                        // Create a new room object via object initializer using the data from the database:
                        Chore chore = new Chore
                        {
                            Id = choreIdValue,
                            Name = choreNameValue
                        };

                        // Add chore object to chores list:
                        chores.Add(chore);
                    }

                    reader.Close();

                    // Return the list of chores who whomever called this method:
                    return chores;
                }

            }
        }

        ///  Returns a single chore with the given id.
        public Chore GetChoreById(int id)
        {
            using (SqlConnection choreConn = Connection)
            {
                choreConn.Open();

                using (SqlCommand cmd = choreConn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Name FROM Chore WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    Chore chore = null; // creating a local chore variable with initial value of null

                    // If we only expect a single row back from the database, we don't need a while loop.
                    if (reader.Read())
                    {
                        // Create a new chore object via object initializer using the data from the database:
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
        }

        ///  Add a new chore to the database:
        public void InsertChore(Chore chore)
        {
            using (SqlConnection choreConn = Connection)
            {
                choreConn.Open();
                using (SqlCommand cmd = choreConn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Chore (Name)
                                         OUTPUT INSERTED.Id
                                         VALUES (@name)";
                    cmd.Parameters.AddWithValue("@name", chore.Name);
                    int choreId = (int)cmd.ExecuteScalar();

                    chore.Id = choreId;
                }
            }
        }
    }
}
