using System.Collections.Generic;
using AlisaLang.Extensions;

namespace AlisaLang.Parser.AST.Expressions
{
    public class ReturnElement : TreeElement
    {
        public List<TreeElement> Body { get; }

        public ReturnElement(List<TreeElement> body)
        {
            Body = body;
        }

        public override string GetSource()
            => $"return {Body.GetSource()};";
    }
}