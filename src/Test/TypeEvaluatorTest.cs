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
        a = foo(foo(foo(foo(1))))
        return a
    private int foo(int i):
        return i + 1

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


        [TestMethod]
        public void TestFunctionsScope()
        {
            Parser p = new Parser();
            var text = @"  
class Program:   
    private int a
    public static int Main():   
        a = foo(2)
        return i
    private int foo(int i):
        return i

";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var checker = new TypeEvaluator();
            res = checker.Evaluate(p.GetRootNode());
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void TestDevisionByZero1()
        {
            Parser p = new Parser();
            var text = @"  
class Program:   
    private bool a
    private int i
    public static int Main():   
        a = false
        i = 1 div 2
        i = (45 * 6 div 569 + i) mod (i - 5)
        return i  
";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var checker = new TypeEvaluator();
            res = checker.Evaluate(p.GetRootNode());
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestDevisionByZero2()
        {
            Parser p = new Parser();
            var text = @"  
class Program:   
    private bool a
    private int i
    public static int Main():   
        a = false
        i = 1 div 2
        i = (45 * 6 div 0 + i) mod (i - 5)
        return i  
";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var checker = new TypeEvaluator();
            res = checker.Evaluate(p.GetRootNode());
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void TestComparisonWithBoolBad()
        {
            Parser p = new Parser();
            var text = @"  
class Program:   
    private bool a
    private int i
    public static int Main():   
        if (5 < Foo()):
            Console.WriteInt(1)
        return i  
    private static bool Foo():
        return false
";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var checker = new TypeEvaluator();
            res = checker.Evaluate(p.GetRootNode());
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void TestWhileCond()
        {
            Parser p = new Parser();
            var text = @"  
class Program:  
    private int temp  
    private int i
    private bool c
    public static int Main():   
        while (i > c):
            Console.WriteInt(i)
            i = i - 1
        return temp  

";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var checker = new TypeEvaluator();
            res = checker.Evaluate(p.GetRootNode());
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void TestArrDeclaration()
        {
            Parser p = new Parser();
            var text = @"  
class Program:  
    private int[100] temp  
    private int[1] i
    private bool c
    public static int Main():   
        Console.WriteInt(1)
        return 0  

";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var checker = new TypeEvaluator();
            res = checker.Evaluate(p.GetRootNode());
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestArrDeclarationBadIndex()
        {
            Parser p = new Parser();
            var text = @"  
class Program:  
    private int[100] temp  
    private int[0] i
    private bool c
    public static int Main():   
        Console.WriteInt(i)
        return 0  

";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var checker = new TypeEvaluator();
            res = checker.Evaluate(p.GetRootNode());
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void TestArrUse()
        {
            Parser p = new Parser();
            var text = @"  
class Program:  
    private int[100] temp  
    private int a
    private bool c
    public static int Main():   
        temp[2] = 10
        temp[0] = a
        temp[4] = a *6
        temp[5] = temp[0]
        temp[5] = temp[0] - 1
        foo(temp[3])
        return a  
    public static int foo(int a):
        Console.WriteInt(a)
        return 0

";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var checker = new TypeEvaluator();
            res = checker.Evaluate(p.GetRootNode());
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestArrUseBadIndexOverflow()
        {
            Parser p = new Parser();
            var text = @"  
class Program:  
    private int[100] temp  
    private int a
    private bool c
    public static int Main():   
        temp[100] = 10
        temp[3] = a
        temp[4] = a *6
        temp[5] = temp[2]
        temp[5] = temp[1] - 1
        return 0  

";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var checker = new TypeEvaluator();
            res = checker.Evaluate(p.GetRootNode());
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void TestArrUseBadIndexUnderflow()
        {
            Parser p = new Parser();
            var text = @"  
class Program:  
    private int[100] temp  
    private int a
    private bool c
    public static int Main():   
        temp[-1] = 10
        temp[3] = a
        temp[4] = a *6
        temp[5] = temp[0]
        temp[5] = temp[0] - 1
        return 0  

";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var checker = new TypeEvaluator();
            res = checker.Evaluate(p.GetRootNode());
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void TestArrUseBadWrite()
        {
            Parser p = new Parser();
            var text = @"  
class Program:  
    private int[100] temp  
    private int a
    private bool c
    public static int Main():   
        temp[10] = c
        temp[3] = a
        temp[4] = a *6
        temp[5] = temp[0]
        temp[5] = temp[0] - 1
        return 0  

";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var checker = new TypeEvaluator();
            res = checker.Evaluate(p.GetRootNode());
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void TestArrUseBadRead()
        {
            Parser p = new Parser();
            var text = @"  
class Program:  
    private int[100] temp  
    private int a
    private bool c
    public static int Main():   
        temp[10] = a
        temp[3] = a
        temp[4] = a *6
        c = temp[0]
        temp[5] = temp[0] - 1
        return a  

";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var checker = new TypeEvaluator();
            res = checker.Evaluate(p.GetRootNode());
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void TestArrUseExpr()
        {
            Parser p = new Parser();
            var text = @"  
class Program:  
    private int[100] temp  
    private int a
    private bool c
    public static int Main():   
        temp[10 + a] = a
        return a  

";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var checker = new TypeEvaluator();
            res = checker.Evaluate(p.GetRootNode());
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestArrBadUseExpr()
        {
            Parser p = new Parser();
            var text = @"  
class Program:  
    private int[100] temp  
    private int a
    private bool c
    public static int Main():   
        temp[c] = a
        return a  

";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var checker = new TypeEvaluator();
            res = checker.Evaluate(p.GetRootNode());
            Assert.IsFalse(res);
        }


        [TestMethod]
        public void TestArrBadUsage()
        {
            Parser p = new Parser();
            var text = @"  
class Program:  
    private int[100] temp  
    private int a
    private bool c
    public static int Main():   
        a = temp * 3
        return a  

";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var checker = new TypeEvaluator();
            res = checker.Evaluate(p.GetRootNode());
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void TestArrBadReturn()
        {
            Parser p = new Parser();
            var text = @"  
class Program:  
    private int[100] temp  
    private int a
    private bool c
    public static int Main():   
        temp[c] = a
        return temp  

";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var checker = new TypeEvaluator();
            res = checker.Evaluate(p.GetRootNode());
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void TestArrDeclareTooBig()
        {
            Parser p = new Parser();
            var text = @"  
class Program:  
    private int[1909090909090909009000] temp  
    private int a
    private bool c
    public static int Main():   
        temp[0] = a
        return 1  

";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var checker = new TypeEvaluator();
            res = checker.Evaluate(p.GetRootNode());
            Assert.IsFalse(res);
        }


        [TestMethod]
        public void TestDivideBig()
        {
            Parser p = new Parser();
            var text = @"  
class Program:  
    private int[19] temp  
    private int a
    private bool c
    public static int Main():   
        temp[0] = 32 mod 8908098800099080980
        return 1  

";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var checker = new TypeEvaluator();
            res = checker.Evaluate(p.GetRootNode());
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void TestArrFuncArgBad()
        {
            Parser p = new Parser();
            var text = @"  
class Program:  
    private int[100] temp  
    private int a
    private bool c
    public static int Main(int[20] d):   
        temp[3] = a
        temp[4] = a *6
        temp[5] = temp[0]
        temp[5] = temp[0] - 1
        return 0  


";
            var res = p.Parse(text);
            Assert.IsTrue(res);

            var checker = new TypeEvaluator();
            res = checker.Evaluate(p.GetRootNode());
            Assert.IsFalse(res);
        }
    }
}
