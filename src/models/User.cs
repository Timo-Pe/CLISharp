using System.Collections.Generic;

namespace CLISharp.Src.models
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public int Level { get; set; }

        public Dictionary<string, int> Resources { get; set; } = [];


    }
}