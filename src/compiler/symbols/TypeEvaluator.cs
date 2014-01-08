using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    public class TypeEvaluator : AstNodeVisitor
    {
        public ErrorsEventDispatcher ErrorDispatcher { get; protected set; }

        private SymbolTable table;
        private TypeResolver resolver;
        private bool result;

        private string currFunctionReturnType = null;

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
            var tableBuilder = new SymbolTableBuilder();

            result = true;
            table = tableBuilder.Build(node);
            table.UseGlobalScope();
            resolver = new TypeResolver(table);
            currFunctionReturnType = null;

            try
            {
                node.Accept(this);
            }
            catch (SymbolNotFoundException e)
            {
                DispatchError(e.Expr.TextPosition, "'" + e.Id + "' identifier not found.");
            }

            return result;
        }

        override public bool Visit(AstProgram node)
        {
            table.UseGlobalScope();
            return true;
        }

        override public bool Visit(AstClass node)
        {
            table.UseChildScope();
            return true;
        }

        override public bool Visit(AstClassBody node)
        {
            foreach (AstClassField classField in node.ClassFields)
            {
                classField.Accept(this);
            }
            foreach (AstClassMethod classMethod in node.ClassMethods)
            {
                classMethod.Accept(this);
                table.UseParentScope();
            }

            return false;
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
            return false;
        }

        override public bool Visit(AstClassMethod node)
        {
            table.UseNamedChildScope(node.Name.Id);
            currFunctionReturnType = node.TypeDef.Id;

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

            for (var i = 0; i < node.CallArgs.Arguments.Count; ++i)
            {
                var argument = node.CallArgs.Arguments[i];
                var typeMatched = resolver.Resolve(argument.Expr) == funcInfo.ArgumentTypes[i];
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
            var funcInfo = table.LookupFunction(node.Target.Id + node.Name.Id);

            var methodExists = funcInfo != null;
            if (!methodExists)
            {
                DispatchError(node.TextPosition, "Object " + node.Target.Id + " does not have method " + node.Name.Id);
                return false;
            }

            var argsCount = funcInfo.ArgumentTypes.Count;
            var realCount = node.CallArgs.Arguments.Count;
            if (argsCount != realCount)
            {
                DispatchError(node.TextPosition, "Expected " + argsCount + " arguments, got " + realCount);
                return false;
            }

            for (var i = 0; i < node.CallArgs.Arguments.Count; ++i)
            {
                var argument = node.CallArgs.Arguments[i];
                var typeMatched = resolver.Resolve(argument.Expr) == funcInfo.ArgumentTypes[i];
                if (!typeMatched)
                {
                    DispatchError(node.TextPosition, "Invalid arguments for method call " + node.Target.Id + "." + node.Name.Id + "(" + funcInfo.TypesToString() + ")");
                    return false;
                }
            }   

            return true;
        }

        override public bool Visit(AstExternalMethodCallStatement node)
        {
            return true;
        }

        override public bool Visit(AstReturnStatement node)
        {
            if (currFunctionReturnType != null)
            {
                var type = resolver.Resolve(node.Expression);
                if (type != currFunctionReturnType)
                {
                    DispatchError(node.TextPosition, "Return value(" + type + ") does not match function return type(" + currFunctionReturnType + ")");
                    return false;
                }
            }

            return true;
        }

        override public bool Visit(AstIfStatement node)
        {
            return true;
        }

        public override bool Visit(AstWhileStatement node)
        {
            return true;
        }

        override public bool Visit(AstAssignStatement node)
        {
            var leftType = resolver.Resolve(node.Variable);
            var rightType = resolver.Resolve(node.NewValue);

            if (leftType != rightType)
            {
                DispatchError(node.TextPosition, "Can not assign " + rightType + " to variable of type " + leftType );
                return false;
            }

            return true;
        }

        override public bool Visit(AstBoolValueExpression node)
        {
            return true;
        }

        override public bool Visit(AstIntegerValueExpression node)
        {
            var value = node.Value;
            if (value.Length > BuiltInTypes.INTEGER_NUMBER_LENGTH)
            {
                DispatchError(node.TextPosition, "Too long integer value. Maximum " + BuiltInTypes.INTEGER_NUMBER_LENGTH + " digits allowed");
            }

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
            var leftType = resolver.Resolve(node.Left);
            var rightType = resolver.Resolve(node.Right);
            if (leftType != BuiltInTypes.INT || rightType != BuiltInTypes.INT)
            {
                DispatchError(node.TextPosition, "Multiplication operation available only for integer types.");
                return false;
            }

            return true;
        }

        override public bool Visit(AstDivExpression node)
        {
            var leftType = resolver.Resolve(node.Left);
            var rightType = resolver.Resolve(node.Right);
            if (leftType != BuiltInTypes.INT || rightType != BuiltInTypes.INT)
            {
                DispatchError(node.TextPosition, "Div operation available only for integer types.");
                return false;
            }

            return CheckZeroDevision(node.Right);
        }

        private bool CheckZeroDevision(AstExpression node)
        {
            // todo: cast?

            var castNode = node;

            if (node is AstSimpleUnaryExpr)
            {
                var term = node as AstSimpleUnaryExpr;
                castNode = term.SimpleTerm.Expr;
            }
            
            var intValExpr = castNode as AstIntegerValueExpression;
            if (intValExpr != null)
            {
                if (Convert.ToInt32(intValExpr.Value) == 0)
                {
                    DispatchError(node.TextPosition, "Division by zero");
                    return false;
                }
            }

            return true;
        }

        override public bool Visit(AstModExpression node)
        {
            var leftType = resolver.Resolve(node.Left);
            var rightType = resolver.Resolve(node.Right);
            if (leftType != BuiltInTypes.INT || rightType != BuiltInTypes.INT)
            {
                DispatchError(node.TextPosition, "Mod operation available only for integer types.");
                return false;
            }

            return CheckZeroDevision(node.Right);
        }

        override public bool Visit(AstAddExpression node)
        {
            var leftType = resolver.Resolve(node.Left);
            var rightType = resolver.Resolve(node.Right);
            if (leftType != BuiltInTypes.INT || rightType != BuiltInTypes.INT)
            {
                DispatchError(node.TextPosition, "Add operation available only for integer types.");
                return false;
            }
            return true;
        }

        override public bool Visit(AstSubExpression node)
        {
            var leftType = resolver.Resolve(node.Left);
            var rightType = resolver.Resolve(node.Right);
            if (leftType != BuiltInTypes.INT || rightType != BuiltInTypes.INT)
            {
                DispatchError(node.TextPosition, "Sub operation available only for integer types.");
                return false;
            }
            return true;
        }

        override public bool Visit(AstNegateUnaryExpr node)
        {
            var termType = resolver.Resolve(node.SimpleTerm);

            if (termType != BuiltInTypes.INT)
            {
                DispatchError(node.TextPosition, "Negate operation available only for integer types.");
                return false;
            }
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
            var leftType = resolver.Resolve(node.Left);
            var rightType = resolver.Resolve(node.Right);
            if (leftType != BuiltInTypes.BOOL || rightType != BuiltInTypes.BOOL)
            {
                DispatchError(node.TextPosition, "Boolean operations available only for bool types.");
                return false;
            }

            return true;
        }

        public override bool Visit(AstAndExpression node)
        {
            var leftType = resolver.Resolve(node.Left);
            var rightType = resolver.Resolve(node.Right);
            if (leftType != BuiltInTypes.BOOL || rightType != BuiltInTypes.BOOL)
            {
                DispatchError(node.TextPosition, "Boolean operations available only for bool types.");
                return false;
            }
            return true;
        }

        public override bool Visit(AstNotExpression node)
        {
            var type = resolver.Resolve(node.Expr);
            if (type != BuiltInTypes.BOOL)
            {
                DispatchError(node.TextPosition, "Boolean operations available only for bool types.");
                return false;
            }
            return true;
        }

        public override bool Visit(AstLtComparison node)
        {
            var leftType = resolver.Resolve(node.Left);
            var rightType = resolver.Resolve(node.Right);
            if (leftType != BuiltInTypes.INT || rightType != BuiltInTypes.INT)
            {
                DispatchError(node.TextPosition, "Comparison operations available only for integer types.");
                return false;
            }
            return true;
        }

        public override bool Visit(AstGtComparison node)
        {
            var leftType = resolver.Resolve(node.Left);
            var rightType = resolver.Resolve(node.Right);
            if (leftType != BuiltInTypes.INT || rightType != BuiltInTypes.INT)
            {
                DispatchError(node.TextPosition, "Comparison operations available only for integer types.");
                return false;
            }
            return true;
        }

        public override bool Visit(AstLteComparison node)
        {
            var leftType = resolver.Resolve(node.Left);
            var rightType = resolver.Resolve(node.Right);
            if (leftType != BuiltInTypes.INT || rightType != BuiltInTypes.INT)
            {
                DispatchError(node.TextPosition, "Comparison operations available only for integer types.");
                return false;
            }
            return true;
        }

        public override bool Visit(AstGteComparison node)
        {
            var leftType = resolver.Resolve(node.Left);
            var rightType = resolver.Resolve(node.Right);
            if (leftType != BuiltInTypes.INT || rightType != BuiltInTypes.INT)
            {
                DispatchError(node.TextPosition, "Comparison operations available only for integer types.");
                return false;
            }
            return true;
        }

        public override bool Visit(AstEqualComparison node)
        {
            var leftType = resolver.Resolve(node.Left);
            var rightType = resolver.Resolve(node.Right);
            if (leftType != BuiltInTypes.INT || rightType != BuiltInTypes.INT)
            {
                DispatchError(node.TextPosition, "Comparison operations available only for integer types.");
                return false;
            }
            return true;
        }
    }
}