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
        }

        [TestMethod]
        public void TestAssignment()
        {
            Parser p = new Parser();
            var text = @"  
class Program:  
    private int temp  

    public static int Main():   
        int i = 0
        bool b = 1 < 2  
        bool c = false
        temp = i 
        return temp  

";
            var res = p.Parse(text);
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void TestFunctions()
        {
            Parser p = new Parser();
            var text = @"  
class Program:   

    public static int Main():   
        int i = foo(2)
        return i  
    private int foo(int i):
        return i

";
            var res = p.Parse(text);
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void TestMath()
        {
            Parser p = new Parser();
            var text = @"  
class Program:   

    public static int Main():   
        int a = 0
        int i = 1 + a
        return i  
";
            var res = p.Parse(text);
            Assert.IsFalse(res);
        }
    }
}
