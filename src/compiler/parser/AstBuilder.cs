using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;

namespace compiler
{
    class AstBuilder
    {
        private AstProgram rootNode;
        private Stack<AstNode> nodes;

        // http://en.wikipedia.org/wiki/Shunting-yard_algorithm
        public AstBuilder()
        {
            rootNode = null;
            nodes = new Stack<AstNode>();
        }

        public AstProgram GetRootNode()
        {
            return rootNode;
        }

        public void CallAction(string methodName)
        {
            if (methodName.Length < 1)
            {
                return;
            }

            var methodInfo = typeof(AstBuilder).GetMethod(methodName, BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance);
            if (methodInfo == null)
            {
                throw new MissingMethodException("Action '" + methodName + "' is not declared! Fix the table.");
            }

            methodInfo.Invoke(this, new Object[] { });
        }

        public void AddAstIdNode(Token t)
        {
            var node = new AstIdExpression(t.Attribute);
            nodes.Push(node);
        }

        public void AddAstIntegerValueNode(Token t)
        {
            var node = new AstIntegerValueExpression(t.Attribute);
            nodes.Push(node);
        }

        // #PS_PROGRAM #CLASS_DEF EOF
        private void ConstructPsProgram()
        {
            var astClass = nodes.Pop() as AstClass;
            var program = new AstProgram(astClass);
            rootNode = program;

            if (nodes.Count > 0)
            {
                throw new Exception("bad code detected.");
            }
        }
        
        // #CLASS_DEF CLASS ID #CLASS_BODY
        private void ConstructClass()
        {
            var classBody = nodes.Pop() as AstClassBody;
            var id = nodes.Pop() as AstIdExpression;

            var klass = new AstClass(id, classBody);
            nodes.Push(klass);
        }

        // #CLASS_BODY BLOCK_START #CLASS_DECLARATIONS BLOCK_END
        private void ConstructClassBody()
        {
            var fieldsList = new List<AstClassField>();
            var methodsList = new List<AstClassMethod>();

            AstNode curr = nodes.Peek() as AstNode;
            while (curr is AstClassField || curr is AstClassMethod)
            {
                if (curr is AstClassField)
                {
                    fieldsList.Insert(0, curr as AstClassField);
                }
                else
                {
                    methodsList.Insert(0, curr as AstClassMethod);
                }
                nodes.Pop();
                curr = nodes.Peek() as AstNode;
            }

            var classBody = new AstClassBody(fieldsList, methodsList);
            nodes.Push(classBody);
        }

        // #CLASS_DECLARATIONS #FIELD_DECLARATION #CLASS_DECLARATIONS
        private void ConstructClassFieldDeclarations()
        {
        }

        // #CLASS_DECLARATIONS #METHOD_DECLARATION #CLASS_DECLARATIONS
        private void ConstructClassMethodDeclarations()
        {
        }

        // #CLASS_DECLARATIONS
        private void ConstructEmptyClassDeclarations()
        {
        }

        // #FIELD_DECLARATION #VISIBILITY_MODIFIER #STATIC_MODIFIER #TYPE_DEFINITION ID LINE_END
        private void ConstructFieldDeclaration()
        {
            var name = nodes.Pop() as AstIdExpression;
            var typeDef = nodes.Pop() as AstIdExpression;
            var staticModifier = nodes.Pop() as AstStaticModifier;
            var visibilityModifier = nodes.Pop() as AstVisibilityModifier;
            
            var fieldDeclaration = new AstClassField(visibilityModifier, staticModifier, typeDef, name);
            nodes.Push(fieldDeclaration);
        }

        // #VISIBILITY_MODIFIER PUBLIC
        private void ConstructPublicVisibilityModifier()
        {
            var mod = new AstVisibilityModifier(VisibilityModifier.PUBLIC);
            nodes.Push(mod);
        }

        // #VISIBILITY_MODIFIER PRIVATE
        private void ConstructPrivateVisibilityModifier()
        {
            var mod = new AstVisibilityModifier(VisibilityModifier.PRIVATE);
            nodes.Push(mod);
        }

