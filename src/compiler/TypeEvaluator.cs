using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    class TypeEvaluator : AstNodeVisitor
    {
        public ErrorsEventDispatcher ErrorDispatcher { get; protected set; }

        private SymbolTable table;
        private bool result;

        public TypeEvaluator()
        {
            ErrorDispatcher = new ErrorsEventDispatcher();
        }

        protected void DispatchError(SourcePosition position, string description, int code = -1)
        {
            result = false;
            ErrorDispatcher.DispatchError(position, description, code);
        }

        public SymbolTable GetSymbolTable()
        {
            return table;
        }

        public bool Evaluate(AstProgram node)
        {
            result = true;
            table = new SymbolTable(0);
            node.Accept(this);

            return result;
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
            table.EnterSymbol(node.Name.Id, node.TypeDef.Id);

            return false;
        }

        override public bool Visit(AstClassMethod node)
        {
            //TODO: use tables stack to store arg list
            //
            // table.BeginScope()

            var argumentsTypes = new List<string>();
            foreach (var argDef in node.ArgumentsDefinition.ArgumentsDefinition)
            {
                argumentsTypes.Add(argDef.TypeDef.Id);
                table.EnterSymbol(argDef.Name.Id, argDef.TypeDef.Id);
            }

            table.EnterFunction("", node.Name.Id, node.TypeDef.Id, argumentsTypes);

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
            // check method exists
            // check args count
            // check arg types

            var funcInfo = table.LookupFunction(node.Name.Id);

            var methodExists = funcInfo != null;
            if (!methodExists)
            {
                DispatchError(node.TextPosition, "Method " + node.Name.Id + " not found in current scope.");
                return false;
            }

            var argsCount = funcInfo.ArgumentTypes.Count;
            var realCount = node.CallArgs.Arguments.Count;
            if (argsCount != realCount)
            {
                DispatchError(node.TextPosition, "Expected " + argsCount + " arguments, got " + realCount);
                return false;
            }

            foreach (var argument in node.CallArgs.Arguments)
            {
                var typeMatched = true; // argument.Type == funcInfo.ArgumentTypes[i]
                if (!typeMatched)
                {
                    DispatchError(node.TextPosition, "Invalid arguments for method call " + node.Name.Id + "(" + funcInfo.TypesToString() + ")");
                    return false;
                }
            }            

            

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