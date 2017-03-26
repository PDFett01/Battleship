using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Ships;
using BattleShip.BLL.Responses;
using BattleShip.UI;




namespace BattleShip.UI
{
    public class BattleShipGame
    {
        public Player[] Players { get; set; }
        public Board GameBoard { get; set; }



        Player p1 = new Player();
        Player p2 = new Player();
        Player currentplayer = new Player();
        


        GameView view = new GameView();
        public void PlayGame()
        {

            GameInput input = new GameInput();
            //Welcome Screen
            input.WelcomeScreen();
            //Input Player Name
            p1.Name = GameInput.PromptForString("Player 1 please enter your name");
            Console.Clear();
            p2.Name = GameInput.PromptForString("Player 2 please enter your name");
            Console.Clear();
            //Assign Player Board
            AssignCurrentPlayer();
            //Each player has a turn until victory
            Turns();
            //Play again option
            input.EndGame();
        }
      
        public void SetCoordinatesPleaseWork()
        {
        
            GameBoard = new Board();
            currentplayer.Board = GameBoard;
            GameInput.ConsoleWriteReadClear(currentplayer.Name + " prepare to place your ships. Press any key to continue.");
            view.DisplayBoard(currentplayer.Board);

            //refactor
            foreach (ShipType ship in Enum.GetValues(typeof(ShipType)))
            {


                bool isShipPlaced = false;
                do
                {
                    Coordinate coordinate = GameInput.PromptForCoordinate(currentplayer.Name + ", enter a Coordinate to place your " + ship + " Bro! Example B2", currentplayer.Name, ship);
                    ShipDirection direction = GameInput.PromptForDirection($"Dude Bro, Enter a direction for your ship! Example U for Up, D for Down, L for Left, R for Right");
                    PlaceShipRequest placeshiprequest = new PlaceShipRequest()
                    {
                        Coordinate = coordinate,
                        Direction = direction,
                        ShipType = ship
                    };
                    var response = currentplayer.Board.PlaceShip(placeshiprequest);

                    switch (response)
                    {
                        case BLL.Responses.ShipPlacement.NotEnoughSpace:
                            Console.WriteLine("Not Enough Space");
                            break;
                        case BLL.Responses.ShipPlacement.Overlap:
                            Console.WriteLine("Overlaps");
                            break;
                        default:
                            isShipPlaced = true;
                            break;
                    }
                } while (!isShipPlaced);
            }
            Console.Clear();
   
        }

        public void Turns()
        {
            Random rnd = new Random();
            int turn = rnd.Next(1, 3);
            if (turn == 2)
            {
               GameInput.ConsoleWriteReadClear(p1.Name + " you go first. Press any key to continue.");
            }
            else
            {
                GameInput.ConsoleWriteReadClear(p2.Name + " you go first. Press any key to continue.");
            }

            //current player
            ShotStatus status = new ShotStatus();
            do
            {
                if (turn % 2 == 0)
                {
                    status = PlayerTakeTurn(p1.Name, p1.Board);
                    turn++;
                }
                else if (turn % 2 == 1)
                {
                    status = PlayerTakeTurn(p2.Name, p2.Board);
                    turn--;
                }

            } while (status != ShotStatus.Victory);
            Console.ReadLine();
        }

        ShotStatus PlayerTakeTurn(string Name, Board board)
        {
            ShotStatus status = new ShotStatus();
            bool EndTurn = false;
            do
            {
                
                view.DisplayBoard(board);
                Coordinate coordinate = GameInput.PromptForShotCoordinate(Name + ", Enter a coordinate to fire a shot");

                if (coordinate == null)
                {
                    return ShotStatus.Victory;
                }
                var response = board.FireShot(coordinate);

                switch (response.ShotStatus)
                {
                    case ShotStatus.Duplicate:
                        GameInput.ConsoleWriteReadClear(Name + "! Bro get it together that coordinate has already been fired, try again! Press any key to continue.");
                        break;
                    case ShotStatus.Invalid:
                        GameInput.ConsoleWriteReadClear(Name +"! Dude, not a valid coordinate. Press any key to continue.");
                        break;
                    case ShotStatus.Miss:
                        GameInput.ConsoleWriteReadClear("You Missed Dawg, Press any key to continue.");
                        EndTurn = true;
                        break;
                    case ShotStatus.Hit:
                        GameInput.ConsoleWriteReadClear("Hit! Press any key to continue.");
                        EndTurn = true;
                        break;
                    case ShotStatus.HitAndSunk:
                        GameInput.ConsoleWriteReadClear(Name + " you sunk a ship bra! Press any key to continue.");
                        EndTurn = true;
                        break;
                    case ShotStatus.Victory:
                    default:
                        Console.WriteLine(Name + ", you win!");
                        Console.WriteLine("Way to go Bro!");
                        Console.WriteLine("Press any key to continue.");

                        status = response.ShotStatus;
                        EndTurn = true;
                        break;

                }

            } while (!EndTurn);
            
            return status;
            
        }
        public void AssignCurrentPlayer()
        {

                int x = 0;
            while (x <= 1)
            {
                if (x == 0)
                {
                    currentplayer.Name = p1.Name;
                    currentplayer.Board = p1.Board;
                    x++;
                    SetCoordinatesPleaseWork();
                    p1.Board = currentplayer.Board;


                }
                else
                {
                    currentplayer.Name = p2.Name;
                    currentplayer.Board = p2.Board;
                    SetCoordinatesPleaseWork();
                    p2.Board = currentplayer.Board;
                    break;
                }
            }    
        }


    }

    

}


  