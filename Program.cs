
using System;
using System.Diagnostics;

namespace Rubik
{
   public enum RubikColor
   {
      Error = -1,
      Red = 0,
      Blue = 1,
      Green = 2,
      White = 3,
      Yellow = 4,
      Orange = 5
   }
   
   public enum RSide
   {
      Front = 0,
      Left = 1,
      Right = 2,
      Down = 3,
      Up = 4,
      Back = 5
   }
   
   public enum RotationType
   {
      Clockwise = 0,
      CounterClockwise = 1,
      Halfturn = 2
   }

   internal static class Program
   {
      private static void Main(string[] args)
      {
         var cube = new RubikCube();
         var solver = new RubikSolver(cube);
         var keyInfo = new ConsoleKeyInfo();
         
         Console.WriteLine("*DIRECT CONTROL MODE*\n");
         RubikUtil.PrintSidesData(cube);
         while (keyInfo.Key != ConsoleKey.Escape)
         {
            keyInfo = Console.ReadKey(true);
            // Console.Clear();
         
            if (keyInfo.Key != ConsoleKey.Tab)
            {
               Console.WriteLine("*DIRECT CONTROL MODE*\n");
               HandleControl(keyInfo, ref cube, ref solver);
               RubikUtil.PrintSidesData(cube);
            }
            else
            {
               Console.WriteLine("*COMMAND SEQUENCE INPUT MODE*\n");
               RubikUtil.PrintSidesData(cube);
               var str = Console.ReadLine();
               cube.RotateByCommandSequence(str);
               // Console.Clear();
               Console.WriteLine("*DIRECT CONTROL MODE*\n");
               RubikUtil.PrintSidesData(cube);
            }
         }
      }

      private static void HandleControl(ConsoleKeyInfo key, ref RubikCube cube, ref RubikSolver solver)
      {
         var shiftPressed = (key.Modifiers & ConsoleModifiers.Shift) != 0;

         switch (key.Key)
         {
            case ConsoleKey.P:
               PerformanceTest();
               break;
            case ConsoleKey.Z:
               solver.SolveStep0();
               solver.SolveStep1();
               solver.SolveStep2();
               solver.SolveStep3();
               solver.SolveStep4();
               foreach (var rotation in solver.GetRotationsArray())
                  Console.Write(RubikUtil.RotationCommandToText(rotation) + " ");
               Console.WriteLine();
               break;
            case ConsoleKey.X:
               foreach (var rotation in solver.GetRotationsArray())
                  Console.Write(RubikUtil.RotationCommandToText(rotation) + " ");
               Console.WriteLine();
               break;
            case ConsoleKey.C:
               foreach (var rotation in solver.GetRotationsArray(true))
                  Console.Write(RubikUtil.RotationCommandToText(rotation) + " ");
               Console.WriteLine();
               break;
            case ConsoleKey.Q:
               cube = new RubikCube();
               solver = new RubikSolver(cube);
               
               if (shiftPressed)
               {
                  var r = new Random();

                  for (var i = 0; i < 20; i++)
                  {
                     var command = ((RSide) r.Next(0, 6), (RotationType) r.Next(0, 3));
                     cube.RotateSide(command);
                     Console.Write(RubikUtil.RotationCommandToText(command) + " ");
                  }
                  Console.WriteLine();
               }
               break;
            
            case ConsoleKey.G:
               cube.RotateSide(RSide.Left, shiftPressed);
               break;
            case ConsoleKey.Y:
               cube.RotateSide(RSide.Front, shiftPressed);
               break;
            case ConsoleKey.J:
               cube.RotateSide(RSide.Right, shiftPressed);
               break;
            case ConsoleKey.N:
               cube.RotateSide(RSide.Back, shiftPressed);
               break;
            case ConsoleKey.T:
               cube.RotateSide(RSide.Up, shiftPressed);
               break;
            case ConsoleKey.M:
               cube.RotateSide(RSide.Down, shiftPressed);
               break;
         }
      }

      private static void PerformanceTest()
      {
         const int testsCount = 1000;
         long totalTime = 0;
         var sw = Stopwatch.StartNew();
         var rnd = new Random();
         var totalTurnsCountNormalized = 0;
         var totalTurnsCount = 0;
         var totalTurnsStep0 = 0;
         var totalTurnsStep1 = 0;
         var totalTurnsStep2 = 0;
         var totalTurnsStep3 = 0;
         var totalTurnsStep4 = 0;

         for (var i = 0; i < testsCount; i++)
         {
            var r = new RubikCube();
            var s = new RubikSolver(r);

            for (var j = 0; j < 20; j++)
               r.RotateSide(((RSide) rnd.Next(0, 6), (RotationType) rnd.Next(0, 3)));

            sw.Restart();
            s.SolveStep0();
            s.SolveStep1();
            s.SolveStep2();
            s.SolveStep3();
            s.SolveStep4();

            totalTime += sw.ElapsedMilliseconds;
            totalTurnsCountNormalized += s.GetRotationsArray(true).Length;
            totalTurnsCount += s.TotalRotationsCount;
            totalTurnsStep0 += s.RotationsCountStep0;
            totalTurnsStep1 += s.RotationsCountStep1;
            totalTurnsStep2 += s.RotationsCountStep2;
            totalTurnsStep3 += s.RotationsCountStep3;
            totalTurnsStep4 += s.RotationsCountStep3;

            foreach (var side in r.Sides)
            {
               var centerColor = side.Faces[1, 1].Color;
               foreach (var face in side.Faces)
               {
                  if (face.Color != centerColor)
                     Console.WriteLine("BUG! RUBIK IS NOT COMPLETE");
               }
            }
         }

         sw.Stop();
         Console.WriteLine(
            "\nTests: {0}\nAverage time to solve: {1}ms\nAverage turns to solve: {2}\nAverage normalized turns to solve: {3}\nAverage turns for:\n" +
            "\tStep 0: {4}\n\tStep 1: {5}\n\tStep 2: {6}\n\tStep 3: {7}\n\tStep 4: {8}", testsCount,
            totalTime * 1f / testsCount, totalTurnsCount / testsCount, totalTurnsCountNormalized / testsCount,
            totalTurnsStep0 / testsCount, totalTurnsStep1 / testsCount, totalTurnsStep2 / testsCount,
            totalTurnsStep3 / testsCount, totalTurnsStep4 / testsCount);
      }
   }
}

