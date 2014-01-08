using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using compiler;

namespace Test
{
    [TestClass]
    public class CodePathCheckerTest
    {
        [TestMethod]
        public void TestReachable()
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

            var eval = new TypeEvaluator();
            res = eval.Evaluate(p.GetRootNode());
            Assert.IsTrue(res);

            var checker = new CodePathChecker();
            res = checker.Check(p.GetRootNode());
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestUnreachable()
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
        return temp  
        temp = i 


";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var eval = new TypeEvaluator();
            res = eval.Evaluate(p.GetRootNode());
            Assert.IsTrue(res);

            var checker = new CodePathChecker();
            res = checker.Check(p.GetRootNode());
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void TestUnreachableThen()
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
        if (c):
            return i
            c = true
        else:
            i = 666 + (999  - 89)
        return temp


";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var eval = new TypeEvaluator();
            res = eval.Evaluate(p.GetRootNode());
            Assert.IsTrue(res);

            var checker = new CodePathChecker();
            res = checker.Check(p.GetRootNode());
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void TestReachableThen()
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
        if (c):
            c = true
            return i
        else:
            i = 666 + (999  - 89)
        return temp


";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var eval = new TypeEvaluator();
            res = eval.Evaluate(p.GetRootNode());
            Assert.IsTrue(res);

            var checker = new CodePathChecker();
            res = checker.Check(p.GetRootNode());
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestAllCodePathsReturnValue()
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
        if (c):
            c = true
            return i
        else:
            i = 666 + (999  - 89)
        return temp
";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var eval = new TypeEvaluator();
            res = eval.Evaluate(p.GetRootNode());
            Assert.IsTrue(res);

            var checker = new CodePathChecker();
            res = checker.Check(p.GetRootNode());
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestAllCodePathsReturnValueBad()
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
        if (c):
            c = true
            return i
        else:
            i = 666 + (999  - 89)
";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var eval = new TypeEvaluator();
            res = eval.Evaluate(p.GetRootNode());
            Assert.IsTrue(res);

            var checker = new CodePathChecker();
            res = checker.Check(p.GetRootNode());
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void TestAllCodePathsReturnValueIfElse()
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
        if (c):
            c = true
            return i
        else:
            i = 666 + (999  - 89)
            return temp
";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var eval = new TypeEvaluator();
            res = eval.Evaluate(p.GetRootNode());
            Assert.IsTrue(res);

            var checker = new CodePathChecker();
            res = checker.Check(p.GetRootNode());
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestAllCodePathsReturnValueIfElseBad()
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
        if (c):
            c = true
        else:
            i = 666 + (999  - 89)
            return temp
";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var eval = new TypeEvaluator();
            res = eval.Evaluate(p.GetRootNode());
            Assert.IsTrue(res);

            var checker = new CodePathChecker();
            res = checker.Check(p.GetRootNode());
            Assert.IsFalse(res);
        }
    }
}
