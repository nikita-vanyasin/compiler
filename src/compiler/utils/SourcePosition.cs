using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    public class SourcePosition
    {
        public int Line;
        public int Column;

        public override string ToString()
        {
            return String.Format("line {0}, column {1}", Line, Column);
        }
    }
}