        // #STATIC_MODIFIER STATIC
        private void ConstructStaticModifier()
        {
            var mod = new AstStaticModifier(StaticModifier.STATIC);
            nodes.Push(mod);
        }

        // #STATIC_MODIFIER
        private void ConstructEmptyStaticModifier()
        {
            var mod = new AstStaticModifier(StaticModifier.INSTANCE);
            nodes.Push(mod);
        }

        // #TYPE_DEFINITION BOOL
        private void ConstructBoolTypeDefinition()
        {
            var typeDef = new AstIdExpression("bool");
            nodes.Push(typeDef);
        }

        // #TYPE_DEFINITION CHAR
        private void ConstructCharTypeDefinition()
        {
            var typeDef = new AstIdExpression("char");
            nodes.Push(typeDef);
        }

        // #TYPE_DEFINITION INT
        private void ConstructIntTypeDefinition()
        {
            var typeDef = new AstIdExpression("int");
            nodes.Push(typeDef);
        }
        
        // #METHOD_DECLARATION #METHOD_HEADER BLOCK_START #STATEMENTS_BLOCK BLOCK_END
        private void ConstructMethodDeclaration()
        {
            var statementsBlock = nodes.Pop() as AstStatementsBlock;
            
            var argumentsDefList = nodes.Pop() as AstArgumentsDefList;            
            var name = nodes.Pop() as AstIdExpression;
            var typeDef = nodes.Pop() as AstIdExpression;
            var staticMod = nodes.Pop() as AstStaticModifier;
            var visibilityMod = nodes.Pop() as AstVisibilityModifier;
            
            var method = new AstClassMethod(
                visibilityMod,
                staticMod,
                typeDef,
                name,
                argumentsDefList,
                statementsBlock
            );
            nodes.Push(method);
        }

        // #METHOD_HEADER #VISIBILITY_MODIFIER #STATIC_MODIFIER #TYPE_DEFINITION ID #METHOD_ARGS
        private void ConstructMethodHeader()
        {
        }

        // #METHOD_ARGS LEFT_PAREN #ARGUMENTS_DEFINITION RIGHT_PAREN
        private void ConstructMethodArgs()
        {
            var argumentsDefList = new List<AstArgumentDef>();
            var curr = nodes.Peek() as AstNode;
            while (curr is AstArgumentDef)
            {
                nodes.Pop();
                argumentsDefList.Insert(0, curr as AstArgumentDef);
                curr = nodes.Peek() as AstNode;
            }

            var argsDefList = new AstArgumentsDefList(argumentsDefList);
            nodes.Push(argsDefList);
        }

        // #METHOD_ARGS LEFT_PAREN RIGHT_PAREN
        private void ConstructEmptyMethodArgs()
        {
            var argsDefList = new AstArgumentsDefList(new List<AstArgumentDef>());
            nodes.Push(argsDefList);
        }

        // #ARGUMENTS_DEFINITION #ARGUMENT_DEFINITION
        private void ConstructArgumentsDefinition()
        {
        }

        // #ARGUMENTS_DEFINITION #ARGUMENT_DEFINITION COMMA #ARGUMENTS_DEFINITION
        /*private void ConstructArgumentsDefinition()
        {
        }
        */

        // #ARGUMENT_DEFINITION #TYPE_DEFINITION ID
        private void ConstructArgumentDefinition()
        {
            var argName = nodes.Pop() as AstIdExpression;
            var typeDef = nodes.Pop() as AstIdExpression;
            var argDef = new AstArgumentDef(typeDef, argName);

            nodes.Push(argDef);
        }

        // #STATEMENTS_BLOCK #STATEMENTS
        private void ConstructStatementsBlock()
        {
            var astStatementsList = nodes.Pop() as AstStatementsList;

            var block = new AstStatementsBlock(astStatementsList);
            nodes.Push(block);
        }

