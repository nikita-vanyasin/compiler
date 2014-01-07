using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace compiler
{
    abstract class CodeGenerator : ErrorsEventDispatcher, AstNodeVisitor
    {
        abstract public bool Generate(AstProgram astRootNode, Stream outStream);

        abstract public bool Visit(AstNode node);
    }
}
