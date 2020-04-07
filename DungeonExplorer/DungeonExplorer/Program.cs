using System;
using System.Windows.Input;
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

        public static Random rnd = new Random();
        public static int Highscore {get; set;}

        static void Main(string[] args)
        {
            Console.Title = "Dungeon Explorer!";
            Console.SetWindowSize(WINDOW_WIDTH, WINDOW_HEIGHT);
            Console.SetBufferSize(WINDOW_WIDTH, WINDOW_HEIGHT);
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Program.Highscore = 0;

            while (true) {
                Game.PlayGame();
            }

        }
    }
}
