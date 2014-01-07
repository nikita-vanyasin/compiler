using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using compiler;
using System.Collections;
using System.Collections.Generic;

namespace Test
{
    [TestClass]
    public class CallableSymbolTest
    {
        [TestMethod]
        public void TestTypesToString()
        {
            var list = new List<string>() { 
                "1", "2", "3"
            };
            var s = new CallableSymbol("", "test", "int", list);

            Assert.AreEqual("1, 2, 3", s.TypesToString());
        }
    }
}
