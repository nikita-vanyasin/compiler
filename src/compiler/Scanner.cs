using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    public class Scanner
    {
        private enum ScannerState
        {
            NORMAL,
            WAIT_BLOCK_START,
            WAIT_BLOCK_END
        }

        const UInt16 INDENT_SIZE = 4;

        private BaseScanner baseScanner;
        private ScannerState currentState;
        private UInt16 currIndentationLevel;
        private UInt16 indentSize = INDENT_SIZE;

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
            currentState = ScannerState.NORMAL;

            baseScanner.SetText(text);
        }

        public Token GetNextToken()
        {
            Token newToken = GetNextNotSpace();

            // if state is wait for block_start
            if (newToken.Type == TokenType.COLON)
            {
                return ReadBlockStart(/* pass new token here*/);
            }
            // else if state is wait for block end
            //         if token is line end - check indention
            //             return block end if indent level уменьшен либо error если увеличен или не совпадает
            //             switch state

            // store state in stack!!!
            
            return newToken;
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

        private Token ReadBlockStart()
        {
            Token newToken = GetNextNotSpace();
            if (newToken.Type != TokenType.LINE_END)
            {
                return new Token(TokenType.ERROR, "at " + baseScanner.GetCurrentPositionAsString() + ": Expected line ending");
            }

            // read (currIndentionLevel + 1) * indentSize
            // return block start, or error
            // switch state
            
        }
    }
}
