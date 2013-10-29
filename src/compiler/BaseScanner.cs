using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    class BaseScanner
    {
        private int currCharIndex;
        private char currChar;
        private string text;

        private delegate bool ReadStringCondition();

        public void SetText(string text)
        {
            Reset();
            this.text = text;

            UpdateCurrChar();
            SkipWhiteSpaces();
        }

        public Token GetNextToken()
        {
            if (currCharIndex >= text.Length)
            {
                return new Token(TokenType.EOF);
            }

            UpdateCurrChar();
            Token t = Scan();
            ++currCharIndex;

            return t;
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
                case '=': return new Token(TokenType.ASSIGNMENT, currChar);
                case ':': return new Token(TokenType.COLON, currChar);
                case '.': return new Token(TokenType.DOT, currChar);
                case ',': return new Token(TokenType.COMMA, currChar);
                case '(': return new Token(TokenType.LEFT_PAREN, currChar);
                case ')': return new Token(TokenType.RIGHT_PAREN, currChar);
                case '+': return new Token(TokenType.PLUS, currChar);
                case '-': return new Token(TokenType.MINUS, currChar);
                case '*': return new Token(TokenType.MULTIPLICATION, currChar);
                case '!': return new Token(TokenType.NOT, currChar);
                case '<': return new Token(TokenType.LT, currChar);
                case '>': return new Token(TokenType.GT, currChar);
                case '&': return AmpersandSwitchBranch();

                default: return DefaultSwitchBranch();
            }
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
            string errMsg = "at " + GetCurrentPositionAsString() + ": Unknown token";
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
