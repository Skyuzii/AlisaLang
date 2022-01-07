using System;
using Alisa.Language.Models;

namespace Alisa.Language.Exceptions
{
    public class ASTException : Exception
    {
        public TreeElement Element;
        
        public ASTException(TreeElement element, string message) 
            : base($"\"{message}\" ({element.File}:{element.Line}:{element.CharIndex})")
        {
        }
    }
}