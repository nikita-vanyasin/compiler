using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler.utils
{
	internal class AstDumper : AstNodeVisitor
	{
		private int m_level = 0;
		private StringBuilder m_builder = new StringBuilder();

		public string Dump(AstProgram root)
		{
			Visit(root);
			string result = m_builder.ToString();
			m_builder.Clear();
			return result;
		}

		private void Log(string what)
		{
			m_builder.Append(string.Format("{0}{1}{2}", '\t' * m_level, what, Environment.NewLine));
		}

		public override bool Visit(AstProgram node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstClass node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstClassBody node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstVisibilityModifier node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstStaticModifier node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstClassField node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstClassMethod node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstArgumentsDefList node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstArgumentDef node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstStatementsBlock node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstStatementsList node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstThisMethodCallExpression node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstThisMethodCallStatement node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstExternalMethodCallExpression node)
		{
			Log(node.ToString());
			return true;
		}

        public override bool Visit(AstStringLiteralExpression node)
        {
            Log(node.ToString());
            return true;
        }

		public override bool Visit(AstExternalMethodCallStatement node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstReturnStatement node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstIfStatement node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstAssignStatement node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstBoolValueExpression node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstIntegerValueExpression node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstIdExpression node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstArgumentsCallList node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstCallArgument node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstMulExpression node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstDivExpression node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstModExpression node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstAddExpression node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstSubExpression node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstNegateUnaryExpr node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstSimpleUnaryExpr node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstSimpleTermExpr node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstOrExpression node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstAndExpression node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstNotExpression node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstWhileStatement node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstLtComparison node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstGtComparison node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstLteComparison node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstGteComparison node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstEqualComparison node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstNotEqualComparison node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstIdArrayExpression node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstArrayInitializerStatement node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstIntegerListExpression node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstExpressionList node)
		{
			Log(node.ToString());
			return true;
		}

		public override bool Visit(AstArrayIndex node)
		{
			Log(node.ToString());
			return true;
		}
	}
}
