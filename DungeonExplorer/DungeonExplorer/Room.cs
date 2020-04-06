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
        List<int[]> _doors;
        List<string> _floorPlan;

        static Random rnd = new Random();

        public Room(int entranceX, int entranceY)
        {
            _doors = new List<int[]>();
            _doors.Add(new int[] { entranceX, entranceY });
            _width = rnd.Next(5,Program.WINDOW_WIDTH-3);
            _height = rnd.Next(5, Program.WINDOW_HEIGHT-3);
            generateDoors();
        }

        public Room()
        {
            _floorPlan = new List<string>();
            _doors = new List<int[]>();
            _width = rnd.Next(5, Program.WINDOW_WIDTH - 4);
            _height = rnd.Next(5, Program.WINDOW_HEIGHT - 4);
            generateDoors();
            generateFloorPlan();
            displayFloorPlan();
        }

        private void generateDoors()
        {
            int numDoors = (_doors.Count >0)? rnd.Next(_doors.Count + 1):rnd.Next(1, 4);
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
        private void generateFloorPlan()
        {
            string line, temp;

            double calc = (Program.WINDOW_WIDTH - _width) / 2; // calculating spaces left, right and on top of the room
            int spacesL = (int)Math.Ceiling(calc);
            int spacesR = 2;// Program.WINDOW_WIDTH - spacesL - _width;

            calc = (Program.WINDOW_HEIGHT - _height) / 2;
            int spacesT = (int)Math.Ceiling(calc);


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

        public void displayFloorPlan()
        {
            foreach(var line in _floorPlan)
            {
                Console.WriteLine(line);
            }
        }


    }
}
