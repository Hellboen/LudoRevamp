using System;
using System.Collections.Generic;
using System.Linq;

namespace LudoRevamp
{
    internal class Program
    {
        private static readonly Random getrandom = new Random();
        public static int CurrentTurn = 1;
        public static int MoveType;
        public static int ChosenPiece;

        //Blue Player
        public static List<Pawn> Blu = new List<Pawn>
        {
            new Pawn("1", 10, 10, 3),
            new Pawn("2", 13, 10, 3),
            new Pawn("3", 10, 13, 3),
            new Pawn("4", 13, 13, 3)
        };

        //Green Player
        public static List<Pawn> Grn = new List<Pawn>
        {
            new Pawn("1", 1, 1, 0),
            new Pawn("2", 4, 1, 0),
            new Pawn("3", 1, 4, 0),
            new Pawn("4", 4, 4, 0)
        };

        //Red Player
        public static List<Pawn> Red = new List<Pawn>
        {
            new Pawn("1", 10, 1, 1),
            new Pawn("2", 13, 1, 1),
            new Pawn("3", 10, 4, 1),
            new Pawn("4", 13, 4, 1)
        };

        //Yellow Player
        public static List<Pawn> Yel = new List<Pawn>
        {
            new Pawn("1", 1, 10, 2),
            new Pawn("2", 4, 10, 2),
            new Pawn("3", 1, 13, 2),
            new Pawn("4", 4, 13, 2)
        };

        //The shared playing field
        public static List<Fields> CommonFields = new List<Fields>
        {
            new Fields(0, 0, 6),
            new Fields(1, 1, 6),
            new Fields(2, 2, 6),
            new Fields(3, 3, 6),
            new Fields(4, 4, 6),
            new Fields(5, 5, 6),
            new Fields(6, 6, 5),
            new Fields(7, 6, 4),
            new Fields(8, 6, 3),
            new Fields(9, 6, 2),
            new Fields(10, 6, 1),
            new Fields(11, 6, 0),
            new Fields(12, 7, 0),
            new Fields(13, 8, 0),
            new Fields(14, 8, 1),
            new Fields(15, 8, 2),
            new Fields(16, 8, 3),
            new Fields(17, 8, 4),
            new Fields(18, 8, 5),
            new Fields(19, 9, 6),
            new Fields(20, 10, 6),
            new Fields(21, 11, 6),
            new Fields(22, 12, 6),
            new Fields(23, 13, 6),
            new Fields(24, 14, 6),
            new Fields(25, 14, 7),
            new Fields(26, 14, 8),
            new Fields(27, 13, 8),
            new Fields(28, 12, 8),
            new Fields(29, 11, 8),
            new Fields(30, 10, 8),
            new Fields(31, 9, 8),
            new Fields(32, 8, 9),
            new Fields(33, 8, 10),
            new Fields(34, 8, 11),
            new Fields(35, 8, 12),
            new Fields(36, 8, 13),
            new Fields(37, 8, 14),
            new Fields(38, 7, 14),
            new Fields(39, 6, 14),
            new Fields(40, 6, 13),
            new Fields(41, 6, 12),
            new Fields(42, 6, 11),
            new Fields(43, 6, 10),
            new Fields(44, 6, 9),
            new Fields(45, 5, 8),
            new Fields(46, 4, 8),
            new Fields(47, 3, 8),
            new Fields(48, 2, 8),
            new Fields(49, 1, 8),
            new Fields(50, 0, 8),
            new Fields(51, 0, 7)
        };

        //The individual players' homestretches
        public static List<Fields> HomeBlue = new List<Fields>
        {
            new Fields(5, 8, 7),
            new Fields(4, 9, 7),
            new Fields(3, 10, 7),
            new Fields(2, 11, 7),
            new Fields(1, 12, 7),
            new Fields(0, 13, 7)
        };

        public static List<Fields> HomeGreen = new List<Fields>
        {
            new Fields(0, 1, 7),
            new Fields(1, 2, 7),
            new Fields(2, 3, 7),
            new Fields(3, 4, 7),
            new Fields(4, 5, 7),
            new Fields(5, 6, 7)
        };

        public static List<Fields> HomeRed = new List<Fields>
        {
            new Fields(0, 7, 1),
            new Fields(1, 7, 2),
            new Fields(2, 7, 3),
            new Fields(3, 7, 4),
            new Fields(4, 7, 5),
            new Fields(5, 7, 6)
        };

