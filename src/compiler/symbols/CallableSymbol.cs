using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    public class CallableSymbol : Symbol
    {
        public string Target { get; protected set; }
        public List<string> ArgumentTypes { get; protected set; }
        
        public CallableSymbol(string target, string name, string type, List<string> callArgTypes)
            :base(name, type, null)
        {
            Target = target;
            ArgumentTypes = callArgTypes;
        }

        public string TypesToString()
        {
            return string.Join(", ", ArgumentTypes);
        }
    }
}
