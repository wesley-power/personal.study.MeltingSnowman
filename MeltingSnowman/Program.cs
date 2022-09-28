using System.Diagnostics.Metrics;

namespace MeltingSnowman
{
    internal class Program
    {
        static bool gameOver = false;
        static bool playAgain = true;

        static List<string> unguessed = new List<string>() {"A", "B", "C", "D", "E", "F", "G", "H", "I",
                           "J", "K", "L", "M", "N", "O", "P", "Q", "R",
                           "S", "T", "U", "V", "W", "X", "Y", "Z"};
        static List<string> guessed = new List<string>();

        static void Main(string[] args)
        {
            int wrongGuesses = 0;

            while (playAgain)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.Clear();

                int difficulty = GetDifficulty();

                Words w = new Words();

                char[] blankWord = new char[w.Word.Length];
                for (int i = 0; i < w.Word.Length; i++)
                    blankWord[i] = '_';

                string letter = "";
                bool isInWord = false;
                bool wonGame = false;

                while (!gameOver && !wonGame)
                {
                    ShowDisplay(difficulty, wrongGuesses, blankWord);

                    letter = GetLetter(guessed);

                    isInWord = CheckLetter(w.Word, letter);

                    if (isInWord)
                    {
                        blankWord = UpdateBlankWord(w.Word, letter, blankWord);
                    }

                    else
                        wrongGuesses++;

                    Console.Clear();

                    if (wrongGuesses == difficulty)
                        gameOver = true;

                    if (w.Word == String.Join("", blankWord))
                        wonGame = true;
                }

                ShowEnd(gameOver, w);

                playAgain = PlayAgain();

                foreach (string c in guessed)
                {
                    unguessed.Add(c);
                }

                guessed.Clear();
                unguessed.Sort();
                gameOver = false;
                wonGame = false;
                wrongGuesses = 0;

            }

            Console.Write("\n\nThank you for playing Melting Snowman!\n\n\n\n");
            System.Threading.Thread.Sleep(2500);
        }

        public static int GetDifficulty()
        {
            string difficulty = "test";
            int fail = 0;
            while (true)
            {
                Console.Clear();

                Console.WriteLine("    ...        *                        *       *\r\n" +
                "      ...   *         * ..   ...                        *\r\n *    " +
                "  ...        *           *            *\r\n          ...           " +
                "    ...                          *\r\n            ..               " +
                "             *\r\n    *        ..        *                       *" +
                "\r\n           __##____              *                      *\r\n  " +
                "*    *  /  ##  ****                   *\r\n         /        ****  " +
                "             *         *  X   *\r\n   *    /        ******     *   " +
                "                 XXX      *\r\n       /___________*****          * " +
                "            XXXXX\r\n        |            ***          _    *      " +
                " XXXXXXX   X\r\n    *   | ___        |          _|_|_     *   XXXXX" +
                "XXX  XXX\r\n  *     | | |   ___  | *     *   (_)         XXXXXXXXXX" +
                "XXXXX\r\n        | |_|   | |  ****     >-(_:_)-<  *        X   XXXX" +
                "XXX\r\n    *********** | | *******    (__:__)            X      X\r" +
                "\n************************************************************");

                if (fail == 0)
                    Console.WriteLine("\nWelcome to Melting Snowman! Guess the word before Frosty melts.\n\nPress any key to continue.");

                if (fail == 1)
                    Console.WriteLine("\nInvalid entry. You must enter 1, 2, or 3 to continue.");

                fail = 0;

                int easy = 10;
                int normal = 8;
                int hard = 6;

                Console.Write($"\nEasy:\t1 || {easy} wrong guesses\n" +
                    $"Normal:\t2 || {normal} wrong guesses\nHard:\t3 || {hard} wrong guesses" +
                    "\n\nSelect your difficutly level: ");

                difficulty = Console.ReadLine();

                if (difficulty != null)
                    difficulty = difficulty.ToUpper();

                Console.WriteLine("\n");

                if (difficulty == "1")
                    return easy;
                else if (difficulty == "2")
                    return normal;
                else if (difficulty == "3")
                    return hard;
                else
                    fail = 1;
            }
        }

