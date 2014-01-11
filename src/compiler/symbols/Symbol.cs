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
        public object Size { get; protected set; }
		public int FlatSize 
		{
			get
			{
				if(!(Size is int[])) return 0;
				return (Size as int[]).Aggregate((s, t) => s * t);
			}
		}
        public bool Used { get; set; }
        public bool BuiltIn { get; set; }

        public Symbol(string name, string type, object size)
        {
            Name = name;
            Type = type;
            Size = size;
            Used = false;
            BuiltIn = false;
        }

        public static bool IsArray(Symbol sym)
        {
            return sym.Size is int[];
        }
	}
}
