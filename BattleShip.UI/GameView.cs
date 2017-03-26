using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;

namespace BattleShip.UI
{
    class GameView
    {
        public void DisplayBoard(Board board)
        {
            //battleship is going to be 10 by 10 instead of 3 by 3
            for (int row = 0; row < 11; row++)
            {
                for (int colums = 0; colums < 11; colums++)
                {
                    if (row == 0 && colums == 0)
                    {
                        Console.Write("   ");
                    }
                    else if (row == 0 && colums > 0)
                    {
                        Console.Write(char.ConvertFromUtf32(colums + 64) );
                        if (colums < 10)
                        {
                            Console.Write("|");
                        }
                    }
                    else if (colums == 0 && row > 0)
                    {
                        Console.Write(row.ToString().PadRight(2) + " ");
                    }
                    else
                    {
                        switch (board.CheckCoordinate(new Coordinate (colums, row)))
                        {
                            case ShotHistory.Hit:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("H");
                                Console.ResetColor();
                                break;
                            case ShotHistory.Miss:
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("M");
                                Console.ResetColor();
                                break;
                            case ShotHistory.Unknown:
                                Console.Write("X");
                                break;

                        }
                        if (colums < 10)
                        {
                            Console.Write("|");
                        }
                    }

                }
                Console.WriteLine();
            }

        }























    }
}
