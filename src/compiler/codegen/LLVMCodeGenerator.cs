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
        private uint unnamedVariable = 0;
        private StreamWriter codeStream;
        private string currReturnType = "";
        private List<string> classVariables = new List<string>();


        private void DefineLocalClassVariables()
        {
            foreach (var variable in classVariables)
            {
                var symbolTableVariable = table.Lookup(variable);
                codeStream.WriteLine("%" + variable + "= load " + GetLLVMType(symbolTableVariable.Type) + "* @" + variable);
            }
        }

        private void CreateLLVMBuiltIn()
        {
            codeStream.WriteLine("declare i32 @puts(i8*)\n");
        }

        private void ResetUnnamedVariable()
        {
            unnamedVariable = 0;
        }

        private string CreateUnnamedVariable()
        {
            unnamedVariable++;
            string returnVal = "%" + unnamedVariable.ToString();
            
            return returnVal;
        }

        private string GetCurrUnnamedVariable()
        {
            return "%" + unnamedVariable.ToString();
        }

        private string GetLLVMType(string type)
        {
            switch (type)
            {
                case "int":
                    return "i32";
                case "bool":
                    return "i1";
                default:
                    throw new NotImplementedException();
            }
        }

        private string GetDefaultTypeValue(AstIdExpression node)
        {
            switch (node.Id)
            {
                case "int":
                    return "0";
                case "bool":
                    return "0";
                default:
                    throw new NotImplementedException();
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
            CreateLLVMBuiltIn();
            return true;
        }

        override public bool Visit(AstClass node)
        {
           //TODO something
            node.Body.Accept(this);
            return false;
        }

        override public bool Visit(AstClassBody node)
        {
            //TODO something
            return true;
        }

        override public bool Visit(AstVisibilityModifier node)
        {
            return false;
        }

        override public bool Visit(AstStaticModifier node)
        {
            return false;
        }

        override public bool Visit(AstClassField node)
        {
            codeStream.WriteLine("@" + node.Name.Id + " = " + GetLLVMVisibility(node.Visibility) + " global " + GetLLVMType(node.TypeDef.Id) + " " + GetDefaultTypeValue(node.TypeDef));
            classVariables.Add(node.Name.Id);
            return false;
        }

        override public bool Visit(AstClassMethod node)
        {
            ResetUnnamedVariable();
            codeStream.Write("define " + GetLLVMType(node.TypeDef.Id) + " @" + node.Name.Id.ToLower());
            currReturnType = GetLLVMType(node.TypeDef.Id);
            node.ArgumentsDefinition.Accept(this);
            node.StatementsBlock.Accept(this);
            codeStream.WriteLine("}");
            return false;
        }

        override public bool Visit(AstArgumentsDefList node)
        {
            codeStream.Write("(");
            List<string> LLWMArgDefList = new List<string>();
            foreach(var argument in node.ArgumentsDefinition)
            {
                LLWMArgDefList.Add(GetLLVMType(argument.TypeDef.Id) + " %" + argument.Name.Id);    
            }
            codeStream.Write(string.Join(",", LLWMArgDefList.ToArray()));
            codeStream.Write("){\n");
           // DefineLocalClassVariables();
            return false;
        }

        override public bool Visit(AstArgumentDef node)
        {            
            return false;
        }

        override public bool Visit(AstStatementsBlock node)
        {
           /* codeStream.Write("{\n");
            
            node.Statements.Accept(this);

            codeStream.Write("}\n");*/
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
            node.Expression.Accept(this);
            codeStream.WriteLine("\nret " + currReturnType + " " + GetCurrUnnamedVariable());
            return false;
        }

        override public bool Visit(AstIfStatement node)
        {
            return true;
        }

        override public bool Visit(AstAssignStatement node)
        {
            var symbolTableVariable = table.Lookup(node.Variable.Id);
            //codeStream.Write();
            if (classVariables.Contains(node.Variable.Id))
            {
                node.NewValue.Accept(this);

                codeStream.WriteLine("store " + GetLLVMType(symbolTableVariable.Type) + " " + GetCurrUnnamedVariable()
                 + ", " + GetLLVMType(symbolTableVariable.Type) + "* @" + node.Variable.Id);
                return false;
            }
            else
            {
                node.NewValue.Accept(this);
                codeStream.WriteLine("%" + node.Variable.Id + "= add " + GetLLVMType(symbolTableVariable.Type) + " 0, " + GetCurrUnnamedVariable());
                
                return false;
            }
        }

        override public bool Visit(AstBoolValueExpression node)
        {
           // codeStream.Write(" " + node.Value.ToString());
            return true;
        }

        override public bool Visit(AstIntegerValueExpression node)
        {
            //codeStream.WriteLine(CreateUnnamedVariable() + " = " + node.Value);
            codeStream.WriteLine(CreateUnnamedVariable() + " = add i32 0, " + node.Value);
            return true;
        }

        override public bool Visit(AstIdExpression node)
        {
            //symbolTable.Add("id", );
            var symbolTableVariable = table.Lookup(node.Id);
            if (classVariables.Contains(node.Id))
            {
                codeStream.WriteLine(CreateUnnamedVariable() + " = load " + GetLLVMType(symbolTableVariable.Type) + "* @" + node.Id);
                return true;
            }
            else
            {
                codeStream.WriteLine(CreateUnnamedVariable() + " = add " + GetLLVMType(symbolTableVariable.Type) + " 0, %" + node.Id);
                return true;
            }
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
            node.Left.Accept(this);
            string addLine = " = add i32 " + GetCurrUnnamedVariable() + ", ";
           // addLine = (CreateUnnamedVariable() + addLine);
            node.Right.Accept(this);
            addLine += GetCurrUnnamedVariable();
            codeStream.WriteLine(CreateUnnamedVariable() + addLine);
            
          //  codeStream.Write("\n");
            return false;
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
