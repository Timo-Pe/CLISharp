
using System;
using CLISharp.Src.Auth;
using CLISharp.Src.Controller;
using CLISharp.Src.models;

Console.WriteLine("---------- Bienvenu sur CLISharp ! ----------");
Console.WriteLine("---- Souhaitez-vous lancer le programme ? ----");
Console.WriteLine("[Entrée] Pour acceder au menu, [Autre] pour quitter.");

var input = Console.ReadKey(intercept: true);

if (input.Key == ConsoleKey.Enter)
{
    Console.Clear();
    try
    {
        // Initialisation de l'authentification
        Auth auth = new Auth();

        User? userAuthenticated = auth.Init();
        if (userAuthenticated != null)
        {
            // Lancement du jeu
            GameController gameController = new GameController(userAuthenticated);
            gameController.Start();

        }

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

