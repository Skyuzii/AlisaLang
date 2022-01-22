using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using AlisaLang.Compiler;
using AlisaLang.Language;
using AlisaLang.Lexer;
using AlisaLang.Libs;
using AlisaLang.Parser;

namespace Alisa
{
    class Program
    {
        static void Main(string[] args)
        {
            InvokeWithTime(() =>
            {
                var file = args[0];
                var text = InvokeWithTime(() => File.ReadAllText(file), "Код загружен в память");
                var lexemes = InvokeWithTime(() => new AlisaLexer(file, text).GetTokens(), "Код разбит на лексемы");
                var tree = InvokeWithTime(() => new AlisaParser(file, lexemes).GetTreeElements(), "Построили AST");
                InvokeWithTime(() => new AlisaCompiler(tree, file).Compile(), "Скомпилировали");
            }, "Всего ушло");
            Console.ReadLine();
        }

        private static void InvokeWithTime(Action action, string resultMsg)
        {
            var sw = Stopwatch.StartNew();
            action.Invoke();
            sw.Stop();

            Console.WriteLine($"{resultMsg} [{sw.Elapsed}]");
        }
        
        private static T InvokeWithTime<T>(Func<T> action, string resultMsg)
        {
            var sw = Stopwatch.StartNew();
            var res = action.Invoke();
            sw.Stop();

            Console.WriteLine($"{resultMsg} [{sw.Elapsed}]");
            return res;
        }
    }
}