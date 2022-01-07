using Alisa.Reader;

namespace Alisa.Language.Models.Lexeme
{
    public class StringLexeme : TreeElement, ILexeme
    {
        public string Value;
                
        public StringLexeme(string value, string file, StringReader reader)
            : base(file, reader.Position, reader.Line, reader.CharIndex)
        {
            Value = value;
        }
        
        public StringLexeme(string value, string file, int position, int line, int charIndex)
            : base(file, position, line, charIndex)
        {
            Value = value;
        }
    }
}