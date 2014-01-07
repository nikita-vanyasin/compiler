using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    public class SymbolTable
    {
        private Dictionary<string, Symbol> table;
        private Dictionary<string, CallableSymbol> functionsTable;

        public int NestingLevel { get; protected set; }

        public SymbolTable(int nestingLevel)
        {
            NestingLevel = nestingLevel;
            table = new Dictionary<string, Symbol>();
            functionsTable = new Dictionary<string, CallableSymbol>();
        }

        public void EnterFunction(string target, string name, string type, List<string> callArgTypes)
        {
            var s = new CallableSymbol(target, name, type, callArgTypes);
            functionsTable[target + name] = s;
        }

        public void EnterSymbol(string name, string type = "", int size = -1)
        {
            Symbol s = new Symbol(name, type, size);
            table[name] = s;
        }

        public CallableSymbol LookupFunction(string key)
        {
            try
            {
                CallableSymbol s = functionsTable[key];
                return s;
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        public Symbol Lookup(string name)
        {
            try
            {
                Symbol s = table[name];
                return s;
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }
    }
}
