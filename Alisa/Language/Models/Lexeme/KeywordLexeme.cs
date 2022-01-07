using Alisa.Reader;

namespace Alisa.Language.Models.Lexeme
{
    public class KeywordLexeme : TreeElement, ILexeme
    {
        public string Value { get; }
        
        public KeywordLexeme(string value, string file, StringReader reader)
            : base(file, reader.Position, reader.Line, reader.CharIndex)
        {
            Value = value;
        }
    }
}