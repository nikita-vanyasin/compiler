using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    class AstProgram : AstNode
    {
        public AstClass Class { get; protected set; }

        public AstProgram(AstClass klass)
        {
            Class = klass;
        }
    }

    class AstClass : AstNode
    {
        public AstIdExpression Name { get; protected set; }
        public AstClassBody Body { get; protected set; }

        public AstClass(AstIdExpression name, AstClassBody body)
        {
            Name = name;
            Body = body;
        }
    }

    class AstClassBody : AstNode
    {
        public List<AstClassField> ClassFields { get; protected set; }
        public List<AstClassMethod> ClassMethods { get; protected set; }

        public AstClassBody(List<AstClassField> classFields, List<AstClassMethod> classMethods)
        {
            ClassFields = classFields;
            ClassMethods = classMethods;
        }
    }

    class AstVisibilityModifier : AstNode
    {
        public VisibilityModifier Value { get; protected set; }

        public AstVisibilityModifier(VisibilityModifier value)
        {
            Value = value;
        }
    }

    class AstStaticModifier : AstNode
    {
        public StaticModifier Value { get; protected set; }

        public AstStaticModifier(StaticModifier value)
        {
            Value = value;
        }
    }

    class AstClassField : AstNode
    {
        public AstVisibilityModifier Visibility { get; protected set; }
        public AstStaticModifier Static { get; protected set; }
        public AstIdExpression TypeDef { get; protected set; }
        public AstIdExpression Name { get; protected set; }

        public AstClassField(
            AstVisibilityModifier visibility,
            AstStaticModifier staticMod,
            AstIdExpression typeDef,
            AstIdExpression name
        )
        {
            Visibility = visibility;
            Static = staticMod;
            TypeDef = typeDef;
            Name = name;
        }
    }

    class AstClassMethod : AstNode
    {
        public AstVisibilityModifier Visibility { get; protected set; }
        public AstStaticModifier Static { get; protected set; }
        public AstIdExpression TypeDef { get; protected set; }
        public AstIdExpression Name { get; protected set; }
        public AstArgumentsDefList ArgumentsDefinition { get; protected set; }
        public AstStatementsBlock StatementsBlock { get; protected set; }

        public AstClassMethod(
            AstVisibilityModifier visibility,
            AstStaticModifier staticMod,
            AstIdExpression typeDef,
            AstIdExpression name,
            AstArgumentsDefList argumentsDefinition,
            AstStatementsBlock statementsBlock
        )
        {
            Visibility = visibility;
            Static = staticMod;
            TypeDef = typeDef;
            Name = name;
            ArgumentsDefinition = argumentsDefinition;
            StatementsBlock = statementsBlock;
        }
    }

    class AstArgumentsDefList : AstNode
    {
        public List<AstArgumentDef> ArgumentsDefinition { get; protected set; }

        public AstArgumentsDefList(List<AstArgumentDef> list)
        {
            ArgumentsDefinition = list;
        }
    }

    class AstArgumentDef : AstNode
    {
        public AstIdExpression TypeDef { get; protected set; }
        public AstIdExpression Name { get; protected set; }

        public AstArgumentDef(AstIdExpression typeDef, AstIdExpression name)
        {
            TypeDef = typeDef;
            Name = name;
        }
    }

    class AstStatementsBlock : AstNode
    {
        public AstStatementsList Statements { get; protected set; }

        public AstStatementsBlock(AstStatementsList statements)
        {
            Statements = statements;
        }
    }

    class AstStatementsList : AstNode
    {
        public List<AstStatement> Statements { get; protected set; }

        public AstStatementsList(List<AstStatement> list)
        {
            Statements = list;
        }
    }

    abstract class AstStatement : AstNode
    {
    }
    
    abstract class AstExpression : AstNode
    {
    }

    abstract class AstFunctionCallExpression : AstExpression
    {
    }

    class AstThisMethodCallExpression : AstFunctionCallExpression
    {
        public AstIdExpression Name { get; protected set; }
        public AstArgumentsCallList CallArgs { get; protected set; }

        public AstThisMethodCallExpression(AstIdExpression name, AstArgumentsCallList callArgs)
        {
            Name = name;
            CallArgs = callArgs;
        }
    }

    class AstThisMethodCallStatement : AstStatement
    {
        public AstThisMethodCallExpression Expr { get; protected set; }

        public AstThisMethodCallStatement(AstThisMethodCallExpression expr)
        {
            Expr = expr;
        }
    }

    class AstExternalMethodCallExpression : AstFunctionCallExpression
    {
        public AstIdExpression Target { get; protected set; }
        public AstIdExpression Name { get; protected set; }
        public AstArgumentsCallList CallArgs { get; protected set; }

        public AstExternalMethodCallExpression(AstIdExpression target, AstIdExpression name, AstArgumentsCallList callArgs)
        {
            Target = target;
            Name = name;
            CallArgs = callArgs;
        }
    }

    class AstExternalMethodCallStatement : AstStatement
    {
        public AstExternalMethodCallExpression Expr { get; protected set; }

        public AstExternalMethodCallStatement(AstExternalMethodCallExpression expr)
        {
            Expr = expr;
        }
    }

    class AstReturnStatement : AstStatement
    {
        public AstExpression Expression { get; protected set; }

        public AstReturnStatement(AstExpression expr)
        {
            Expression = expr;
        }
    }

    class AstIfStatement : AstStatement
    {
        public AstExpression Condition { get; protected set; }
        public AstStatementsBlock ThenBlock { get; protected set; }
        public AstStatementsBlock ElseBlock { get; protected set; }

        public AstIfStatement(AstExpression cond, AstStatementsBlock thenBlock, AstStatementsBlock elseBlock)
        {
            Condition = cond;
            ThenBlock = thenBlock;
            ElseBlock = elseBlock;
        }
    }

    class AstAssignStatement : AstStatement
    {
        public AstIdExpression Variable { get; protected set; }
        public AstExpression NewValue { get; protected set; }

        public AstAssignStatement(AstIdExpression var, AstExpression newValue)
        {
            Variable = var;
            NewValue = newValue;
        }
    }

    class AstTerm : AstExpression
    {
        public AstExpression Expression { get; protected set; }

        public AstTerm(AstExpression expr)
        {
            Expression = expr;
        }
    }

    class AstBoolValueExpression : AstExpression
    {
        public BoolValue Value { get; protected set; }

        public AstBoolValueExpression(BoolValue val)
        {
            Value = val;
        }
    }

    class AstIntegerValueExpression : AstExpression
    {
        public string Value { get; protected set; }

        public AstIntegerValueExpression(string val)
        {
            Value = val;
        }
    }

    class AstIdExpression : AstExpression
    {
        public string Id { get; protected set; }

        public AstIdExpression(string id)
        {
            Id = id;
        }
    }

    class AstArgumentsCallList : AstNode
    {
        public List<AstCallArgument> Arguments { get; protected set; }

        public AstArgumentsCallList(List<AstCallArgument> list)
        {
            Arguments = list;
        }
    }

    class AstCallArgument : AstExpression
    {
        public AstExpression Expr { get; protected set; }

        public AstCallArgument(AstExpression expr)
        {
            Expr = expr;
        }
    }

    class AstCompareOperation : AstExpression
    {
        public CompareOp Value { get; protected set; }

        public AstCompareOperation(CompareOp val)
        {
            Value = val;
        }
    }
    



}