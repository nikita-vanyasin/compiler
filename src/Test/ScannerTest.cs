using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using compiler;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Test
{
    [TestClass]
    public class ScannerTest
    {
        [TestMethod]
        public void TestGetNextToken()
        {
            Scanner s = new Scanner();
            s.SetText(
                @"  
class Program:  
    private int temp  

    public static int Main():   
        if ( true) :
            Console.WriteInt(temp)   
        return 0  

"
            );

            var expectation = new List<Token>() {
                new Token(TokenType.CLASS, "class"),
                new Token(TokenType.ID, "Program"),
                new Token(TokenType.BLOCK_START, "1"),
                    new Token(TokenType.PRIVATE, "private"),
                    new Token(TokenType.INT, "int"),
                    new Token(TokenType.ID, "temp"),
                    new Token(TokenType.LINE_END, "\n"),
                    new Token(TokenType.LINE_END, "\n"),
                    new Token(TokenType.PUBLIC, "public"),
                    new Token(TokenType.STATIC, "static"),
                    new Token(TokenType.INT, "int"),
                    new Token(TokenType.ID, "Main"),
                    new Token(TokenType.LEFT_PAREN, "("),
                    new Token(TokenType.RIGHT_PAREN, ")"),
                    new Token(TokenType.BLOCK_START, "2"),
                        new Token(TokenType.IF, "if"),
                        new Token(TokenType.LEFT_PAREN, "("),
                        new Token(TokenType.TRUE, "true"),
                        new Token(TokenType.RIGHT_PAREN, ")"),
                        new Token(TokenType.BLOCK_START, "3"),
                            new Token(TokenType.ID, "Console"),
                            new Token(TokenType.DOT, "."),
                            new Token(TokenType.ID, "WriteInt"),
                            new Token(TokenType.LEFT_PAREN, "("),
                            new Token(TokenType.ID, "temp"),
                            new Token(TokenType.RIGHT_PAREN, ")"),
                            new Token(TokenType.LINE_END, "\n"),
                        new Token(TokenType.BLOCK_END, "3"),
                        new Token(TokenType.RETURN, "return"),
                        new Token(TokenType.INTEGER_VALUE, "0"),
                        new Token(TokenType.LINE_END, "\n"),
                    new Token(TokenType.BLOCK_END, "2"),
                new Token(TokenType.BLOCK_END, "1"),
                new Token(TokenType.EOF, "\0")
            };

            Token t;
            var i = 0;
            do
            {
                t = s.GetNextToken();

                if (i == 29)
                {
                    Trace.Write("bug!");
                }

                Trace.Write("asserting[" + i + "] that " + expectation[i].Type + " equal " + t.Type);
                Trace.WriteLine("and " + expectation[i].Attribute + " equal " + t.Attribute);

                Assert.AreEqual(expectation[i].Type, t.Type);
                Assert.AreEqual(expectation[i].Attribute, t.Attribute);
                ++i;
            } while (Token.IsCorrectToken(t));
        }
    }
}
