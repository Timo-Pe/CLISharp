
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using CLISharp.Src.models;
using System.Linq;

namespace CLISharp.Src.Auth
{
    public class Auth
    {
        public const string PATH_FILE_AUTH = "./src/auth";
        private string filePath = $"{Auth.PATH_FILE_AUTH}/auth.json";
        private List<User> users = new();

        private User? userAuthenticated;


        private void Exit()
        {
            Console.WriteLine("Au revoir !");
            Environment.Exit(0);
        }

        public User? Init()
        {
            EnsureFileExists();
            users = LoadUsers();


            Console.WriteLine("Veuillez entrer une commande : Login, Register, Exit");

            string input = Console.ReadLine() ?? string.Empty;

            switch (input)
            {
                case "Login":
                    this.userAuthenticated = this.Login();
                    break;
                case "Register":
                    this.userAuthenticated = this.Register();
                    break;
                case "Exit":
                    this.Exit();
                    break;
                default:
                    Console.WriteLine("Commande inconnue. Tapez 'help' pour voir les commandes disponibles.");
                    break;
            }

            return this.userAuthenticated;

        }
        public User Register()
        {
            Console.WriteLine("=== Enregistrement d'un nouvel utilisateur ===");
            Console.Write("Entrez un pseudo : ");
            string username = Console.ReadLine() ?? string.Empty;

            if (users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Ce pseudo est déjà pris. Veuillez en choisir un autre.");
                return Register();
            }

            Console.Write("Entrez un mot de passe : ");
            string password = Console.ReadLine() ?? string.Empty;

            User newUser = new User
            {
                Id = users.Count + 1,
                Username = username,
                Password = password,
                Level = 1,
                Resources = new Dictionary<string, int>
                    {
                        { "Bois", 100 },
                        { "Pierre", 100 },
                        { "Or", 50 },
                        { "Nourriture", 100 }
                    }
            };

            users.Add(newUser);
            SaveUsers();
            Console.WriteLine($"Utilisateur {username} enregistré avec succès !");
            return newUser;
        }

        private void SaveUsers()
        {
            try
            {
                string jsonContent = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, jsonContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la sauvegarde des utilisateurs : {ex.Message}");
            }
        }

        public User? Login()
        {
            Console.WriteLine("=== Connexion ===");
            Console.Write("Entrez votre pseudo : ");
            string username = Console.ReadLine() ?? string.Empty;

            User? foundUser = users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            if (foundUser == null)
            {
                Console.WriteLine("Utilisateur introuvable. Voulez-vous vous inscrire ? (y/n)");
                string response = Console.ReadLine()?.ToLower() ?? "n";
                if (response == "y")
                {
                    return Register();
                }
                return null;
            }

            Console.Write("Entrez votre mot de passe : ");
            string password = Console.ReadLine() ?? string.Empty;

            if (foundUser.Password.Equals(password, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Connexion réussie. Bienvenue {foundUser.Username} !");
                return foundUser;
            }
            else
            {
                Console.WriteLine("Mot de passe incorrect. Veuillez réessayer.");
                return Login();
            }
        }

        private void EnsureFileExists()
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Le fichier {filePath} n'existe pas. Création d'un fichier vide.");
                File.WriteAllText(filePath, "[]");
            }
        }

        private List<User> LoadUsers()
        {
            try
            {
                string jsonContent = File.ReadAllText(filePath);
                var users = JsonSerializer.Deserialize<List<User>>(jsonContent);

                if (users == null)
                {
                    Console.WriteLine("Le fichier JSON est vide ou mal formaté. Initialisation avec une liste vide.");
                    return new List<User>();
                }

                return users;
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Erreur lors de la désérialisation du fichier JSON : {jsonEx.Message}");
                return new List<User>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur inattendue : {ex.Message}");
                return new List<User>();
            }
        }
    }
}


