using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    class ProductionInfo
    {
        public string Head { get; private set; }
        public int Length { get; private set; }
        public string Action { get; private set; }

        public ProductionInfo(string head, int length, string action)
        {
            this.Head = head;
            this.Length = length;
            this.Action = action;
        }
    }
}