        // #STATEMENTS_BLOCK PASS LINE_END
        private void ConstructPassStatementsBlock()
        {
            var statementsList = new List<AstStatement>();
            var astStatementsList = new AstStatementsList(statementsList);
            var block = new AstStatementsBlock(astStatementsList);
            nodes.Push(block);
        }

        // #STATEMENTS #STATEMENT #STATEMENTS_S
        private void ConstructStatements()
        {
            var list = nodes.Pop() as AstStatementsList;
            var oneMore = nodes.Pop() as AstStatement;

            list.Statements.Insert(0, oneMore);
            nodes.Push(list);
        }

        // #STATEMENTS_S #STATEMENTS
        private void ConstructStatementsS()
        {
        }

        // #STATEMENTS_S
        private void ConstructStatementsEmptyS()
        {
            nodes.Push(new AstStatementsList(new List<AstStatement>()));
        }

        // #STATEMENT ////////////////
        private void ConstructStatement()
        {
        }

        // #STATEMENT #FUNC_CALL LINE_END
        private void ConstructFuncCallStatement()
        {
            var funcCallExpr = nodes.Pop() as AstFunctionCallExpression;
            if (funcCallExpr is AstThisMethodCallExpression)
            {
                var stmt = new AstThisMethodCallStatement(funcCallExpr as AstThisMethodCallExpression);
                nodes.Push(stmt);
            }
            else if (funcCallExpr is AstExternalMethodCallExpression)
            {
                var stmt = new AstExternalMethodCallStatement(funcCallExpr as AstExternalMethodCallExpression);
                nodes.Push(stmt);
            }
            else
            {
                throw new Exception("bad code detected again.");
            }
        }

        // #STATEMENT RETURN #EXPRESSION LINE_END
        private void ConstructReturnStatement()
        {
            var expr = nodes.Pop() as AstExpression;
            var returnStmt = new AstReturnStatement(expr);
            nodes.Push(returnStmt);
        }

        // #ASSIGN_STATEMENT ID ASSIGNMENT #EXPRESSION
        private void ConstructAssignStatement()
        {
            var expr = nodes.Pop() as AstExpression;
            var variable = nodes.Pop() as AstIdExpression;
            var assignStmt = new AstAssignStatement(variable, expr);
            nodes.Push(assignStmt);
        }

        // #IF_STATEMENT #IF_THEN_STATEMENT
        private void ConstructIfStatement()
        {
        }

        // #IF_STATEMENT #IF_THEN_STATEMENT ELSE BLOCK_START #STATEMENTS_BLOCK BLOCK_END
        private void ConstructIfElseStatement()
        {
            var elseBlock = nodes.Pop() as AstStatementsBlock;
            var ifStmt = nodes.Pop() as AstIfStatement;

            var thenElseStmt = new AstIfStatement(
                ifStmt.Condition,
                ifStmt.ThenBlock,
                elseBlock
            );

            nodes.Push(thenElseStmt);
        }

        // #IF_THEN_STATEMENT IF LEFT_PAREN #OR_TEST RIGHT_PAREN BLOCK_START #STATEMENTS_BLOCK BLOCK_END
        private void ConstructIfThenStatement()
        {
            var thenBlock = nodes.Pop() as AstStatementsBlock;
            var orTest = nodes.Pop() as AstExpression;

            var elseBlock = new AstStatementsBlock(new AstStatementsList(new List<AstStatement>()));
            var ifThenStmt = new AstIfStatement(orTest, thenBlock, elseBlock);
            nodes.Push(ifThenStmt);
        }

        // #EXPRESSION (#TERM|#ADD_EXPRESSION|#SUB_EXPRESSION)
        private void ConstructExpression()
        {
        }


        // #TERM (#UNARY_EXPRESSION|#MUL_EXPRESSION|#DIV_EXPRESSION|#MOD_EXPRESSION)
        private void ConstructTerm()
        {
        }

