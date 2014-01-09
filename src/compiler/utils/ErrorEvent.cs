using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    public class ErrorEvent : EventArgs
    {
        public static string GetTextByCode(int code)
        {
            return "";
        }

        public string Description { get; private set; }
        public int Code { get; private set; }
        public SourcePosition Position { get; private set; }
        public bool IsError { get; set; }

        public ErrorEvent(SourcePosition position, string description, int code)
        {
            IsError = true;
            this.Position = position;
            this.Description = description;
            this.Code = code;
        }

        public override string ToString()
        {
            return Description + " at " + GetTextByCode(Code) + "(" + Position + ")";
        }
    }
}
