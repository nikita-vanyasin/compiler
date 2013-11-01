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
        }

        public Token GetNextToken()
        {
            Token newToken = GetNextNotSpace();

            switch (newToken.Type)
            {
                case TokenType.COLON:
                    return ReadBlockStart(newToken);
                case TokenType.LINE_END:
                    return CheckIndentation(newToken);
                case TokenType.EOF:
                    return SendBlockClosingTokens(newToken);
                default:
                    return newToken;
            }
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

        private Token GetNextNotSpace()
        {
            Token newToken;
            do
            {
                newToken = baseScanner.GetNextToken();
            } while (newToken.Type == TokenType.SPACE);

            return newToken;
        }

        private Token CheckIndentation(Token currToken)
        {
            // 1 empty line - skip/ read and return line end
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
            } while (t.Type == TokenType.SPACE);

            if (t.Type == TokenType.LINE_END)
            {
                return baseScanner.GetNextToken();
/*
                baseScanner.GetNextToken();
                return CheckIndentation(t);*/ // skipping whitespaced line 1
            }

            if (counter == GetSpacesCount())
            {
                return baseScanner.GetNextToken(); // 4
            }

            if ((counter < GetSpacesCount()) && ((counter % indentSize) == 0)) // 2
            {
                counter -= indentSize;
                /*
                // store to buffer block ends, that will be pushed away in GetNextToken()
                if (counter > 0)
                {

                }*/

                var result = new Token(TokenType.BLOCK_END, currIndentationLevel.ToString());

                LeaveBlock();

                return result;
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
                else
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
    }
}
