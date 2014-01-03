using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace compiler
{
    public class IndentationException : Exception
    {
        public IndentationException() { }
        public IndentationException(string message) : base(message) { }
        public IndentationException(string message, Exception inner) : base(message, inner) { }
        protected IndentationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    public class Scanner
    {
        const ushort INDENT_SIZE = 4;

        private BaseScanner baseScanner;
        private ushort currIndentationLevel;
        private ushort indentSize = INDENT_SIZE;
        private Token nextToken = null;

        public Scanner()
        {

        }

        public void SetIndentSize(UInt16 newSize)
        {
            indentSize = newSize;
        }

        public void SetText(string text)
        {
            baseScanner = new BaseScanner();
            currIndentationLevel = 0;

            baseScanner.SetText(text);
            SkipWhiteSpaces();
        }

        public Token GetNextToken()
        {
            if (nextToken != null)
            {
                var tmp = nextToken;
                nextToken = null;
                return tmp;
            }

            Token newToken = GetNextNotSpace();
            Token result;

            switch (newToken.Type)
            {
                case TokenType.COLON:
                    result = ReadBlockStart(newToken);
                    break;
                case TokenType.LINE_END:
                    result = CheckIndentation(newToken);
                    break;
                case TokenType.EOF:
                    result = SendBlockClosingTokens(newToken);
                    break;
                default:
                    result = newToken;
                    break;
            }

            return result;
        }

        public SourcePosition GetSourcePosition()
        {
            return baseScanner.GetSourcePosition();
        }

        private Token GetNextNotSpace()
        {
            Token newToken;
            do
            {
                newToken = baseScanner.GetNextToken();
            } while (newToken.Type == TokenType.SPACE);

            return newToken;
        }


        private Token SendBlockClosingTokens(Token currToken)
        {
            if (currIndentationLevel > 0)
            {
                var result = new Token(TokenType.BLOCK_END, currIndentationLevel.ToString());
                LeaveBlock();
                return result;
            }

            return currToken;
        }

        private Token CheckIndentation(Token currToken)
        {
            // 1 empty line - skip 
            // 2 level decreased - if more than 1 level - store in stack and return blockend
            // 3 level wrong level - return error
            // 4 right level - return next token
            
            ushort counter = 0;
            Token t;
            do
            {
                t = baseScanner.GetForwardToken();
                if (t.Type == TokenType.SPACE)
                {
                    baseScanner.GetNextToken();
                    ++counter;
                }
                else if (t.Type == TokenType.LINE_END)
                {
                    baseScanner.GetNextToken();
                    counter = 0;
                }
            } while (t.Type == TokenType.SPACE || t.Type == TokenType.LINE_END);
            
            if (counter == GetSpacesCount())
            {
                nextToken = baseScanner.GetNextToken(); // 4
                return currToken;
            }

            if ((counter < GetSpacesCount()) && ((counter % indentSize) == 0)) // 2
            {
                var result = new Token(TokenType.BLOCK_END, currIndentationLevel.ToString());
                LeaveBlock();
                nextToken = result;
                return currToken;
            }

            return GetErrorToken("Wrong indentation level."); //3
        }

        private Token ReadBlockStart(Token currToken)
        {
            currToken = GetNextNotSpace();
            if (currToken.Type != TokenType.LINE_END)
            {
                return GetErrorToken("Expected line ending");
            }

            EnterBlock();

            try
            {
                ReadIndents();
            }
            catch (IndentationException e)
            {
                return GetErrorToken(e.Message);
            }

            return new Token(TokenType.BLOCK_START, currIndentationLevel.ToString());            
        }

        private void EnterBlock()
        {
            ++currIndentationLevel;
        }

        private void LeaveBlock()
        {
            --currIndentationLevel;
            Debug.Assert(currIndentationLevel >= 0);
        }

        private void ReadIndents()
        {
            for (var i = 0; i < GetSpacesCount(); ++i)
            {
                var t = baseScanner.GetForwardToken();
                if (t.Type == TokenType.SPACE)
                {
                    baseScanner.GetNextToken();
                }
                else if (t.Type == TokenType.LINE_END)
                {
                    baseScanner.GetNextToken();
                    i = 0;
                }
                else if (t.Type != TokenType.EOF)
                {
                    throw new IndentationException("Expected " + GetSpacesCount() + " spaces. Got " + i);
                }
            }
        }
        
        private int GetSpacesCount()
        {
            return indentSize * currIndentationLevel;
        }

        private Token GetErrorToken(string msg)
        {
            return new Token(TokenType.ERROR, "at " + baseScanner.GetCurrentPositionAsString() + ": " + msg);
        }

        private void SkipWhiteSpaces()
        {
            Token t = baseScanner.GetForwardToken();
            while (t.Type == TokenType.SPACE || t.Type == TokenType.LINE_END)
            {
                baseScanner.GetNextToken();
                t = baseScanner.GetForwardToken();
            }
        }
    }
}
