using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    public class SymbolTable
    {
        private SymbolsScope globalScope;
        private SymbolsScope currScope;
        
        public SymbolTable()
        {
            globalScope = new SymbolsScope();
            UseGlobalScope();
        }

        public void UseGlobalScope()
        {
            currScope = globalScope;
        }

        public void UseNamedChildScope(string id)
        {
            try
            {
                currScope = currScope.Childs[id];
            }
            catch (KeyNotFoundException)
            {
                currScope = currScope.AddChild(id);
            }
        }

        public void UseChildScope()
        {
            if (currScope.Childs.Count == 0)
            {
                currScope = currScope.AddChild();
            }
            else
            {
                currScope = currScope.Childs.First().Value;
            }
        }

        public void UseParentScope()
        {
            if (currScope.Parent == null)
            {
                throw new InvalidOperationException("parent scope does not exist");
            }

            currScope = currScope.Parent;
        }

        public void EnterFunction(string target, string name, string type, List<string> callArgTypes)
        {
            currScope.EnterFunction(target, name, type, callArgTypes);
        }

        public void EnterSymbol(string name, string type = "", int size = -1)
        {
            currScope.EnterSymbol(name, type, size);
        }

        public CallableSymbol LookupFunction(string key)
        {
            return currScope.LookupFunction(key);
        }

        public Symbol Lookup(string name)
        {
            return currScope.Lookup(name);
        }

        public Symbol LookupParent(string name)
        {
            return currScope.Parent.Lookup(name);
        }
    }
}
