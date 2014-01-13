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
        private Dictionary<string, uint> variableCounter = new Dictionary<string, uint>();

        //for function
        private Stack<List<string>> funcCallArgStack = new Stack<List<string>>();
        private Stack<string> currFuncCallArgStack = new Stack<string>();

        //test negative
        private bool IsNegative = false;
        private bool IsNot = false;

        //if counter
        private uint ifCount = 0;

        //while counter
        private uint whileCount = 0;

        //arr
        private bool arrAssign = false;
		private Stack<List<string>> m_indexVars = new Stack<List<string>>();
		private List<string> m_lastIndex;
		private List<string> m_currentIndex = new List<string>();

        //strings
        private int stringLiteralCounter = 0;
        private List<string> stringConstants;
        private Dictionary<int, int> stringLengths;

        private uint CreateWhileUse()
        {
            whileCount++;
            return whileCount;
        }

        private uint CreateIfUse()
        {
            ifCount++;
            return ifCount;
        }
        
        private void SaveArg(string arg)
        {
            currFuncCallArgStack.Push(arg);
			if(m_indexVars.Count != 0)
				m_currentIndex.Add(arg);
        }

        private string GetLastSavedArg()
        {
            string arg = currFuncCallArgStack.Pop();
            currFuncCallArgStack.Clear();
            return arg;
        }

        private List<string> GetCurrFuncArg()
        {
            return funcCallArgStack.Pop();
        }

        private void SaveArgInList(string argName)
        {
            if (funcCallArgStack.Count != 0)
            {
                funcCallArgStack.Peek().Add(argName);
            }
        }

        private string GetBoolLLVM(BoolValue value)
        {
            if (value == BoolValue.TRUE)
            {
                return "1";
            }
            return "0";
        }

        private void CallPrint()
        {
            codeStream.WriteLine(CreateUnnamedVariable() + " = getelementptr [3 x i8]* @.str, i64 0, i64 0");
            string strCallF = " = call i32 (i8 *, ...)* @printf(i8* " + GetCurrUnnamedVariable() + ", ";
            codeStream.Write(CreateUnnamedVariable() + strCallF);
            SaveArg("i32 " + GetCurrUnnamedVariable());
        }
        
        private void CallWriteString()
        {
            string strCallF = " = call i32 (i8 *, ...)* @printf(i8* " + GetCurrUnnamedVariable() + ", ";
            codeStream.Write(CreateUnnamedVariable() + strCallF);
            SaveArg("i32 " + GetCurrUnnamedVariable());
        }

        private void CallRead()
        {
            codeStream.WriteLine(CreateUnnamedVariable() + " = alloca i32");
            var tmpVar = GetCurrUnnamedVariable();
            codeStream.WriteLine(CreateUnnamedVariable() + " = getelementptr [3 x i8]* @.rstr, i64 0, i64 0");
            string strCallF = " = call i32 (i8 *, ...)* @scanf(i8* " + GetCurrUnnamedVariable() + ", i32* " + tmpVar + ")";
            codeStream.WriteLine(CreateUnnamedVariable() + strCallF);

            // TODO: if scanf returned not 1 -> print error and terminate program
            // GetCurrentUnnamedVariable() = icmp eq i32 4, 5
            // br i1 <cond>, label <iftrue>, label <iffalse>

            codeStream.WriteLine(CreateUnnamedVariable() + " = load i32* " + tmpVar);            
            SaveArg("i32 " + GetCurrUnnamedVariable());
        }

        private void CallRand()
        {
            var str = " = call i32 ()* @rand()";
            codeStream.WriteLine(CreateUnnamedVariable() + str);
            SaveArg("i32 " + GetCurrUnnamedVariable());
        }

        private void CallSpace()
        {
            codeStream.WriteLine(CreateUnnamedVariable() + " = getelementptr [2 x i8]* @.spacestr, i64 0, i64 0");
            string strCallF = " = call i32 (i8 *, ...)* @printf(i8* " + GetCurrUnnamedVariable();
            codeStream.WriteLine(CreateUnnamedVariable() + strCallF + ")");
            SaveArg("i32 " + GetCurrUnnamedVariable());
        }

        private void CallEndL()
        {
            codeStream.WriteLine(CreateUnnamedVariable() + " = getelementptr [2 x i8]* @.endlstr, i64 0, i64 0");
            string strCallF = " = call i32 (i8 *, ...)* @printf(i8* " + GetCurrUnnamedVariable();
            codeStream.WriteLine(CreateUnnamedVariable() + strCallF + ")");
            SaveArg("i32 " + GetCurrUnnamedVariable());
        }


		private void CallStrstr()
		{
			codeStream.Write(CreateUnnamedVariable() +	" = call i8* (i8*, i8*)* @strstr(" );
			SaveArg("i8* " + GetCurrUnnamedVariable());
		}


        private void GetLLVMBuilInFucntion(string target, string name)
        {
            switch (target)
            {
                case "Console":
                    switch (name)
                    {
                        case "WriteInt":
                            CallPrint();
                            return;
                        case "WriteBool":
                            CallPrint();
                            return;
                        case "ReadInt":
                            CallRead();
                            return; 
                        case "WriteSpace":
                            CallSpace();
                            return;
                        case "WriteLine":
                            CallEndL();
                            return;
                        case "WriteString":
                            CallWriteString();
                            return;
                        default:
                            throw new NotImplementedException();
                    }
                case "Math":
                    switch (name)
                    {
                        case "Rand":
                            CallRand();
                            return;
                        default:
                            throw new NotImplementedException();
                    }
				case "String":
					switch (name)
					{
						case "Strstr":
							CallStrstr();
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
                codeStream.WriteLine("%" + variable + " = load " + GetLLVMType(symbolTableVariable.Type) + "* @" + variable);
            }
        }

        private void CreateLLVMBuiltIn()
        {
            codeStream.WriteLine("@.spacestr = internal constant [2 x i8] c\"\\20\\00\"");
            codeStream.WriteLine("@.endlstr = internal constant [2 x i8] c\"\\0A\\00\"");
            codeStream.WriteLine("@.str = internal constant [3 x i8] c\"%d\\00\"");
            codeStream.WriteLine("@.rstr = internal constant [3 x i8] c\"%d\\00\"");
            codeStream.WriteLine("@.emptystr = internal constant [1 x i8] c\"\\00\"");
            codeStream.WriteLine("declare i32 @printf(i8 *, ...)");
            codeStream.WriteLine("declare i32 @scanf(i8*, ...)");
            codeStream.WriteLine("declare i32 @rand()");
            codeStream.WriteLine("declare void @srand(i32 *)");
            codeStream.WriteLine("declare i32 @time(i32 *)");
			codeStream.WriteLine("declare i8* @strstr(i8*, i8*)");
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
                case BuiltInTypes.STRING:
                    return "i8*";
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
                case BuiltInTypes.STRING:
                    return "null";
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
            stringLiteralCounter = 0;
            var findStringsVisitor = new FindAllStringConstantsVisitor();
            stringConstants = findStringsVisitor.FindAll(astRootNode);

            codeStream = new StreamWriter(outStream);
            codeStream.AutoFlush = true;
            astRootNode.Accept(this);
            codeStream.Flush();
            return true;
        }

        private void SaveStringConstants()
        {
            stringLengths = new Dictionary<int,int>();
            for (var i = 0; i < stringConstants.Count; ++i)
            {
                var str = stringConstants[i];
                SaveStringConstant(i, str);
            }
        }

        private void SaveStringConstant(int index, string str)
        {
            var name = GetStringConstantName(index);
            var length = str.Length + 1;
            var value = str + "\\00";
            var def = name + " = internal constant [" + length + " x i8] c\"" + value + "\"";
            codeStream.WriteLine(def);

            stringLengths[index] = length;
        }

        private string GetStringConstantName(int index)
        {
            return "@.strconst" + index;
        }

        override public bool Visit(AstProgram node)
        {
            table.UseGlobalScope();
            SaveStringConstants();
            CreateLLVMBuiltIn();
            return true;
        }

        override public bool Visit(AstClass node)
        {
            table.UseChildScope();
            node.Body.Accept(this);
            return false;
        }

        override public bool Visit(AstClassBody node)
        {
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
            var symbolTableElement = table.Lookup(node.Name.Id);
            if (Symbol.IsArray(symbolTableElement))
            {
                codeStream.WriteLine("@" + symbolTableElement.Name + " = " + GetLLVMVisibility(node.Visibility) + " global " +
					GetArrayId(symbolTableElement.Size as int[]) +
                  " zeroinitializer");
            }
            else
            {
                codeStream.WriteLine("@" + node.Name.Id + " = " + GetLLVMVisibility(node.Visibility) + " global "
					+ GetLLVMType(node.TypeDef.Id) + " " + GetDefaultTypeValue(node.TypeDef));
                classVariables.Add(node.Name.Id);
            }
            return false;
        }

		private string GetArrayId(int[] size)
		{
			string result = "i32";
			foreach (int dim in size)
			{
				result = string.Format("[{0} x {1}]", dim, result);
			}

			return result;
		}

        override public bool Visit(AstClassMethod node)
        {
            table.UseNamedChildScope(node.Name.Id);

            ResetUnnamedVariable();
            var isEntryPoint = node.Name.Id == "Main";
            var name = isEntryPoint ? "main" : node.Name.Id;
            codeStream.Write("define " + GetLLVMType(node.TypeDef.Id) + " @" + name);

            currReturnType = GetLLVMType(node.TypeDef.Id);
            node.ArgumentsDefinition.Accept(this);

            if (isEntryPoint)
            {
                InitStringFields();
                CallRandomize();
            }

            node.StatementsBlock.Accept(this);
            var type = GetLLVMType(node.TypeDef.Id);
            var fakeRetVal = GetDefaultTypeValue(node.TypeDef);
            codeStream.WriteLine("ret " + type + " " + fakeRetVal);//fake return for 
            codeStream.WriteLine("}");

            table.UseParentScope();

            return false;
        }

        private void InitStringFields()
        {
            foreach (var field in classVariables)
            {
                var s = table.Lookup(field);
                if (s.Type == BuiltInTypes.STRING)
                {
                    InitStringField(field);
                }
            }
        }

        private void InitStringField(string name)
        {
            codeStream.WriteLine(CreateUnnamedVariable() + " = getelementptr [1 x i8]* @.emptystr, i64 0, i64 0");
            codeStream.WriteLine("store  i8* " + GetCurrUnnamedVariable() + ", i8** @" + name);
        }

        private void CallRandomize()
        {
            // srand(time(null)
            // %1 = call i32 (i32 *) @time(i32 * null)
            // call void (i32 *) @srand(%1) 
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
            funcCallArgStack.Push(new List<string>());
            var symbolFunc = table.LookupFunction(node.Name.Id);
            node.CallArgs.Accept(this);
            codeStream.Write(CreateUnnamedVariable() + " = call " + GetLLVMType(symbolFunc.Type) + " @" + symbolFunc.Name + "(");      
            codeStream.Write(string.Join(",", GetCurrFuncArg().ToArray()));
            codeStream.WriteLine(")");
            SaveArg(GetLLVMType(symbolFunc.Type) + " " + GetCurrUnnamedVariable());
            return false;
        }

        override public bool Visit(AstThisMethodCallStatement node)
        {
            return true;
        }

        override public bool Visit(AstExternalMethodCallExpression node)
        {
            funcCallArgStack.Push(new List<string>());
            var symbolFunc = table.LookupFunction(node.Target.Id + node.Name.Id);

            if (symbolFunc.ArgumentTypes.Count != 0)
            {
                node.CallArgs.Accept(this);
                GetLLVMBuilInFucntion(node.Target.Id, node.Name.Id);
                codeStream.Write(string.Join(",", GetCurrFuncArg().ToArray()));
                codeStream.WriteLine(")");
                
                return false;
            }
            else
            {
                GetLLVMBuilInFucntion(node.Target.Id, node.Name.Id);   
            }
            return false;
        }

        override public bool Visit(AstExternalMethodCallStatement node)
        {
            return true;
        }

        override public bool Visit(AstReturnStatement node)
        {
            node.Expression.Accept(this);
            codeStream.WriteLine("ret " + currReturnType + " " + GetCurrUnnamedVariable());
            CreateUnnamedVariable();
            return false;
        }

        override public bool Visit(AstIfStatement node)
        {
            node.Condition.Accept(this);
            string currIf = CreateIfUse().ToString();

            var condExprResult = GetCurrUnnamedVariable();
            codeStream.WriteLine(CreateUnnamedVariable() + " = icmp eq i1 1, " + condExprResult);
            codeStream.WriteLine("br i1 " + GetCurrUnnamedVariable() + ", label %then" + currIf + ", label %else" + currIf);
            codeStream.WriteLine("then" + currIf.ToString() + ":");
            node.ThenBlock.Statements.Accept(this);
            codeStream.WriteLine("br label %endif" + currIf);
            codeStream.WriteLine("else" + currIf + ":");
            node.ElseBlock.Accept(this);
            codeStream.WriteLine("br label %endif" + currIf);
            codeStream.WriteLine("endif" + currIf + ":");
            return false;
        }

        override public bool Visit(AstAssignStatement node)
        {
            var symbolTableVariable = table.Lookup(node.Variable.Id);
            node.NewValue.Accept(this);
            if (Symbol.IsArray(symbolTableVariable))
            {
                arrAssign = true;
                var newValueVar = GetCurrUnnamedVariable();
                node.Variable.Accept(this);
                var index = GetCurrUnnamedVariable();
                codeStream.WriteLine("store i32 " + newValueVar + ", "
                    + GetLLVMType(symbolTableVariable.Type) + "* " + GetCurrUnnamedVariable());
                arrAssign = false;
            }
            else if (classVariables.Contains(node.Variable.Id))
            {
                codeStream.WriteLine("store " + GetLLVMType(symbolTableVariable.Type) + " " + GetCurrUnnamedVariable()
                 + ", " + GetLLVMType(symbolTableVariable.Type) + "* @" + node.Variable.Id);
                
            }
            else if (symbolTableVariable.Type == BuiltInTypes.STRING)
            {
            }
            else
            {
                UseVaribaleCatched(node.Variable.Id);

                var name = GetCurrVariableState(node.Variable.Id);
                var type = GetLLVMType(symbolTableVariable.Type);
                codeStream.WriteLine("%" + name + " = add " + type + " 0, " + GetCurrUnnamedVariable());
            }
          
            return false;
        }

        override public bool Visit(AstBoolValueExpression node)
        {
            if (IsNot)
            {
                codeStream.WriteLine(CreateUnnamedVariable() + " = xor i1 1, " + GetBoolLLVM(node.Value));
            }
            else
            {
                codeStream.WriteLine(CreateUnnamedVariable() + " = add i1 0, " + GetBoolLLVM(node.Value));
            }
            SaveArg("i8 " + GetCurrUnnamedVariable());
            return true;
        }

        override public bool Visit(AstIntegerValueExpression node)
        {
            string saveOperation = "add ";
            if (IsNegative)
            {
                saveOperation = "sub ";
            }
            codeStream.WriteLine(CreateUnnamedVariable() + " = " + saveOperation + " i32 0, " + node.Value);
            SaveArg("i32 " + GetCurrUnnamedVariable());
            return true;
        }

        public override bool Visit(AstStringLiteralExpression node)
        {
            var name = GetStringConstantName(stringLiteralCounter);
            var length = this.stringLengths[stringLiteralCounter];
            var op = "getelementptr [" + length + " x i8]* " + name + ", i64 0, i64 0";

            codeStream.WriteLine(CreateUnnamedVariable() + " = " + op);
            SaveArg("i8* " + GetCurrUnnamedVariable());

            ++stringLiteralCounter;

            return true;
        }

        override public bool Visit(AstIdExpression node)
        {
            var symbolTableVariable = table.Lookup(node.Id);
            var type = symbolTableVariable.Type;
            var llvmType = GetLLVMType(symbolTableVariable.Type);
            if (classVariables.Contains(node.Id))
            {
                codeStream.WriteLine(CreateUnnamedVariable() + " = load " + llvmType + "* @" + node.Id);
                if (IsNegative)
                {
                    string positiveVar = GetCurrUnnamedVariable();
                    codeStream.WriteLine(CreateUnnamedVariable() + " = sub " + llvmType + " 0, " + positiveVar);
                }
                if (IsNot)
                {
                    string inverseVal = GetCurrUnnamedVariable();
                    codeStream.WriteLine(CreateUnnamedVariable() + " = xor " + llvmType + " " + inverseVal + ", 1");
                }
            }
            else if (type == BuiltInTypes.STRING)
            {
                codeStream.WriteLine(CreateUnnamedVariable() + " = alloca i8*");
                var id = GetCurrUnnamedVariable();
                codeStream.WriteLine("store i8* %" + GetCurrVariableState(node.Id) + ", i8** " + GetCurrUnnamedVariable());
                codeStream.WriteLine(CreateUnnamedVariable() + " = load i8** " + id);
            }
            else
            {
                if (IsNot)
                {
                    codeStream.WriteLine(CreateUnnamedVariable() + " = xor " + llvmType + " %" + GetCurrVariableState(node.Id) + ", 0");
                }
                else
                {
                    string saveOperation = "add ";
                    if (IsNegative)
                    {
                        saveOperation = "sub ";
                    }
                    codeStream.WriteLine(CreateUnnamedVariable() + " = " + saveOperation + llvmType + " 0, %" + GetCurrVariableState(node.Id));
                }
            }

            SaveArg(llvmType + " " + GetCurrUnnamedVariable());

            return true;
        }

        override public bool Visit(AstArgumentsCallList node)
        {
            foreach (var arg in node.Arguments)
            {
                arg.Expr.Accept(this);
                SaveArgInList(GetLastSavedArg());

            }
            return false;
        }

        override public bool Visit(AstCallArgument node)
        {
            return true;
        }
        
        override public bool Visit(AstMulExpression node)
        {
            node.Left.Accept(this);
            string addLine = " = mul i32 " + GetCurrUnnamedVariable() + ", ";
            node.Right.Accept(this);
            addLine += GetCurrUnnamedVariable();
            codeStream.WriteLine(CreateUnnamedVariable() + addLine);
            SaveArg("i32 " + GetCurrUnnamedVariable());
            return false;
        }

        override public bool Visit(AstModExpression node)
        {
            node.Left.Accept(this);
            string addLine = " = urem i32 " + GetCurrUnnamedVariable() + ", ";
            node.Right.Accept(this);
            addLine += GetCurrUnnamedVariable();
            codeStream.WriteLine(CreateUnnamedVariable() + addLine);
            SaveArg("i32 " + GetCurrUnnamedVariable());
            return false;
        }

        override public bool Visit(AstDivExpression node)
        {
            node.Left.Accept(this);
            string addLine = " = sdiv i32 " + GetCurrUnnamedVariable() + ", ";
            node.Right.Accept(this);
            addLine += GetCurrUnnamedVariable();
            codeStream.WriteLine(CreateUnnamedVariable() + addLine);
            SaveArg("i32 " + GetCurrUnnamedVariable());
            return false;
        }

        override public bool Visit(AstAddExpression node)
        {
            node.Left.Accept(this);
            string addLine = " = add i32 " + GetCurrUnnamedVariable() + ", ";
            node.Right.Accept(this);
            addLine += GetCurrUnnamedVariable();
            codeStream.WriteLine(CreateUnnamedVariable() + addLine);
            SaveArg("i32 " + GetCurrUnnamedVariable());
            return false;
        }

        override public bool Visit(AstSubExpression node)
        {
            node.Left.Accept(this);
            string addLine = " = sub i32 " + GetCurrUnnamedVariable() + ", ";
            node.Right.Accept(this);
            addLine += GetCurrUnnamedVariable();
            codeStream.WriteLine(CreateUnnamedVariable() + addLine);

            SaveArg("i32 " + GetCurrUnnamedVariable());
            return false;
        }

        override public bool Visit(AstNegateUnaryExpr node)
        {
            IsNegative = true;
            return true;
        }

        override public bool Visit(AstSimpleUnaryExpr node)
        {
            IsNegative = false;
            return true;
        }

        override public bool Visit(AstSimpleTermExpr node)
        {
            return true;
        }


        public override bool Visit(AstOrExpression node)
        {
            node.Left.Accept(this);
            string addLine = " = or i1 " + GetCurrUnnamedVariable() + ", ";
            node.Right.Accept(this);
            addLine += GetCurrUnnamedVariable();
            codeStream.WriteLine(CreateUnnamedVariable() + addLine);
            SaveArg("i1 " + GetCurrUnnamedVariable());
            return false;
        }

        public override bool Visit(AstAndExpression node)
        {
            node.Left.Accept(this);
            string addLine = " = and i1 " + GetCurrUnnamedVariable() + ", ";
            node.Right.Accept(this);
            addLine += GetCurrUnnamedVariable();
            codeStream.WriteLine(CreateUnnamedVariable() + addLine);
            SaveArg("i1 " + GetCurrUnnamedVariable());
            return false;
        }

        public override bool Visit(AstNotExpression node)
        {
            IsNot = true;
            node.Expr.Accept(this);
            IsNot = false;
            return false;
        }

        public override bool Visit(AstWhileStatement node)
        {
            string currWhileIndex = CreateWhileUse().ToString(); ;
            codeStream.WriteLine("br label %whilecond" + currWhileIndex);
            codeStream.WriteLine("whilestart" + currWhileIndex + ":");
            node.Statements.Accept(this);
            codeStream.WriteLine("br label %whilecond" + currWhileIndex);
            codeStream.WriteLine("whilecond" + currWhileIndex + ":");
            node.Condition.Accept(this);
            var condExprResult = GetCurrUnnamedVariable();
            codeStream.WriteLine(CreateUnnamedVariable() + " = icmp eq i1 1, " + condExprResult);
            codeStream.WriteLine("br i1 " + GetCurrUnnamedVariable() + ", label %whilestart" + currWhileIndex + ", label %endwhile" + currWhileIndex);
            codeStream.WriteLine("endwhile" + currWhileIndex + ":");
            //codeStream
            return false;
        }


        public override bool Visit(AstLtComparison node)
        {
            node.Left.Accept(this);
            string addLine = " = icmp slt i32 " + GetCurrUnnamedVariable() + ", ";
            node.Right.Accept(this);
            addLine += GetCurrUnnamedVariable();
            codeStream.WriteLine(CreateUnnamedVariable() + addLine);
            SaveArg("i1 " + GetCurrUnnamedVariable());
            return false;
        }
        public override bool Visit(AstGtComparison node)
        {
            node.Left.Accept(this);
            string addLine = " = icmp sgt i32 " + GetCurrUnnamedVariable() + ", ";
            node.Right.Accept(this);
            addLine += GetCurrUnnamedVariable();
            codeStream.WriteLine(CreateUnnamedVariable() + addLine);
            SaveArg("i1 " + GetCurrUnnamedVariable());
            return false;
        }

        public override bool Visit(AstLteComparison node)
        {
            node.Left.Accept(this);
            string addLine = " = icmp sle i32 " + GetCurrUnnamedVariable() + ", ";
            node.Right.Accept(this);
            addLine += GetCurrUnnamedVariable();
            codeStream.WriteLine(CreateUnnamedVariable() + addLine);
            SaveArg("i1 " + GetCurrUnnamedVariable());
            return false;
        }

        public override bool Visit(AstGteComparison node)
        {
            node.Left.Accept(this);
            string addLine = " = icmp sge i32 " + GetCurrUnnamedVariable() + ", ";
            node.Right.Accept(this);
            addLine += GetCurrUnnamedVariable();
            codeStream.WriteLine(CreateUnnamedVariable() + addLine);
            SaveArg("i1 " + GetCurrUnnamedVariable());
            return false;
        }

        public override bool Visit(AstEqualComparison node)
        {
            node.Left.Accept(this);
            string addLine = " = icmp eq i32 " + GetCurrUnnamedVariable() + ", ";
            node.Right.Accept(this);
            addLine += GetCurrUnnamedVariable();
            codeStream.WriteLine(CreateUnnamedVariable() + addLine);
            SaveArg("i1 " + GetCurrUnnamedVariable());
            return false;
        }

        public override bool Visit(AstNotEqualComparison node)
        {
            node.Left.Accept(this);
            string addLine = " = icmp ne i32 " + GetCurrUnnamedVariable() + ", ";
            node.Right.Accept(this);
            addLine += GetCurrUnnamedVariable();
            codeStream.WriteLine(CreateUnnamedVariable() + addLine);
            SaveArg("i1 " + GetCurrUnnamedVariable());
            return false;
        }

        public override bool Visit(AstIdArrayExpression node)
        {
            node.Index.Accept(this);
			StringBuilder arrIndex = new StringBuilder();
			foreach (string s in m_lastIndex)
				arrIndex.AppendFormat(", {0} ", s);

            var tableSymbol = table.Lookup(node.Id);
            codeStream.WriteLine(CreateUnnamedVariable() + " = getelementptr " + GetArrayId(tableSymbol.Size as int[]) + 
				"* @" + node.Id +
                 " , i32 0 " + arrIndex);
            if (!arrAssign)
            {
				codeStream.WriteLine(CreateUnnamedVariable() + " = getelementptr " + GetArrayId(tableSymbol.Size as int[]) +
					"* @" + node.Id +
                    " , i32 0 " + arrIndex);
                string strLoad = " = load i32* " + GetCurrUnnamedVariable();
                codeStream.WriteLine(CreateUnnamedVariable() + strLoad);
                SaveArg("i32 " + GetCurrUnnamedVariable());
            }
            return false;
        }


        public override bool Visit(AstArrayInitializerStatement node)
        {
            var tableSymbol = table.Lookup(node.Id.Id);
            List<int> valueList = node.GetValuesList();
            for (var i = 0; i < valueList.Count(); i++)
            {
                codeStream.WriteLine(CreateUnnamedVariable() + " = getelementptr " + GetArrayId(tableSymbol.Size as int[]) + "* @" +
                tableSymbol.Name + ", i32 0, i32 " + i.ToString());
                codeStream.WriteLine("store i32 " + valueList[i].ToString() + ", i32* " + GetCurrUnnamedVariable());
            }

            return false;
        }

		public override bool Visit(AstIntegerListExpression node)
		{
			return true;
		}

		public override bool Visit(AstExpressionList node)
		{
			m_indexVars.Push(new List<string>());
			foreach (var e in node.Expr)
			{
				e.Accept(this);
				m_indexVars.Peek().Add(m_currentIndex.Last());
				m_currentIndex.Clear();
			}

			m_lastIndex = m_indexVars.Pop();

			return true;
		}

		public override bool Visit(AstArrayIndex node)
		{
			// TODO
			return true;
		}
	}
}
