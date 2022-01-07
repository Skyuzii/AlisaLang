using Alisa.Reader;

namespace Alisa.Language.Models.Lexeme
{
    public class NumericLexeme : TreeElement, ILexeme
    {
        public float Value { get; }
        
        public NumericLexeme(float value, string file, StringReader reader)
            : base(file, reader.Position, reader.Line, reader.CharIndex)
        {
            Value = value;
        }
    }
}