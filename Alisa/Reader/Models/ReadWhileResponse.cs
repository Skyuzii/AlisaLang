namespace Alisa.Reader.Models
{
    public class ReadWhileResponse
    {
        public readonly bool IsContinue;
        public readonly object Value;
        
        public ReadWhileResponse(bool isContinue, object value = null)
        {
            Value = value;
            IsContinue = isContinue;
        }
        
        public static implicit operator ReadWhileResponse(bool isContinue) => new ReadWhileResponse(isContinue);
        public static implicit operator ReadWhileResponse(string value) => new ReadWhileResponse(true, value);
        public static implicit operator ReadWhileResponse(char value) => new ReadWhileResponse(true, value);
    }
}