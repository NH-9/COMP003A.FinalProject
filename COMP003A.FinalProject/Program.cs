namespace COMP003A.FinalProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pokemon Information Storage System");
            Console.WriteLine("----------------------------------");

            int choice = 0;

            while (choice != 5)
            {
                Console.WriteLine("1. Add New Pokemon \n2. View All Records \n3. Search Records \n4. Display Summary Statistics \n5. Exit\n");
                choice = ValidateInput();
                Console.WriteLine(choice);
            }

            static int ValidateInput()
            {
                int choice = 0;
                bool valid = false;
                
                while (!valid)
                {
                    Console.Write("Enter Choice: ");

                    try
                    {
                        choice = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Invalid Numeric Input\n");
                        continue;
                    }

                    if (choice >= 1 && choice <= 5)
                    {
                        valid = true;
                    }
                    else
                    {
                        Console.WriteLine("Number Out Of Range\n");
                    }
                }

                return choice;
            }

        }
    }
}
