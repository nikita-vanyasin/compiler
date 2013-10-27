using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    class Scanner
    {
        private int currCharIndex;
        private char currChar;
        private string text;

        private delegate bool ReadStringCondition();

        public void SetText(string text)
        {
            Reset();
            this.text = text;
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

        private Token Scan()
        {
           // SkipWhiteSpaces();
            return ReadToken();
        }

        private Token ReadToken()
        {
            switch (currChar)
            {
                case '=': return new Token(TokenType.ASSIGNMENT, currChar);
                case ':': return new Token(TokenType.COLON, currChar);
                case '.': return new Token(TokenType.DOT, currChar);
                case ',': return new Token(TokenType.COMMA, currChar);
                case '(': return new Token(TokenType.LEFT_PAREN, currChar);
                case ')': return new Token(TokenType.RIGHT_PAREN, currChar);
                case '+': return new Token(TokenType.PLUS, currChar);
                case '-': return new Token(TokenType.MINUS, currChar);
                case '*': return new Token(TokenType.MULTIPLICATION, currChar);
              //  case '/': return SlashSwitchBranch();
                case '!': return new Token(TokenType.NOT, currChar);
                case '<': return new Token(TokenType.LT, currChar);
                case '>': return new Token(TokenType.GT, currChar);
              //  case '&': return AmpersandSwitchBranch();
              //  case '|': return PalkaSwitchBranch();

                default: return new Token(TokenType.ERROR);//DefaultSwitchBranch();
            }
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
