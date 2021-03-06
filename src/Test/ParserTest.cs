﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using compiler;

namespace Test
{
    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        public void TestMustWorkTest()
        {
            Parser p = new Parser();
            var text = @"  
class Program:  
    private int temp  

    public static int Main():   
        if ( true) :
            Console.WriteInt(temp)   
        return 0  

";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var testVisitor = new TestAstValidVisitor();
            res = testVisitor.TestTree(p.GetRootNode());
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestString()
        {
            Parser p = new Parser();
            var text = @"  
class Program:  
    private string s

    public static int Main():   
        s = ""hello, world!"" 
        return 0  

";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var testVisitor = new TestAstValidVisitor();
            res = testVisitor.TestTree(p.GetRootNode());
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestStringFunc()
        {
            Parser p = new Parser();
            var text = @"  
class Program:  
    private string s

    public static int Main(string arg):   
        Main(""hello, world!"")
        return 0  

";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var testVisitor = new TestAstValidVisitor();
            res = testVisitor.TestTree(p.GetRootNode());
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestEmptyStatement()
        {
            Parser p = new Parser();
            var text = @"  
class Program:  

    public static int Main():   
        if ( true) :           
        return 0  

";
            var res = p.Parse(text);
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void TestInsertedStatement()
        {
            Parser p = new Parser();
            var text = @"  
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

";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var testVisitor = new TestAstValidVisitor();
            res = testVisitor.TestTree(p.GetRootNode());
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestAssignment()
        {
            Parser p = new Parser();
            var text = @"  
class Program:  
    private int temp  
    private int i
    private bool c
    public static int Main():   
        i = 0
        c = false
        temp = i 
        return temp  

";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var testVisitor = new TestAstValidVisitor();
            res = testVisitor.TestTree(p.GetRootNode());
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestValidAssignment()
        {
            Parser p = new Parser();
            var text = @"  
class Program:  
    private bool b
    public static int Main():   
        b = 1 < 2  
        return 0  

";

            var res = p.Parse(text);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestFunctions()
        {
            Parser p = new Parser();
            var text = @"  
class Program:   
    private int a
    public static int Main():   
        a = foo(2)
        return a
    private int foo(int i):
        return i

";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var testVisitor = new TestAstValidVisitor();
            res = testVisitor.TestTree(p.GetRootNode());
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestMath()
        {
            Parser p = new Parser();
            var text = @"  
class Program:   
    private int a
    private int i
    public static int Main():   
        a = 0
        i = 1 + a
        return i  
";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var testVisitor = new TestAstValidVisitor();
            res = testVisitor.TestTree(p.GetRootNode());
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestConditions()
        {
            Parser p = new Parser();
            var text = @"  
class Program:   
    private int a
    private int i
    public static int Main():   
        if (true && false):
            if (false && !true && true):
                if (false && !true && true):
                    if(!true && true || false ):
                        if(!true && Console.ReadBool() || false):
                            return c
";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var testVisitor = new TestAstValidVisitor();
            res = testVisitor.TestTree(p.GetRootNode());
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestWhileNodes()
        {
            Parser p = new Parser();
            var text = @"  
class Program:  
    private int temp  
    private bool test

    public static int Main():   
        while (test):
            Console.WriteInt(10)
        return 0  

";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var testVisitor = new TestAstValidVisitor();
            res = testVisitor.TestTree(p.GetRootNode());
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestComparison()
        {
            Parser p = new Parser();
            var text = @"  
class Program:  
    private int temp  
    private bool test

    public static int Main():   
        while (temp < 5):
            Console.WriteInt(10)
        return 0  

";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var testVisitor = new TestAstValidVisitor();
            res = testVisitor.TestTree(p.GetRootNode());
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestComparisonEqual()
        {
            Parser p = new Parser();
            var text = @"  
class Program:  
    private int temp  
    private bool test

    public static int Main():   
        while (temp == 0):
            Console.WriteInt(10)
        return 0  

";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var testVisitor = new TestAstValidVisitor();
            res = testVisitor.TestTree(p.GetRootNode());
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestComparisonNotEqual()
        {
            Parser p = new Parser();
            var text = @"  
class Program:  
    private int temp  
    private bool test

    public static int Main():   
        while (temp != 0):
            Console.WriteInt(10)
        return 0  

";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var testVisitor = new TestAstValidVisitor();
            res = testVisitor.TestTree(p.GetRootNode());
            Assert.IsTrue(res);
        }


        [TestMethod]
        public void TestArrays()
        {
            Parser p = new Parser();
            var text = @"  
class Program:   
    private int[10] a
    private int i
    public static int Main():   
        foo(a[3])
        i = a[2] 

";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var testVisitor = new TestAstValidVisitor();
            res = testVisitor.TestTree(p.GetRootNode());
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestArrayInit()
        {
            Parser p = new Parser();
            var text = @"  
class Program:   
    private int[10] a
    private int i
    public static int Main():   
        a = {21, 21, 21, 21, 2}

";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var testVisitor = new TestAstValidVisitor();
            res = testVisitor.TestTree(p.GetRootNode());
            Assert.IsTrue(res);
        }

		[TestMethod]
		public void TestMultiDimArray()
		{
			Parser p = new Parser();
			var text = @"  
class Program:   
    private int[10, 10, 10] a
    private int i
    public static int Main():   
        a = {21, 21, 21, 21, 2}

";
			var res = p.Parse(text);
			Assert.IsTrue(res);

			var testVisitor = new TestAstValidVisitor();
			res = testVisitor.TestTree(p.GetRootNode());
			Assert.IsTrue(res);
		}

		[TestMethod]
		public void TestMulDimArray_wrongIndexNumber()
		{
			Parser p = new Parser();
			var text = @"  
class Program:   
    private int[10, 10, 10] a
    private int i
    public static int Main():   
        a[1, 2] = 3
		i = a[1]

";
			var res = p.Parse(text);
			Assert.IsFalse(res);
		}

		[TestMethod]
		public void TestMulDimArray_indicesOutOfRange()
		{
			Parser p = new Parser();
			var text = @"  
class Program:   
    private int[10, 10, 10] a
    private int i
    public static int Main():   
        a[1, 2, -1] = 3
		i = a[12, 21, 3]

";
			var res = p.Parse(text);
			Assert.IsFalse(res);

		}
    }
}
