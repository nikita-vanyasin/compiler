using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    public enum ParseActionKind : sbyte
    {
        ERROR,
        REDUCE,
        SHIFT,
        ACCEPT
    }

    class ParseAction
    {
        public ParseActionKind Kind = ParseActionKind.ERROR;
        public int Number { get; private set; }

        public ParseAction(ParseActionKind kind, int number = ParseTable.ERROR_STATE)
        {
            this.Kind = kind;
            this.Number = number;
        }

        public override bool Equals(object obj)
        {
            ParseAction other = obj as ParseAction;
            if (other != null)
            {
                return (this.Kind == other.Kind) &&
                       (this.Number == other.Number);
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            // TODO: use XOR or something
            return this.Kind.GetHashCode() + this.Number.GetHashCode();
        }
    }
}
