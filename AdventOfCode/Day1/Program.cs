using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


/*
   Below is my solution to the first day of the Advent of Code 2016. The Adevent of Code is an online programming 
   challenge that features a series of complex problems that can be solved using computer programming. The challenge
   is not language specific, however, my solutions will all be written in C#.

    Advent of Code: https://adventofcode.com/2016
*/

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Adjust the current directory to the correct folder level
            Directory.SetCurrentDirectory(@"..\..\");

            // Read the data from the input file, split on comma character, trim, and save to list
            List<string> inputList = System.IO.File.ReadAllText(@"PuzzleInput.txt").Split(',').Select(i => i.Trim()).ToList();

            // Use your magic compass
            MagicCompass myCompass = new MagicCompass();

            // Use magic map
            MagicMap myMap = new MagicMap();

            // Loop through the inputs
            foreach (var input in inputList)
            {
                // Perform a turn and walk the correct number of blocks in the determined direction
                myMap.Walk(myCompass.Turn(input[0]), int.Parse(input.Substring(1)));
            }

            // Write distance from the origin out to the window, the solution to the first part of the challenge
            Console.WriteLine(string.Format("Day 1 Challenge 1 Result: {0} blocks.", myMap.DistanceFromOrigin()));

            // Write distance from the origin of the first location visited twice out to the window, the solution to the second part of the challenge
            Console.WriteLine(string.Format("Day 1 Challenge 2 Result: {0} blocks.", myMap.FirstLocationVistedTwice()));

            // Leave window open to read result
            Console.Read();
        }

        // Magic Map class used for tracking distance from your original location
        private class MagicMap
        {
            private int xAxis;
            private int yAxis;

            // Use for tracking the locations you have visited for the second part of the challenge
            private List<Tuple<int, int>> tracker;


            public MagicMap()
            {
                xAxis = 0;
                yAxis = 0;
                tracker = new List<Tuple<int, int>>();
            }

            /*
              Walk the given number of blocks in the given direction, update your distance from the origin (x-axis, y-axis). Assume
              North and East are postive, South and West are negative (like a graph). Track each step for the second part of the challenge.
             */
            public void Walk(char direction, int steps)
            {
                // Loop through the steps and update your tracker for the second part of the challenge
                for (int step = 0; step < steps; step++)
                {
                    switch (direction)
                    {
                        case 'N':
                            xAxis++;
                            break;
                        case 'S':
                            xAxis--;
                            break;
                        case 'E':
                            yAxis++;
                            break;
                        case 'W':
                            yAxis--;
                            break;
                    }

                    // Update tracker for second part of challenge
                    tracker.Add(Tuple.Create(xAxis, yAxis));
                }
            }



            // Calculate how far you are from the origin, make all negative distances postive before getting the sum
            public int DistanceFromOrigin()
            {
                return (Math.Abs(xAxis) + Math.Abs(yAxis));
            }

            // Get the number of blocks away you are from the origin when you vist your first location twice
            public int FirstLocationVistedTwice()
            {
                // Get the coordinate of the first location that you visited twice
                var duplicateCoordinate = tracker.GroupBy(x => new { x.Item1, x.Item2 }).Where(x => x.Count() > 1).FirstOrDefault();

                // Calculate how far the coordinate is away from the origin, make all negative distances positive before getting the sum
                return (Math.Abs(duplicateCoordinate.Key.Item1) + Math.Abs(duplicateCoordinate.Key.Item2));
            }
        }

        // Magic Compass class used for tracking your direction
        private class MagicCompass
        {
            private int index;
            private char[] direction;

            // Empty constructor initialize index and direction, you start by facing north
            public MagicCompass()
            {
                index = 0;
                direction = new char[4] { 'N', 'E', 'S', 'W' };
            }

            // Use a Swith to determine which direciton you are turning, return your direction after the turn
            public char Turn(char direction)
            {
                return direction == 'R' ? TurnRight() : TurnLeft();
            }

            /* 
             Turn to the right by increasing the index, if the index is equal to the direction array length -1, reset the index to 0 (indicating you are now facing north)
             Return the direction you are now facing after your turn.
            */
            private char TurnRight()
            {
                if (index == direction.Length - 1)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }

                return direction[index];
            }

            /* 
              Turn to the left by decreasing the index, if the index is equal to 0 (meaning you are facing north), 
               set the index to the direction array length (indicating you are now facing west). Return the direction you are now facing after your turn. 
            */
            private char TurnLeft()
            {
                if (index == 0)
                {
                    index = direction.Length - 1;
                }
                else
                {
                    index--;
                }

                return direction[index];
            }
        }
    }
}
