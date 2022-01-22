namespace AlisaLang.Parser.AST.ControlFlow
{
    public class VariableElement : TreeElement
    {
        public string Value { get; }

        public VariableElement(string value)
        {
            Value = value;
        }

        public override string GetSource() => Value;
    }
}