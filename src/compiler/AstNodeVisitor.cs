using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    public abstract class AstNodeVisitor
    {
        abstract public bool Visit(AstProgram node);
        abstract public bool Visit(AstClass node);
        abstract public bool Visit(AstClassBody node);
        abstract public bool Visit(AstVisibilityModifier node);
        abstract public bool Visit(AstStaticModifier node);
        abstract public bool Visit(AstClassField node);
        abstract public bool Visit(AstClassMethod node);
        abstract public bool Visit(AstArgumentsDefList node);
        abstract public bool Visit(AstArgumentDef node);
        abstract public bool Visit(AstStatementsBlock node);
        abstract public bool Visit(AstStatementsList node);
        abstract public bool Visit(AstThisMethodCallExpression node);
        abstract public bool Visit(AstThisMethodCallStatement node);
        abstract public bool Visit(AstExternalMethodCallExpression node);
        abstract public bool Visit(AstExternalMethodCallStatement node);
        abstract public bool Visit(AstReturnStatement node);
        abstract public bool Visit(AstIfStatement node);
        abstract public bool Visit(AstAssignStatement node);
        abstract public bool Visit(AstBoolValueExpression node);
        abstract public bool Visit(AstIntegerValueExpression node);
        abstract public bool Visit(AstIdExpression node);
        abstract public bool Visit(AstArgumentsCallList node);
        abstract public bool Visit(AstCallArgument node);
        abstract public bool Visit(AstCompareOperation node);
        abstract public bool Visit(AstMulExpression node);
        abstract public bool Visit(AstDivExpression node);
        abstract public bool Visit(AstModExpression node);
        abstract public bool Visit(AstAddExpression node);
        abstract public bool Visit(AstSubExpression node);
        abstract public bool Visit(AstNegateUnaryExpr node);
        abstract public bool Visit(AstSimpleUnaryExpr node);
        abstract public bool Visit(AstSimpleTermExpr node);     
    }
}