﻿using Alisa.Reader;

namespace Alisa.Language.Models.Lexeme
{
    public class SpecialLexeme : TreeElement, ILexeme
    {
        public string Value { get; }
        
        public SpecialLexeme(string value, string file, StringReader reader)
            : base(file, reader.Position, reader.Line, reader.CharIndex)
        {
            Value = value;
        }
    }
}