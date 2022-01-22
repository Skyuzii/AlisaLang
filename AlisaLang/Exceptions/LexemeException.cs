using System;
using AlisaLang.Lexer;

namespace AlisaLang.Language.Exceptions
{
    public class LexemeException : Exception
    {
        public LexemeException(Lexeme lexeme, string message) 
            : base($"\"{message}\" ({lexeme.File}:{lexeme.Line}:{lexeme.CharIndex})")
        {
        }
    }
}