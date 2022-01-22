using System;

namespace AlisaLang.Language.Exceptions
{
    public class TokenizerException : Exception
    {
        public TokenizerException(string file, int line, int position, string message) : base(message + $" ({file}:{line}:{position})")
        {
        }
    }
}