        public static List<Fields> HomeYellow = new List<Fields>
        {
            new Fields(5, 7, 8),
            new Fields(4, 7, 9),
            new Fields(3, 7, 10),
            new Fields(2, 7, 11),
            new Fields(1, 7, 12),
            new Fields(0, 7, 13)
        };

        public static string[] Teams = new string[4]
        {
            "Green's turn", "Red's turn", "Yellow's turn", "Blue's turn"
        };

        private static void Main()
        {
            Console.SetWindowSize(19, 19);
            Console.SetBufferSize(19, 19);
            
            //Used for debugging when I want to test the kill functionality.
            //Red[0].field = CommonFields[7].field;
            //Red[0].xPos = CommonFields[7].xPos;
            //Red[0].yPos = CommonFields[7].yPos;
            //CommonFields[7].occupants.Add(Red[0]);
            Game();
        }

        //Main game loop.
        private static void Game()
        {
            while (!PlayerWon())
            {
                Console.Clear();
                RenderBoard();
                DebugMode();
                Console.ReadKey(true);

                var diceRoll = getrandom.Next(1, 7);
                //int diceRoll = 6;
                if (diceRoll == 6)
                {
                    Console.Clear();

                    Console.SetCursorPosition(0, 0);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine(Teams[CurrentTurn - 1]);
                    Console.WriteLine($"You rolled a {diceRoll}!");
                    Console.WriteLine("Type 1 to move an ");
                    Console.WriteLine("active pawn on the board");
                    Console.WriteLine("");
                    Console.WriteLine("Type 2 to spawn an");
                    Console.WriteLine("inactive pawn");
                    Console.BackgroundColor = ConsoleColor.Black;

                    //Makes sure the player doesn't type some stupid number or string.
                    var inputSuccess = false;
                    while (!inputSuccess)
                    {
                        var moveInput = Console.ReadLine();
                        if (int.TryParse(moveInput, out var userMoveResult) && userMoveResult >= 1 && userMoveResult <= 4)
                        {
                            inputSuccess = true;
                            MoveType = userMoveResult;
                        }
                        else Console.WriteLine("Type a valid number please :)");
                    }

                    Console.WriteLine("Now enter which piece to manipulate(1 - 4)");

                    inputSuccess = false;
                    while (!inputSuccess)
                    {
                        var userInput = Console.ReadLine();
                        if (int.TryParse(userInput, out var userPiece) && userPiece >= 1 && userPiece <= 4)
                        {
                            ChosenPiece = userPiece - 1;
                            inputSuccess = true;
                        }
                        else Console.WriteLine("1-4, please");
                    }
                }
                else if (diceRoll != 6)
                {
                    Console.Clear();
                    MoveType = 1;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine(Teams[CurrentTurn - 1]);

                    Console.WriteLine($"You rolled a {diceRoll}!");
                    Console.WriteLine("Which pawn would ");
                    Console.WriteLine("you like to move?");
                    Console.WriteLine("(1 - 4)");
                    Console.BackgroundColor = ConsoleColor.Black;

                    var inputSuccess = false;
                    while (!inputSuccess)
                    {
                        var userInput = Console.ReadLine();
                        if (int.TryParse(userInput, out var userPiece) && userPiece >= 1 && userPiece <= 4)
                        {
                            ChosenPiece = userPiece - 1;
                            inputSuccess = true;
                        }
                        else Console.WriteLine("1-4, please");
                    }

                }

                //The switch that decides whether to spawn or move a piece
                switch (MoveType)
                {
                    case 1:
                        switch (CurrentTurn)
                        {
                            case 1:
                                if (!Grn[ChosenPiece].isHome())
                                {
                                    HandleMove(Grn[ChosenPiece], diceRoll);
                                    if (diceRoll == 6)
                                        break;
                                    CurrentTurn = 2;
                                }
                                else if (Grn[ChosenPiece].isHome())
                                {
                                    Console.Clear();
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    Console.WriteLine("This piece is home and cannot be ");
                                    Console.WriteLine("manipulated");
                                    Console.ReadKey();
                                    if (diceRoll == 6)
                                        break;
                                    CurrentTurn = 2;
                                }
                                break;

                            case 2:
                                if (!Red[ChosenPiece].isHome())
                                {
                                    HandleMove(Red[ChosenPiece], diceRoll);
                                    if (diceRoll == 6)
                                        break;
                                    CurrentTurn = 3;
                                }
                                else if (Red[ChosenPiece].isHome())
                                {
                                    Console.Clear();
                                    Console.WriteLine("This piece is home and cannot be ");
                                    Console.WriteLine("manipulated");
                                    if (diceRoll == 6)
                                        break;
                                    CurrentTurn = 3;
                                    Console.ReadKey();
                                }

                                break;

                            case 3:
                                if (!Yel[ChosenPiece].isHome())
                                {
                                    HandleMove(Yel[ChosenPiece], diceRoll);
                                    if (diceRoll == 6)
                                        break;
                                    CurrentTurn = 4;
                                }
                                else if (Yel[ChosenPiece].isHome())
                                {
                                    Console.Clear();
                                    Console.WriteLine("This piece is home and cannot be ");
                                    Console.WriteLine("manipulated");
                                    if (diceRoll == 6)
                                        break;
                                    CurrentTurn = 4;
                                    Console.ReadKey();
                                }

                                break;

                            case 4:
                                if (!Blu[ChosenPiece].isHome())
                                {
                                    HandleMove(Blu[ChosenPiece], diceRoll);
                                    if (diceRoll == 6)
                                        break;
                                    CurrentTurn = 1;
                                }
                                else if (Blu[ChosenPiece].isHome())
                                {
                                    Console.Clear();
                                    Console.WriteLine("This piece is home and cannot be ");
                                    Console.WriteLine("manipulated");
                                    if (diceRoll == 6)
                                        break;
                                    CurrentTurn = 1;
                                    Console.ReadKey();
                                }

                                break;
                        }

                        break;

                    case 2:
                        switch (CurrentTurn)
                        {
                            case 1:
                                if (Grn[ChosenPiece].isHome())
                                    HandleSpawn(Grn[ChosenPiece]);
                                break;

                            case 2:
                                if (Red[ChosenPiece].isHome())
                                    HandleSpawn(Red[ChosenPiece]);
                                break;

                            case 3:
                                if (Yel[ChosenPiece].isHome())
                                    HandleSpawn(Yel[ChosenPiece]);
                                break;

                            case 4:
                                if (Blu[ChosenPiece].isHome())
                                    HandleSpawn(Blu[ChosenPiece]);
                                break;
                        }

                        break;
                }
            }

            if (PlayerWon()) Console.WriteLine(WhoWon());
            Console.ReadKey(true);
        }

