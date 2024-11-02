using System;

namespace SudokuSolver
{
    public class Program
    {
        public static void Main()
        {
            int[,] grid = new int[9, 9];
            ResetGrid(grid);

            
            grid[0, 2] = 1;
            grid[0, 3] = 4;
            grid[1, 0] = 7;
            grid[1, 4] = 6;
            

            Console.WriteLine("\n ---- Initial Sudoku Grid  ----- \n");
            Display(grid);

            if (SolveSudoku(grid))
            {
                Console.WriteLine("\n ----- Solved Sudoku Grid ----- \n");
                Display(grid);
            }
            else
            {
                Console.WriteLine("No solution exists for the given Sudoku grid.");
            }
        }

        public static void ResetGrid(int[,] grid)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    grid[i, j] = 0;
                }
            }
        }

        public static bool IsSafe(int[,] grid, int row, int col, int num)
        {
            // Check if 'num' is not in current row and column
            for (int x = 0; x < 9; x++)
            {
                if (grid[row, x] == num || grid[x, col] == num)
                {
                    return false;
                }
                    
            }

            // Check if 'num' is not in current 3x3 subgrid
            int startRow = row - row % 3;
            int startCol = col - col % 3;


            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (grid[i + startRow, j + startCol] == num)
                        return false;
                }
            }

            return true;
        }

        public static bool SolveSudoku(int[,] grid)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    // Find an empty cell
                    if (grid[row, col] == 0)
                    {
                        for (int num = 1; num <= 9; num++)
                        {
                            // Check if placing 'num' in the cell is safe
                            if (IsSafe(grid, row, col, num))
                            {
                                grid[row, col] = num; // Place the number

                                // Recursively try to solve for the next empty cell
                                if (SolveSudoku(grid))
                                {
                                    //Safe to Add said Number
                                    return true;

                                }
                                // Setting it back to zero so it can go again 
                                grid[row, col] = 0;
                            }
                        }
                        return false; // Loop Again
                    }
                }
            }
            return true; // Puzzle solved
        }

        public static void Display(int[,] grid)
        {
            Console.WriteLine("\n\n Sudoku Grid: \n\n");
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(grid[i, j] + " ");
                    if ((j + 1) % 3 == 0 && j < 8) Console.Write("  ");
                }
                Console.WriteLine();
                if ((i + 1) % 3 == 0 && i < 8) Console.WriteLine();
            }
        }
    }
}
