
using System;
using CLISharp.Src.Auth;

namespace CLISharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenu sur CLISharp !");
            Console.WriteLine("Souhaitez-vous lancer le programme ?");
            Console.WriteLine("[Entrée] pour lancer le programme, [Autre] pour quitter.");

            var input = Console.ReadKey(intercept: true);

            if (input.Key == ConsoleKey.Enter)
            {
                Console.Clear();
                try
                {
                    Auth.Connect();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            else
            {
                Console.WriteLine("\nAu revoir !");
                Environment.Exit(0);
            }
        }
    }

}

