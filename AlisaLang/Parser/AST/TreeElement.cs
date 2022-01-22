using AlisaLang.Lexer;

namespace AlisaLang.Parser.AST
{
    public abstract class TreeElement
    {
        public abstract string GetSource();
        // public int Position { get; }
        // public int Line { get; }
        // public int CharIndex { get; }
        //
        // public string File { get; set; }
        //
        // public TreeElement(Lexeme lexeme)
        // {
        //     Position = lexeme.Position;
        //     Line = lexeme.Line;
        //     CharIndex = lexeme.CharIndex;
        //     File = lexeme.File;
        // }
    }
}