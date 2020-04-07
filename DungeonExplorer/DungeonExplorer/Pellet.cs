using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{

    enum PelletType
    {
        Standard
    }

    class Pellet
    {

        private int _x, _y, _clock;
        private int[] _velocity;
        private PelletType _type;
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

        public Pellet(Player origin, ConsoleKey direction, PelletType type=PelletType.Standard)
        {
            _type = type;
            _x = origin.X;
            _y = origin.Y;
            _clock = 1;

            if (_type == PelletType.Standard)
            {
                _symbol = 'o';
                switch (direction)
                {
                    case ConsoleKey.UpArrow:
                        _velocity = new int[] { 0, -1 };
                        break;
                    case ConsoleKey.RightArrow:
                        _velocity = new int[] { 1, 0 };
                        break;
                    case ConsoleKey.DownArrow:
                        _velocity = new int[] { 0, 1 };
                        break;
                    case ConsoleKey.LeftArrow:
                        _velocity = new int[] { -1, 0 };
                        break;
                    default:
                        _velocity = new int[] { 0, 0 };
                        Console.WriteLine("Invalid Pellet Direction");
                        break;
                }
            }
            Move();

        }

        public void Move(){
            _x += _velocity[0];
            if (_clock++ == 1)
            {
                _y += _velocity[1];
                _clock = 0;
            }
        }

    }
}
