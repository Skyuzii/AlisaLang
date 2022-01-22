using System.Collections.Generic;
using AlisaLang.Extensions;

namespace AlisaLang.Parser.AST.ControlFlow
{
    public class WhileElement : TreeElement
    {
        public List<TreeElement> Condition { get; }
        public List<TreeElement> Body { get; }

        public WhileElement(List<TreeElement> condition, List<TreeElement> body)
        {
            Condition = condition;
            Body = body;
        }

        public override string GetSource()
            => $"while ({Condition.GetSource()}) {Body.GetSource()}";
    }
}