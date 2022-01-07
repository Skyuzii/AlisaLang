using Alisa.Reader;

namespace Alisa.Language.Models.Lexeme
{
    public class BoolLexeme : TreeElement, ILexeme
    {
        public bool Value { get; }
        
        public BoolLexeme(bool value, string file, StringReader reader)
            : base(file, reader.Position, reader.Line, reader.CharIndex)
        {
            Value = value;
        }
    }
}