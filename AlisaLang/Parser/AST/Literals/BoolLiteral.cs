namespace AlisaLang.Parser.AST.Literals
{
    public class BoolLiteral : BaseLiteral
    {
        public BoolLiteral(string value) : base(value){}

        public override string GetSource()
            => Value;
    }
}