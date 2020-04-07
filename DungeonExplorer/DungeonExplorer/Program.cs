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

            int menuChoice;

            Console.Clear();

            while (true) {
                DisplayMenu();

                if (Int32.TryParse(Console.ReadLine(), out menuChoice))
                {
                    switch (menuChoice)
                    {
                        case 1:
                            Console.Clear();
                            Game.PlayGame();
                            break;
                        case 2:
                            Console.Clear();
                            DisplayHelpMenu();
                            Console.Clear();
                            break;
                        case 3:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Invalid Input");
                            break;

                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Input");
                }

            }

        }

        static void DisplayMenu()
        {
            Console.WriteLine("\n\n");
            Console.WriteLine("         DUNGEON EXPlORER!");
            Console.WriteLine();
            Console.WriteLine("Main Menu:");
            Console.WriteLine("   1. Play Dungeon Explorer");
            Console.WriteLine("   2. Help");
            Console.WriteLine("   3. Quit");
            Console.WriteLine("\n\n");
        }

        static void DisplayHelpMenu()
        {
            Console.WriteLine("\n\n");
            Console.WriteLine("         DUNGEON EXPlORER!");
            Console.WriteLine();
            Console.WriteLine("Help Menu:");
            Console.WriteLine();
            Console.WriteLine("   Aim of the game:");
            Console.WriteLine("       Navigate your way through different rooms");
            Console.WriteLine("       and kill any monsters that you encounter!");
            Console.WriteLine("");
            Console.WriteLine("   Controls:");
            Console.WriteLine("       W - Move up");
            Console.WriteLine("       A - Move left");
            Console.WriteLine("       S - Move down");
            Console.WriteLine("       D - Move right");
            Console.WriteLine();
            Console.WriteLine("       ↑ - Shoot up");
            Console.WriteLine("       ← - Shoot left");
            Console.WriteLine("       ↓ - Shoot down");
            Console.WriteLine("       → - Shoot right");
            Console.WriteLine();
            Console.Write("   Press any key to return to the main menu");
            Console.ReadKey();
        }
    }
}
