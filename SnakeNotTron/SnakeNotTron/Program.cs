using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace SnakeNotTron
{
    class Program
    {
        static int[,] gameboard = new int[40, 20];
        static bool[,] playerHistory = new bool[40, 20];
        static int[,] historyAge = new int[40, 20];
        static bool gameOver = false;
        static int startX = 0;
        static int startY = 0;
        static int playerX = startX;
        static int playerY = startY;
        static int counter = 0;
        static int playerDX = 1;
        static int playerDY = 0;
        static int playerLength = 0;
        static int foodX = -1;
        static int foodY = -1;
        static Random rand = new Random();

        static void Main(string[] args)
        {
            //INITIALIZE GAME BOARD
            

            for (int i = 0; i < playerHistory.GetLength(1); i++)
            {
                for (int j = 0; j < playerHistory.GetLength(0); j++)
                {
                    playerHistory[j, i] = false;
                    historyAge[j, i] = -1;
                    Console.Write(".");
                }
                Console.WriteLine();
            }

            
            foodX = rand.Next(0, 39);
            foodY = rand.Next(0, 19);
            Console.SetCursorPosition(foodX, foodY);
            Console.Write('X');


            //MAIN GAME LOOP
            while (UpdateScreen(playerX, playerY))
            {
                counter++;
                // Console.WriteLine(counter.ToString());
                var stopwatch = Stopwatch.StartNew();
                Thread.Sleep(240);
                stopwatch.Stop();

                ConsoleKeyInfo keyPress = new ConsoleKeyInfo();

                if (Console.KeyAvailable)
                    keyPress = Console.ReadKey(true);

                switch (keyPress.Key)
                {
                    case ConsoleKey.A:
                        playerDX = -1;
                        playerDY = 0;
                        break;
                    case ConsoleKey.S:
                        playerDX = 0;
                        playerDY = 1;
                        break;
                    case ConsoleKey.D:
                        playerDX = 1;
                        playerDY = 0;
                        break;
                    case ConsoleKey.W:
                        playerDX = 0;
                        playerDY = -1;
                        break;
                    case ConsoleKey.LeftArrow:
                        playerDX = -1;
                        playerDY = 0;
                        break;
                    case ConsoleKey.DownArrow:
                        playerDX = 0;
                        playerDY = 1;
                        break;
                    case ConsoleKey.RightArrow:
                        playerDX = 1;
                        playerDY = 0;
                        break;
                    case ConsoleKey.UpArrow:
                        playerDX = 0;
                        playerDY = -1;
                        break;
                }

                


            };
            //END MAIN GAME LOOP
            Console.SetCursorPosition(0, 19);
            Console.WriteLine("\n" + "BOOM!");
            Console.ReadLine();
        }

        static bool UpdateScreen(int pX, int pY)
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < gameboard.GetLength(1); i++)
            {
                for (int j = 0; j < gameboard.GetLength(0); j++)
                {
                    int s = gameboard[j, i];

                    if(historyAge[j,i] > -1)
                        historyAge[j, i]--;

                    if (j == pX && i == pY) //MARK THE PLAYER'S POSITION
                    {
                        Console.SetCursorPosition(j, i);
                        Console.Write('O');
                        historyAge[j, i] = playerLength;
                        if(j == foodX && i == foodY)
                        {
                            playerLength++;
                            do
                            {
                                foodX = rand.Next(0, 39);
                                foodY = rand.Next(0, 19);
                            }
                            while ((historyAge[foodX, foodY] > -1) ^ ((foodX == pX && foodY == pY)));


                            Console.SetCursorPosition(foodX, foodY);
                            Console.Write('X');
                        }
                    }
                    else if (historyAge[j, i] > -1 && (!(j == pX && i == pY))) //IS CURRENTHISTORY AND NOT CURRENTPOSITION
                    {
                        //Console.SetCursorPosition(j, i);
                        //Console.Write(historyAge[j, i]);
                    }                 
                    else
                    {
                        Console.SetCursorPosition(j, i);
                        Console.Write('.');
                    }
                    if(j == foodX && i == foodY)
                    {
                        Console.SetCursorPosition(j, i);
                        Console.Write('X');
                    }

                    Console.SetCursorPosition(0, 20);
                    Console.WriteLine(foodX + ", " + foodY + "   ");


                }
            }

            
            playerHistory[playerX, playerY] = true; //PLAYER HAS BEEN ON THIS SPACE
            //historyAge[playerX, playerY] = 0;

            playerX += playerDX;
            playerY += playerDY;

            if (playerX < 40 && playerY < 20 && playerX > -1 && playerY > -1 && !gameOver && (historyAge[playerX,playerY] < 0)) //OUT OF BOUNDS OR INTO SELF?
                return true; //KEEP PLAYING
            else
                return false; //GAMEOVER
        }
    }
}