        /*######################################################*/
        /*################--METHODS AND LOGIC--#################*/
        /*######################################################*/


        //Our general movement method that moves your piece around on the board
        //Calls the Kill method when necessary.
        public static void HandleMove(Pawn pawn, int Roll)
        {

            var res = pawn.field + Roll;

            //Puts the currently selected pawn in its corresponding home stretch
            if (pawn.moveLength + Roll > 50)
            {
                CommonFields[pawn.field].occupants.Remove(pawn);

                if (pawn.team == 0)
                {
                    var homeRes = pawn.moveLength + Roll - 52;
                    pawn.isHomeStretch = true;
                    pawn.field = HomeGreen[homeRes].field;
                    HomeGreen[homeRes].occupants.Add(pawn);
                    pawn.xPos = HomeGreen[homeRes].xPos;
                    pawn.yPos = HomeGreen[homeRes].yPos;
                }

                else if (pawn.team == 1)
                {
                    var homeRes = pawn.moveLength + Roll - 52;
                    pawn.isHomeStretch = true;
                    pawn.field = HomeRed[homeRes].field;

                    HomeRed[homeRes].occupants.Add(pawn);
                    pawn.xPos = HomeRed[homeRes].xPos;
                    pawn.yPos = HomeRed[homeRes].yPos;


                }
                else if (pawn.team == 2)
                {
                    var homeRes = pawn.moveLength + Roll - 52;
                    pawn.isHomeStretch = true;
                    pawn.field = HomeYellow[homeRes].field;

                    HomeYellow[homeRes].occupants.Add(pawn);
                    pawn.xPos = HomeYellow[homeRes].xPos;
                    pawn.yPos = HomeYellow[homeRes].yPos;

                }

                else if (pawn.team == 3)
                {
                    var homeRes = pawn.moveLength + Roll - 52;
                    pawn.isHomeStretch = true;
                    pawn.field = HomeBlue[homeRes].field;

                    HomeBlue[homeRes].occupants.Add(pawn);
                    pawn.xPos = HomeBlue[homeRes].xPos;
                    pawn.yPos = HomeBlue[homeRes].yPos;

                }

            }

            if (pawn.isHomeStretch)
            {
                if (pawn.team == 0)

                {
                    if (pawn.field + Roll > 5)
                    {

                        pawn.field = HomeGreen[Math.Abs(5 - (pawn.field + Roll))].field;
                        pawn.xPos = HomeGreen[Math.Abs(5 - (pawn.field + Roll))].xPos;
                        pawn.yPos = HomeGreen[Math.Abs(5 - (pawn.field + Roll))].yPos;
                    }

                    else if (pawn.field + Roll == 5) pawn.hasWon = true;
                }

                else if (pawn.team == 1)
                {
                    if (pawn.field + Roll > 5)
                    {
                        pawn.field = HomeRed[Math.Abs(5 - (pawn.field + Roll))].field;
                        pawn.xPos = HomeRed[Math.Abs(5 - (pawn.field + Roll))].xPos;
                        pawn.yPos = HomeRed[Math.Abs(5 - (pawn.field + Roll))].yPos;
                    }

                    else if (pawn.field + Roll == 5) pawn.hasWon = true;
                }
                else if (pawn.team == 2)
                {
                    if (pawn.field + Roll > 5)
                    {

                        pawn.field = HomeYellow[Math.Abs(5 - (pawn.field + Roll))].field;
                        pawn.xPos = HomeYellow[Math.Abs(5 - (pawn.field + Roll))].xPos;
                        pawn.yPos = HomeYellow[Math.Abs(5 - (pawn.field + Roll))].yPos;
                    }

                    else if (pawn.field + Roll == 5) pawn.hasWon = true;
                }
                else if (pawn.team == 3)
                {
                    if (pawn.field + Roll > 5)
                    {

                        pawn.field = HomeBlue[Math.Abs(5 - (pawn.field + Roll))].field;
                        pawn.xPos = HomeBlue[Math.Abs(5 - (pawn.field + Roll))].xPos;
                        pawn.yPos = HomeBlue[Math.Abs(5 - (pawn.field + Roll))].yPos;
                    }

                    else if (pawn.field + Roll == 5) pawn.hasWon = true;

                }
            }

            if (res > 51) res -= 51;

            if (!pawn.isHomeStretch && CommonFields[res].occupants.Count <= 1)
            {
                if (CheckTeam(pawn, res))
                {
                    HandleKillEnemy(pawn.field, pawn);
                }
                pawn.moveLength += Roll;
                pawn.field = CommonFields[res].field;
                pawn.xPos = CommonFields[res].xPos;
                pawn.yPos = CommonFields[res].yPos;
                CommonFields[res].occupants.Add(pawn);
                CommonFields[pawn.field].occupants.Remove(pawn);

            }

            else if (!pawn.isHomeStretch && CommonFields[pawn.field + Roll].occupants.Count >= 2 && CheckTeam(pawn, res))
            {
                CommonFields[pawn.field].occupants.Remove(pawn);
                pawn.xPos = pawn.xDef;
                pawn.yPos = pawn.yDef;
                pawn.moveLength = 0;
                pawn.field = 0;
            }
        }


