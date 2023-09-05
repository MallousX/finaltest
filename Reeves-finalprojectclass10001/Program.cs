using System;

namespace MazeGame
{
    class Program
    {
        // The 2D array representing the maze. (JamesR)
        static char[,] maze;

        // The player's current position in the maze. (JamesR)
        static int playerX, playerY;

        // The total number of moves the player can make. (JamesR)
        static int moves = 10;

        // Constant to define the size of the maze. (JamesR)
        const int mazeSize = 5;

        static void Main(string[] args)
        {
            // Initialize the maze. (TimP)
            InitializeMaze();

            // Game loop. (JamesR)
            while (moves > 0)
            {
                // Display the current state of the maze. (JamesR)
                DisplayMaze();

                // Display the number of remaining moves. (JamesR)
                Console.WriteLine($"Moves left: {moves}");

                // Prompt the user for their move. (JamesR)
                Console.WriteLine("Move (WASD):");
                char move = Console.ReadKey().KeyChar;

                // Process the user's move. (JamesR)
                MakeMove(move);

                // Check for end game conditions. (JamesR)
                if (maze[playerX, playerY] == 'E')
                {
                    Console.WriteLine("\nYou won!");
                    return;
                }
                else if (maze[playerX, playerY] == 'T')
                {
                    Console.WriteLine("\nYou hit a trap! Game over!");
                    return;
                }

                // Decrement the move counter. (JamesR)
                moves--;
            }

            // Player ran out of moves. (JamesR)
            Console.WriteLine("\nOut of moves! Game over!");
        }

        static void InitializeMaze()
        {
            // Create a new maze filled with empty spaces. (TimP)
            maze = new char[mazeSize, mazeSize];
            for (int i = 0; i < mazeSize; i++)
                for (int j = 0; j < mazeSize; j++)
                    maze[i, j] = '.';

            // Set starting and ending positions.(TimP)
            maze[0, 0] = 'S';
            maze[mazeSize - 1, mazeSize - 1] = 'E';

            // Place a trap and a treasure in random locations. (TimP)
            Random r = new Random();
            maze[r.Next(mazeSize), r.Next(mazeSize)] = 'T';
            maze[r.Next(mazeSize), r.Next(mazeSize)] = 'R';

            // Initialize the player's starting position. (TimP)
            playerX = 0;
            playerY = 0;
        }

        static void DisplayMaze()
        {
            // Loop through each cell in the maze and display its content. (JamesR)
            Console.WriteLine();
            for (int i = 0; i < mazeSize; i++)
            {
                for (int j = 0; j < mazeSize; j++)
                {
                    // If the current cell is the player's position, display 'P'. (JamesR)
                    if (i == playerX && j == playerY)
                        Console.Write('P');
                    else
                        Console.Write(maze[i, j]);
                }
                Console.WriteLine();
            }
        }

        static void MakeMove(char move)
        {
            // Update the player's position based on the input move. (JamesR)
            switch (move)
            {
                case 'w':
                    if (playerX > 0) playerX--;
                    break;
                case 'a':
                    if (playerY > 0) playerY--;
                    break;
                case 's':
                    if (playerX < mazeSize - 1) playerX++;
                    break;
                case 'd':
                    if (playerY < mazeSize - 1) playerY++;
                    break;
            }

            // If the player's new position is a treasure, remove the treasure. (JamesR)
            if (maze[playerX, playerY] == 'R')
            {
                maze[playerX, playerY] = '.';
            }

            // Move traps to new random locations. (MarkT)
            Random r = new Random();
            for (int i = 0; i < mazeSize; i++)
                for (int j = 0; j < mazeSize; j++)
                    if (maze[i, j] == 'T')
                        maze[i, j] = '.';

            maze[r.Next(mazeSize), r.Next(mazeSize)] = 'T';
        }
    }
}