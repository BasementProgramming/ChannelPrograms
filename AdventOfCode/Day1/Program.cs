using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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

            // Write distance from the origin out to the window
            Console.WriteLine(string.Format("Day 1 Challenge 1 Distance from your Starting Location: {0} blocks.", myMap.DistanceFromOrigin()));

            // Lave window open to read result
            Console.Read();
        }

        // Magic Map class used for tracking distance from your original location
        private class MagicMap
        {
            private int xAxis;
            private int yAxis;

            public MagicMap()
            {
                xAxis = 0;
                yAxis = 0;
            }

            /*
              Take the given number of blocks in the given direction, update your distance from the origin (x-axis, y-axis). Assume
              North and East are postive, South and West are negative (like a graph)
             */
            public void Walk(char direction, int steps)
            {
                switch (direction)
                {
                    case 'N':
                        xAxis = xAxis + steps;
                        break;
                    case 'S':
                        xAxis = xAxis - steps;
                        break;
                    case 'E':
                        yAxis = yAxis + steps;
                        break;
                    case 'W':
                        yAxis = yAxis - steps;
                        break;
                }
            }

            // Calculate how far you are from the origin, make all negative distances postive before getting the sum
            public int DistanceFromOrigin()
            {
                return (System.Math.Abs(xAxis) + System.Math.Abs(yAxis));
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
