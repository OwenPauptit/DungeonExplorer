using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{

    enum EnemyType
    {
        Standard = 'ѫ',
        BigStandard = 'Ѫ',
        StaticShooter = 'Ж',
        StaticPulser = 'Ӝ'
    }
    class Enemy
    {

        private int _x, _y, _clock, _clockResetNum, _health;
        private char _symbol;
        EnemyType _type;
        private Player _player;
        bool _isDead;

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
        public bool IsDead
        {
            get
            {
                return _isDead;
            }
        }

        public Enemy(int x, int y, Player p, EnemyType type=EnemyType.Standard)
        {
            _x = x;
            _y = y;
            _clock = 0;
            _type = type;
            _player = p;
            _isDead = false;

            switch (_type)
            {
                case EnemyType.Standard:
                    _symbol = 'ѫ';
                    _clockResetNum = 3;
                    _health = 1;
                    break;
                case EnemyType.BigStandard:
                    _clockResetNum = 6;
                    _health = 2;
                    _symbol = 'Ѫ';
                    break;
                case EnemyType.StaticShooter:
                    _health = 1;
                    _symbol = 'Ж';
                    _clockResetNum = 10;
                    break;
                case EnemyType.StaticPulser:
                    _health = 1;
                    _symbol = 'Ӝ';
                    _clockResetNum = 20;
                    break;

                default:
                    _symbol = 'M';
                    break;
            }
        }

        public void TakeDamage(int damage = 1)
        {
            --_health;
            if (_health <= 0)
            {
                _isDead = true;
            }
        }

        public void Move()
        {
            if (_clock == _clockResetNum)
            {
                _clock = 0;

                int xDif = _player.X - _x;
                int yDif = _player.Y - _y;

                if (_type == EnemyType.StaticShooter)
                {
                    if (Math.Abs(xDif) > Math.Abs(yDif))
                    {
                        if (xDif < 0)
                        {
                            Game.CreatePellet(this, ConsoleKey.LeftArrow, PelletType.StaticShooter);
                        }
                        else
                        {
                            Game.CreatePellet(this, ConsoleKey.RightArrow, PelletType.StaticShooter);
                        }
                    }
                    else
                    {
                        if (yDif < 0)
                        {
                            Game.CreatePellet(this, ConsoleKey.UpArrow, PelletType.StaticShooter);
                        }
                        else
                        {
                            Game.CreatePellet(this, ConsoleKey.DownArrow, PelletType.StaticShooter);
                        }
                    }
                }
                else if (_type == EnemyType.StaticPulser)
                {
                    Game.CreatePellet(this, ConsoleKey.LeftArrow, PelletType.StaticShooter);
                    Game.CreatePellet(this, ConsoleKey.RightArrow, PelletType.StaticShooter);
                    Game.CreatePellet(this, ConsoleKey.DownArrow, PelletType.StaticShooter);
                    Game.CreatePellet(this, ConsoleKey.UpArrow, PelletType.StaticShooter);
                }
                else
                {


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
            }
            else
            {
                ++_clock;
            }
           
        }
    }
}
