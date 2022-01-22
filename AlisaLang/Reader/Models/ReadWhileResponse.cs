using AlisaLang.Lexer;
using AlisaLang.Parser.AST;

namespace AlisaLang.Reader.Models
{
    public class ReadWhileResponse
    {
        public bool IsContinue { get; }
        public object Value { get; }
        
        public ReadWhileResponse(bool isContinue, object value = null)
        {
            Value = value;
            IsContinue = isContinue;
        }
        
        public static implicit operator ReadWhileResponse(bool isContinue) => new ReadWhileResponse(isContinue);
        public static implicit operator ReadWhileResponse(string value) => new ReadWhileResponse(true, value);
        public static implicit operator ReadWhileResponse(Lexeme value) => new ReadWhileResponse(true, value);
        public static implicit operator ReadWhileResponse(TreeElement value) => new ReadWhileResponse(true, value);
        public static implicit operator ReadWhileResponse(char value) => new ReadWhileResponse(true, value);
    }
}