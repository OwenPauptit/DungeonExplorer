using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    class Player
    {

        private int _x, _y;
        private char _symbol;

        public int X
        {
            get
            {
                return _x;
            }
        }

        public int Y
        {
            get
            {
                return _y;
            }
        }

        public char Symbol
        {
            get
            {
                return _symbol;
            }
        }


        public Player(int xCoord, int yCoord, char s = '♀')
        {
            _x = xCoord;
            _y = yCoord;
            _symbol = s;
        }

        public Player(char s = '☻')
        {
            _x = Console.WindowWidth / 2;
            _y = Console.WindowHeight / 2;
            _symbol = s;
        }

        public void Move(ConsoleKey direction)
        {
            switch (direction)
            {
                case ConsoleKey.W:
                    --_y;
                    break;
                case ConsoleKey.A:
                    --_x;
                    break;
                case ConsoleKey.S:
                    ++_y;
                    break;
                case ConsoleKey.D:
                    ++_x;
                    break;
            }
        }

        public void SetPosition(int x, int y)
        {
            _x = x;
            _y = y;
        }
        public void Move(ConsoleKey input, Room r)
        {
            switch (input)
            {
                case ConsoleKey.W:
                    if (!r.IsValidPlayerMove(_x, --_y)) { ++_y; }
                    break;
                case ConsoleKey.A:
                    if (!r.IsValidPlayerMove(--_x, _y)) { ++_x; }
                    break;
                case ConsoleKey.S:
                    if (!r.IsValidPlayerMove(_x, ++_y)) { --_y; }
                    break;
                case ConsoleKey.D:
                    if (!r.IsValidPlayerMove(++_x, _y)) { --_x; }
                    break;
            }
            switch (input)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.DownArrow:
                case ConsoleKey.RightArrow:
                case ConsoleKey.LeftArrow:
                    Game.CreatePellet(this, input);
                    break;
            }
            
        }

    }
}
