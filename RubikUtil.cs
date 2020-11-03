using System;
using System.Linq;

namespace Rubik
{
    public static class RubikUtil
    {
        public static void PrintSidesData(RubikCube cube)
        {
            var colorNames = Enum.GetNames(typeof(RubikColor));
            
            for (var y = 0; y < 3; y++)
            {
                Console.Write("         ");
                for (var x = 0; x < 3; x++)
                    RubikUtil.PrintColoredSymbol(colorNames[cube.Sides[(int) RSide.Up].Faces[x, y].Color].First());

                Console.WriteLine();
            }

            for (var y = 0; y < 3; y++)
            {
                for (var x = 0; x < 3; x++)
                    RubikUtil.PrintColoredSymbol(colorNames[cube.Sides[(int) RSide.Left].Faces[x, y].Color].First());
                for (var x = 0; x < 3; x++)
                    RubikUtil.PrintColoredSymbol(colorNames[cube.Sides[(int) RSide.Front].Faces[x, y].Color].First());
                for (var x = 0; x < 3; x++)
                    RubikUtil.PrintColoredSymbol(colorNames[cube.Sides[(int) RSide.Right].Faces[x, y].Color].First());
                for (var x = 0; x < 3; x++)
                    RubikUtil.PrintColoredSymbol(colorNames[cube.Sides[(int) RSide.Back].Faces[x, y].Color].First());
                Console.WriteLine();
            }

            for (var y = 0; y < 3; y++)
            {
                Console.Write("         ");
                for (var x = 0; x < 3; x++)
                    RubikUtil.PrintColoredSymbol(colorNames[cube.Sides[(int) RSide.Down].Faces[x, y].Color].First());

                Console.WriteLine();
            }
        }

        public static string RotationCommandToText((RSide, RotationType) command)
        {
            var (rubikSide, rotationType) = command;
            var txt = rubikSide switch
            {
                RSide.Front => "F",
                RSide.Left => "L",
                RSide.Right => "R",
                RSide.Down => "D",
                RSide.Up => "U",
                RSide.Back => "B",
                _ => throw new ArgumentOutOfRangeException()
            };
            txt += rotationType switch
            {
                RotationType.Clockwise => "",
                RotationType.CounterClockwise => "'",
                RotationType.Halfturn => "2",
                _ => throw new ArgumentOutOfRangeException()
            };
            return txt;
        }
        
        public static (RotationType, RSide) TextToRotationCommand(string text)
        {
            RotationType type;
            RSide side;

            switch (text)
            {
                case "F": case "B": case "R": case "L": case "U": case "D":
                    type = RotationType.Clockwise;
                    break;
                case "F'": case "B'": case "R'": case "L'": case "U'": case "D'":
                    type = RotationType.CounterClockwise;
                    break;
                case "F2": case "B2": case "R2": case "L2": case "U2": case "D2":
                    type = RotationType.Halfturn;
                    break;
                default:
                    throw new Exception("Can not resolve rotation command from text: [" + text + "]");
            }
        
            switch (text)
            {
                case "F": case "F2": case "F'":
                    side = RSide.Front;
                    break;
                case "B": case "B2": case "B'":
                    side = RSide.Back;
                    break;
                case "L": case "L2": case "L'":
                    side = RSide.Left;
                    break;
                case "R": case "R2": case "R'":
                    side = RSide.Right;
                    break;
                case "U": case "U2": case "U'":
                    side = RSide.Up;
                    break;
                case "D": case "D2": case "D'":
                    side = RSide.Down;
                    break;
                default:
                    throw new Exception("Can not resolve rotation command from text: [" + text + "]");
            }
            return (type, side);
        }
        
        public static void PrintColoredSymbol(char symbol)
        {
            Console.BackgroundColor = Console.ForegroundColor = symbol switch
            {
                'R' => ConsoleColor.Red,
                'G' => ConsoleColor.Green,
                'B' => ConsoleColor.Blue,
                'Y' => ConsoleColor.Yellow,
                'W' => ConsoleColor.White,
                'O' => ConsoleColor.Magenta,
                _ => Console.ForegroundColor
            };
            Console.Write("   ");
            // Console.Write("[{0}]", symbol);
            Console.ResetColor();
        }
    }
}