        public static void ShowDisplay(int difficulty, int wrongGuesses, char[] blankWord)
        {
            Console.WriteLine("\n\n\n\n\n                     *  .  *\r\n                   . _\\/ " +
                "\\/_ .\r\n                    \\  \\ /  /             .      .\r\n      ..    ..  " +
                "  -==>: X :<==-           _\\/  \\/_\r\n      '\\    /'      / _/ \\_ \\          " +
                "    _\\/\\/_\r\n        \\\\//       '  /\\ /\\  '         _\\_\\_\\/\\/_/_/_\r\n " +
                "  _.__\\\\\\///__._    *  '  *            / /_/\\/\\_\\ \\\r\n    '  ///\\\\\\  ' " +
                "                          _/\\/\\_\r\n        //\\\\                              " +
                " /\\  /\\\r\n      ./    \\.             ._    _.       '      '\r\n      ''    ''" +
                "             (_)  (_)                  <> \\  / <>\r\n                            " +
                ".\\::/.                   \\_\\/  \\/_/\r\n           .:.          _.=._\\\\//_.=." +
                "_                  \\\\//\r\n      ..   \\o/   ..      '=' //\\\\ '='             " +
                "_<>_\\_\\<>/_/_<>_\r\n      :o|   |   |o:         '/::\\'                 <> / /<>" +
                "\\ \\ <>\r\n       ~ '. ' .' ~         (_)  (_)      _    _       _ //\\\\ _\r\n  " +
                "         >O<             '      '     /_/  \\_\\     / /\\  /\\ \\\r\n       _ .' " +
                ". '. _                        \\\\//       <> /  \\ <>\r\n      :o|   |   |o:     " +
                "              /\\_\\\\><\\\\ \\/\r\n           ':'                           _//\\" +
                "\\_\r\n                                        \\_\\  /_/");
            
            Console.Write("\n\nUnguessed: ");
            foreach (string c in unguessed)
                Console.Write(c + " ");

            Console.Write("\n\nGuessed: ");
            foreach (string c in guessed)
                Console.Write(c + " ");

            if (difficulty - wrongGuesses > 1)
                Console.WriteLine("\n\nYou have " + (difficulty - wrongGuesses) + " wrong guesses remaining.");
            else
                Console.WriteLine("\n\nYou have " + (difficulty - wrongGuesses) + " wrong guess remaining.");

            Console.WriteLine("\n\nYour Word: " + String.Join(" ", blankWord));

            Console.WriteLine("\n" + ShowSnowman(difficulty, wrongGuesses));
        }

        public static string GetLetter(List<string> guessed)
        {
            while (true)
            {
                Console.Write("\nGuess a letter: ");
                string letter = Console.ReadLine();

                if (letter is not null)
                    letter = letter.ToUpper();

                if (guessed.Contains(letter))
                    Console.WriteLine($"You have already guessed the letter {letter}! Try again.\n");

                else if (letter.Length == 1 && Char.IsLetter(letter, 0))
                    return letter;
                
                else
                    Console.WriteLine("That is not a valid entry.\n");
            }
        }

        public static bool CheckLetter(string word, string letter)
        {
            bool inWord;

            Console.WriteLine(word + " " + letter);

            if (word.Contains(letter))
                inWord = true;

            else
                inWord = false;

            unguessed.Remove(letter);
            guessed.Add(letter);
            guessed.Sort();

            return inWord;
        }

