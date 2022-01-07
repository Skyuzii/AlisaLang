using Alisa.Reader;

namespace Alisa.Language.Models.Lexeme
{
    public class NullLexeme : TreeElement, ILexeme
    {
        public NullLexeme(string file, StringReader reader)
            : base(file, reader.Position, reader.Line, reader.CharIndex)
        {
            
        }
    }
}