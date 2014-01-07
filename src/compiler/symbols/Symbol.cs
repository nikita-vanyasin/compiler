using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    public class Symbol
    {
        public string Name { get; protected set; }
        public string Type { get; protected set; }
        public int Size { get; protected set; }

        public Symbol(string name, string type, int size = -1)
        {
            Name = name;
            Type = type;
            Size = size;
        }
    }
}
