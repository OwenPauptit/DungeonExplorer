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
            Game.PlayGame();


        }
    }
}
