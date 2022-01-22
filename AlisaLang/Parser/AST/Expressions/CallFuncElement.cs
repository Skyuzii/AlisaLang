using System.Collections.Generic;
using AlisaLang.Extensions;

namespace AlisaLang.Parser.AST.Expressions
{
    public class CallFuncElement : TreeElement
    {
        public TreeElement Variable { get; }
        public List<TreeElement> Args { get; }

        public CallFuncElement(TreeElement variable, List<TreeElement> args)
        {
            Variable = variable;
            Args = args;
        }

        public override string GetSource()
            => $"{Variable.GetSource()}{Args.GetSource()};";
    }
}