        //Our kill handler that sends enemy pieces you land on home
        //and sends you home if the field is occupied by two enemy pieces.
        //also makes sure you don't send yourself home.
        public static void HandleKillEnemy(int res, Pawn pawn)
        {
            for (var a = 0; a < 4; a++)
                if (Grn[a].field == CommonFields[res + 6].field && !CheckTeam(pawn, res))
                {
                    Grn[a].xPos = Grn[a].xDef;
                    Grn[a].yPos = Grn[a].yDef;
                    Grn[a].moveLength = 0;
                    Grn[a].field = 0;
                    CommonFields[res + 6].occupants.Remove(Grn[a]);
                }
                else if (Red[a].field == CommonFields[res + 6].field && !CheckTeam(pawn, res))
                {
                    Red[a].xPos = Red[a].xDef;
                    Red[a].yPos = Red[a].yDef;
                    Red[a].moveLength = 0;
                    Red[a].field = 0;
                    CommonFields[res + 6].occupants.Remove(Red[a]);
                }
                else if (Yel[a].field == CommonFields[res + 6].field && !CheckTeam(pawn, res))
                {
                    Yel[a].xPos = Yel[a].xDef;
                    Yel[a].yPos = Yel[a].yDef;
                    Yel[a].moveLength = 0;
                    Yel[a].field = 0;
                    CommonFields[res + 6].occupants.Remove(Yel[a]);
                }
                else if (Blu[a].field == CommonFields[res + 6].field && !CheckTeam(pawn, res))
                {
                    Blu[a].xPos = Blu[a].xDef;
                    Blu[a].yPos = Blu[a].yDef;
                    Blu[a].moveLength = 0;
                    Blu[a].field = 0;
                    CommonFields[res + 6].occupants.Remove(Blu[a]);
                }
        }

