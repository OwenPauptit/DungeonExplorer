using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{

    enum EnemyType
    {
        Standard = 'Ѫ'
    }
    class Enemy
    {

        private int _x, _y, _clock;
        private char _symbol;
        EnemyType _type;
        private Player _player;

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

        public Enemy(int x, int y, Player p, EnemyType type=EnemyType.Standard)
        {
            _x = x;
            _y = y;
            _clock = 0;
            _type = type;
            _player = p;

            switch (_type)
            {
                case EnemyType.Standard:
                    _symbol = 'Ѫ';
                    break;
                default:
                    _symbol = 'M';
                    break;
            }
        }

        public void Move()
        {
            if (_clock == 3)
            {
                _clock = 0;

                int xDif = _player.X - _x;
                int yDif = _player.Y - _y;

                if (xDif == 0 && yDif == 0)
                {

                }
                else
                {

                    if (Math.Abs(xDif) > Math.Abs(yDif))
                    {
                        _x += xDif / Math.Abs(xDif);
                        if (!Game.IsValidEnemyMove(this))
                        { 
                            _x -= xDif / Math.Abs(xDif);
                            if (yDif != 0)
                            {
                                _y += yDif / Math.Abs(yDif);
                                if (!Game.IsValidEnemyMove(this))
                                {
                                    _y -= yDif / Math.Abs(yDif);
                                }
                            }
                        }
                    }
                    else
                    {
                        _y += yDif / Math.Abs(yDif);
                        if (!Game.IsValidEnemyMove(this))
                        {
                            _y -= yDif / Math.Abs(yDif);
                            if (xDif != 0)
                            {
                                _x += xDif / Math.Abs(xDif);
                                if (!Game.IsValidEnemyMove(this))
                                {
                                    _x -= xDif / Math.Abs(xDif);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                ++_clock;
            }
        }

    }
}
