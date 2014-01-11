using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    class TraverseAllAstNodesVisitor : AstNodeVisitor
    {
        public override bool Visit(AstArrayIndex node)
        {
            return true;
        }

        public override bool Visit(AstArrayInitializerStatement node)
        {
            return true;
        }

        public override bool Visit(AstEqualComparison node)
        {
            return true;
        }

        public override bool Visit(AstExpressionList node)
        {
            return true;
        }

        public override bool Visit(AstGtComparison node)
        {
            return true;
        }

        public override bool Visit(AstGteComparison node)
        {
            return true;
        }

        public override bool Visit(AstIdArrayExpression node)
        {
            return true;
        }

        public override bool Visit(AstIntegerListExpression node)
        {
            return true;
        }

        public override bool Visit(AstLtComparison node)
        {
            return true;
        }

        public override bool Visit(AstLteComparison node)
        {
            return true;
        }

        public override bool Visit(AstNotEqualComparison node)
        {
            return true;
        }

        public override bool Visit(AstStringLiteralExpression node)
        {
            return true;
        }

        public override bool Visit(AstWhileStatement node)
        {
            return true;
        }

        override public bool Visit(AstProgram node)
        {
            return true;
        }

        override public bool Visit(AstClass node)
        {
            return true;
        }

        override public bool Visit(AstClassBody node)
        {
            return true;
        }

        override public bool Visit(AstVisibilityModifier node)
        {
            return true;
        }

        override public bool Visit(AstStaticModifier node)
        {
            return true;
        }

        override public bool Visit(AstClassField node)
        {
            return true;
        }

        override public bool Visit(AstClassMethod node)
        {
            return true;
        }

        override public bool Visit(AstArgumentsDefList node)
        {
            return true;
        }

        override public bool Visit(AstArgumentDef node)
        {
            return true;
        }

        override public bool Visit(AstStatementsBlock node)
        {
            return true;
        }

        override public bool Visit(AstStatementsList node)
        {
            return true;
        }

        override public bool Visit(AstThisMethodCallExpression node)
        {
            return true;
        }

        override public bool Visit(AstThisMethodCallStatement node)
        {
            return true;
        }

        override public bool Visit(AstExternalMethodCallExpression node)
        {
            return true;
        }

        override public bool Visit(AstExternalMethodCallStatement node)
        {
            return true;
        }

        override public bool Visit(AstReturnStatement node)
        {
            return true;
        }

        override public bool Visit(AstIfStatement node)
        {
            return true;
        }

        override public bool Visit(AstAssignStatement node)
        {
            return true;
        }

        override public bool Visit(AstBoolValueExpression node)
        {
            return true;
        }

        override public bool Visit(AstIntegerValueExpression node)
        {
            return true;
        }

        override public bool Visit(AstIdExpression node)
        {
            return true;
        }

        override public bool Visit(AstArgumentsCallList node)
        {
            return true;
        }

        override public bool Visit(AstCallArgument node)
        {
            return true;
        }

        override public bool Visit(AstMulExpression node)
        {
            return true;
        }

        override public bool Visit(AstDivExpression node)
        {
            return true;
        }

        override public bool Visit(AstModExpression node)
        {
            return true;
        }

        override public bool Visit(AstAddExpression node)
        {
            return true;
        }

        override public bool Visit(AstSubExpression node)
        {
            return true;
        }

        override public bool Visit(AstNegateUnaryExpr node)
        {
            return true;
        }

        override public bool Visit(AstSimpleUnaryExpr node)
        {
            return true;
        }

        override public bool Visit(AstSimpleTermExpr node)
        {
            return true;
        }

        public override bool Visit(AstOrExpression node)
        {
            return true;
        }

        public override bool Visit(AstAndExpression node)
        {
            return true;
        }

        public override bool Visit(AstNotExpression node)
        {
            return true;
        }
    }
}
