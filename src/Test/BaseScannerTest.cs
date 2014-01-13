using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using compiler;

namespace Test
{
    [TestClass]
    public class BaseScannerTest
    {
        [TestMethod]
        public void TestGetNextToken()
        {
            BaseScanner s = new BaseScanner();
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
                new Token(TokenType.SPACE, " "),
                new Token(TokenType.SPACE, " "),
                new Token(TokenType.LINE_END, "\n"),
                new Token(TokenType.CLASS, "class"),
                new Token(TokenType.SPACE, " "),
                new Token(TokenType.ID, "Program"),
                new Token(TokenType.COLON, ":"),
                new Token(TokenType.SPACE, " "),
                new Token(TokenType.SPACE, " "),
                new Token(TokenType.LINE_END, "\n"),
                    new Token(TokenType.SPACE, " "),
                    new Token(TokenType.SPACE, " "),
                    new Token(TokenType.SPACE, " "),
                    new Token(TokenType.SPACE, " "),
                    new Token(TokenType.PRIVATE, "private"),
                    new Token(TokenType.SPACE, " "),
                    new Token(TokenType.INT, "int"),
                    new Token(TokenType.SPACE, " "),
                    new Token(TokenType.ID, "temp"),
                    new Token(TokenType.SPACE, " "),
                    new Token(TokenType.SPACE, " "),
                    new Token(TokenType.LINE_END, "\n"),
                    new Token(TokenType.LINE_END, "\n"),
                    new Token(TokenType.SPACE, " "),
                    new Token(TokenType.SPACE, " "),
                    new Token(TokenType.SPACE, " "),
                    new Token(TokenType.SPACE, " "),
                    new Token(TokenType.PUBLIC, "public"),
                    new Token(TokenType.SPACE, " "),
                    new Token(TokenType.STATIC, "static"),
                    new Token(TokenType.SPACE, " "),
                    new Token(TokenType.INT, "int"),
                    new Token(TokenType.SPACE, " "),
                    new Token(TokenType.ID, "Main"),
                    new Token(TokenType.LEFT_PAREN, "("),
                    new Token(TokenType.RIGHT_PAREN, ")"),
                    new Token(TokenType.COLON, ":"),
                    new Token(TokenType.SPACE, " "),
                    new Token(TokenType.SPACE, " "),
                    new Token(TokenType.SPACE, " "),
                    new Token(TokenType.LINE_END, "\n"),
                        new Token(TokenType.SPACE, " "),
                        new Token(TokenType.SPACE, " "),
                        new Token(TokenType.SPACE, " "),
                        new Token(TokenType.SPACE, " "),
                        new Token(TokenType.SPACE, " "),
                        new Token(TokenType.SPACE, " "),
                        new Token(TokenType.SPACE, " "),
                        new Token(TokenType.SPACE, " "),
                        new Token(TokenType.IF, "if"),
                        new Token(TokenType.SPACE, " "),
                        new Token(TokenType.LEFT_PAREN, "("),
                        new Token(TokenType.SPACE, " "),
                        new Token(TokenType.TRUE, "true"),
                        new Token(TokenType.RIGHT_PAREN, ")"),
                        new Token(TokenType.SPACE, " "),
                        new Token(TokenType.COLON, ":"),
                        new Token(TokenType.LINE_END, "\n"),
                            new Token(TokenType.SPACE, " "),
                            new Token(TokenType.SPACE, " "),
                            new Token(TokenType.SPACE, " "),
                            new Token(TokenType.SPACE, " "),
                            new Token(TokenType.SPACE, " "),
                            new Token(TokenType.SPACE, " "),
                            new Token(TokenType.SPACE, " "),
                            new Token(TokenType.SPACE, " "),
                            new Token(TokenType.SPACE, " "),
                            new Token(TokenType.SPACE, " "),
                            new Token(TokenType.SPACE, " "),
                            new Token(TokenType.SPACE, " "),
                            new Token(TokenType.ID, "Console"),
                            new Token(TokenType.DOT, "."),
                            new Token(TokenType.ID, "WriteInt"),
                            new Token(TokenType.LEFT_PAREN, "("),
                            new Token(TokenType.ID, "temp"),
                            new Token(TokenType.RIGHT_PAREN, ")"),
                            new Token(TokenType.SPACE, " "),
                            new Token(TokenType.SPACE, " "),
                            new Token(TokenType.SPACE, " "),
                            new Token(TokenType.LINE_END, "\n"),
                            new Token(TokenType.SPACE, " "),
                            new Token(TokenType.SPACE, " "),
                            new Token(TokenType.SPACE, " "),
                            new Token(TokenType.SPACE, " "),
                            new Token(TokenType.SPACE, " "),
                            new Token(TokenType.SPACE, " "),
                            new Token(TokenType.SPACE, " "),
                            new Token(TokenType.SPACE, " "),
                        new Token(TokenType.RETURN, "return"),
                        new Token(TokenType.SPACE, " "),
                        new Token(TokenType.INTEGER_VALUE, "0"),
                        new Token(TokenType.SPACE, " "),
                        new Token(TokenType.SPACE, " "),
                        new Token(TokenType.LINE_END, "\n"),
                        new Token(TokenType.LINE_END, "\n"),
                new Token(TokenType.EOF, "\0")
            };

            Token t;
            var i = 0;
            do
            {
                t = s.GetNextToken();
                
                Assert.AreEqual(expectation[i].Type, t.Type);
                Assert.AreEqual(expectation[i].Attribute, t.Attribute);
                ++i;
            } while (Token.IsCorrectToken(t));
        }
    }
}
