using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    public class SymbolsScope
    {
        public Dictionary<string, SymbolsScope> Childs { get; set; }
        public SymbolsScope Parent { get; set; }
        public SymbolsScope Next { get; set; }

        private Dictionary<string, Symbol> table;
        private Dictionary<string, CallableSymbol> functionsTable;

        public SymbolsScope()
        {
            table = new Dictionary<string, Symbol>();
            functionsTable = new Dictionary<string, CallableSymbol>();

            Parent = null;
            Childs = new Dictionary<string, SymbolsScope>();
            Next = null;
        }

        public SymbolsScope AddChild(string key = "")
        {
            var newScope = new SymbolsScope();
            newScope.Parent = this;

            if (this.Childs.Count == 0)
            {
                if (key == "")
                {
                    key = "0";
                }
                this.Childs.Add(key, newScope);
                return newScope;
            }

            var lastChild = this.Childs.Last();
            lastChild.Value.Next = newScope;

            if (key == "")
            {
                key = (Convert.ToInt16(lastChild.Key) + 1).ToString();
            }
            this.Childs.Add(key, newScope);

            return newScope;
        }

        public void EnterFunction(string target, string name, string type, List<string> callArgTypes, bool builtin = false)
        {
            if (functionsTable.ContainsKey(target + name))
            {
                throw new CallableSymbolAlreadyDefinedException(name);
            }

            var s = new CallableSymbol(target, name, type, callArgTypes);
            s.BuiltIn = builtin;
            functionsTable[target + name] = s;
        }

        public void EnterSymbol(string name, string type = "", int size = -1)
        {
            if (table.ContainsKey(name))
            {
                throw new SymbolAlreadyDefinedException(name);
            }

            Symbol s = new Symbol(name, type, size);
            table[name] = s;
        }

        public CallableSymbol LookupFunction(string key)
        {
            try
            {
                var s = functionsTable[key];
                return s;
            }
            catch (KeyNotFoundException)
            {
                if (Parent == null)
                {
                    return null;
                }

                return Parent.LookupFunction(key);
            }
        }

        public Symbol Lookup(string name)
        {
            try
            {
                var s =  table[name];
                return s;
            }
            catch (KeyNotFoundException)
            {
                if (Parent == null)
                {
                    return null;
                }

                return Parent.Lookup(name);
            }
        }

        public List<Symbol> GetAllDeclaredSymbols()
        {
            var result = table.Values.ToList();
            result.AddRange(functionsTable.Values.ToList());

            foreach (var child in Childs.Values)
            {
                result.AddRange(child.GetAllDeclaredSymbols());
            }
            return result;
        }
    }
}
