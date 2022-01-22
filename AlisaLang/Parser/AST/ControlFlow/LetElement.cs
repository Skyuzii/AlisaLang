using System.Collections.Generic;
using AlisaLang.Extensions;
using AlisaLang.Parser.AST.Expressions;

namespace AlisaLang.Parser.AST.ControlFlow
{
    public class LetElement : TreeElement
    {
        public VariableElement VariableElement { get; }

        public List<TreeElement> Body { get; }

        public LetElement(VariableElement variableElement, List<TreeElement> body)
        {
            VariableElement = variableElement;
            Body = body;
        }

        public override string GetSource()
            => $"dynamic {VariableElement.GetSource()} {GetBody()}";

        private string GetBody()
        {
            var source = Body?.GetSource();
            if (source == null || source.EndsWith(";")) return source;

            return source + ";";
        }
    }
}