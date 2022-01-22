using AlisaLang.Lexer;

namespace AlisaLang.Parser.AST.Literals
{
    public class NumberLiteral : BaseLiteral
    {
        public NumberLiteral(string value) : base(value)
        {
        }

        public override string GetSource()
            => Value;
    }
}