        public static char[] UpdateBlankWord(string word, string letter, char[] blankWord)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (word.Substring(i, 1) == letter)
                    blankWord[i] = Convert.ToChar(letter);
            }

            return blankWord;
        }

        public static string ShowSnowman(int difficulty, int wrongGuesses)
        {
            if (wrongGuesses == 0)
                return "    _\r\n  _|_|_\r\n   (_)\r\n>-(_:_)-<\r\n (__:__)";

            else if (wrongGuesses == 1)
                return "\n    _\r\n   (_)\r\n>-(_:_)-<\r\n (__:__)";

            else if (wrongGuesses == 2 && difficulty == 10)
                return "\n    _\r\n   (_)\r\n -(_:_)-<\r\n (__:__)";

            else if ((wrongGuesses == 3 && difficulty == 10) || (wrongGuesses == 2 && difficulty != 10))
                return "\n    _\r\n   (_)\r\n  (_:_)-<\r\n (__:__)";

            else if (wrongGuesses == 4 && difficulty == 10)
                return "\n    _\r\n   (_)\r\n  (_:_)-\r\n (__:__)";

            else if ((wrongGuesses == 5 && difficulty == 10) || (wrongGuesses == 3 && difficulty != 10))
                return "\n    _\r\n   (_)\r\n  (_:_)\r\n (__:__)";

            else if ((wrongGuesses == 6 && difficulty == 10) || (wrongGuesses == 4 && difficulty == 8))
                return "\n    _\r\n   (_)\r\n  (___)\r\n (__:__)";

            else if ((wrongGuesses == 7 && difficulty == 10) || (wrongGuesses == 5 && difficulty == 8))
                return "\n    _\r\n   (_)\r\n  (___)\r\n (_____)";

            else if ((wrongGuesses == 8 && difficulty == 10) || (wrongGuesses == 6 && difficulty == 8) || (wrongGuesses == 4 && difficulty == 6))
                return "\n\n   ___\r\n  (___)\r\n (_____)";

            else if ((wrongGuesses == 9 && difficulty == 10) || (wrongGuesses == 7 && difficulty == 8) || (wrongGuesses == 5 && difficulty == 6))
                return "\n\n\n  _____  \r\n (_____)";

            else
                return "\n\n\n\n";
        }

        public static void ShowEnd(bool gameOver, Words w)
        {
            if (gameOver)
            {
                Console.WriteLine("   _____                         ____                 \r\n" +
                "  / ____|                       / __ \\                \r\n | |  __  __ " +
                "_ _ __ ___   ___  | |  | |_   _____ _ __ \r\n | | |_ |/ _` | '_ ` _ \\ /" +
                " _ \\ | |  | \\ \\ / / _ \\ '__|\r\n | |__| | (_| | | | | | |  __/ | |__" +
                "| |\\ V /  __/ |   \r\n  \\_____|\\__,_|_| |_| |_|\\___|  \\____/  \\_/ " +
                "\\___|_|  " +
                "\n\nThe snowman melted!" +
                "\n\nThe word was " + w.Word + ".");
            }
            else
            {
                Console.WriteLine(" __     __                    _       _ \r\n \\ \\   " +
                    "/ /                   (_)     | |\r\n  \\ \\_/ /__  _   _  __      " +
                    "___ _ __ | |\r\n   \\   / _ \\| | | | \\ \\ /\\ / / | '_ \\| |\r\n " +
                    "   | | (_) | |_| |  \\ V  V /| | | | |_|\r\n    |_|\\___/ \\__,_|  " +
                    " \\_/\\_/ |_|_| |_(_)" +
                    "\n\n    ...        *                        *       *\r\n" +
                    "      ...   *         * ..   ...                        *\r\n *    " +
                    "  ...        *           *            *\r\n          ...           " +
                    "    ...                          *\r\n            ..               " +
                    "             *\r\n    *        ..        *                       *" +
                    "\r\n           __##____              *                      *\r\n  " +
                    "*    *  /  ##  ****                   *\r\n         /        ****  " +
                    "             *         *  X   *\r\n   *    /        ******     *   " +
                    "                 XXX      *\r\n       /___________*****          * " +
                    "            XXXXX\r\n        |            ***          _    *      " +
                    " XXXXXXX   X\r\n    *   | ___        |          _|_|_     *   XXXXX" +
                    "XXX  XXX\r\n  *     | | |   ___  | *     *   (_)         XXXXXXXXXX" +
                    "XXXXX\r\n        | |_|   | |  ****     >-(_:_)-<  *        X   XXXX" +
                    "XXX\r\n    *********** | | *******    (__:__)            X      X\r" +
                    "\n************************************************************"+
                    "\n\nThe word was " + w.Word + ".");

            }

        }

        public static bool PlayAgain()
        {
            while (true)
            {
                Console.Write("\n\nWould you like to play again?\nEnter Y for \"yes\" and N for \"no\": ");

                string entry = Console.ReadLine();

                if (entry is not null)
                    entry = entry.ToUpper();

                if (entry == "Y")
                    return true;

                else if (entry == "N")
                    return false;

                Console.WriteLine("\nThat is not a valid entry. Please try again.");
            }
        }
    }
}