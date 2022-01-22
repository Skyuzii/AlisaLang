namespace AlisaLang.Parser.AST.Literals
{
    public class NullLiteral : BaseLiteral
    {
        public NullLiteral(string value) : base(value)
        {
        }

        public override string GetSource() => Value;
    }
}