        // #MUL_EXPRESSION #UNARY_EXPRESSION MULTIPLICATION #TERM
        private void ConstructMulExpr()
        {
            var right = nodes.Pop() as AstExpression;
            var left = nodes.Pop() as AstUnaryExpr;

            var mulExpr = new AstMulExpression(left, right);
            nodes.Push(mulExpr);
        }

        // #DIV_EXPRESSION #UNARY_EXPRESSION DIV #TERM
        private void ConstructDivExpr()
        {
            var right = nodes.Pop() as AstExpression;
            var left = nodes.Pop() as AstUnaryExpr;

            var mulExpr = new AstDivExpression(left, right);
            nodes.Push(mulExpr);
        }

        // #MOD_EXPRESSION #UNARY_EXPRESSION MOD #TERM
        private void ConstructModExpr()
        {
            var right = nodes.Pop() as AstExpression;
            var left = nodes.Pop() as AstUnaryExpr;

            var mulExpr = new AstModExpression(left, right);
            nodes.Push(mulExpr);
        }

        // #UNARY_EXPRESSION MINUS #SIMPLE_TERM
        private void ConstructNegateUnaryExpr()
        {
            var simpleTerm = nodes.Pop() as AstSimpleTermExpr;
            var unExpr = new AstNegateUnaryExpr(simpleTerm);
            nodes.Push(unExpr);
        }

        // #UNARY_EXPRESSION #SIMPLE_TERM
        private void ConstructUnaryExpr()
        {
            var simpleTerm = nodes.Pop() as AstSimpleTermExpr;
            var unExpr = new AstSimpleUnaryExpr(simpleTerm);
            nodes.Push(unExpr);
        }

        private void ConstructSimpleTerm()
        {
            var expr = nodes.Pop() as AstExpression;
            var simpleTerm = new AstSimpleTermExpr(expr);
            nodes.Push(simpleTerm);
        }

        // #ADD_EXPRESSION #EXPRESSION PLUS #TERM
        private void ConstructAddExpr()
        {
            var right = nodes.Pop() as AstExpression;
            var left = nodes.Pop() as AstExpression;

            var expr = new AstAddExpression(left, right);
            nodes.Push(expr);
        }

        // #SUB_EXPRESSION #EXPRESSION MINUS #TERM
        private void ConstructSubExpr()
        {
            var right = nodes.Pop() as AstExpression;
            var left = nodes.Pop() as AstExpression;

            var expr = new AstSubExpression(left, right);
            nodes.Push(expr);
        }
        
        // #FUNC_CALL #THIS_METHOD_CALL
        private void ConstructFuncCall()
        {
        }

        // #FUNC_CALL #EXTERNAL_METHOD_CALL
        /*private void ConstructFuncCall()
        {
        }
        */

        // #THIS_METHOD_CALL ID #CALL_ARGS
        private void ConstructThisMethodCall()
        {
            var callArgs = nodes.Pop() as AstArgumentsCallList;
            var name = nodes.Pop() as AstIdExpression;

            var thisMethodCall = new AstThisMethodCallExpression(name, callArgs);
            nodes.Push(thisMethodCall);
        }

        // #EXTERNAL_METHOD_CALL ID DOT ID #CALL_ARGS
        private void ConstructExternalMethodCall()
        {
            var callArgs = nodes.Pop() as AstArgumentsCallList;
            var name = nodes.Pop() as AstIdExpression;
            var target = nodes.Pop() as AstIdExpression;           

            var thisMethodCall = new AstExternalMethodCallExpression(target, name, callArgs);
            nodes.Push(thisMethodCall);
        }

        // #CALL_ARGS LEFT_PAREN #CALL_ARGS_LIST RIGHT_PAREN
        private void ConstructCallArgs()
        {
            var callArgs = new List<AstCallArgument>();
            var curr = nodes.Peek() as AstNode;
            while (curr is AstCallArgument)
            {
                nodes.Pop();
                callArgs.Insert(0, curr as AstCallArgument);
                curr = nodes.Peek() as AstNode;
            }

            var astArgumentsCallList = new AstArgumentsCallList(callArgs);
            nodes.Push(astArgumentsCallList);
        }

