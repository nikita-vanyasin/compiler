using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    public class AstNode
    {
        public SourcePosition TextPosition { get; protected set; }

        public AstNode()
        {

        }

        public void SetTextPosition(SourcePosition pos)
        {
            TextPosition = pos;
        }

        virtual public void Accept(AstNodeVisitor visitor)
		{
        }
    }
}
