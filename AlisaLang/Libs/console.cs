using System;

namespace AlisaLang.Libs
{
    public static class console
    {
        public static void print(params dynamic[] args)
        {
            Console.WriteLine(string.Join(',', args));
        }
        
        public static dynamic input()
        {
            return Console.ReadLine();
        }
        
        public static dynamic input(params dynamic[] args)
        {
            Console.WriteLine(string.Join(',', args));
            return Console.ReadLine();
        }
    }
}