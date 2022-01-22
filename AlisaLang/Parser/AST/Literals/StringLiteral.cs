namespace AlisaLang.Parser.AST.Literals
{
    public class StringLiteral : BaseLiteral
    {
        public StringLiteral(string value) : base(value)
        {
        }

        public override string GetSource()
            => $"\"{Value}\"";
    }
}