using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Roommates.Models;


namespace Roommates.Repositories
{
    ///  Class responsible for interacting with Roommate data and inherits from the BaseRepository class so that it can use
    ///  the BaseRepository's Connection property:
    public class RoommateRepository : BaseRepository
    {
        ///  When new RoommateRepository is instantiated, pass the connection string along to the BaseRepository
        public RoommateRepository(string connectionString) : base(connectionString) { }

        ///  Returns a single roommate with the given id:
        public Roommate GetRoommateById(int id)
        {
            using (SqlConnection roommateConn = Connection)
            {
                roommateConn.Open();
                using (SqlCommand cmd = roommateConn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT FirstName, RentPortion, RoomId
                                        FROM Roommate 
                                        INNER JOIN Room
                                        ON Roommate.RoomId = Room.Id
                                        WHERE Roommate.Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    Roommate roommate = null; // creating a local roommate variable with initial value of null
                    // If we only expect a single row back from the database, we don't need a while loop:
                    if (reader.Read())
                    {
                        // Create a new roommate object via object initializer using the data from the database:
                        roommate = new Roommate
                        {
                            Id = id,
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            RentPortion = reader.GetInt32(reader.GetOrdinal("RentPortion")),
                            Room = new Room
                            {
                                Name = reader.GetString(reader.GetOrdinal("Name"))
                            }
                        };
                    }

                    reader.Close();

                    return roommate;
                }
            }
        }

    }
}


