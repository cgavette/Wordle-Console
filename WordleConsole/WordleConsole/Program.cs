namespace WordleConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            char[,] board = new char[6, 5]; // [ y,x ]
            int[,] trackerBoard = new int[6, 5]; // [ y,x ]
            bool correctWord = false;
            int numberOfGuesses = 0;
            string answerWord;
            string guessWord;

            clearBoard(board, trackerBoard);

            startScreen();

            Console.WriteLine("Enter your word for testing. (5 letters)");
            answerWord = Console.ReadLine().ToString();

            Console.Clear();

            do
            {
                printBoard(board, trackerBoard);

                guessWord = getUserGuess();

                if (guessWord == answerWord)
                {
                    board = updateBoard(board, trackerBoard, answerWord, guessWord, numberOfGuesses);
                    correctWord = true;
                    Console.Clear();
                    printBoard(board, trackerBoard);
                }
                else
                {
                    board = updateBoard(board, trackerBoard, answerWord, guessWord, numberOfGuesses);
                    numberOfGuesses++;
                    Console.Clear();
                }

            } while ((correctWord == false) && (numberOfGuesses <= 5));

            endScreen(answerWord, correctWord);

        }

        static void startScreen()
        {

            Console.WriteLine("Welcome to WORDLE!");
            Console.WriteLine($"- Guess the correct word in 6 tries.");
            Console.WriteLine($"- Each guess must be a vild 5-letter word.");
            Console.WriteLine("The color of the characters will change to show how close your guess was to the word.");
            Console.WriteLine();

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Write("Green");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine(" means that the character is both in the word and in the correct spot.");

            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.Write("Yellow");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine(" means that the character is in the word but in the wrong spot.");

            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Write("Gray");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine(" means that the character is not in the word at any spot.");

            Console.WriteLine();
            Console.WriteLine("Click enter to begin.");
            Console.ReadLine();

            Console.Clear();

            return;
        }

        static void endScreen(string answerWord, bool correctWord)
        {
            if (correctWord)
            {
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Correct! Congratulations!!!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Sorry, the correct word was \"{answerWord}\"");
                Console.ForegroundColor = ConsoleColor.White;
            }
            return;
        }

        static string getUserGuess()
        {
            string guessWord;
            bool validGuess;

            do
            {
                Console.Write("Enter guess: ");
                guessWord = Console.ReadLine().ToString();

                if (guessWord.Length == 5)
                {
                    validGuess = true;
                }
                else
                {
                    Console.WriteLine("Guess word must have 5 letters.");
                    validGuess = false;
                }

            } while (!validGuess);

            return guessWord;
        }

        static void printBoard(char[,] board, int[,] trackerBoard)
        {
            string spaceBetween = "  ";

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    switch (trackerBoard[i, j])
                    {
                        case 0:
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(board[i, j]);
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(spaceBetween);
                            break;
                        case 1:
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(board[i, j]);
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(spaceBetween);
                            break;
                        case 2:
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(board[i, j]);
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(spaceBetween);
                            break;
                        case 3:
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(board[i, j]);
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(spaceBetween);
                            break;
                    }
                }

                Console.WriteLine();

            }

            Console.WriteLine();

        }

        static void clearBoard(char[,] board, int[,] trackerBoard)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = '_';
                    trackerBoard[i, j] = 0;
                }
            }
        }

        static char[,] updateBoard(char[,] board, int[,] trackerBoard, string answerWord, string guessWord, int guessNumber)
        {
            for (int i = 0; i < 5; i++)
            {
                if (guessWord[i] == answerWord[i])
                {
                    board[guessNumber, i] = guessWord[i];
                    trackerBoard[guessNumber, i] = 3;
                    //separate tracker board do the same thing but with like uh 1 or 2 or somethingn like that
                }
                else if (answerWord.Contains(guessWord[i]))
                {
                    board[guessNumber, i] = guessWord[i];
                    trackerBoard[guessNumber, i] = 2;
                }
                else
                {
                    board[guessNumber, i] = guessWord[i];
                    trackerBoard[guessNumber, i] = 1;
                }
            }

            return board;
        }
    }
}