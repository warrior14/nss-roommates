using System;
using System.Collections.Generic;
using System.Linq;
using Roommates.Repositories;
using Roommates.Models;

namespace Roommates
{
    class Program
    {
        //  This is the address of the database.
        //  We define it here as a constant since it will never change.
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true";

        static void Main(string[] args)
        {
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING); // instantiating new RoomRepository
            ChoreRepository choreRepo = new ChoreRepository(CONNECTION_STRING); // instantiating new ChoreRepository
            RoommateRepository roommateRepo = new RoommateRepository(CONNECTION_STRING); // instantiating new RoommateRepository

            bool runProgram = true;
            while (runProgram)
            {
                string selection = GetMenuSelection();

                switch (selection)
                {
                    case ("Show all rooms"):
                        List<Room> rooms = roomRepo.GetAllRooms();
                        foreach (Room r in rooms)
                        {
                            Console.WriteLine($"{r.Name} has an Id of {r.Id} and a max occupancy of {r.MaxOccupancy}");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Search for room"):
                        Console.Write("Room Id: ");
                        int id = int.Parse(Console.ReadLine());

                        Room room = roomRepo.GetRoomById(id);

                        Console.WriteLine($"{room.Id} - {room.Name} Max Occupancy({room.MaxOccupancy})");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    //Console.Write("Room Id: ");
                    //int id = int.Parse(Console.ReadLine());
                    //string input = Console.ReadLine();

                    //Room room = roomRepo.GetById(id);

                    //if (id < room.Id && id > room.Id && room.Id != int.TryParse(id, out id){}

                    //char firstChar = input[0];
                    //bool isNumber = Char.IsDigit(firstChar);
                    //if (id < room.Id && id > room.Id)
                    //{
                    //    Console.WriteLine("Please enter a valid Room Id!");
                    //}
                    //else
                    //{
                    //    Console.WriteLine($"{room.Id} - {room.Name} Max Occupancy({room.MaxOccupancy})");
                    //    Console.Write("Press any key to continue");
                    //    Console.ReadKey();
                    //}

                    case ("Add a room"):
                        Console.Write("Room name: ");
                        string name = Console.ReadLine();

                        Console.Write("Max occupancy: ");
                        int max = int.Parse(Console.ReadLine());

                        Room roomToAdd = new Room()
                        {
                            Name = name,
                            MaxOccupancy = max
                        };

                        roomRepo.InsertRoom(roomToAdd);

                        Console.WriteLine($"{roomToAdd.Name} has been added and assigned an Id of {roomToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Update a room"):
                        List<Room> roomOptions = roomRepo.GetAllRooms();
                        foreach (Room r in roomOptions)
                        {
                            Console.WriteLine($"{r.Id} - {r.Name} Max Occupancy({r.MaxOccupancy})");
                        }

                        Console.Write("Which room would you like to update? ");
                        int selectedRoomId = int.Parse(Console.ReadLine());
                        Room selectedRoom = roomOptions.FirstOrDefault(r => r.Id == selectedRoomId);

                        Console.Write("New Name: ");
                        selectedRoom.Name = Console.ReadLine();

                        Console.Write("New Max Occupancy: ");
                        selectedRoom.MaxOccupancy = int.Parse(Console.ReadLine());

                        roomRepo.UpdateRoom(selectedRoom);

                        Console.WriteLine("Room has been successfully updated");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Remove a room"):
                        List<Room> roomChoices = roomRepo.GetAllRooms();
                        foreach (Room r in roomChoices)
                        {
                            Console.WriteLine($"{r.Id} - {r.Name} Max Occupancy({r.MaxOccupancy})");
                        }

                        Console.Write("Which room would you like to remove? ");
                        int chosenRoomId = int.Parse(Console.ReadLine());

                        roomRepo.DeleteRoom(chosenRoomId);

                        Console.WriteLine("Room has been successfully removed! :(");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Show all chores"):
                        List<Chore> chores = choreRepo.GetAllChores();
                        foreach (Chore c in chores)
                        {
                            Console.WriteLine($"{c.Name} has an Id of {c.Id}! :)");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Search for chore"):
                        Console.Write("Chore Id: ");
                        int choreId = int.Parse(Console.ReadLine());

                        Chore chore = choreRepo.GetChoreById(choreId);

                        Console.WriteLine($"{chore.Id} - {chore.Name} :D");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Add a chore"):
                        Console.Write("Chore name: ");
                        string choreName = Console.ReadLine();

                        Chore choreToAdd = new Chore()
                        {
                            Name = choreName
                        };

                        choreRepo.InsertChore(choreToAdd);

                        Console.WriteLine($"{choreToAdd.Name} has been added and assigned an Id of {choreToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Update a chore"):
                        List<Chore> choreOptions = choreRepo.GetAllChores();
                        foreach (Chore c in choreOptions)
                        {
                            Console.WriteLine($"{c.Id} - {c.Name}.");
                        }

                        Console.Write("Which chore would you like to update? ");
                        int selectedChoreId = int.Parse(Console.ReadLine());
                        Chore selectedChore = choreOptions.FirstOrDefault(c => c.Id == selectedChoreId);

                        Console.Write("New Name: ");
                        selectedChore.Name = Console.ReadLine();

                        choreRepo.UpdateChore(selectedChore);

                        Console.WriteLine("Chore has been successfully updated");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Remove a chore"):
                        List<Chore> choreChoices = choreRepo.GetAllChores();
                        foreach (Chore c in choreChoices)
                        {
                            Console.WriteLine($"{c.Id} - {c.Name}");
                        }

                        Console.Write("Which chore would you like to remove? ");
                        int chosenChoreId = int.Parse(Console.ReadLine());

                        choreRepo.DeleteChore(chosenChoreId);

                        Console.WriteLine("Chore has been successfully removed! :(");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Show all unassigned chores"):
                        List<Chore> unassignedChores = choreRepo.GetUnassignedChores();
                        foreach (Chore c in unassignedChores)
                        {
                            Console.WriteLine($"{c.Name} is an unassigned chore that has an Id of {c.Id}! :)");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Assign a chore to roommate"):
                        List<Chore> allChores = choreRepo.GetAllChores();
                        foreach (Chore c in allChores)
                        {
                            Console.WriteLine($"{c.Name} has an Id of {c.Id}");
                        }

                        Console.Write("Which chore would you like to assign? ");
                        int assignedChoreId = int.Parse(Console.ReadLine());

                        List<Roommate> allRoommates = roommateRepo.GetAllRoommates();
                        foreach (Roommate rm in allRoommates)
                        {
                            Console.WriteLine($"Roommate {rm.FirstName} {rm.LastName} has an Id of {rm.Id}");
                        }

                        Console.Write($"Which roommate would you like to assign the chore to? ");
                        int assignedRoommateId = int.Parse(Console.ReadLine());

                        choreRepo.AssignChore(assignedRoommateId, assignedChoreId);

                        Console.WriteLine($"Chore has been successfully assigned to selected roommate! :D");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Show number of chores assigned to each roommate"):
                        List<ChoreCount> choreCounts = choreRepo.GetChoreCounts();
                        foreach (ChoreCount count in choreCounts)
                        {
                            Console.WriteLine($"{count.RoommateName} is assigned {count.Count} chore(s)! :O");
                        }

                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Show all roommates"):
                        List<Roommate> roommates = roommateRepo.GetAllRoommates();
                        foreach (Roommate rm in roommates)
                        {
                            Console.WriteLine($"{rm.FirstName} {rm.LastName} has an Id of {rm.Id} with a RentPortion of {rm.RentPortion}. This roomie moved in on {rm.MovedInDate} and occupies {rm.Room.Name}.");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Search for roommate"):
                        Console.Write("Roommate Id: ");
                        int roommateId = int.Parse(Console.ReadLine());

                        Roommate roommate = roommateRepo.GetRoommateById(roommateId);

                        Console.WriteLine($"{roommate.Id} - {roommate.FirstName} Rent Portion - {roommate.RentPortion} Occupying - {roommate.Room.Name} :D");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Exit"):
                        runProgram = false;
                        break;
                }
            }
        }

        static string GetMenuSelection()
        {
            Console.Clear();

            List<string> options = new List<string>()
            {
                "Show all rooms",
                "Search for room",
                "Add a room",
                "Update a room",
                "Remove a room",
                "Show all chores",
                "Search for chore",
                "Add a chore",
                "Update a chore",
                "Remove a chore",
                "Show all unassigned chores",
                "Assign a chore to roommate",
                "Show number of chores assigned to each roommate",
                "Show all roommates",
                "Search for roommate",
                "Exit"
            };

            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Select an option > ");

                    string input = Console.ReadLine();
                    int index = int.Parse(input) - 1;
                    return options[index];
                }
                catch (Exception)
                {

                    continue;
                }
            }
        }
    }
}