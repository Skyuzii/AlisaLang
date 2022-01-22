using AlisaLang.Language.Exceptions;
using AlisaLang.Parser.AST;
using AlisaLang.Parser.AST.Literals;
using AlisaLang.Reader;

namespace AlisaLang.Lexer
{
    public class Lexeme
    {
        public int Position { get; }
        public int Line { get; }
        public int CharIndex { get; }
        public LexemeType Type { get; }
        public string Value { get; }
        public string File { get; }

        public Lexeme(LexemeType type, string value, string file, int position, int line, int charIndex)
        {
            Type = type;
            Value = value;
            File = file;
            Position = position;
            Line = line;
            CharIndex = charIndex;
        }
        
        public Lexeme(LexemeType type, string value, string file, StringReader reader)
        {
            Type = type;
            Value = value;
            File = file;
            Position = reader.Position;
            Line = reader.Line;
            CharIndex = reader.CharIndex;
        }

        public void Exception(string message)
            => throw new LexemeException(this, message);
    }
}