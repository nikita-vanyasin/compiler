using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    public class SymbolTableBuilder : AstNodeVisitor
    {
        private SymbolTable table;
        
        public SymbolTable Build(AstProgram node)
        {
            table = new SymbolTable();

            AddBuiltInSymbols();
            node.Accept(this);

            return table;
        }

        private void AddBuiltInSymbols()
        {
            var writeIntList = new List<string>() { 
                BuiltInTypes.INT
            };
            table.EnterFunction("Console", "WriteInt", BuiltInTypes.VOID, writeIntList);


            var writeIntBool = new List<string>() { 
                BuiltInTypes.BOOL
            };
            table.EnterFunction("Console", "WriteBool", BuiltInTypes.VOID, writeIntBool);
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
            table.UseNamedChildScope(node.Name.Id);

            var argumentsTypes = new List<string>();
            foreach (var argDef in node.ArgumentsDefinition.ArgumentsDefinition)
            {
                argumentsTypes.Add(argDef.TypeDef.Id);
                table.EnterSymbol(argDef.Name.Id, argDef.TypeDef.Id);
            }

            table.UseParentScope();
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

        public override bool Visit(AstWhileStatement node)
        {
            return true;
        }
    }
}
