namespace AlisaLang.Parser.AST.Literals
{
    public abstract class BaseLiteral : TreeElement
    {
        public string Value { get; }

        public BaseLiteral(string value)
        {
            Value = value;
        }
    }
}