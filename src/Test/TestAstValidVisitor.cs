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

        public bool Visit(AstNode node)
        {
            throw new Exception("You must implement Visit method for each node. Thie code should be unreacheble");
        }

        public bool Visit(AstProgram node)
        {
            ErrorIfIsNull(node);
            ErrorIfIsNull(node.Class);

            return true;
        }

        public bool Visit(AstClass node)
        {
            ErrorIfIsNull(node.Body);
            ErrorIfIsNull(node.Name);
            return true;
        }

        public bool Visit(AstClassBody node)
        {
            return true;
        }

        public bool Visit(AstVisibilityModifier node)
        {
            return true;
        }

        public bool Visit(AstStaticModifier node)
        {
            return true;
        }

        public bool Visit(AstClassField node)
        {
            ErrorIfIsNull(node.Name);
            ErrorIfIsNull(node.TypeDef);
            return true;
        }

        public bool Visit(AstClassMethod node)
        {
            ErrorIfIsNull(node.ArgumentsDefinition);
            ErrorIfIsNull(node.Name);
            ErrorIfIsNull(node.StatementsBlock);
            ErrorIfIsNull(node.TypeDef);
            return true;
        }

        public bool Visit(AstArgumentsDefList node)
        {
            ErrorIfIsNull(node.ArgumentsDefinition);
            return true;
        }

        public bool Visit(AstArgumentDef node)
        {
            ErrorIfIsNull(node.Name);
            ErrorIfIsNull(node.TypeDef);
            return true;
        }

        public bool Visit(AstStatementsBlock node)
        {
            ErrorIfIsNull(node.Statements);
            return true;
        }

        public bool Visit(AstStatementsList node)
        {
            ErrorIfIsNull(node.Statements);
            return true;
        }

        public bool Visit(AstThisMethodCallExpression node)
        {
            ErrorIfIsNull(node.CallArgs);
            ErrorIfIsNull(node.Name);
            return true;
        }

        public bool Visit(AstThisMethodCallStatement node)
        {
            ErrorIfIsNull(node.Expr);
            return true;
        }

        public bool Visit(AstExternalMethodCallExpression node)
        {
            ErrorIfIsNull(node.CallArgs);
            ErrorIfIsNull(node.Name);
            ErrorIfIsNull(node.Target);
            return true;
        }

        public bool Visit(AstExternalMethodCallStatement node)
        {
            ErrorIfIsNull(node.Expr);
            return true;
        }

        public bool Visit(AstReturnStatement node)
        {
            ErrorIfIsNull(node.Expression);
            return true;
        }

        public bool Visit(AstIfStatement node)
        {
            ErrorIfIsNull(node.Condition);
            ErrorIfIsNull(node.ThenBlock);
            ErrorIfIsNull(node.ElseBlock);
            return true;
        }

        public bool Visit(AstAssignStatement node)
        {
            ErrorIfIsNull(node.NewValue);
            ErrorIfIsNull(node.Variable);
            return true;
        }
        
        public bool Visit(AstBoolValueExpression node)
        {
            ErrorIfIsNull(node.Value);
            return true;
        }

        public bool Visit(AstIntegerValueExpression node)
        {
            ErrorIfIsNull(node.Value);
            return true;
        }

        public bool Visit(AstIdExpression node)
        {
            ErrorIfIsNull(node.Id);
            return true;
        }

        public bool Visit(AstArgumentsCallList node)
        {
            ErrorIfIsNull(node.Arguments);
            return true;
        }

        public bool Visit(AstCallArgument node)
        {
            ErrorIfIsNull(node.Expr);
            return true;
        }

        public bool Visit(AstCompareOperation node)
        {
            ErrorIfIsNull(node.Value);
            return true;
        }

        public bool Visit(AstMulExpression node)
        {
            ErrorIfIsNull(node.Left);
            ErrorIfIsNull(node.Right);
            return true;
        }

        public bool Visit(AstDivExpression node)
        {
            ErrorIfIsNull(node.Left);
            ErrorIfIsNull(node.Right);
            return true;
        }

        public bool Visit(AstModExpression node)
        {
            ErrorIfIsNull(node.Left);
            ErrorIfIsNull(node.Right);
            return true;
        }

        public bool Visit(AstAddExpression node)
        {
            ErrorIfIsNull(node.Left);
            ErrorIfIsNull(node.Right);
            return true;
        }

        public bool Visit(AstSubExpression node)
        {
            ErrorIfIsNull(node.Left);
            ErrorIfIsNull(node.Right);
            return true;
        }

        public bool Visit(AstNegateUnaryExpr node)
        {
            ErrorIfIsNull(node.SimpleTerm);
            return true;
        }

        public bool Visit(AstSimpleUnaryExpr node)
        {
            ErrorIfIsNull(node.SimpleTerm);
            return true;
        }

        public bool Visit(AstSimpleTermExpr node)
        {
            ErrorIfIsNull(node.Expr);
            return true;
        }
    }
}
