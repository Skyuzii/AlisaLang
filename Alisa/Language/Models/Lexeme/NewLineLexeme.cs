using Alisa.Reader;

namespace Alisa.Language.Models.Lexeme
{
    public class NewLineLexeme : TreeElement, ILexeme
    {
        public NewLineLexeme(string file, StringReader reader)
            : base(file, reader.Position, reader.Line, reader.CharIndex)
        {
        }
    }
}