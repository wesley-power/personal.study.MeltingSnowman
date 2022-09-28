using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeltingSnowman
{
    internal class Words
    {
        //Properties
        public string Word { get; private set; }

        //Constructor
        public Words()
        {
            bool isValid = false;
            string entry = "-1";

            while (!isValid)
            {
                Console.Write("CHOSE MODE:\n" +
                              "1) Random word\n" +
                              "2) Enter a word for a friend\n" +
                              "Enter 1 or 2: ");

                entry = Console.ReadLine();

                if (entry == "1" || entry == "2")
                    isValid = true;

                else
                    Console.WriteLine("\nInvalid entry. You must choose a mode.\n");

            }
            if (entry == "1")
                Word = RandomWord();

            else
                Word = InputWord();
        }

        private static string RandomWord()
        {
            string[] words = new string[] {"COFFEE", "GOVERNMENT", "HAMBURGER", "MOBILE", "TELEVISION", "ISLAND", "ELEPHANT", "CHILDREN", "PARADISE",
                "POSTER", "HYDROPHOBIC", "TOASTER", "WINDOW", "WEBSITE", "INSURANCE", "GIRAFFE", "COBALT", "SAXOPHONE", "VIOLIN", "TROMBONE", "ADDITION",
                "AMUSEMENT", "ADVERTISEMENT", "XYLOPHONE", "ANIMAL", "BALANCE", "BEAUTIFUL", "BECAUSE", "BEHAVIOR", "BROTHER", "BUILDING", "BUSINESS", "BUTTON", 
                "CARRIAGE", "CHEMICAL", "CAMERA", "CIRCLE", "COMMITTEE", "COMPETITION", "CONSCIOUS", "CONNECTION", "DIGESTION", "DISCOVERY", "DESTRUCTION",
                "DISTRIBUTION", "DEVELOPMENT", "DRAWER", "DELICATE", "ELASTIC", "ELECTRIC", "EXISTENCE", "EXPERIENCE", "EDUCATION", "FAMILY", "FEATHER", 
                "FATHER", "FICTION", "FEEBLE", "FLOWER", "FREQUENT", "FORWARD", "FUTURE", "GENERAL", "GARDEN", "GROWTH", "HEALTHY", "HEARING", "HOSPITAL",
                "HOLLOW", "HISTORY", "IMPORTANT", "IMPULSE", "INCREASE", "JOURNEY", "KETTLE", "KNOWLEDGE", "LANGUAGE", "LEARNING", "LEATHER", "LIBRARY", 
                "LIQUID", "MACHINE", "MANAGER", "MARRIED", "MATERIAL", "MINUTE", "MORNING", "MOUNTAIN", "MUSCLE", "MILITARY", "NECESSARY", "NARROW", "NATURAL",
                "NATION", "NUMBER", "OBSERVATION", "ORGANIZATION", "OPERATION", "OPINION", "PARALLEL", "PLEASURE", "PHYSICAL", "POLITICAL", "PROPERTY", "PROTEST",
                "QUESTION", "QUALITY", "REGULAR", "RELIGION", "RELATION", "REPRESENTATIVE", "RESPONSIBLE", "SCISSORS", "SECRETARY", "SEPARATE", "STATEMENT", 
                "STRUCTURE", "SUBSTANCE", "SURPRISE", "TENDENCY", "THOUGHT", "TOMORROW", "TRANSPORT", "TROUSERS", "TOGETHER", "UMBRELLA", "VICIOUS", "WEATHER",
                "WARBLE", "WRITING", "YESTERDAY", "ZEALOUS"};

            Random random = new Random();

            string word = words[random.Next(0, words.Length)];

            return word;
        }

        private static string InputWord()
        {
            string[] bannedWords = new string[] {"FUCK", "BITCH", "CUNT", "TWAT", "ASSHOLE", "PUSSY"};
            bool isValid = false;
            string word = "    ";

            while (!isValid)
            {
                Console.Write("\nEnter your word: ");
                word = Console.ReadLine();

                if (word is not null)
                    word = word.ToUpper();

                bool pass1 = true;
                bool pass2 = true;

                if (word == null || word.Length < 3)
                {
                    Console.WriteLine("Invalid entry. Your word must be at least six characters long.");
                    continue;
                }

                if (word is not null)
                {
                    foreach (char c in word)
                        if (!Char.IsLetter(c))
                        {
                            Console.WriteLine("This is not a valid entry. Words may only contain letters.\n");
                            pass1 = false;
                            break;
                        }

                    foreach (string bannedWord in bannedWords)
                        if (word.Contains(bannedWord))
                        {
                            Console.WriteLine("Your entry contains a banned word. Please try again.\n");
                            pass2 = false;
                            break;
                        }
                }

                if (pass1 && pass2)
                    isValid = true;
            }

            return word;
        }
    }
}
