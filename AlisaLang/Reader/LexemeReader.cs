using System.Collections.Generic;
using AlisaLang.Lexer;

namespace AlisaLang.Reader
{
    public class LexemeReader : AbstractReader<List<Lexeme>, Lexeme>
    {
        public LexemeReader(List<Lexeme> parts):  base(parts) {}
        
        protected override List<Lexeme> AddToT(object part, List<Lexeme> list)
        {
            switch (part)
            {
                case Lexeme element:
                    list.Add(element);
                    break;
                case List<Lexeme> elements:
                    list.AddRange(elements);
                    break;
            }

            return list;
        }

        protected override List<Lexeme> CreateT()
            => new();

        public override Lexeme Read(int offset = 0)
            => IsEmpty(offset) ? null : Content[Position + offset];

        public override bool IsEmpty(int offset = 0)
            => Position + offset < 0 || Position + offset >= Content.Count;
    }
}