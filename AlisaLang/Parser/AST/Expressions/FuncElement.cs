using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlisaLang.Extensions;
using AlisaLang.Parser.AST.ControlFlow;
using AlisaLang.Parser.AST.Literals;

namespace AlisaLang.Parser.AST.Expressions
{
    public class FuncElement : TreeElement
    {
        public TreeElement Variable { get; }
        public List<TreeElement> Arguments { get; }
        public List<TreeElement> Body { get; }
        public FuncElement(TreeElement variable, List<TreeElement> arguments, List<TreeElement> body)
        {
            Variable = variable;
            Arguments = arguments;
            Body = body;
        }

        public override string GetSource()
        {
            return Variable.GetSource() == "main" ? 
                $"private static void Main(string[] args) {Body.GetSource()}" : 
                $"private static {GetReturnType()} {Variable.GetSource()} {GetSourceFuncArguments()} {Body.GetSource()}";
        }

        private string GetReturnType()
            => Body.Any(x => x is ReturnElement) ? "dynamic" : "void";
        
        private string GetSourceFuncArguments()
        {
            var sb = new StringBuilder();
            foreach (var treeElement in Arguments)
            {
                string type = null;
                if (treeElement is VariableElement)
                {
                    type = "dynamic ";
                }
                
                sb.Append($"{type}{treeElement.GetSource()}");
            }

            return sb.ToString();
        }
    }
}