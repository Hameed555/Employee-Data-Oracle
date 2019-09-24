using System;
using Oracle.ManagedDataAccess.Client;

namespace EmployeeDetails
{
    public class Program
    {
        private readonly UserInteraction _userInteraction = new UserInteraction();
        public static void Main(string[] args)
        {
            Console.WriteLine("CRUD Operation Started....");
            Console.WriteLine("**** ********* ***********");
            Program p = new Program();
            p.Operation();
            Console.ReadLine();
        }

        public void Operation()
        {
            bool loopContinue = true;

            while (loopContinue)
            {
                Console.WriteLine("\nSelect Your CRUD Operation");
                Console.WriteLine("****** **** **** *********");
                Console.WriteLine("1. Create");
                Console.WriteLine("2. Update");
                Console.WriteLine("3. Delete");
                Console.WriteLine("4. Display");
                Console.WriteLine("5. Exit");

                int operationNumber = 0;

                Console.Write("\n Enter The Operation : ");
                string selectOperation = Console.ReadLine();

                if (int.TryParse(selectOperation, out operationNumber))
                {
                    switch (operationNumber)
                    {
                        case 1:
                            _userInteraction.CreateOperation();
                            Console.WriteLine("------------------------------------------------------------------------------------");
                            break;
                        case 2:
                            _userInteraction.UpdateOperation();
                            Console.WriteLine("------------------------------------------------------------------------------------");
                            break;
                        case 3:
                            _userInteraction.DeleteOperation();
                            Console.WriteLine("------------------------------------------------------------------------------------");
                            break;
                        case 4:
                            _userInteraction.DisplayOperation();
                            break;
                        case 5:
                            loopContinue = false;
                            Console.WriteLine("\n Press any key to exit");
                            break;
                        default:
                            Console.WriteLine("Sorry!! Please Give An Valid Data..");
                            break;
                    }
                }
                else
                    Console.WriteLine("Sorry!! Please Give An Valid Data..");
            }
        }
    }
}