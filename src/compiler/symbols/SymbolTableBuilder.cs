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
            AddBuiltInFunc("Console", "WriteInt", BuiltInTypes.VOID, writeIntList);


            var writeIntBool = new List<string>() { 
                BuiltInTypes.BOOL
            };
            AddBuiltInFunc("Console", "WriteBool", BuiltInTypes.VOID, writeIntBool);

            var readIntList = new List<string>(){
            };
            AddBuiltInFunc("Console", "ReadInt", BuiltInTypes.INT, readIntList);

             var writeSpaceList = new List<string>()
             {
             };
             AddBuiltInFunc("Console", "WriteSpace", BuiltInTypes.VOID, writeSpaceList);
             var writeEndLineList = new List<string>()
             {
             };
             AddBuiltInFunc("Console", "WriteLine", BuiltInTypes.VOID, writeEndLineList);
        }

        private void AddBuiltInFunc(string target, string name, string type, List<string> types)
        {
            table.EnterFunction(target, name, type, types, true);
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
			int[] size = null;
            if (node.TypeDef is AstIdArrayExpression)
            {
                var indexNode = (node.TypeDef as AstIdArrayExpression).Index;
				try
				{
					size = (indexNode as AstIntegerListExpression).GetSize();
				}
				catch (OverflowException e)
				{
					throw new ArraySizeIncorrectException(e.Message);
				}
            }

			if (size == null)
				table.EnterSymbol(node.Name.Id, node.TypeDef.Id, -1);
			else
				table.EnterSymbol(node.Name.Id, node.TypeDef.Id, size);

            return false;
        }

        override public bool Visit(AstClassMethod node)
        {
            table.UseNamedChildScope(node.Name.Id);

            var argumentsTypes = new List<string>();
            foreach (var argDef in node.ArgumentsDefinition.ArgumentsDefinition)
            {
                argumentsTypes.Add(argDef.TypeDef.Id);
                table.EnterSymbol(argDef.Name.Id, argDef.TypeDef.Id, null);
            }

            table.UseParentScope();

            var entryPoint = node.Name.Id == "Main";
            table.EnterFunction("", node.Name.Id, node.TypeDef.Id, argumentsTypes, entryPoint);
            
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

        public override bool Visit(AstLtComparison node)
        {
            return true;
        }
        public override bool Visit(AstGtComparison node)
        {
            return true;
        }

        public override bool Visit(AstLteComparison node)
        {
            return true;
        }

        public override bool Visit(AstGteComparison node)
        {
            return true;
        }

        public override bool Visit(AstEqualComparison node)
        {
            return true;
        }

        public override bool Visit(AstNotEqualComparison node)
        {
            return true;
        }

        public override bool Visit(AstIdArrayExpression node)
        {
            return true;
        }

        public override bool Visit(AstArrayInitializerStatement node)
        {
            return true;
        }

		public override bool Visit(AstIntegerListExpression node)
		{
			// TODO: check
			return true;
		}

		public override bool Visit(AstExpressionList node)
		{
			// TODO: check
			return true;
		}

		public override bool Visit(AstArrayIndex node)
		{
			// TODO : check
			return true;
		}
	}
}
