using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Ships;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.UI;




namespace BattleShip.UI
{
    public class GameInput
    {
        public string Name { get; set; }
        public string Userinput { get; set; }
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public string xander { get; set; }
        public string yonder { get; set; }



        public void WelcomeScreen()
        {
            Console.WriteLine(
@"
           _____                         ____        _   _   _           _     _         ____                _ 
          / ____|                       |  _ \      | | | | | |         | |   (_)       |  _ \              | |
         | (___  _   _ _ __   ___ _ __  | |_) | __ _| |_| |_| | ___  ___| |__  _ _ __   | |_) |_ __ ___  ___| |
          \___ \| | | | '_ \ / _ \ '__| |  _ < / _` | __| __| |/ _ \/ __| '_ \| | '_ \  |  _ <| '__/ _ \/ __| |
          ____) | |_| | |_) |  __/ |    | |_) | (_| | |_| |_| |  __/\__ \ | | | | |_) | | |_) | | | (_) \__ \_|
         |_____/ \__,_| .__/ \___|_|    |____/ \__,_|\__|\__|_|\___||___/_| |_|_| .__/  |____/|_|  \___/|___(_)
                      | |                                                       | |                            
                      |_|                                                       |_|                            


                                    `. |    / \
                                      ||   /   \____.--=''''==--.._
                                      ||_.'--=='    |__  __  __  _.'
                                      ||  |    |    |\ ||  ||  || |                        ___
                         ____         ||__|____|____| \||__||__||_/    __________________/|   |
                        |    |______  |===.---. .---.========''''=-._ |     |     |     / |   |
                        |    ||     |\| |||   | |   |      '===' ||  \|_____|_____|____/__|___|
                        |-.._||_____|_\___'---' '---'______....---===''======//=//////========|
                        |--------------\------------------/-----------------//-//////---------/
                        |               \                /                 // //////         /
                        |                \______________/                 // //////         /
                        |                                        _____===//=//////=========/
                        |=================================================================/
                        '----------------------------------------------------------------`



                                             Press any key to continue, Bro!
");
            Console.ReadLine();
            Console.Clear();

        }



        public static ShipDirection PromptForDirection(string message)
        {
            while (true)
            {
                //Step 1 Prompt User with Message
                Console.WriteLine(message);
                //Step 2 Get User Input
                    switch (Console.ReadLine().ToUpper())
                    {
                    case "U":
                        return ShipDirection.Up;
                    case "D":
                        return ShipDirection.Down;
                    case "L":
                        return ShipDirection.Left;
                    case "R":
                        return ShipDirection.Right;
                    default:
                        Console.WriteLine($"Come on Bro, that is not a valid direction");
                        Console.ReadLine();
                        break;
                    }

            }
        }
        public static Coordinate PromptForCoordinate(string message, string name, ShipType ship)
        {
            while (true)
            {
                //Step 1 Prompt User with Message;
                Console.WriteLine(message);
                //Step 2 Get User Input
                string userInput = Console.ReadLine().ToUpper();
                //Step 3 Parse user input into x and y
                if (string.IsNullOrEmpty(userInput) || (userInput.Length == 1) || (userInput.Length > 3))
                {
                    Console.WriteLine("Try Again");
                    continue;
                }
                else
                {
                    int x;
                    int y;
                    x = userInput[0] - 64;
                    if (int.TryParse(userInput.Substring(1), out y))
                    {

                        y = int.Parse(userInput.Substring(1));
                        //Step 4 Validate user Coordinate
                        if (x >= 1 && x <= 10 && y >= 1 && y <= 10)
                        {
                            //Step 5 If Valide return Coordinate
                            return new Coordinate(x, y);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Coordinate Bro!");
                            Console.ReadLine();
                        }
                    } else
                    {
                        Console.WriteLine("Invalid Coordinate Bro!");
                        Console.ReadLine();
                    }
                }
            }

        }

        public static string PromptForString(string message)
        {
            while (true)
            {
                Console.WriteLine(message);
                string userInput = Console.ReadLine();
                if (string.IsNullOrEmpty(userInput))
                {
                    Console.WriteLine("Input is required");
                    continue;
                }
                return userInput;
            }
        }

        public static Coordinate PromptForShotCoordinate(string message)
        {
            while (true)
            {
                //Step 1 Prompt User with Message;
                Console.WriteLine(message);
                //Step 2 Get User Input
                string userInput = Console.ReadLine().ToUpper();
                //Step 3 Parse user input into x and y
                if (string.IsNullOrEmpty(userInput) || (userInput.Length == 1) || (userInput.Length > 3))
                {
                    Console.WriteLine("Try Again");
                    continue;
                }
                else
                {
                    int x;
                    int y;
                    x = userInput[0] - 64;
                    if (int.TryParse(userInput.Substring(1), out y))
                    {

                        y = int.Parse(userInput.Substring(1));
                        //Step 4 Validate user Coordinate
                        if (x >= 1 && x <= 10 && y >= 1 && y <= 10)
                        {
                            //Step 5 If Valide return Coordinate
                            return new Coordinate(x, y);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Coordinate Bro!");
                            Console.ReadLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Coordinate Bro!");
                        Console.ReadLine();
                    }
                }
            }
        }

        public void EndGame()
        {
            Console.WriteLine("Dude Bro's would you like to play again? Enter y or press any key to quit.");
            Userinput = Console.ReadLine();


            if (Userinput == "y" || Userinput == "Y")
            {
                Console.Clear();
                BattleShipGame StartGame = new BattleShipGame();
                StartGame.PlayGame();
            }
            else
            {
                return;
            }
            
        }
        public static void ConsoleWriteReadClear(string message)
        {
            Console.WriteLine(message);
            Console.ReadLine();
            Console.Clear();
        } 
       
    }
    
    


}
