using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ConsolePacman
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char[,] map = ReadMap("map.txt");
            Console.CursorVisible = false;
            int pacmanX = 1, pacmanY = 1;
            int score = 0;           
            ConsoleKeyInfo pressedKey = new ConsoleKeyInfo();
            Task.Run(() =>
            {
                while (true)
                {
                    pressedKey = Console.ReadKey();
                }
            });

            while (true)
            {
                if (getMaxScores(map, '*') == 0)
                {
                    MessageBox.Show("YOU WIN! \n\nGame Over");
                    break;
                }
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                DrawMap(map);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(0, 15);
                Console.Write($"Score: {score}");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(pacmanX, pacmanY);
                Console.Write('@');
                Thread.Sleep(200);
                //pressedKey = Console.ReadKey();


                HandleInput(pressedKey, ref pacmanX, ref pacmanY, map,ref score);

            }





        }
        private static char[,] ReadMap(string path)
        {
            string[] file = File.ReadAllLines(path);
            char[,] map = new char[GetMaxLenght(file), file.Length];
            for (int x = 0; x < map.GetLength(0); x++)
                for (int y = 0; y < map.GetLength(1); y++)
                    map[x, y] = file[y][x];
            return map;

        }

        private static int GetMaxLenght(string[] lines)
        {
            int maxLenght = lines[0].Length;
            foreach (string line in lines)
            {
                if (line.Length > maxLenght)
                    maxLenght = line.Length;
            }
            return maxLenght;
        }
        private static void DrawMap(char[,] map)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    if (map[x, y] == '*')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;                    
                    }
                    if (map[x, y] == '#')
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;                   
                    }
                    
                    Console.Write(map[x, y]);
                    
                }
                Console.WriteLine();
            }
        }

        private static void HandleInput(ConsoleKeyInfo pressedKey, ref int pacmanX, ref int pacmanY, char[,] map, ref int score)
        {
            int[] direction = GetDirection(pressedKey);
            int nextPacmanPositionX = pacmanX + direction[0];
            int nextPacmanPositionY = pacmanY + direction[1];
            if (map[nextPacmanPositionX, nextPacmanPositionY] != '#')
            {
                pacmanX = nextPacmanPositionX;
                pacmanY = nextPacmanPositionY;
            }
            if (map[pacmanX,pacmanY] == '*')
            {
                score++;
                map[pacmanX, pacmanY] = ' ';
            }

        }

        private static int[] GetDirection(ConsoleKeyInfo pressedKey)
        {
            int[] direction = { 0, 0 };
            switch (pressedKey.Key)
            {
                case ConsoleKey.UpArrow:
                    direction[1] = -1;
                    break;
                case ConsoleKey.DownArrow:
                    direction[1] = 1;
                    break;
                case ConsoleKey.LeftArrow:
                    direction[0] = -1;
                    break;
                case ConsoleKey.RightArrow:
                    direction[0] = 1;
                    break;
            }
            return direction;
        }
        private static int getMaxScores(char[,] map, char symbol)
        {
            int maxScore = 0;
            foreach (char c in map)
            {
                if (c == symbol) {
                    maxScore++;
                }
            }
            return maxScore;
        }
        

        
    }


}
