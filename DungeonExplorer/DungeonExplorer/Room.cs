using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    class Room
    {

        private readonly int _width, _height;
        private int spacesL, spacesT;
        bool _changedRooms;
        int[] _nextEntrance;
        List<int[]> _doors;
        List<string> _floorPlan;

        static Random rnd = new Random();

        public bool ChangedRooms
        {
            get
            {
                return _changedRooms;
            }
        }

        public Room(int entranceX, int entranceY)
        {
            _changedRooms = false;
            _nextEntrance = new int[2];
            _floorPlan = new List<string>();
            _doors = new List<int[]>();
            if (entranceX > 3 && entranceX < Program.WINDOW_WIDTH - 5)
            {
                _width = rnd.Next(entranceX + 2, Program.WINDOW_WIDTH - 4);
            }
            else
            {
                _width = rnd.Next(5, Program.WINDOW_WIDTH - 4);
            }
            if (entranceY > 3 && entranceY < Program.WINDOW_HEIGHT - 5)
            {
                _height = rnd.Next(entranceY + 2, Program.WINDOW_HEIGHT - 4);
            }
            else
            {
                _height = rnd.Next(5, Program.WINDOW_HEIGHT - 4);
            }
            _doors.Add(GenerateEntrance(entranceX,entranceY));
            generateDoors();
            GenerateFloorPlan();
            DisplayFloorPlan();
        }

        public Room()
        {
            _changedRooms = false;
            _nextEntrance = new int[2];
            _floorPlan = new List<string>();
            _doors = new List<int[]>();
            _width = rnd.Next(5, Program.WINDOW_WIDTH - 4);
            _height = rnd.Next(5, Program.WINDOW_HEIGHT - 4);
            generateDoors();
            GenerateFloorPlan();
            DisplayFloorPlan();
        }

        private int[] GenerateEntrance(int x, int y)
        {
            if (x == -1) { x = _width - 1; }
            else if (y == -1) { y = _height - 1; }
            return new int[] { x, y };
        }

        private void generateDoors()
        {
            int numDoors = (_doors.Count >0)? rnd.Next(_doors.Count + 1,4):rnd.Next(1, 4);
            int x, y, wall;
            bool valid;
            while (_doors.Count < numDoors)
            {
                wall = rnd.Next(1, 5); // 1 = top, 2 = right, 3 = bottom, 4 = left
                switch (wall)
                {
                    case 1:
                        x = rnd.Next(1, _width - 1);
                        y = 0;
                        break;
                    case 2:
                        x = 0;
                        y = rnd.Next(1, _height - 1);
                        break;
                    case 3:
                        x = rnd.Next(1, _width - 1);
                        y = _height-1;
                        break;
                    case 4:
                        x = _width-1;
                        y = rnd.Next(1, _height - 1);
                        break;
                    default:
                        x = -1; // need for compiler
                        y = -1;
                        Console.WriteLine("Unexpected Random Outcome");
                        Environment.Exit(-1);
                        break;
                }
                valid = true;
                foreach (var d in _doors)
                {
                    if (    (d[0] == x && (x == 0 || x == _width-1) )    ||    ( d[1] == y && (y == 0 || y == _height-1) )) // check if 2 doors are on the same wall - not allowed
                    {
                        valid = false;
                        break;
                    }
                }
                if (valid)
                {
                    _doors.Add(new int[] { x, y });
                }  
            }
            return;
        }

        /*private List<string> InsertPlayer(int x, int y, char symbol)
        {
            List<string> temp = new List<string>();
            temp = _floorPlan;
            string line = temp[y];
            string newLine = "";
            for (int i = 0; i < line.Length; ++i)
            {
                if (i == x)
                {
                    newLine += symbol;
                }
                else
                {
                    newLine += line[i];

                }
            }
            temp[y] = newLine;
            return temp;
        }*/

        private void GenerateFloorPlan()
        {
            string line, temp;

            double calc = (Program.WINDOW_WIDTH - _width) / 2; // calculating spaces left, right and on top of the room
            spacesL = (int)Math.Ceiling(calc);
            int spacesR = 2;// Program.WINDOW_WIDTH - spacesL - _width;

            calc = (Program.WINDOW_HEIGHT - _height) / 2;
            spacesT = (int)Math.Ceiling(calc);


            for (int i = 0; i < this._height+ spacesT+1; ++i) // this used for clarity as could be confusion between Program.WINDOW_WIDTH / Program.WINDOW_HEIGHT and _width / _height
            {


                if (i < spacesT || i == _height + spacesT) {
                    line = new string(' ', _width + spacesL);
                }
                else if (i == spacesT || i == this._height +spacesT-1)
                {
                    line = new string(' ',spacesL) + new string('#', this._width) + new string(' ',spacesR);
                }
                else { 
                    line = new string(' ', spacesL) + "#" + new string(' ', this._width-2) + "#" + new string(' ', spacesR);
                }

                foreach( var d in _doors)
                {

                    if ((d[0] == 0 || d[0] == _width - 1) && (d[1] + spacesT == i + 1 || d[1] + spacesT == i - 1))
                    {
                        temp = "";
                        for (int k = 0; k < line.Length; ++k)
                        {
                            if (d[0] == 0)
                            {
                                if (k + 1 == d[0] + spacesL)
                                {
                                    temp += "#";
                                }
                                else
                                {
                                    temp += line[k];
                                }
                            }
                            else
                            {
                                if (k - 1 == d[0] + spacesL)
                                {
                                    temp += "#";
                                }
                                else
                                {
                                    temp += line[k];
                                }

                            }
                        }
                        line = temp;
                    }
                    else if (d[1] == 0 && d[1] + spacesT == i + 1)
                    {
                        temp = "";
                        for (int k = 0; k < line.Length; ++k)
                        {
                            if (k + 1 == d[0] + spacesL || k - 1 == d[0] + spacesL)
                            {
                                temp += "#";
                            }
                            else
                            {
                                temp += line[k];
                            }
                        }
                        line = temp;
                    }
                    else if (d[1] == _height - 1 && d[1] + spacesT == i - 1)
                    {
                        temp = "";
                        for (int k = 0; k < line.Length; ++k)
                        {
                            if (k + 1 == d[0] + spacesL || k - 1 == d[0] + spacesL)
                            {
                                temp += "#";
                            }
                            else
                            {
                                temp += line[k];
                            }
                        }
                        line = temp;
                    }



                    if (d[1] + spacesT == i)
                    {
                        temp = "";
                        for (int k = 0; k < line.Length; ++k)
                        {
                            if (k == d[0] + spacesL) 
                            {
                                temp += " ";
                            }
                            else
                            {
                                temp += line[k];
                            }
                        }
                        line = temp;
                    }
                }


                _floorPlan.Add(line);

            }


        }

        public void DisplayFloorPlan()
        {
            foreach(var line in _floorPlan)
            {
                Console.WriteLine(line);
            }
        }

        public void DisplayFloorPlan(List<string> floorPlan)
        {
            foreach (var line in floorPlan)
            {
                Console.WriteLine(line);
            }
        }

        public void DisplayFloorPlan(int x, int y, char symbol)
        {
            for (int row = 0; row < _floorPlan.Count; ++row)
            {
                if (row == y)
                {
                    for(int col = 0; col < _floorPlan[y].Length; ++col)
                    {
                        if (col == x)
                        {
                            Console.Write(symbol);
                        }
                        else
                        {
                            Console.Write(_floorPlan[row][col]);
                        }
                    }
                    Console.Write("\n");
                }
                else
                {
                    Console.WriteLine(_floorPlan[row]);

                }
            }
        }

        public void DisplayFloorPlan(int x, int y, char symbol, List<Pellet> pellets)
        {
            List<string> temp = new List<string>();
            string line;
            for (int row = 0; row < _floorPlan.Count; ++row)
            {
                if (row == y)
                {
                    line = "";
                    for (int col = 0; col < _floorPlan[y].Length; ++col)
                    {
                        if (col == x)
                        {
                            line += symbol;
                        }
                        else
                        {
                            line += _floorPlan[row][col];
                        }
                    }
                }
                else
                {
                    line = _floorPlan[row];
                }
                temp.Add(line);

                foreach (var p in pellets)
                {

                    if (row == p.Y)
                    {
                        line = "";
                        for (int col = 0; col < _floorPlan[row].Length; ++col)
                        {
                            if (col == p.X)
                            {
                                line += p.Symbol;
                            }
                            else
                            {
                                line += temp[row][col];
                            }
                        }
                        temp[row] = line;
                    }
                }

            }
            DisplayFloorPlan(temp);
        }

        public int GetNewPlayerX()
        {
            return _doors[0][0] + spacesL;
        }

        public int GetNewPlayerY()
        {
            return _doors[0][1] + spacesT;
        }

        public void GetEntranceCoords(out int x, out int y)
        {
            if (_nextEntrance[0] == 0) 
            {
                x = -1;
                y = _nextEntrance[1];
            }
            else if(_nextEntrance[0] == _width - 1)
            {
                x = 0;
                y = _nextEntrance[1];
            }else if (_nextEntrance[1] == 0)
            {
                x = _nextEntrance[0];
                y = -1;
            }
            else
            {
                x = _nextEntrance[0];
                y = 0;
            }
        }
        public bool IsValidPlayerMove
            (int x, int y)
        {
            foreach(var d in _doors)
            {
                if (d[0] + spacesL == x && d[1] + spacesT == y)
                {
                    _nextEntrance = d;
                    _changedRooms = true;
                }else if((d[0] == 0 && x < d[0] + spacesL ) || (d[0] == _width - 1 && x > d[0] + spacesL) || (d[1] == 0 && y < d[1] + spacesT) || (d[1] == _height - 1 && y > d[1] + spacesT))
                {
                    _nextEntrance = d;
                    _changedRooms = true;
                }
            }
            return _floorPlan[y][x] != '#';
        }


    }
}
