using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    static class Game
    {

        static List<Pellet> pellets;
        static List<Enemy> enemies;
        static List<Pellet> deadPellets;
        static List<Enemy> deadEnemies;
        static Room currentRoom;
        static Player player;
        public static void PlayGame()
        {
            Console.CursorVisible = false;

            Console.WriteLine();

            currentRoom = new Room();
            player = new Player();
            pellets = new List<Pellet>();
            enemies = new List<Enemy>();
            deadPellets = new List<Pellet>();
            deadEnemies = new List<Enemy>();

            int nextEntranceX, nextEntranceY, enemiesKilled = 0;

            var watch = System.Diagnostics.Stopwatch.StartNew();
            watch.Stop();
            watch.Reset();
            var elapsedMs = watch.ElapsedMilliseconds;
            string title = Console.Title;

            Console.WriteLine("Press any key to start");
            Console.ReadKey();
            while (true)
            {
                do
                {
                    watch.Start();

                    if (currentRoom.ChangedRooms)
                    {
                        pellets.Clear();
                        enemies.Clear();
                        currentRoom.GetEntranceCoords(out nextEntranceX, out nextEntranceY);
                        currentRoom = new Room(nextEntranceX, nextEntranceY);
                        player.SetPosition(currentRoom.GetNewPlayerX(), currentRoom.GetNewPlayerY());
                        FillRoom();
                    }
                    Console.Clear();
                    Console.WriteLine("Enemies Killed: {0}     Highscore: {1}", enemiesKilled, Program.Highscore);
                    currentRoom.DisplayFloorPlan(player.X, player.Y, player.Symbol, pellets, enemies);

                    foreach (var e in enemies)
                    {
                        if (e.X == player.X && e.Y == player.Y)
                        {

                            Console.WriteLine("You Died...");
                            if (enemiesKilled > Program.Highscore)
                            {
                                Program.Highscore = enemiesKilled;
                                Console.WriteLine("New Highscore!");
                            }
                            Console.Write("Press Enter to continue");
                            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                            Console.Clear();
                            return;
                        }
                        e.Move();
                    }
                    foreach (var p in pellets)
                    {

                        foreach (var e in enemies)
                        {
                            if (e.X == p.X && e.Y == p.Y)
                            {
                                deadEnemies.Add(e);
                                deadPellets.Add(p);
                                ++enemiesKilled;
                                Console.Beep();
                            }
                        }

                        if (p.X < 0 || p.X > Program.WINDOW_WIDTH || p.Y < 0 || p.Y > Program.WINDOW_HEIGHT)
                        {
                            deadPellets.Add(p);
                        }
                        else
                        {
                            p.Move();
                        }
                    }

                    foreach(var p in deadPellets)
                    {
                        pellets.Remove(p);
                    }
                    deadPellets.Clear();
                    foreach(var e in deadEnemies)
                    {
                        enemies.Remove(e);
                    }
                    deadEnemies.Clear();

                    Thread.Sleep(12);

                    watch.Stop();
                    elapsedMs = watch.ElapsedMilliseconds;
                    Console.Title = title + "        FPS: " + (1000/elapsedMs).ToString();
                    watch.Reset();

                } while (!Console.KeyAvailable);
                
                player.Move(Console.ReadKey().Key, currentRoom);


            }
        }

        public static void CreatePellet(Player origin, ConsoleKey direction, PelletType type=PelletType.Standard)
        {
            pellets.Add(new Pellet(origin, direction, type));
        }
        private static void CreateEnemy(EnemyType type=EnemyType.Standard)
        {
            int L = (int)Math.Ceiling((double)((Program.WINDOW_WIDTH - currentRoom.Width) / 2));
            int T = (int)Math.Floor((double)((Program.WINDOW_HEIGHT - currentRoom.Height) / 2));
            int x = Program.rnd.Next(L + 2, L + currentRoom.Width - 1);
            int y = Program.rnd.Next(T + 2, T + currentRoom.Height - 1);

            enemies.Add(new Enemy(x,y,player,type));
        }

        private static void FillRoom()
        {
            int numEnemies = Program.rnd.Next(0,(int)(currentRoom.Width * currentRoom.Height / 80));
            for (int i = 0; i < numEnemies; ++i)
            {
                CreateEnemy();
            }
        }

        public static bool IsValidEnemyMove(Enemy enemy)
        {
            bool valid = true;

            valid = currentRoom.IsValidEnemyMove(enemy.X, enemy.Y);

            if (valid)
            {
                foreach (var e in enemies)
                {
                    if (e != enemy)
                    {
                        if (e.X == enemy.X && e.Y == enemy.Y)
                        {
                            valid = false;
                            break;
                        }
                    }
                }
            }

            return valid;
        }


    }
}
