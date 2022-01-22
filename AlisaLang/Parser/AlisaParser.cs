using System;
using System.Collections.Generic;
using System.Linq;
using AlisaLang.Lexer;
using AlisaLang.Parser.AST;
using AlisaLang.Parser.AST.ControlFlow;
using AlisaLang.Parser.AST.Expressions;
using AlisaLang.Parser.AST.Literals;
using AlisaLang.Parser.AST.Operators;
using AlisaLang.Reader;

namespace AlisaLang.Parser
{
    public class AlisaParser
    {
        private readonly string _file;
        private readonly LexemeReader _reader;

        public AlisaParser(string file, List<Lexeme> lexemes)
        {
            _file = file;
            _reader = new LexemeReader(lexemes);
        }

        public List<TreeElement> GetTreeElements()
        {
            var elements = new List<TreeElement>();

            while (!_reader.IsEmpty())
            {
                var lexeme = _reader.Read();
                _reader.Next();
                if (lexeme.Type == LexemeType.NewLine)
                {
                    continue;
                }
                
                var element = GetTreeElement(lexeme);
                if (element == null) break;
                elements.Add(element);
            }

            return elements;
        }

        private TreeElement GetTreeElement(Lexeme lexeme)
        {
            return lexeme.Type switch
            {
                LexemeType.Bool => GetBoolElement(lexeme),
                LexemeType.String => GetStringElement(lexeme),
                LexemeType.Number => GetNumberElement(lexeme),
                LexemeType.Null => GetNullElement(lexeme),
                LexemeType.Keyword => GetKeywordElement(lexeme),
                LexemeType.Variable => GetVariableElement(lexeme),
                LexemeType.Operator => GetOperatorElement(lexeme),
                LexemeType.Special => GetSpecialElement(lexeme),
            };
        }

        private TreeElement GetSpecialElement(Lexeme lexeme) => new SpecialElement(lexeme.Value);
        private TreeElement GetOperatorElement(Lexeme lexeme) => new OperatorElement(lexeme.Value);
        private TreeElement GetKeywordElement(Lexeme lexeme)
        {
            return lexeme.Value switch
            {
                "if" => GetIfElement(lexeme),
                "let" => GetLetElement(lexeme),
                "func" => GetFuncElement(lexeme),
                "return" => GetReturnElement(lexeme),
                "while" => GetWhileElement(lexeme),
                "break" => GetBreakElement(lexeme),
                "continue" => GetContinueElement(lexeme),
            };
        }

        private TreeElement GetContinueElement(Lexeme lexeme) => new ContinueElement();

        private TreeElement GetBreakElement(Lexeme lexeme) => new BreakElement();

        private WhileElement GetWhileElement(Lexeme lexeme)
        {
            var condition = ParseBeforeOpenBracket();
            var body = ParseInBrackets("{", "}");

            return new WhileElement(condition, body);
        }

        private ReturnElement GetReturnElement(Lexeme lexeme)
        {
            var lexemes = _reader.ReadWhile(item => item.Type == LexemeType.NewLine ? false : item);
            var body = GetTreeElements(lexemes);

            return new ReturnElement(body);
        }

        private FuncElement GetFuncElement(Lexeme lexeme)
        {
            _reader.ReadWhile(item => item.Type != LexemeType.Variable);
            var variable = new VariableElement(_reader.Read().Value);
            _reader.Next();
            var arguments = ParseInBrackets("(", ")");
            var body = ParseInBrackets("{", "}");

            return new FuncElement(variable, arguments, body);
        }

        private LetElement GetLetElement(Lexeme lexeme)
        {
            _reader.ReadWhile(item => item.Type != LexemeType.Variable);
            var variable = GetVariableElement(_reader.Read());

            _reader.Next();
            lexeme = _reader.Read();
            if (lexeme.Type is not LexemeType.Operator) return new LetElement((VariableElement)variable, null);
         
            var body = _reader.ReadWhile(item => item.Type == LexemeType.NewLine ? false : item);
            return new LetElement((VariableElement)variable, GetTreeElements(body));
        }