        // #CALL_ARGS LEFT_PAREN RIGHT_PAREN
        private void ConstructEmptyCallArgs()
        {
            var astArgumentsCallList = new AstArgumentsCallList(new List<AstCallArgument>());
            nodes.Push(astArgumentsCallList);
        }

        // #CALL_ARGS_LIST #EXPRESSION
        private void ConstructCallArgsList()
        {
            var expr = nodes.Pop() as AstExpression;
            var callArg = new AstCallArgument(expr);
            nodes.Push(callArg);
        }

        // #CALL_ARGS_LIST #EXPRESSION COMMA #CALL_ARGS_LIST
        private void ConstructCallArgsListAdd()
        {
            var some = nodes.Pop();
            var expr = nodes.Pop() as AstExpression;
            var callArg = new AstCallArgument(expr);
            nodes.Push(callArg);
            nodes.Push(some);
        }

        // #BOOL_VALUE TRUE
        private void ConstructBoolTrueValue()
        {
            var boolVal = new AstBoolValueExpression(BoolValue.TRUE);
            nodes.Push(boolVal);
        }

        // #BOOL_VALUE FALSE
        private void ConstructBoolFalseValue()
        {
            var boolVal = new AstBoolValueExpression(BoolValue.FALSE);
            nodes.Push(boolVal);
        }

        // #OR_TEST #AND_TEST
        private void ConstructOrTest()
        {

        }

        // #OR_TEST #AND_TEST OR #OR_TEST
        private void ConstructOrCompoundTest()
        {
            var right = nodes.Pop() as AstExpression;
            var left = nodes.Pop() as AstExpression;
            var orExpr = new AstOrExpression(left, right);
            nodes.Push(orExpr);
        }

        // #AND_TEST #NOT_TEST
        private void ConstructAndTest()
        {

        }

        // #AND_TEST #NOT_TEST AND #AND_TEST
        private void ConstructAndCompoundTest()
        {
            var right = nodes.Pop() as AstExpression;
            var left = nodes.Pop() as AstExpression;
            var andExpr = new AstAndExpression(left, right);
            nodes.Push(andExpr);
        }

        // #NOT_TEST NOT #NOT_TEST
        private void ConstructNotTest()
        {
            var expr = nodes.Pop() as AstExpression;
            var notExpr = new AstNotExpression(expr);
            nodes.Push(notExpr);
        }

        // #NOT_TEST #EXPRESSION
        private void ConstructNotExpressionTest()
        {

        }



        /* comparisions temporary disabled. see next iteration tasks.
        // #NOT_TEST #EXPRESSION #COMPARISON_S
        private void ConstructNotCompoundTest()
        {

        }

        // #COMPARISON_S #COMPARE_OPERATION #EXPRESSION
        private void ConstructComparisonS()
        {

        }

        // #COMPARISON_S
        private void ConstructEmptyComparisonS()
        {

        }

        // #COMPARE_OPERATION LT
        private void ConstructCompareOperationLt()
        {
            var op = new AstCompareOperation(CompareOp.LT);
            nodes.Push(op);
        }

        // #COMPARE_OPERATION GT
        private void ConstructCompareOperationGt()
        {
            var op = new AstCompareOperation(CompareOp.GT);
            nodes.Push(op);
        }

        // #COMPARE_OPERATION LTE
        private void ConstructCompareOperationLte()
        {
            var op = new AstCompareOperation(CompareOp.LTE);
            nodes.Push(op);
        }

        // #COMPARE_OPERATION GTE
        private void ConstructCompareOperationGte()
        {
            var op = new AstCompareOperation(CompareOp.GTE);
            nodes.Push(op);
        }
        
        // #COMPARE_OPERATION EQUAL
        private void ConstructCompareOperationEqual()
        {
            var op = new AstCompareOperation(CompareOp.EQUAL);
            nodes.Push(op);
        }
        */
    }
}