        //Checks for team before killing
        public static bool CheckTeam(Pawn pawn, int res)
        {

            return CommonFields[res].occupants.Any(item => item.team != pawn.team);
        }

        //This method handles our spawn.
        public static void HandleSpawn(Pawn pawn)
        {
            switch (pawn.team)
            {
                case 0:
                    CommonFields[1].occupants.Add(pawn);
                    pawn.moveLength = 0;
                    pawn.field = CommonFields[1].field;
                    pawn.xPos = CommonFields[1].xPos;
                    pawn.yPos = CommonFields[1].yPos;
                    break;
                case 1:
                    CommonFields[14].occupants.Add(pawn);
                    pawn.moveLength = 0;
                    pawn.field = CommonFields[14].field;
                    pawn.xPos = CommonFields[14].xPos;
                    pawn.yPos = CommonFields[14].yPos;
                    break;
                case 2:
                    CommonFields[40].occupants.Add(pawn);
                    pawn.moveLength = 0;
                    pawn.field = CommonFields[40].field;
                    pawn.xPos = CommonFields[40].xPos;
                    pawn.yPos = CommonFields[40].yPos;
                    break;
                case 3:
                    CommonFields[27].occupants.Add(pawn);
                    pawn.moveLength = 0;
                    pawn.field = CommonFields[27].field;
                    pawn.xPos = CommonFields[27].xPos;
                    pawn.yPos = CommonFields[27].yPos;
                    break;
            }
        }

        /*######################################################*/
        /*################--RENDERS AND BOOLEANS--#################*/
        /*######################################################*/

        public static bool PlayerWon()
        {
            var resultGrn = 0;
            var resultRed = 0;
            var resultYel = 0;
            var resultBlu = 0;
            for (var a = 0; a < 4; a++)
                if (Grn[a].hasWon)
                {
                    resultGrn += 1;
                    if (resultGrn == 4) return true;
                }
                else if (Red[a].hasWon)
                {
                    resultRed += 1;
                    if (resultRed == 4) return true;
                }
                else if (Yel[a].hasWon)
                {
                    resultYel += 1;
                    if (resultYel == 4) return true;
                }
                else if (Blu[a].hasWon)
                {
                    resultBlu += 1;
                    if (resultBlu == 4) return true;
                }

            return false;
        }

        //Checks who won the game
        public static string WhoWon()
        {
            var resultGrn = 0;
            var resultRed = 0;
            var resultYel = 0;
            var resultBlu = 0;
            for (var a = 0; a < 4; a++)
                if (Grn[a].hasWon)
                {
                    resultGrn += 1;
                    if (resultGrn == 4) return "Green has won the game!";
                }
                else if (Red[a].hasWon)
                {
                    resultRed += 1;
                    if (resultRed == 4) return "Red has won the game!";
                }
                else if (Yel[a].hasWon)
                {
                    resultYel += 1;
                    if (resultYel == 4) return "Yellow has won the game!";
                }
                else if (Blu[a].hasWon)
                {
                    resultBlu += 1;
                    if (resultBlu == 4) return "Blue has won the game!";
                }

            return "";
        }

        //Renders our board using our RenderRectangle method.
        private static void RenderBoard()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();

            Console.SetCursorPosition(2, 2);
            Console.BackgroundColor = ConsoleColor.Green;

            Console.BackgroundColor = ConsoleColor.White;
            RenderRectangle(2, 2, 15, 15);

            Console.BackgroundColor = ConsoleColor.Black;
            RenderRectangle(8, 8, 3, 3);

            Console.BackgroundColor = ConsoleColor.Green;
            RenderRectangle(2, 2, 6, 6);

            Console.BackgroundColor = ConsoleColor.Red;
            RenderRectangle(11, 2, 6, 6);

