
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using CLISharp.Src.models;

namespace CLISharp.Src.Auth
{
    public class Auth
    {
        public const string PATH_FILE_AUTH = "./src/auth";

        public static void Connect()
        {

            List<User>? users = Auth.ReadFile();

            Console.WriteLine(users); // TODO continuer ici 
        }


        public static List<User>? ReadFile()
        {
            string filePath = $"{Auth.PATH_FILE_AUTH}/auth.json";

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Erreur : Le fichier {filePath} n'existe pas.");
                return new List<User>();
            }

            try
            {

                string jsonContent = File.ReadAllText(filePath);

                List<User>? users = JsonSerializer.Deserialize<List<User>>(jsonContent);

                if (users == null || users.Count == 0)
                {
                    Console.WriteLine("Aucun utilisateur trouvé dans le fichier JSON.");
                    return new List<User>();
                }

                return users;
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Erreur lors de la désérialisation du fichier JSON : {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur inattendue : {ex.Message}");
            }

            return new List<User>(); // En cas d'erreur, retourner une liste vide
        }
    }
}


