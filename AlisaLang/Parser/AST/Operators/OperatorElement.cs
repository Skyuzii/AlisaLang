namespace AlisaLang.Parser.AST.Operators
{
    public class OperatorElement : TreeElement
    {
        public string Operator { get; }

        public OperatorElement(string @operator)
        {
            Operator = @operator;
        }

        public override string GetSource() => Operator;
    }
}