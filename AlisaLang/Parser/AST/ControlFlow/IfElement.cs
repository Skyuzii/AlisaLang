using System.Collections.Generic;
using System.Text;
using AlisaLang.Extensions;
using AlisaLang.Lexer;

namespace AlisaLang.Parser.AST.ControlFlow
{
    public class IfElement : TreeElement
    {
        public (List<TreeElement>, List<TreeElement>) IfBlock { get; }
        public List<(List<TreeElement>,List<TreeElement>)> ElifBlocks { get; set; }
        public List<TreeElement> ElseBlock { get; set; }

        public IfElement((List<TreeElement>, List<TreeElement>) ifBlock)
        {
            IfBlock = ifBlock;
        }

        public override string GetSource()
        {
            var sourceBuilder = new StringBuilder($"if ({IfBlock.Item1.GetSource()}) {IfBlock.Item2.GetSource()}");

            if (ElifBlocks is not null)
            {
                foreach (var elifBlock in ElifBlocks)
                {
                    sourceBuilder.AppendLine($"else if ({elifBlock.Item1.GetSource()}) {elifBlock.Item2.GetSource()}");
                }   
            }

            if (ElseBlock != null)
            {
                sourceBuilder.AppendLine($"else {ElseBlock.GetSource()}");
            }

            return sourceBuilder.ToString();
        }
    }
}