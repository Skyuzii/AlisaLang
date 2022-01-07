using System;

namespace Alisa.Language.Exceptions
{
    public class TokenizerException : Exception
    {
        public TokenizerException(string file, int line, int position, string message) : base(message + $" ({file}:{line}:{position})")
        {
        }
    }
}