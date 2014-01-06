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

           //     Trace.Write("asserting[" + i + "] that " + expectation[i].Type + " equal " + t.Type);
           //     Trace.WriteLine("and " + expectation[i].Attribute + " equal " + t.Attribute);

                Assert.AreEqual(expectation[i].Type, t.Type);
                Assert.AreEqual(expectation[i].Attribute, t.Attribute);
                ++i;
            } while (Token.IsCorrectToken(t));
        }

        [TestMethod]
        public void TestEmptyClass1()
        {
            Scanner s = new Scanner();
            s.SetText(
                @"  
class Program:  
   

"
            );

            var expectation = new List<Token>() {
                new Token(TokenType.CLASS, "class"),
                new Token(TokenType.ID, "Program"),
                new Token(TokenType.BLOCK_START, "1"),
                new Token(TokenType.BLOCK_END, "1"),
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

        [TestMethod]
        public void TestGetNextToken1()
        {
            Scanner s = new Scanner();
            s.SetText(
                @"  
class Program:  
    private int temp  

    public static int Main():   
        if ( true) :
            Console.WriteInt(temp)
            Console.WriteInt(temp)
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
                            new Token(TokenType.ID, "Console"),
                            new Token(TokenType.DOT, "."),
                            new Token(TokenType.ID, "WriteInt"),
                            new Token(TokenType.LEFT_PAREN, "("),
                            new Token(TokenType.ID, "temp"),
                            new Token(TokenType.RIGHT_PAREN, ")"),
                            new Token(TokenType.LINE_END, "\n"),
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

                //     Trace.Write("asserting[" + i + "] that " + expectation[i].Type + " equal " + t.Type);
                //     Trace.WriteLine("and " + expectation[i].Attribute + " equal " + t.Attribute);

                Assert.AreEqual(expectation[i].Type, t.Type);
                Assert.AreEqual(expectation[i].Attribute, t.Attribute);
                ++i;
            } while (Token.IsCorrectToken(t));
        }


        [TestMethod]
        public void TestGetNextToken2()
        {
            Scanner s = new Scanner();
            s.SetText(
                @"  
class Program:  
    private int temp  

    public static int Main():   
        if ( true) :
            Console.WriteInt(temp) 
            Console.WriteInt(temp) 
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
                            new Token(TokenType.ID, "Console"),
                            new Token(TokenType.DOT, "."),
                            new Token(TokenType.ID, "WriteInt"),
                            new Token(TokenType.LEFT_PAREN, "("),
                            new Token(TokenType.ID, "temp"),
                            new Token(TokenType.RIGHT_PAREN, ")"),
                            new Token(TokenType.LINE_END, "\n"),
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

                //     Trace.Write("asserting[" + i + "] that " + expectation[i].Type + " equal " + t.Type);
                //     Trace.WriteLine("and " + expectation[i].Attribute + " equal " + t.Attribute);

                Assert.AreEqual(expectation[i].Type, t.Type);
                Assert.AreEqual(expectation[i].Attribute, t.Attribute);
                ++i;
            } while (Token.IsCorrectToken(t));
        }



        [TestMethod]
        public void TestGetNextTokenUselessLines()
        {
            Scanner s = new Scanner();
            s.SetText(
                @"  
class Program:  


    private int temp  

    public static int Main():   

        if ( true) :


            Console.WriteInt(temp) 
            Console.WriteInt(temp) 


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
                            new Token(TokenType.ID, "Console"),
                            new Token(TokenType.DOT, "."),
                            new Token(TokenType.ID, "WriteInt"),
                            new Token(TokenType.LEFT_PAREN, "("),
                            new Token(TokenType.ID, "temp"),
                            new Token(TokenType.RIGHT_PAREN, ")"),
                            new Token(TokenType.LINE_END, "\n"),
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

                //     Trace.Write("asserting[" + i + "] that " + expectation[i].Type + " equal " + t.Type);
                //     Trace.WriteLine("and " + expectation[i].Attribute + " equal " + t.Attribute);

                Assert.AreEqual(expectation[i].Type, t.Type);
                Assert.AreEqual(expectation[i].Attribute, t.Attribute);
                ++i;
            } while (Token.IsCorrectToken(t));
        }

        [TestMethod]
        public void TestEmptyFile()
        {
            Scanner s = new Scanner();
            s.SetText(
                @"  

"
            );

            var expectation = new List<Token>() {               
                new Token(TokenType.EOF, "\0")
            };

            Token t;
            var i = 0;
            do
            {
                t = s.GetNextToken();

                //     Trace.Write("asserting[" + i + "] that " + expectation[i].Type + " equal " + t.Type);
                //     Trace.WriteLine("and " + expectation[i].Attribute + " equal " + t.Attribute);

                Assert.AreEqual(expectation[i].Type, t.Type);
                Assert.AreEqual(expectation[i].Attribute, t.Attribute);
                ++i;
            } while (Token.IsCorrectToken(t));
        }

        [TestMethod]
        public void TestEmptyExpression()
        {
            Scanner s = new Scanner();
            s.SetText(
                @"  
class Program:  

    public static int Main():   
        if ( true ) : 
        else :
            return 1
        return 0  
"
            );

            var expectation = new List<Token>() {    
                new Token(TokenType.CLASS, "class"),
                new Token(TokenType.ID, "Program"),
                new Token(TokenType.BLOCK_START, "1"),
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
                        new Token(TokenType.ERROR, ""),
                        new Token(TokenType.BLOCK_START, "3"),
                            new Token(TokenType.LINE_END, "\n"),
                        new Token(TokenType.BLOCK_END, "3"),
                       
                        new Token(TokenType.ELSE, "else"),
                        new Token(TokenType.BLOCK_START, "3"),
                            new Token(TokenType.RETURN, "return"),
                            new Token(TokenType.INTEGER_VALUE, "1"),
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

                //     Trace.Write("asserting[" + i + "] that " + expectation[i].Type + " equal " + t.Type);
                //     Trace.WriteLine("and " + expectation[i].Attribute + " equal " + t.Attribute);

                Assert.AreEqual(expectation[i].Type, t.Type);
                if(t.Type != TokenType.ERROR && expectation[i].Type != TokenType.ERROR)
                    Assert.AreEqual(expectation[i].Attribute, t.Attribute);
                ++i;
            } while (Token.IsCorrectToken(t));
        }

        [TestMethod]
        public void TestAssignment()
        {
            Scanner s = new Scanner();
            s.SetText(
                @"  
class Program:  

    public static int Main():   
        int i = 0
        bool b = 1 < 2       
        return 0  
"
            );

            var expectation = new List<Token>() {    
                new Token(TokenType.CLASS, "class"),
                new Token(TokenType.ID, "Program"),
                new Token(TokenType.BLOCK_START, "1"),
                    new Token(TokenType.PUBLIC, "public"),
                    new Token(TokenType.STATIC, "static"),
                    new Token(TokenType.INT, "int"),
                    new Token(TokenType.ID, "Main"),
                    new Token(TokenType.LEFT_PAREN, "("),
                    new Token(TokenType.RIGHT_PAREN, ")"),
                    new Token(TokenType.BLOCK_START, "2"),
                        new Token(TokenType.INT, "int"),
                        new Token(TokenType.ID, "i"),
                        new Token(TokenType.ASSIGNMENT, "="),
                        new Token(TokenType.INTEGER_VALUE, "0"),
                        new Token(TokenType.LINE_END, "\n"),

                        new Token(TokenType.BOOL, "bool"),
                        new Token(TokenType.ID, "b"),
                        new Token(TokenType.ASSIGNMENT, "="),
                        new Token(TokenType.INTEGER_VALUE, "1"),
                        new Token(TokenType.LT, "<"),
                        new Token(TokenType.INTEGER_VALUE, "2"),
                        new Token(TokenType.LINE_END, "\n"),

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

                //     Trace.Write("asserting[" + i + "] that " + expectation[i].Type + " equal " + t.Type);
                //     Trace.WriteLine("and " + expectation[i].Attribute + " equal " + t.Attribute);

                Assert.AreEqual(expectation[i].Type, t.Type);
                Assert.AreEqual(expectation[i].Attribute, t.Attribute);
                ++i;
            } while (Token.IsCorrectToken(t));
        }

        [TestMethod]
        public void TestElseBlock()
        {
            Scanner s = new Scanner();
            s.SetText(
                @"  
class Program:  
    private int temp  

    public static int Main():   
        if (true) :
            return 1
        else:
            if(true):
                return 0
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
                        new Token(TokenType.RETURN, "return"),
                        new Token(TokenType.INTEGER_VALUE, "1"),
                        new Token(TokenType.LINE_END, "\n"),
                        new Token(TokenType.BLOCK_END, "3"),                       
                        new Token(TokenType.ELSE, "else"),
                        new Token(TokenType.BLOCK_START, "3"),
                            new Token(TokenType.IF, "if"),
                            new Token(TokenType.LEFT_PAREN, "("),
                            new Token(TokenType.TRUE, "true"),
                            new Token(TokenType.RIGHT_PAREN, ")"),
                            new Token(TokenType.BLOCK_START, "4"),
                                new Token(TokenType.RETURN, "return"),
                                new Token(TokenType.INTEGER_VALUE, "0"),
                                new Token(TokenType.LINE_END, "\n"),
                                new Token(TokenType.ID, "Console"),
                                new Token(TokenType.DOT, "."),
                                new Token(TokenType.ID, "WriteInt"),
                                new Token(TokenType.LEFT_PAREN, "("),
                                new Token(TokenType.ID, "temp"),
                                new Token(TokenType.RIGHT_PAREN, ")"),
                                new Token(TokenType.LINE_END, "\n"),
                            new Token(TokenType.BLOCK_END, "4"),
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

                 //    Trace.Write("asserting[" + i + "] that " + expectation[i].Type + " equal " + t.Type);
                 //    Trace.WriteLine("and " + expectation[i].Attribute + " equal " + t.Attribute);

                Assert.AreEqual(expectation[i].Type, t.Type);
                if (t.Type != TokenType.ERROR && expectation[i].Type != TokenType.ERROR)
                    Assert.AreEqual(expectation[i].Attribute, t.Attribute);
                ++i;
            } while (Token.IsCorrectToken(t));
        }

        [TestMethod]
        public void TestFunction()
        {
            Scanner s = new Scanner();
            s.SetText(
                @"  
class Program:  

    public static int Main():   
        int i = foo(2)   
        return i

    private static    int foo(int i):
        return i 
"
            );

            var expectation = new List<Token>() {    
                new Token(TokenType.CLASS, "class"),
                new Token(TokenType.ID, "Program"),
                new Token(TokenType.BLOCK_START, "1"),
                    new Token(TokenType.PUBLIC, "public"),
                    new Token(TokenType.STATIC, "static"),
                    new Token(TokenType.INT, "int"),
                    new Token(TokenType.ID, "Main"),
                    new Token(TokenType.LEFT_PAREN, "("),
                    new Token(TokenType.RIGHT_PAREN, ")"),
                    new Token(TokenType.BLOCK_START, "2"),
                        new Token(TokenType.INT, "int"),
                        new Token(TokenType.ID, "i"),
                        new Token(TokenType.ASSIGNMENT, "="),
                        new Token(TokenType.ID, "foo"),
                        new Token(TokenType.LEFT_PAREN, "("),
                        new Token(TokenType.INTEGER_VALUE, "2"),
                        new Token(TokenType.RIGHT_PAREN, ")"),
                        new Token(TokenType.LINE_END, "\n"),

                        new Token(TokenType.RETURN, "return"),
                        new Token(TokenType.ID, "i"),
                        new Token(TokenType.LINE_END, "\n"),
                    new Token(TokenType.BLOCK_END, "2"),

                    new Token(TokenType.PRIVATE, "private"),
                    new Token(TokenType.STATIC, "static"),
                    new Token(TokenType.INT, "int"),
                    new Token(TokenType.ID, "foo"),
                    new Token(TokenType.LEFT_PAREN, "("),
                    new Token(TokenType.INT, "int"),
                    new Token(TokenType.ID, "i"),
                    new Token(TokenType.RIGHT_PAREN, ")"),
                    new Token(TokenType.BLOCK_START, "2"),
                        new Token(TokenType.RETURN, "return"),
                        new Token(TokenType.ID, "i"),
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

                //     Trace.Write("asserting[" + i + "] that " + expectation[i].Type + " equal " + t.Type);
                //     Trace.WriteLine("and " + expectation[i].Attribute + " equal " + t.Attribute);

                Assert.AreEqual(expectation[i].Type, t.Type);
                Assert.AreEqual(expectation[i].Attribute, t.Attribute);
                ++i;
            } while (Token.IsCorrectToken(t));
        }


        [TestMethod]
        public void TestFewFields()
        {
            Scanner s = new Scanner();
            s.SetText(
                @"  
class Program:   
    private int a
    private int i
    public static int Main():   
        a = 0
        i = 1 + a
        return i  
"
            );

            var expectation = new List<Token>() {    
                new Token(TokenType.CLASS, "class"),
                new Token(TokenType.ID, "Program"),
                new Token(TokenType.BLOCK_START, "1"),
                    new Token(TokenType.PRIVATE, "private"),
                    new Token(TokenType.INT, "int"),
                    new Token(TokenType.ID, "a"),
                    new Token(TokenType.LINE_END, "\n"),
                    new Token(TokenType.PRIVATE, "private"),
                    new Token(TokenType.INT, "int"),
                    new Token(TokenType.ID, "i"),
                    new Token(TokenType.LINE_END, "\n"),
                    new Token(TokenType.PUBLIC, "public"),
                    new Token(TokenType.STATIC, "static"),
                    new Token(TokenType.INT, "int"),
                    new Token(TokenType.ID, "Main"),
                    new Token(TokenType.LEFT_PAREN, "("),
                    new Token(TokenType.RIGHT_PAREN, ")"),
                    new Token(TokenType.BLOCK_START, "2"),
                        new Token(TokenType.ID, "a"),
                        new Token(TokenType.ASSIGNMENT, "="),
                        new Token(TokenType.INTEGER_VALUE, "0"),
                        new Token(TokenType.LINE_END, "\n"),
                        new Token(TokenType.ID, "i"),
                        new Token(TokenType.ASSIGNMENT, "="),
                        new Token(TokenType.INTEGER_VALUE, "1"),
                        new Token(TokenType.PLUS, "+"),
                        new Token(TokenType.ID, "a"),
                        new Token(TokenType.LINE_END, "\n"),

                        new Token(TokenType.RETURN, "return"),
                        new Token(TokenType.ID, "i"),
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

                //     Trace.Write("asserting[" + i + "] that " + expectation[i].Type + " equal " + t.Type);
                //     Trace.WriteLine("and " + expectation[i].Attribute + " equal " + t.Attribute);

                Assert.AreEqual(expectation[i].Type, t.Type);
                Assert.AreEqual(expectation[i].Attribute, t.Attribute);
                ++i;
            } while (Token.IsCorrectToken(t));
        }
    }
}
