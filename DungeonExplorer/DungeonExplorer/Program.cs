using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{

    

    class Program
    {

        public const int WINDOW_WIDTH = 60;
        public const int WINDOW_HEIGHT = 20;


        static void Main(string[] args)
        {
            Console.Title = "Dungeon Explorer!";
            Console.SetWindowSize(WINDOW_WIDTH, WINDOW_HEIGHT);
            Console.SetBufferSize(WINDOW_WIDTH,WINDOW_HEIGHT);
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Room r = new Room();
            Player p = new Player();
            int nextEntranceX, nextEntranceY;
            Console.WriteLine("Press any key to start");
            Console.ReadKey();
            while (true)
            {
                if (r.ChangedRooms)
                {
                    r.GetEntranceCoords(out nextEntranceX, out nextEntranceY);
                    r = new Room(nextEntranceX, nextEntranceY);
                    p.SetPosition(r.GetNewPlayerX(), r.GetNewPlayerY());
                }
                Console.Clear();
                r.DisplayFloorPlan(p.X,p.Y,p.Symbol);
                p.Move(Console.ReadKey().Key,r);
            }


        }
    }
}
