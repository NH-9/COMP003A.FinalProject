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

            while (choice != 5)
            {
                Console.WriteLine("1. Add New Pokemon \n2. View All Records \n3. Search Records \n4. Display Summary Statistics \n5. Exit\n");
                Console.Write("Enter Choice: ");
                bool useMenuLimits = true;
                choice = ValidateInput(useMenuLimits);
                Console.WriteLine(choice);

                Console.WriteLine(pokeInfo.Count);
                switch (choice)
                {
                    case 1:
                        {
                            if (pokeInfo.Count < 6)
                            {
                                pokeInfo.Add(CreatePokemon(pokeInfo));
                                Console.WriteLine(pokeInfo.Count);
                            }
                            else
                            {
                                Console.WriteLine("Maximum Storage Reached");
                            }
                            break;
                        }

                    case 2:
                        {
                            Console.WriteLine("Species, Nickname, Primary Type, Secondary Type, Ability, Nature, Tera Type, Held Item, Offensive Preference, Defensive Preference, Team Role, HP, Physical Attack, Special Attack, Physical Defense, Special Defense, Speed, Base Stat Total, Level, Pokedex Number, Shiny, Fully Evolved, Able to Mega Evolve, Legendary, Mythical");
                            foreach (Pokemon pokemon in pokeInfo)
                            {
                                pokemon.DisplayValue();
                            }
                            break;
                        }

                    case 3:
                        {
                            break;
                        }

                    case 4:
                        {
                            double averageRealStats = 0;
                            double averageBaseStatTotal = 0;

                            foreach(Pokemon pokemon in pokeInfo)
                            {
                                averageRealStats += pokemon.AverageStats();
                                averageBaseStatTotal += pokemon.BaseStatTotal;
                            }

                            averageRealStats /= pokeInfo.Count;
                            averageBaseStatTotal /= pokeInfo.Count;

                            Console.WriteLine($"Average Real Stats: {averageRealStats}, Average Base Stat Total: {averageBaseStatTotal}");

                            break;
                        }

                    case 5:
                        {
                            Console.WriteLine("Program Ended.");
                            break;
                        }
                }
            }

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
                        Console.WriteLine("Invalid Numeric Input\n");
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
                            Console.WriteLine("Number Out Of Range\n");
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

            static Pokemon CreatePokemon(List<Pokemon> storedPokemon)
            {
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

                switch (storedPokemon.Count)
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



                foreach (string field in stringFields)
                {
                    Console.WriteLine(field);
                }
                foreach (int field in intFields)
                {
                    Console.WriteLine(field);
                }
                foreach (bool field in boolFields)
                {
                    Console.WriteLine(field);
                }
            }
        }
    }
}
