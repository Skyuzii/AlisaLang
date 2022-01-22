using System.Collections.Generic;
using AlisaLang.Extensions;
using AlisaLang.Parser.AST.ControlFlow;
using AlisaLang.Parser.AST.Literals;
using AlisaLang.Parser.AST.Operators;

namespace AlisaLang.Parser.AST.Expressions
{
    public class AssignElement : TreeElement
    {
        public TreeElement Left { get; }
        public TreeElement Operator { get; }
        public List<TreeElement> Right { get; }

        public AssignElement(TreeElement left, TreeElement @operator, List<TreeElement> right)
        {
            Left = left;
            Operator = @operator;
            Right = right;
        }

        public override string GetSource()
            => $"{Left.GetSource()} {Operator.GetSource()} {Right.GetSource()};";
    }
}