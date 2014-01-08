using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using compiler;

namespace Test
{
    [TestClass]
    public class TypeEvaluatorTest
    {
        [TestMethod]
        public void TestTrueTypes()
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

            var checker = new TypeEvaluator();
            res = checker.Evaluate(p.GetRootNode());
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

            var checker = new TypeEvaluator();
            res = checker.Evaluate(p.GetRootNode());
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
                        return c
";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var checker = new TypeEvaluator();
            res = checker.Evaluate(p.GetRootNode());
            Assert.IsFalse(res); // c - unknown
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

            var checker = new TypeEvaluator();
            res = checker.Evaluate(p.GetRootNode());
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestExprBad()
        {
            Parser p = new Parser();
            var text = @"  
class Program:   
    private bool a
    private int i
    public static int Main():   
        a = false
        i = 1 + a
        return i  
";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var checker = new TypeEvaluator();
            res = checker.Evaluate(p.GetRootNode());
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void TestConditionsWithInt()
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
                    if(!true && i || false ):
                        return a
";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var checker = new TypeEvaluator();
            res = checker.Evaluate(p.GetRootNode());
            Assert.IsFalse(res); 
        }
    }
}
