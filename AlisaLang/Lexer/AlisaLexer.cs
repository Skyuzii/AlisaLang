using System.Collections.Generic;
using System.Text.RegularExpressions;
using AlisaLang.Language.Exceptions;
using AlisaLang.Reader;

namespace AlisaLang.Lexer
{
    public class AlisaLexer
    {
        private StringReader _reader;
        private readonly string _file;
        
        private string _operatorChars = "+-*/%=|&<>^";
        private string _specialChars = "(){}[],.:@";
        private string _keywords = "if else elif let func return while continue break";
        private Regex _keywordRegex = new("^([a-zA-Z_0-9]*)$");

        public AlisaLexer(string file, string data)
        {
            _file = file;
            _reader = new StringReader(data);
        }

        public List<Lexeme> GetTokens()
        {
            var tokens = new List<Lexeme>();

            while (!_reader.IsEmpty())
            {
                var token = GetToken(_reader.Read());
                if (token == null) break;
                tokens.Add(token);
            }

            return tokens;
        }

        private Lexeme GetToken(char item)
        {
            if (item == '\0') return null;

            if (item == '\n')
            {
                _reader.Next();
                return new Lexeme(LexemeType.NewLine, null, _file, _reader);
            }
            
            if (item == '\r' || item == '\t' || item == ' ')
            {
                _reader.Next();
                return GetToken(_reader.Read());
            }
            
            if (item == '/' && _reader.Read(1) == '/')
            {
                ReadToEndLine();
                return GetToken(_reader.Read());
            }

            if (item == '/' && _reader.Read(1) == '*')
            {
                ReadToEndComment();
                return GetToken(_reader.Read());
            }
            
            if (item == '"')
            {
                _reader.Next();
                return GetStringLexeme();
            }

            if (_operatorChars.Contains(item.ToString()))
            {
                return GetOperatorLexeme();
            }

            if (_specialChars.Contains(item.ToString()))
            {
                _reader.Next();
                return new Lexeme(LexemeType.Special, item.ToString(), _file, _reader);
            }

            if (char.IsDigit(item))
            {
                return GetNumericLexeme();
            }

            if (_keywordRegex.IsMatch(item.ToString()))
            {
                return GetKeywordLexeme();
            }
            
            throw new TokenizerException(
                _file, _reader.Line, 
                _reader.CharIndex, 
                $"Неизвестный символ '{item}'"
            );;
        }

        private Lexeme GetKeywordLexeme()
        {
            var data = _reader.ReadWhile(item => _keywordRegex.IsMatch(item.ToString()) ? item : false);
            if (_keywords.Contains(data)) return new Lexeme(LexemeType.Keyword, data, _file, _reader);

            return data switch
            {
                "true" or "false" => new Lexeme(LexemeType.Bool, data, _file, _reader),
                "null" => new Lexeme(LexemeType.Null, null, _file, _reader),
                _ => new Lexeme(LexemeType.Variable, data, _file, _reader)
            };
        }

        private Lexeme GetNumericLexeme()
        {
            var data = _reader.ReadWhile(item => char.IsDigit(item) || item == '.' ? item : false);
            return new Lexeme(LexemeType.Number, data, _file, _reader);
        }

        private Lexeme GetOperatorLexeme()
        {
            var data = _reader.ReadWhile(item => _operatorChars.Contains(item.ToString()) ? item : false);
            return new Lexeme(LexemeType.Operator, data, _file, _reader);
        }

        private Lexeme GetStringLexeme()
        {
            var data = _reader.ReadWhile(item => item != '"' ? item : false);
            _reader.Next();
            return new Lexeme(LexemeType.String, data, _file, _reader);
        }

        private void ReadToEndLine()
        {
            _reader.ReadWhile(item => item != '\n' && item != '\r');
        }

        private void ReadToEndComment()
        {
            _reader.Next(2);
            var startCommentCnt = 1;
            
            _reader.ReadWhile(item =>
            {
                if (startCommentCnt == 0) return false;
                
                switch (item)
                {
                    case '/' when _reader.Read(1) == '*':
                        _reader.Next();
                        startCommentCnt++;
                        return true;
                    case '*' when _reader.Read(1) == '/':
                        _reader.Next();
                        startCommentCnt--;
                        return true;
                    default:
                        return true;
                }
            });
        }
    }
}