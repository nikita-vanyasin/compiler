using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    class FindAllStringConstantsVisitor : TraverseAllAstNodesVisitor
    {
        private List<string> result;

        public List<string> FindAll(AstProgram root)
        {
            result = new List<string>();
            root.Accept(this);
            return result;
        }

        public override bool Visit(AstStringLiteralExpression node)
        {
            result.Add(node.Value);
            return base.Visit(node);
        }
    }
}
