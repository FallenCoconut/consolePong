using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace consolePong
{
    internal class Program
    {
        public static int gameBorderHeight = 20, gameBorderLenght = 80; //Rows & Columns
        public static char borderSymbol = '■', scoreSymbol = '□';

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.CursorVisible = false;

            int leftScore = 0, rightScore = 0;
            int scoreboardX = gameBorderLenght / 2 - 2, scoreboardY = gameBorderHeight + 3;
            string winnerString = "";

            char paddleSymbol = '|', ballSymbol = '●';
            int leftPaddlePosition = 0, rightPaddlePosition = 0, paddleSize = 3;

            int ballPositionX = gameBorderLenght/2, ballPositionY = gameBorderHeight / 2;
            bool ballGoesRight = false, ballGoesDown = true;

            Console.SetCursorPosition(0, 0);
            DrawGameField();
            Console.SetCursorPosition(gameBorderLenght/2 - 16, gameBorderHeight /2);
            Console.WriteLine("Press any key to start");
            Console.ReadKey();

            //Game runtime
            while (true)
            {
                Console.SetCursorPosition(0, 0);
                DrawGameField();
                for (int i = 0; i<paddleSize; i++)
                {
                    Console.SetCursorPosition(1, i + 1 + leftPaddlePosition);
                    Console.WriteLine(paddleSymbol);
                    Console.SetCursorPosition(gameBorderLenght -2, i + 1 + rightPaddlePosition);
                    Console.WriteLine(paddleSymbol);
                }

                //Ball & Score updater
                while (!Console.KeyAvailable)
                {
                    Console.SetCursorPosition(scoreboardX, scoreboardY);
                    Console.WriteLine("{0} | {1}", leftScore, rightScore);
                    if(leftScore == 10) { winnerString = "Left"; goto endGame; }
                    else if (leftScore == 10) { winnerString = "Right"; goto endGame; }

                    Console.SetCursorPosition(ballPositionX, ballPositionY);
                    Console.WriteLine(ballSymbol);
                    Thread.Sleep(100);
                    Console.SetCursorPosition(ballPositionX, ballPositionY);
                    Console.WriteLine(" ");

                    if (ballGoesDown) { ballPositionY++; }
                    else { ballPositionY--; }
                    if (ballGoesRight) { ballPositionX++; }
                    else { ballPositionX--; }

                    if(ballPositionY == 1 || ballPositionY == gameBorderHeight - 2) { ballGoesDown = !ballGoesDown; }

                    if(ballPositionX == 2)
                    {
                        if(ballPositionY >= leftPaddlePosition + 1 && ballPositionY <= leftPaddlePosition + paddleSize) { ballGoesRight = !ballGoesRight;}
                        else { 
                            rightScore++; ballPositionY = gameBorderHeight / 2; ballPositionX = gameBorderLenght / 2;
                        }
                    }
                    if (ballPositionX == gameBorderLenght-3)
                    {
                        if (ballPositionY >= rightPaddlePosition + 1 && ballPositionY <= rightPaddlePosition + paddleSize) { ballGoesRight = !ballGoesRight; }
                        else { 
                            leftScore++; ballPositionY = gameBorderHeight / 2; ballPositionX = gameBorderLenght / 2;
                        }
                    }


                }

                //Input segment
                switch(Console.ReadKey().Key)
                {
                    case ConsoleKey.Escape: winnerString = "None"; goto endGame;

                    case ConsoleKey.UpArrow:
                        if(rightPaddlePosition > 0) { rightPaddlePosition--; }
                        break;

                    case ConsoleKey.DownArrow: 
                        if(rightPaddlePosition < gameBorderHeight - paddleSize - 2) { rightPaddlePosition++;}
                        break;

                    case ConsoleKey.W:
                        if (leftPaddlePosition > 0) { leftPaddlePosition--; }
                        break;

                    case ConsoleKey.S:
                        if (leftPaddlePosition < gameBorderHeight - paddleSize - 2) { leftPaddlePosition++; }
                        break;
                }
            }

        endGame:;
            Console.SetCursorPosition(gameBorderLenght / 2 - 5, scoreboardY);
            Console.WriteLine("Game Ended!");
            Console.SetCursorPosition(gameBorderLenght / 2 - 6, scoreboardY+1);
            Console.WriteLine("Winner: {0}", winnerString);
            Console.SetCursorPosition(gameBorderLenght / 2 - 10, scoreboardY+2);
            Console.WriteLine("Pres any key to end...");

            Console.ReadKey();
        }

        static void DrawGameField()
        {
            string board = "";
            for (int i = 0; i < gameBorderHeight; i++)
            {
                for (int j = 0; j < gameBorderLenght; j++)
                {
                    if (i == 0 || i == gameBorderHeight - 1) { board += borderSymbol; }
                    else { board += " "; }
                }
                board += "\n";
            }

            Console.WriteLine(board);
        }
    }
}
