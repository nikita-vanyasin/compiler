using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    public class BaseScanner
    {
        private int currCharIndex;
        private char currChar;
        private string text;

        private delegate bool ReadStringCondition();

        public void SetText(string text)
        {
            Reset();
            this.text = text; ;
        }

        public Token GetNextToken()
        {
            if (currCharIndex >= text.Length)
            {
                return new Token(TokenType.EOF, "\0");
            }

            UpdateCurrChar();
            Token t = Scan();
            ++currCharIndex;

            return t;
        }

        public Token GetForwardToken()
        {
            if (currCharIndex >= text.Length)
            {
                return new Token(TokenType.EOF);
            }

            // save current state
            int savedCurrCharIndex = currCharIndex;

            Token t = GetNextToken();

            // restore state
            currCharIndex = savedCurrCharIndex;
            UpdateCurrChar();

            return t;
        }

        public SourcePosition GetSourcePosition()
        {
            var pos = new SourcePosition();
			pos.Position = currCharIndex;
			pos.TokenLength = GetForwardToken().Attribute.Length;
            return pos;
        }

        public string GetCurrentPositionAsString()
        {
            return TextUtils.CharPosToText(text, currCharIndex);
        }

        private Token Scan()
        {
            return ReadToken();
        }

        private Token ReadToken()
        {
            switch (currChar)
            {
                case ' ': return new Token(TokenType.SPACE, currChar);
                case ':': return new Token(TokenType.COLON, currChar);
                case '.': return new Token(TokenType.DOT, currChar);
                case ',': return new Token(TokenType.COMMA, currChar);
                case '(': return new Token(TokenType.LEFT_PAREN, currChar);
                case ')': return new Token(TokenType.RIGHT_PAREN, currChar);
                case '+': return new Token(TokenType.PLUS, currChar);
                case '-': return new Token(TokenType.MINUS, currChar);
                case '*': return new Token(TokenType.MULTIPLICATION, currChar);
                case '!': return NotSwitchBranch();
                case '[': return new Token(TokenType.LEFT_BRACKET, currChar);
                case ']': return new Token(TokenType.RIGHT_BRACKET, currChar);
                case '{': return new Token(TokenType.LEFT_BRACE, currChar);
                case '}': return new Token(TokenType.RIGHT_BRACE, currChar);
                case '<': return LessSwitchBranch();
                case '>': return GreaterSwitchBranch();
                case '\n': return NewLineBranch();
                case '\r': return CarriageReturnBranch();
                case '&': return AmpersandSwitchBranch();
                case '|': return PipeSwitchBranch();
                case '=': return AssignSwitchBranch();

                default: return DefaultSwitchBranch();
            }
        }

        private Token NotSwitchBranch()
        {
            if (GetNextChar() == '=')
            {
                PeekNext();
                return new Token(TokenType.NOT_EQUAL, "!=");
            }

            return new Token(TokenType.NOT, "!");
        }

        private Token LessSwitchBranch()
        {
            if (GetNextChar() == '=')
            {
                PeekNext();
                return new Token(TokenType.LTE, "<=");
            }

            return new Token(TokenType.LT, "<");
        }


        private Token GreaterSwitchBranch()
        {
            if (GetNextChar() == '=')
            {
                PeekNext();
                return new Token(TokenType.GTE, ">=");
            }

            return new Token(TokenType.GT, ">");
        }

        private Token AssignSwitchBranch()
        {
            if (GetNextChar() == '=')
            {
                PeekNext();
                return new Token(TokenType.EQUAL, "==");
            }

            return new Token(TokenType.ASSIGNMENT, currChar);
        }

        private Token NewLineBranch()
        {
            if (GetNextChar() == '\r')
            {
                PeekNext();
            }
            return new Token(TokenType.LINE_END, "\n");
        }

        private Token CarriageReturnBranch()
        {
            if (GetNextChar() == '\n')
            {
                PeekNext();
            }
            return new Token(TokenType.LINE_END, "\n");
        }

        private Token AmpersandSwitchBranch()
        {
            if (GetNextChar() == '&')
            {
                PeekNext();
                return new Token(TokenType.AND, "&&");
            }

            return GetErrorToken();
        }

        private Token PipeSwitchBranch()
        {
            if (GetNextChar() == '|')
            {
                PeekNext();
                return new Token(TokenType.OR, "||");
            }

            return GetErrorToken();
        }

        private void SkipWhiteSpaces()
        {
            while (char.IsWhiteSpace(currChar))
            {
                PeekNext();
            }
        }

        private Token DefaultSwitchBranch()
        {
            if (currChar.IsSimpleLatin())
            {
                return GetKeywordToken(ReadInditifier());
            }
            else if (char.IsDigit(currChar))
            {
                string str = ReadDigits();
                bool nextCharIsAlsoLatin = GetNextChar().IsSimpleLatin();
                if (!nextCharIsAlsoLatin)
                {
                    return new Token(TokenType.INTEGER_VALUE, str);
                }
            }

            return GetErrorToken();
        }

        private Token GetErrorToken()
        {
            string errMsg = "Unknown token";
            return new Token(TokenType.ERROR, errMsg);
        }

        private string ReadInditifier()
        {
            return ReadStringWhileCond(() => currChar.IsSimpleLatin() || char.IsDigit(currChar));
        }

        private string ReadDigits()
        {
            return ReadStringWhileCond(() => char.IsDigit(currChar));
        }

        private string ReadStringWhileCond(ReadStringCondition cond)
        {
            string str = "" + currChar;
            PeekNext();

            while (cond() && currCharIndex < text.Length - 1)
            {
                str += currChar;
                PeekNext();
            }
            PeekPrev();

            return str;
        }

        private Token GetKeywordToken(string str)
        {
            return TokenFactory.CreateByString(str);
        }

        private void Reset()
        {
            currCharIndex = 0;
        }

        private char GetNextChar()
        {
            return (currCharIndex + 1 < text.Length) ? text[currCharIndex + 1] : ' ';
        }

        private void PeekNext()
        {
            ++currCharIndex;
            UpdateCurrChar();
        }

        private void PeekPrev()
        {
            --currCharIndex;
            UpdateCurrChar();
        }

        private void UpdateCurrChar()
        {
            if (!IsEof())
            {
                currChar = text[currCharIndex];
            }
        }

        private bool IsEof()
        {
            return currCharIndex >= text.Length;
        }
    }
}
