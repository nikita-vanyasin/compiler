using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace compiler
{
    class LLVMCodeGenerator : CodeGenerator
    {
        private StreamWriter codeStream;
        private Dictionary<string, string> symbolTable = new Dictionary<string, string>();

       /* private string CreateLLVMMOdule(string name)
        {

        }*/

        private string GetLLVMType(AstIdExpression node)
        {
            switch (node.Id)
            {
                case "int":
                    return "i32";
                case "bool":
                    return "i0";
                default:
                    return node.Id;
            }
        }

        private string GetLLVMVisibility(AstVisibilityModifier node)
        {
            switch (node.Value)
            {
                case VisibilityModifier.PRIVATE:
                    return "private";
                case VisibilityModifier.PUBLIC:
                    return "external";
                default:
                    return "";
            }
        }

        public LLVMCodeGenerator()
            : base()
        {

        }

        public override bool Generate(AstProgram astRootNode, Stream outStream)
        {
            codeStream = new StreamWriter(outStream);
            codeStream.AutoFlush = true;
            astRootNode.Accept(this);
            return true;
            //throw new NotImplementedException();
        }

        override public bool Visit(AstProgram node)
        {           
            return true;
        }

        override public bool Visit(AstClass node)
        {
           //TODO something
            return true;
        }

        override public bool Visit(AstClassBody node)
        {
            //TODO something
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
           // codeStream.WriteLine("@" + node.Name.Id + " = " + GetLLVMVisibility(node.Visibility) + " global alloca " + GetLLVMType(node.TypeDef));
            symbolTable.Add(node.Name.Id, "variable");
            return true;
        }

        override public bool Visit(AstClassMethod node)
        {
            codeStream.Write("define " + GetLLVMType(node.TypeDef) + " @" + node.Name.Id.ToLower());
            symbolTable.Add(node.Name.Id.ToLower(), "method");
            return true;
        }

        override public bool Visit(AstArgumentsDefList node)
        {
            codeStream.Write("(");
            List<string> LLWMArgDefList = new List<string>();
            foreach(var argument in node.ArgumentsDefinition)
            {
                LLWMArgDefList.Add(GetLLVMType(argument.TypeDef) + " %" + argument.Name.Id);
              //  symbolTable.Add(argument.Name.Id + , "variable");            
            }
            codeStream.Write(string.Join(",", LLWMArgDefList.ToArray()));
            codeStream.Write(")");
            return true;
        }

        override public bool Visit(AstArgumentDef node)
        {            
            return true;
        }

        override public bool Visit(AstStatementsBlock node)
        {
            codeStream.Write("{\n");
            
            node.Statements.Accept(this);

            codeStream.Write("}\n");
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
           // codeStream.Write("ret " + node.Expression);
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
            //symbolTable.Add("id", );
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

        override public bool Visit(AstCompareOperation node)
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
    }
}
