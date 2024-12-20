using System;
using CLISharp.Src.models;
using System.Linq;
using System.Collections.Generic;
using System.Reflection.Metadata;


namespace CLISharp.Src.Controller
{
    public class GameController
    {
        private readonly User currentUser;

        private readonly Dictionary<string, Action> commands;

        private bool isRunning = true;

        public GameController(User user)
        {
            this.currentUser = user;

            this.commands = new Dictionary<string, Action>{
                { "exit",       () => this.Exit() },
                { "name",       () => this.ShowName() },
                { "ressources", () => this.ShowResources() },
                { "help",       () => this.ShowHelp() }
            };
        }


        public void Start()
        {
            Console.Clear();
            Console.WriteLine("Bienvenue " + this.currentUser.Username + " !");
            this.ShowResources();
            Console.WriteLine("Que voulez-vous faire ?");

            while (this.isRunning)
            {
                string? input = Console.ReadLine()?.ToLower(); // Lecture de la commande utilisateur
                if (input != null && this.commands.ContainsKey(input))
                {
                    // Exécute l'action associée à la commande
                    this.commands[input].Invoke();
                }
                else
                {
                    Console.WriteLine("Commande inconnue. Tapez 'help' pour voir les commandes disponibles.");
                }
            }


        }

        private void Exit()
        {
            Console.WriteLine("Au revoir !");
            this.isRunning = false;
        }

        private void ShowHelp()
        {
            Console.WriteLine("Commandes disponibles :");
            Console.WriteLine(string.Join(" | ", this.commands.Select(cmd => $"{cmd.Key}")));
        }


        public void ShowName()
        {
            Console.WriteLine("Votre nom est : " + this.currentUser.Username);
        }

        public void ShowResources()
        {
            if (this.currentUser.Resources != null)
            {
                string resourcesLine = string.Join(", ", this.currentUser.Resources.Select(kvp => $"{kvp.Key}: {kvp.Value}"));
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine("Ressources : " + resourcesLine);
                Console.WriteLine("-------------------------------------------------------------");

            }
            else
            {
                Console.WriteLine("Aucune ressource disponible.");
            }


        }
    }
}