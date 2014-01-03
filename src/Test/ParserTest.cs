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
    }
}
