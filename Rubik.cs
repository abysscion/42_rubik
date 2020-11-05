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

   internal static class Rubik
   {
      private static void Main(string[] args)
      {
         try
         {
            var cube = new RubikCube();
            var solver = new RubikSolver(cube);
            var keyInfo = new ConsoleKeyInfo();

            if (args.Length != 0)
            {
               cube.RotateByCommandSequence(args[0]);
               solver.SolveStep0();
               solver.SolveStep1();
               solver.SolveStep2();
               solver.SolveStep3();
               solver.SolveStep4();

               var rotations = solver.GetRotationsArray();
               if (rotations.Length > 0)
               {
                  for (var i = 0; i < rotations.Length - 1; i++)
                     Console.Write(RubikUtil.RotationCommandToText(rotations[i]) + " ");
                  Console.Write(RubikUtil.RotationCommandToText(rotations[rotations.Length - 1]) + "\n");
               }
               return;
            }

            Console.WriteLine("*DIRECT CONTROL MODE*\n");
            RubikUtil.PrintSidesData(cube);
            while (keyInfo.Key != ConsoleKey.Escape)
            {
               keyInfo = Console.ReadKey(true);
               Console.Clear();

               if (keyInfo.Key != ConsoleKey.Tab)
               {
                  Console.WriteLine("*DIRECT CONTROL MODE*\n");
                  HandleControl(keyInfo, ref cube);
                  RubikUtil.PrintSidesData(cube);
               }
               else
               {
                  Console.WriteLine("*COMMAND SEQUENCE INPUT MODE*\n");
                  RubikUtil.PrintSidesData(cube);

                  cube.RotateByCommandSequence(Console.ReadLine());
                  Console.WriteLine("\nPress any key...\n");
                  Console.ReadKey();

                  Console.Clear();
                  Console.WriteLine("*DIRECT CONTROL MODE*\n");
                  RubikUtil.PrintSidesData(cube);
               }
            }
         }
         catch (Exception e)
         {
            Console.WriteLine("Error happened! Error message: " + e);
         }
      }

      private static void HandleControl(ConsoleKeyInfo key, ref RubikCube cube)
      {
         var shiftPressed = (key.Modifiers & ConsoleModifiers.Shift) != 0;
         var solver = new RubikSolver(cube);

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
               var rotations = solver.GetRotationsArray();
               if (rotations.Length > 0)
               {
                  for (var i = 0; i < rotations.Length - 1; i++)
                     Console.Write(RubikUtil.RotationCommandToText(rotations[i]) + " ");
                  Console.Write(RubikUtil.RotationCommandToText(rotations[rotations.Length - 1]) + "\n");
               }
               break;
            case ConsoleKey.Q:
               cube = new RubikCube();
               var r = new Random();

               for (var i = 0; i < 20; i++)
               {
                  var command = ((RSide) r.Next(0, 6), (RotationType) r.Next(0, 3));
                  cube.RotateSide(command);
                  Console.Write(RubikUtil.RotationCommandToText(command) + " ");
               }
               Console.WriteLine();
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
            totalTurnsCount += s.TotalRotationsCount;
            totalTurnsStep0 += s.RotationsCountStep0;
            totalTurnsStep1 += s.RotationsCountStep1;
            totalTurnsStep2 += s.RotationsCountStep2;
            totalTurnsStep3 += s.RotationsCountStep3;
            totalTurnsStep4 += s.RotationsCountStep3;
         }

         sw.Stop();
         Console.WriteLine(
            "\nTests: {0}\nAverage time to solve: {1}ms\nAverage turns to solve: {2}\nAverage turns for:\n" +
            "\tStep 0: {3}\n\tStep 1: {4}\n\tStep 2: {5}\n\tStep 3: {6}\n\tStep 4: {7}\n", testsCount,
            totalTime * 1f / testsCount, totalTurnsCount / testsCount,
            totalTurnsStep0 / testsCount, totalTurnsStep1 / testsCount, totalTurnsStep2 / testsCount,
            totalTurnsStep3 / testsCount, totalTurnsStep4 / testsCount);
      }
   }
}