        private TreeElement GetVariableElement(Lexeme lexeme)
        {
            var variable = new VariableElement(lexeme.Value);
            lexeme = _reader.Read();
            if (lexeme == null) return variable;
            
            switch (lexeme.Type)
            {
                case LexemeType.Operator:
                {
                    switch (lexeme.Value)
                    {
                        case "==":
                            return variable;
                        case "=":
                        case "+=":
                        {
                            _reader.Next();
                            var right = _reader.ReadWhile(item => item.Type == LexemeType.NewLine ? false : item);
                            return new AssignElement(variable, new OperatorElement(lexeme.Value), GetTreeElements(right));
                        }
                        default:
                            return variable;
                    }
                }
                case LexemeType.Special:
                {
                    if (lexeme.Value is "{" or ")" or ",") return variable;
                    if (lexeme.Value == ".")
                    {
                        _reader.Next();
                        lexeme = _reader.Read();
                        variable = new VariableElement($"{variable.Value}.{lexeme.Value}");
                    }
                    
                    if (lexeme.Value != "(")
                        _reader.Next();
                    var args = ParseInBrackets("(", ")");
                    return new CallFuncElement(variable, args);
                }
                default:
                    return variable;
            }
        }

        private IfElement GetIfElement(Lexeme lexeme)
        {
            var condition = ParseBeforeOpenBracket();
            var thenExpression = ParseInBrackets("{", "}");

            var ifElement = new IfElement((condition, thenExpression));

            lexeme = _reader.Read();
            while (lexeme != null && lexeme.Type == LexemeType.Keyword && (lexeme.Value == "elif" || lexeme.Value == "else"))
            {
                _reader.Next();

                switch (lexeme.Value)
                {
                    case "elif":
                    {
                        var elifCondition = ParseBeforeOpenBracket();
                        var elifThenExpression = ParseInBrackets("{", "}");
                        ifElement.ElifBlocks ??= new List<(List<TreeElement>, List<TreeElement>)>();
                        ifElement.ElifBlocks.Add((elifCondition, elifThenExpression));
                        break;
                    }
                    case "else":
                    {
                        var elseThenExpression = ParseInBrackets("{", "}");
                        ifElement.ElseBlock = elseThenExpression;
                        break;
                    }
                }

                lexeme = _reader.Read();
            }
            
            return ifElement;
        }

        private List<TreeElement> ParseBeforeOpenBracket()
        {
            var lexemes = _reader.ReadWhile(item => item.Value == "{" ? false : item);
            return GetTreeElements(lexemes);
        }

        private List<TreeElement> ParseInBrackets(string startBracket, string endBracket)
        {
            var openBracketsCount = 0;
            
            var lexemes = _reader.ReadWhile(item =>
            {
                if (item.Type == LexemeType.Special)
                {
                    if (item.Value == startBracket)
                    {
                        openBracketsCount++;
                    }
                    else if (item.Value == endBracket)
                    {
                        openBracketsCount--;
                        if (openBracketsCount == 0) return false;
                    }
                }
                
                return item;
            });

            lexemes.Add(_reader.Read());
            _reader.Next();
            
            return GetTreeElements(lexemes);
        }

        private List<TreeElement> GetTreeElements(List<Lexeme> lexemes)
        {
            return new AlisaParser(_file, lexemes).GetTreeElements();
        }

        private NumberLiteral GetNumberElement(Lexeme lexeme) => new NumberLiteral(lexeme.Value);

        private StringLiteral GetStringElement(Lexeme lexeme) => new StringLiteral(lexeme.Value);

        private BoolLiteral GetBoolElement(Lexeme lexeme) => new BoolLiteral(lexeme.Value);
        
        private NullLiteral GetNullElement(Lexeme lexeme) => new NullLiteral(null);
    }
}