using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using compiler;

namespace Test
{
    [Serializable]
    public class InvalidAstException : Exception
    {
        public InvalidAstException() { }
        public InvalidAstException(string message) : base(message) { }
        public InvalidAstException(string message, Exception inner) : base(message, inner) { }
        protected InvalidAstException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    class TestAstValidVisitor : AstNodeVisitor
    {        
        public bool TestTree(AstProgram node)
        {
            try
            {
                node.Accept(this);
            }
            catch (InvalidAstException)
            {
                return false;
            }
            return true;
        }

        private void ErrorIfIsNull(Object o)
        {
            if (o == null)
            {
                Error();
            }
        }
        
        private void Error()
        {
            throw new InvalidAstException();
        }
        
        override public bool Visit(AstProgram node)
        {
            ErrorIfIsNull(node);
            ErrorIfIsNull(node.Class);

            return true;
        }

        override public bool Visit(AstClass node)
        {
            ErrorIfIsNull(node.Body);
            ErrorIfIsNull(node.Name);
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

        public override bool Visit(AstWhileStatement node)
        {
            ErrorIfIsNull(node.Condition);
            ErrorIfIsNull(node.Statements);
            return true;
        }

        override public bool Visit(AstClassField node)
        {
            ErrorIfIsNull(node.Name);
            ErrorIfIsNull(node.TypeDef);
            return true;
        }

        override public bool Visit(AstClassMethod node)
        {
            ErrorIfIsNull(node.ArgumentsDefinition);
            ErrorIfIsNull(node.Name);
            ErrorIfIsNull(node.StatementsBlock);
            ErrorIfIsNull(node.TypeDef);
            return true;
        }

        override public bool Visit(AstArgumentsDefList node)
        {
            ErrorIfIsNull(node.ArgumentsDefinition);
            return true;
        }

        override public bool Visit(AstArgumentDef node)
        {
            ErrorIfIsNull(node.Name);
            ErrorIfIsNull(node.TypeDef);
            return true;
        }

        override public bool Visit(AstStatementsBlock node)
        {
            ErrorIfIsNull(node.Statements);
            return true;
        }

        override public bool Visit(AstStatementsList node)
        {
            ErrorIfIsNull(node.Statements);
            return true;
        }

        override public bool Visit(AstThisMethodCallExpression node)
        {
            ErrorIfIsNull(node.CallArgs);
            ErrorIfIsNull(node.Name);
            return true;
        }

        override public bool Visit(AstThisMethodCallStatement node)
        {
            ErrorIfIsNull(node.Expr);
            return true;
        }

        override public bool Visit(AstExternalMethodCallExpression node)
        {
            ErrorIfIsNull(node.CallArgs);
            ErrorIfIsNull(node.Name);
            ErrorIfIsNull(node.Target);
            return true;
        }

        override public bool Visit(AstExternalMethodCallStatement node)
        {
            ErrorIfIsNull(node.Expr);
            return true;
        }

        override public bool Visit(AstReturnStatement node)
        {
            ErrorIfIsNull(node.Expression);
            return true;
        }

        override public bool Visit(AstIfStatement node)
        {
            ErrorIfIsNull(node.Condition);
            ErrorIfIsNull(node.ThenBlock);
            ErrorIfIsNull(node.ElseBlock);
            return true;
        }

        override public bool Visit(AstAssignStatement node)
        {
            ErrorIfIsNull(node.NewValue);
            ErrorIfIsNull(node.Variable);
            return true;
        }
        
        override public bool Visit(AstBoolValueExpression node)
        {
            ErrorIfIsNull(node.Value);
            return true;
        }

        override public bool Visit(AstIntegerValueExpression node)
        {
            ErrorIfIsNull(node.Value);
            return true;
        }

        override public bool Visit(AstIdExpression node)
        {
            ErrorIfIsNull(node.Id);
            return true;
        }

        override public bool Visit(AstArgumentsCallList node)
        {
            ErrorIfIsNull(node.Arguments);
            return true;
        }

        override public bool Visit(AstCallArgument node)
        {
            ErrorIfIsNull(node.Expr);
            return true;
        }
        
        override public bool Visit(AstMulExpression node)
        {
            ErrorIfIsNull(node.Left);
            ErrorIfIsNull(node.Right);
            return true;
        }

        override public bool Visit(AstDivExpression node)
        {
            ErrorIfIsNull(node.Left);
            ErrorIfIsNull(node.Right);
            return true;
        }

        override public bool Visit(AstModExpression node)
        {
            ErrorIfIsNull(node.Left);
            ErrorIfIsNull(node.Right);
            return true;
        }

        override public bool Visit(AstAddExpression node)
        {
            ErrorIfIsNull(node.Left);
            ErrorIfIsNull(node.Right);
            return true;
        }

        override public bool Visit(AstSubExpression node)
        {
            ErrorIfIsNull(node.Left);
            ErrorIfIsNull(node.Right);
            return true;
        }

        override public bool Visit(AstNegateUnaryExpr node)
        {
            ErrorIfIsNull(node.SimpleTerm);
            return true;
        }

        override public bool Visit(AstSimpleUnaryExpr node)
        {
            ErrorIfIsNull(node.SimpleTerm);
            return true;
        }

        override public bool Visit(AstSimpleTermExpr node)
        {
            ErrorIfIsNull(node.Expr);
            return true;
        }

        public override bool Visit(AstOrExpression node)
        {
            ErrorIfIsNull(node.Left);
            ErrorIfIsNull(node.Right);
            return true;
        }

        public override bool Visit(AstAndExpression node)
        {
            ErrorIfIsNull(node.Left);
            ErrorIfIsNull(node.Right);
            return true;
        }

        public override bool Visit(AstNotExpression node)
        {
            ErrorIfIsNull(node.Expr);
            return true;
        }


        public override bool Visit(AstLtComparison node)
        {
            ErrorIfIsNull(node.Left);
            ErrorIfIsNull(node.Right);
            return true;
        }
        public override bool Visit(AstGtComparison node)
        {
            ErrorIfIsNull(node.Left);
            ErrorIfIsNull(node.Right);
            return true;
        }

        public override bool Visit(AstLteComparison node)
        {
            ErrorIfIsNull(node.Left);
            ErrorIfIsNull(node.Right);
            return true;
        }

        public override bool Visit(AstGteComparison node)
        {
            ErrorIfIsNull(node.Left);
            ErrorIfIsNull(node.Right);
            return true;
        }

        public override bool Visit(AstEqualComparison node)
        {
            ErrorIfIsNull(node.Left);
            ErrorIfIsNull(node.Right);
            return true;
        }

        public override bool Visit(AstIdArrayExpression node)
        {
            ErrorIfIsNull(node.Index);
            return true;
        }
    }
}
