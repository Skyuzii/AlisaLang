using System;
using System.IO;
using Alisa.Language;
using Alisa.Language.Models.Lexeme;

namespace Alisa
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = File.ReadAllText("test.txt");
            var lexer = new Lexer("main", data);
            var tokens = lexer.GetTokens();
            
            Console.ReadLine();
        }
    }
}