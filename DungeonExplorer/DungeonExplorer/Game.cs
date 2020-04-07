using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    static class Game
    {

        static List<Pellet> pellets;
        static Room currentRoom;
        static Player player;
        public static void PlayGame()
        {

            currentRoom = new Room();
            player = new Player();
            pellets = new List<Pellet>();

            int nextEntranceX, nextEntranceY;

            Console.WriteLine("Press any key to start");
            Console.ReadKey();
            while (true)
            {
                do
                {
                    if (currentRoom.ChangedRooms)
                    {
                        pellets.Clear();
                        currentRoom.GetEntranceCoords(out nextEntranceX, out nextEntranceY);
                        currentRoom = new Room(nextEntranceX, nextEntranceY);
                        player.SetPosition(currentRoom.GetNewPlayerX(), currentRoom.GetNewPlayerY());
                    }
                    Console.Clear();
                    currentRoom.DisplayFloorPlan(player.X, player.Y, player.Symbol, pellets);

                    foreach (var p in pellets)
                    {
                        p.Move();
                    }

                    Thread.Sleep(40);
                } while (!Console.KeyAvailable);
                
                player.Move(Console.ReadKey().Key, currentRoom);
            }
        }

        public static void CreatePellet(Player origin, ConsoleKey direction, PelletType type=PelletType.Standard)
        {
            pellets.Add(new Pellet(origin, direction, type));
        }


    }
}
