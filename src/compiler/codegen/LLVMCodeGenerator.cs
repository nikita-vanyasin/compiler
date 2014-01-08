﻿using System;
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
        private Dictionary<string, uint> variableCounter = new Dictionary<string, uint>();

        //for function
        private bool inFunc = false;
        private List<string> tmpVariablesArgCallList = new List<string>();
        private List<string> currExprCallTempraryVars = new List<string>();

        private void GetLLVMBuilInFucntion(string target, string name)
        {
            switch (target)
            {
                case "Console":
                    switch (name)
                    {
                        case "WriteInt":
                            codeStream.WriteLine(CreateUnnamedVariable() + "= getelementptr [4 x i8]* @.str, i64 0, i64 0");
                            string strCallF =  "= call i32 (i8 *, ...)* @printf(i8* " + GetCurrUnnamedVariable() + ", ";
                            codeStream.Write(CreateUnnamedVariable() + strCallF);
                            return;
                        default:
                            throw new NotImplementedException();
                    }
                default:
                    throw new NotImplementedException();
            }
        }

        private string GetCurrVariableState(string variable)
        {
            if (variableCounter[variable] == 0)
            {
                return variable;
            }
            return variable + "." + variableCounter[variable];
        }

        private void UseVaribaleCatched(string variable)
        {
            variableCounter[variable] += 1;
        }


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
            codeStream.WriteLine("@.str = internal constant [4 x i8] c\"%d\\0A\\00\"");
            codeStream.WriteLine("declare i32 @printf(i8 *, ...)");

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
                case BuiltInTypes.INT:
                    return "i32";
                case BuiltInTypes.BOOL:
                    return "i1";
                case BuiltInTypes.VOID:
                    return "void";
                default:
                    throw new NotImplementedException();
            }
        }

        private string GetDefaultTypeValue(AstIdExpression node)
        {
            switch (node.Id)
            {
                case BuiltInTypes.INT:
                    return "0";
                case BuiltInTypes.BOOL:
                    return "0";
                case BuiltInTypes.VOID:
                    return "";
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
            variableCounter = new Dictionary<string, uint>();
            List<string> LLWMArgDefList = new List<string>();
            foreach(var argument in node.ArgumentsDefinition)
            {
                LLWMArgDefList.Add(GetLLVMType(argument.TypeDef.Id) + " %" + argument.Name.Id);
                variableCounter[argument.Name.Id] = 0;
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
            return true;
        }

        override public bool Visit(AstStatementsList node)
        {
            return true;
        }

        override public bool Visit(AstThisMethodCallExpression node)
        {
            inFunc = true;
            tmpVariablesArgCallList = new List<string>();
            node.CallArgs.Accept(this);
            var symbolFunc = table.LookupFunction(node.Name.Id);
            codeStream.Write(CreateUnnamedVariable() + " = call " + GetLLVMType(symbolFunc.Type) + " @" + symbolFunc.Name + "(");
            codeStream.Write(string.Join(",", tmpVariablesArgCallList.ToArray()));
            codeStream.WriteLine(")");
            inFunc = false;
            return false;
        }

        override public bool Visit(AstThisMethodCallStatement node)
        {
            return true;
        }

        override public bool Visit(AstExternalMethodCallExpression node)
        {
            inFunc = true;
            tmpVariablesArgCallList = new List<string>();
            node.CallArgs.Accept(this);
            GetLLVMBuilInFucntion(node.Target.Id, node.Name.Id);
            codeStream.Write(string.Join(",", tmpVariablesArgCallList.ToArray()));
            codeStream.WriteLine(")");
            inFunc = false;
            return false;
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
                
            }
            else
            {
                node.NewValue.Accept(this);
                UseVaribaleCatched(node.Variable.Id);
                codeStream.WriteLine("%" + GetCurrVariableState(node.Variable.Id) + "= add " + GetLLVMType(symbolTableVariable.Type) + " 0, " + GetCurrUnnamedVariable());
                
            
            }
          
            return false;
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
            if (inFunc)
            {
                currExprCallTempraryVars.Add("i32 " + GetCurrUnnamedVariable());
            }
            return true;
        }

        override public bool Visit(AstIdExpression node)
        {
            //symbolTable.Add("id", );
            var symbolTableVariable = table.Lookup(node.Id);
            if (classVariables.Contains(node.Id))
            {
                codeStream.WriteLine(CreateUnnamedVariable() + " = load " + GetLLVMType(symbolTableVariable.Type) + "* @" + node.Id);
            }
            else
            {
                codeStream.WriteLine(CreateUnnamedVariable() + " = add " + GetLLVMType(symbolTableVariable.Type) + " 0, %" + GetCurrVariableState(node.Id));
            }
            if (inFunc)
            {
                currExprCallTempraryVars.Add(GetLLVMType(symbolTableVariable.Type) + " " + GetCurrUnnamedVariable());
            }

            return true;
        }

        override public bool Visit(AstArgumentsCallList node)
        {
            foreach (var arg in node.Arguments)
            {
                currExprCallTempraryVars = new List<string>();
                arg.Expr.Accept(this);
                tmpVariablesArgCallList.Add(currExprCallTempraryVars.Last());
            }
            return false;
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
            if (inFunc)
            {
                currExprCallTempraryVars.Add("i32 " + GetCurrUnnamedVariable());
            }
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
