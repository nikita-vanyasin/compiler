using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    public class SourcePosition
    {
        public int Position { get; set; }
        public int TokenLength { get; set; }

        public SourcePosition()
        {
            Position = 0;
        }

        public override bool Equals(object obj)
        {
            var other = obj as SourcePosition;
            if (other != null)
            {
                return this.Position == other.Position && this.TokenLength == other.TokenLength;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode() + TokenLength.GetHashCode();
        }
    }
}
