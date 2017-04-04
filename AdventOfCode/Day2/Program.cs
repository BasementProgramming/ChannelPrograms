using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


/*
   Below is my solution to the second day of the Advent of Code 2016. The Adevent of Code is an online programming 
   challenge that features a series of complex problems that can be solved using computer programming. The challenge
   is not language specific, however, my solutions will all be written in C#.

    Advent of Code: https://adventofcode.com/2016
*/

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Adjust the current directory to the correct folder level
            Directory.SetCurrentDirectory(@"..\..\");

            // Read the data from the input file, break on new line character
            List<string> inputs = System.IO.File.ReadAllText(@"PuzzleInput.txt").Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();

            // String for holding our result to Part 1
            string result = string.Empty;

            // Loops through the strings in the input list
            foreach (var input in inputs)
            {
                // Create our KeyPad
                MagicKeyPad keypad = new MagicKeyPad(3, 3);

                // Loops through the characters in each string
                foreach (var character in input)
                {
                    // Move the given direction on the keypad
                    keypad.Move(character);
                }

                // Add the final key to our result
                result += keypad.GetNumber();
            }

            // Write the keypad code to the window, the solution to the first part of the challenge
            Console.WriteLine(string.Format("Day 2 Challenge 1 Result: {0} blocks.", result));

            // Leave window open to read result
            Console.Read();
        }

        private class MagicKeyPad
        {
            private int[,] numbers;
            int column;
            int row;

            // Create a KeyPad with the given numbers of rows and columns
            public MagicKeyPad(int rows, int columns)
            {
                // Initialize array of the correct size
                numbers = new int[rows, columns];

                // Used to track which number should be placed at the given location
                int index = 1;

                // Fill the array with the correct numbers
                for (int x = 0; x < rows; x++)
                {
                    for (int y = 0; y < columns; y++)
                    {
                        numbers[y, x] = index;
                        index++;
                    }
                }

                // Set our initial location on the KeyPad to the center
                column = columns / 2;
                row = rows / 2;
            }

            // Move in the given direction on the keypad and return the number that is selected
            public int Move(char direction)
            {
                switch (direction)
                {
                    case 'U':
                        return MoveUp();
                    case 'D':
                        return MoveDown();

                    case 'R':
                        return MoveRight();
                    case 'L':
                        return MoveLeft();
                }

                return numbers[column, row];
            }

            // Return the number for the current location on the keypad
            public int GetNumber()
            {
                return numbers[column, row];
            }

            // Move up one row on the keypad, assuming you are not already in the top row
            private int MoveUp()
            {
                if (row > 0)
                {
                    row--;
                }

                return numbers[column, row];
            }

            // Move down one row on the keypad, assuming you are not in the bottom row
            private int MoveDown()
            {
                if (row < numbers.GetLength(1) - 1)
                {
                    row++;
                }

                return numbers[column, row];
            }

            // Move to the right one column on the keypad, assuming you are not already in the last column
            private int MoveRight()
            {
                if (column < numbers.GetLength(0) - 1)
                {
                    column++;
                }

                return numbers[column, row];
            }

            // Move to the left one column on the keypad, asssuming you are not already in the first column
            private int MoveLeft()
            {
                if (column > 0)
                {
                    column--;
                }

                return numbers[column, row];
            }
        }
    }
}
