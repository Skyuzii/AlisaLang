using System.Collections.Generic;
using System.Text;
using AlisaLang.Parser.AST;
using AlisaLang.Parser.AST.Literals;

namespace AlisaLang.Extensions
{
    public static class ListTreeElement
    {
        public static string GetSource<T>(this List<T> treeElements) where T : TreeElement
        {
            var sb = new StringBuilder();
            foreach (var treeElement in treeElements)
            {
                sb.Append(treeElement.GetSource());
            }

            return sb.ToString();
        }
        
        public static string GetSourceFuncArguments<T>(this List<T> treeElements) where T : TreeElement
        {
            var sb = new StringBuilder();
            foreach (var treeElement in treeElements)
            {
                sb.Append(treeElement.GetSource());
            }

            return sb.ToString();
        }
    }
}