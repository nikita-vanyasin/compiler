using System;
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
        public void TestInvalidAssignment()
        {
            Parser p = new Parser();
            var text = @"  
class Program:  
    private bool b
    public static int Main():   
        b = 1 < 2  
        return 0  

";

            // there is no comparision operations support
            var res = p.Parse(text);
            Assert.IsFalse(res);
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
                            return c";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var testVisitor = new TestAstValidVisitor();
            res = testVisitor.TestTree(p.GetRootNode());
            Assert.IsTrue(res);
        }
    }
}