            Console.BackgroundColor = ConsoleColor.Yellow;
            RenderRectangle(2, 11, 6, 6);

            Console.BackgroundColor = ConsoleColor.Blue;
            RenderRectangle(11, 11, 6, 6);

            Console.BackgroundColor = ConsoleColor.Green;
            RenderRectangle(3, 9, 6, 1);
            Console.SetCursorPosition(3, 8);
            Console.Write(" ");

            Console.BackgroundColor = ConsoleColor.Red;
            RenderRectangle(9, 3, 1, 6);
            Console.SetCursorPosition(10, 3);
            Console.Write(" ");

            Console.BackgroundColor = ConsoleColor.Blue;
            RenderRectangle(10, 9, 6, 1);
            Console.SetCursorPosition(15, 10);
            Console.Write(" ");

            Console.BackgroundColor = ConsoleColor.Yellow;
            RenderRectangle(9, 10, 1, 6);
            Console.SetCursorPosition(8, 15);
            Console.Write(" ");

            foreach (var i in HomeGreen)
            {
                Console.SetCursorPosition(i.xPos + 2, i.yPos + 2);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine(i.field);
            }

            foreach (var i in HomeBlue)
            {
                Console.SetCursorPosition(i.xPos + 2, i.yPos + 2);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine(i.field);
            }

            foreach (var i in HomeRed)
            {
                Console.SetCursorPosition(i.xPos + 2, i.yPos + 2);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine(i.field);
            }

            foreach (var i in HomeYellow)
            {
                Console.SetCursorPosition(i.xPos + 2, i.yPos + 2);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.WriteLine(i.field);
            }

            for (var a = 0; a < 4; a++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.SetCursorPosition(Grn[a].xPos + 2, Grn[a].yPos + 2);
                Console.Write(Grn[a].name);

                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.SetCursorPosition(Red[a].xPos + 2, Red[a].yPos + 2);
                Console.Write(Red[a].name);

                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.SetCursorPosition(Yel[a].xPos + 2, Yel[a].yPos + 2);
                Console.Write(Yel[a].name);

                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.SetCursorPosition(Blu[a].xPos + 2, Blu[a].yPos + 2);
                Console.Write(Blu[a].name);

                for (var i = 0; i < 4; i++)
                {
                    Console.SetCursorPosition(0, 0);
                    if (Grn[a].hasWon)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.Write(i);
                    }
                }
            }

            Console.SetCursorPosition(9, 9);
        }

        private static void RenderRectangle(int X, int Y, int width, int height)
        {
            for (var i = 0; i < height; i++)
            {
                Console.SetCursorPosition(X, Y + i);
                Console.WriteLine(new string(' ', width));
            }
        }

        /*######################################################*/
        /*####################--DEBUGGING--#####################*/
        /*######################################################*/

        //A debug mode to check if certain values correspond with what's happening on the screen.
        private static void DebugMode()
        {
            Console.SetCursorPosition(0, 17);
            for (var a = 0; a < 4; a++)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.Write(Grn[a].field);
            }

            Console.SetCursorPosition(0, 18);
            for (var a = 0; a < 4; a++)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Write(Red[a].field);
            }

            Console.SetCursorPosition(4, 17);
            for (var a = 0; a < 4; a++)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(Yel[a].field);
            }

            Console.SetCursorPosition(4, 18);
            for (var a = 0; a < 4; a++)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(Blu[a].field);
            }


            //Red[1].field = HomeRed[5].field;
            //Red[1].xPos = HomeRed[5].xPos;
            //Red[1].yPos = HomeRed[5].yPos;
            //Red[1].hasWon = true;
            //HomeRed[5].occupants.Add(Red[1]);

            //Red[2].field = HomeRed[5].field;
            //Red[2].xPos = HomeRed[5].xPos;
            //Red[2].yPos = HomeRed[5].yPos;
            //Red[2].hasWon = true;
            //HomeRed[5].occupants.Add(Red[2]);

            //Red[3].field = HomeRed[5].field;
            //Red[3].xPos = HomeRed[5].xPos;
            //Red[3].yPos = HomeRed[5].yPos;
            //Red[3].hasWon = true;
            //HomeRed[5].occupants.Add(Red[3]);

            Console.SetCursorPosition(8, 17);
            Console.Write(Yel[0].moveLength);
            Console.Write(Blu[0].moveLength);

        }
    }
}