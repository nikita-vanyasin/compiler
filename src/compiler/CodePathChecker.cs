using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    public class CodePathChecker : AstNodeVisitor
    {
        public ErrorsEventDispatcher ErrorDispatcher { get; protected set; }

        private bool result;

        public CodePathChecker()
        {
            ErrorDispatcher = new ErrorsEventDispatcher();
        }

        public bool Check(AstProgram rootNode)
        {
            result = true;
            rootNode.Accept(this);

            return result;
        }
        
        protected void DispatchError(int position, string description, int code = -1)
        {
            result = false;
            ErrorDispatcher.DispatchError(position, description, code);
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
            var stmts = node.StatementsBlock.Statements.Statements;

            var validCodepaths = CheckStmtsHasReturn(stmts);
            if (!validCodepaths)
            {
                DispatchError(node.TextPosition, "Not all code paths return a value");
                return false;
            }            


            return true;
        }

        private bool CheckStmtsHasReturn(List<AstStatement> stmts)
        {
            // has return after last ifstmt
            // or has return both in ifthen block and ifelseblock

            AstIfStatement lastIfStmt = null;
            var hasReturnAfterLastIf = false;
            foreach (var stmt in stmts)
            {
                if (stmt is AstIfStatement)
                {
                    lastIfStmt = stmt as AstIfStatement;
                    hasReturnAfterLastIf = false;
                }

                if (stmt is AstReturnStatement)
                {
                    hasReturnAfterLastIf = true;
                }
            }

            if (hasReturnAfterLastIf)
            {
                return hasReturnAfterLastIf;
            }
            
            if (lastIfStmt != null)
            {
                var thenBlock = lastIfStmt.ThenBlock.Statements.Statements;
                var elseBlock = lastIfStmt.ElseBlock.Statements.Statements;
                return CheckStmtsHasReturn(thenBlock) && CheckStmtsHasReturn(elseBlock);
            }
            else
            {
                return false;
            }
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
            var stmts = node.Statements.Statements;
            for (var i = 0; i < stmts.Count; ++i)
            {
                var stmt = stmts[i];
                if (stmt is AstReturnStatement && i < stmts.Count - 1)
                {
                    DispatchError(stmts[i + 1].TextPosition, "Unreachable code detected");
                    return false;
                }
            }
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
