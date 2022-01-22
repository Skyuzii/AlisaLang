namespace AlisaLang.Parser.AST.ControlFlow
{
    public class SpecialElement : TreeElement
    {
        public string Value { get; }

        public SpecialElement(string value)
        {
            Value = value;
        }

        public override string GetSource() => Value;
    }
}