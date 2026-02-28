using System.Collections.Generic;
using System.Diagnostics;
using System.Transactions;

namespace COMP003A.FinalProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pokemon Information Storage System");
            Console.WriteLine("----------------------------------");

            int choice = 0;
            List<Pokemon> pokeInfo = new List<Pokemon>();

            // This loop displays a menu and accepts user input to choose a specific option; ends when user chooses to exit
            while (choice != 5)
            {
                Console.WriteLine("\n1. Add New Pokemon \n2. View All Pokemon Values \n3. Search By Type \n4. Display Stat Averages \n5. Exit\n");
                Console.Write("Enter Choice: ");
                bool useMenuLimits = true;
                choice = ValidateInput(useMenuLimits);

                switch (choice)
                {
                    case 1:
                        {
                            // Lets user create and store new pokemon

                            if (pokeInfo.Count < 6)
                            {
                                pokeInfo.Add(CreatePokemon(pokeInfo.Count));
                            }
                            else
                            {
                                Console.WriteLine("Maximum Storage Reached");
                            }
                            break;
                        }

                    case 2:
                        {
                            // Displays each category and then the values of each pokemon
                            // I'd like this to display in columns for a cleaner output, but I don't have time to work out how to make it happen

                            if (pokeInfo.Count > 0)
                            {
                                Console.WriteLine("Species, Nickname, Primary Type, Secondary Type, Ability, Nature, Tera Type, Held Item, Offensive Preference, Defensive Preference, Team Role, HP, Physical Attack, Special Attack, Physical Defense, Special Defense, Speed, Base Stat Total, Level, Pokedex Number, Shiny, Fully Evolved, Able to Mega Evolve, Legendary, Mythical");
                                foreach (Pokemon pokemon in pokeInfo)
                                {
                                    pokemon.DisplayValue();
                                }
                            }
                            else
                            {
                                Console.WriteLine("No Pokemon Detected");
                            }
                            break;
                        }

                    case 3:
                        {
                            // Lets user search for pokemon by type
                            // Every pokemone has 1 or 2 types which defines much of what they can do

                            Console.Write("Enter a Type: ");
                            string type = Console.ReadLine().ToUpper();
                            int found = 0;

                            foreach (Pokemon pokemon in pokeInfo)
                            {
                                if (type == pokemon.Type1 || type == pokemon.Type2)
                                {
                                    pokemon.DisplayValue();
                                    found++;
                                }
                            }
                            if (found == 0)
                            {
                                Console.WriteLine("No Matches Found");
                            }

                            break;
                        }

                    case 4:
                        {
                            // Displays the average real stat total and base stat total of each stored pokemon
                            // Base stats are the primary value that decides how many of each stat a pokemon gains upon leveling up; higher base stat total is often used to gauge overall power
                            // Real stats are the actual stat values a pokemon currently has; depends mostly on base stats and current level

                            if (pokeInfo.Count > 0)
                            {
                                double averageRealStatTotal = 0;
                                double averageBaseStatTotal = 0;

                                foreach (Pokemon pokemon in pokeInfo)
                                {
                                    averageRealStatTotal += pokemon.AverageStats();
                                    averageBaseStatTotal += pokemon.BaseStatTotal;
                                }

                                averageRealStatTotal /= pokeInfo.Count;
                                averageBaseStatTotal /= pokeInfo.Count;

                                Console.WriteLine($"Average Real Stats: {averageRealStatTotal}, Average Base Stat Total: {averageBaseStatTotal}");
                            }
                            else
                            {
                                Console.WriteLine("No Pokemon Detected");
                            }
                            break;
                        }

                    case 5:
                        {
                            // Ends program

                            Console.WriteLine("Program Ended.");
                            break;
                        }
                }
            }

            // Used to put interger inputs though a try/catch for parsing
            // Uses parameter value to decide if/how range should be limited
            // There are only 1 range to be enforced in this project so this value is a bool,
            // but in the future I would use an interger for scalability 

            static int ValidateInput(bool useMenuLimits)
            {
                int choice = 0;
                bool valid = false;
                
                while (!valid)
                {
                    try
                    {
                        choice = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Invalid Numeric Input");
                        Console.Write("Please try again: ");
                        continue;
                    }

                    if (useMenuLimits)
                    {
                        if (choice >= 1 && choice <= 5)
                        {
                            valid = true;
                        }
                        else
                        {
                            Console.WriteLine("Number Out Of Range");
                            Console.Write("Please try again: ");
                            continue;
                        }
                    }
                    else
                    {
                        valid = true;
                    }
                }

                return choice;
            }

            // This method is used to create new objects (pokemon) and return that object to main
            static Pokemon CreatePokemon(int storedPokemon)
            {
                // To avoid making a write/readline for each input, this stores each question in a List and creates Lists to store each type of input
                // The loop then writes each question and prompts an input, converting the input to the right data type based on which question is being asked
                // The questions List is ordered by required data type so that index number can be used to decide how input should be converted
                // I'm a bit proud of this one :) 

                List<string> questions = new List<string>() { "Pokemon Species", "Pokemon Nickname ('none' if N/A')", "Primary Type", "Secondary Type ('none' if N/A)", "Ability", "Nature", "Tera Type", "Held Item ('none' if N/A)", "HP", "Physical Attack", "Special Attack", "Physical Defense", "Special Defense", "Speed", "Base Stat Total", "Level", "Pokedex Number", "Shiny (y/n)", "Fully Evolved (y/n)", "Able to Mega Evolve (y/n)", "Legendary (y/n)", "Mythical (y/n)"};
                List<string> stringFields = new List<string>();
                List<int> intFields = new List<int>();
                List<bool> boolFields = new List<bool>();
                bool useMenuLimits = false;
                
                Console.WriteLine("Enter Each Requested Value");
                
                for (int i = 0; i < questions.Count; i++)
                {
                    Console.Write($"{questions[i]}: ");

                    if (i < 8)
                    {
                        stringFields.Add(Console.ReadLine().ToUpper());
                    }
                    else if (i < 17)
                    {
                        int input = ValidateInput(useMenuLimits);
                        intFields.Add(input);
                    }
                    else
                    {
                        bool input = (Console.ReadLine().ToLower() == "y");
                        boolFields.Add(input);
                    }
                }
                
                // These if/else chains use previous user inputs to map the conditional class fields.
                // The intFields values represent pokemon stats and are used to categorize what kind of offense or defence situations they are better for.
                // They are also used to categorize a pokemons general role in a team. These are all terms common in competitive pokemon.
                // These descriptions are way more naunced in the actual game, but this works for the program.

                // 0 = Hit Points, 1 = Physical Attack, 2 = Special Attack, 3 = Physical Defense, 4 = Special Defense, 5 = Speed

                int offenseDecider = intFields[1] - intFields[2];
                if (offenseDecider >= -15 && offenseDecider <= 15)
                {
                    stringFields.Add("MIXXED ATTACKER");
                }
                else if (intFields[1] > intFields[2])
                {
                    stringFields.Add("PHYSICAL ATTACKER");
                }
                else
                {
                    stringFields.Add("SPECIAL ATTACKER");
                }

                int defenceDecider = intFields[3] - intFields[4];
                if (defenceDecider >= -15 && defenceDecider <= 15)
                {
                    stringFields.Add("MIXXED BULK");
                }
                else if (intFields[3] > intFields[4])
                {
                    stringFields.Add("PHYSICALLY BULKY");
                }
                else
                {
                    stringFields.Add("SPECIALLY BULKY");
                }

                if (intFields[1] > 100 || intFields[2] > 100)
                {
                    if (intFields[5] > 100)
                    {
                        stringFields.Add("FAST SWEEPER");
                    }
                    else if (intFields[0] > 100)
                    {
                        stringFields.Add("BULKY SWEEPER");
                    }
                    else
                    {
                        stringFields.Add("ROUNDED SWEEPER");
                    }
                }
                else if (intFields[3] > 100 || intFields[4] > 100)
                {
                    stringFields.Add("BULKY SUPPORT");
                }
                else
                {
                    stringFields.Add("ROUNDED SUPPORT");
                }

                // This constructs a new object to store all of the values as a new pokemon
                // I don't know how to name new varaibles dynamically so I capped the number of objects stored at 6
                // (This is the number of pokemon you can have on a team in game)
                // The switch case will name the new object based on how many are already stored

                switch (storedPokemon)
                {
                    case 0:
                        {
                            Pokemon pokemon1 = new Pokemon(stringFields, intFields, boolFields);
                            return pokemon1;
                        }

                    case 1:
                        {
                            Pokemon pokemon2 = new Pokemon(stringFields, intFields, boolFields);
                            return pokemon2;
                        }

                    case 2:
                        {
                            Pokemon pokemon3 = new Pokemon(stringFields, intFields, boolFields);
                            return pokemon3;
                        }

                    case 3:
                        {
                            Pokemon pokemon4 = new Pokemon(stringFields, intFields, boolFields);
                            return pokemon4;
                        }

                    case 4:
                        {
                            Pokemon pokemon5 = new Pokemon(stringFields, intFields, boolFields);
                            return pokemon5;
                        }

                    default:
                        {
                            Pokemon pokemon6 = new Pokemon(stringFields, intFields, boolFields);
                            return pokemon6;
                        }
                }
            }
        }
    }
}
