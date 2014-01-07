using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    class LLVMCodeGenerator : CodeGenerator
    {
        public override bool Generate(AstProgram astRootNode, System.IO.Stream outStream)
        {
            throw new NotImplementedException();
        }

        override public bool Visit(AstNode node)
        {
            throw new NotImplementedException();
        }
    }
}
