using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Alisa.Language.Exceptions;
using Alisa.Language.Models;
using Alisa.Language.Models.Lexeme;
using Alisa.Reader;

namespace Alisa.Language
{
    public class Lexer
    {
        private StringReader _reader;
        private readonly string _file;
        
        private string _operatorChars = "+-*/%=|&<>^";
        private string _specialChars = "(){}[],.:@";
        private string _keywords = "if else elif var";
        private Regex _keywordRegex = new("^([a-zA-Z_]*)$");

        public Lexer(string file, string data)
        {
            _file = file;
            _reader = new StringReader(data);
        }

        public List<TreeElement> GetTokens()
        {
            var tokens = new List<TreeElement>();

            while (!_reader.IsEmpty())
            {
                var token = GetToken(_reader.Read());
                if (token == null) break;
                tokens.Add(token);
            }

            return tokens;
        }

        private TreeElement GetToken(char item)
        {
            if (item == '=')
            {
                Console.WriteLine("test");
            }
            
            if (item == '\0') return null;

            if (item == '\n')
            {
                _reader.Next();
                return new NewLineLexeme(_file, _reader);
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

            if (_operatorChars.Contains(item))
            {
                return GetOperatorLexeme();
            }

            if (_specialChars.Contains(item))
            {
                _reader.Next();
                return new SpecialLexeme(item.ToString(), _file, _reader);
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

        private TreeElement GetKeywordLexeme()
        {
            var data = _reader.ReadWhile(item => _keywordRegex.IsMatch(item.ToString()) ? item : false);
            if (_keywords.Contains(data)) return new KeywordLexeme(data, _file, _reader);
            if (data == "true" || data == "false") return new BoolLexeme(data == "true", _file, _reader);
            if (data == "null") return new NullLexeme(_file, _reader);

            return new VariableLexeme(data, _file, _reader);
        }

        private TreeElement GetNumericLexeme()
        {
            var data = _reader.ReadWhile(item => char.IsDigit(item) || item == '.' ? item : false);
            return new NumericLexeme(float.Parse(data), _file, _reader);
        }

        private TreeElement GetOperatorLexeme()
        {
            var data = _reader.ReadWhile(item => _operatorChars.Contains(item) ? item : false);

            return new OperatorLexeme(data, _file, _reader);
        }

        private TreeElement GetStringLexeme()
        {
            var data = _reader.ReadWhile(item => item != '"' ? item : false);
            _reader.Next();

            return new StringLexeme(data, _file, _reader);
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