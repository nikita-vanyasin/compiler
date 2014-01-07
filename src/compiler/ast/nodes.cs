using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    public class AstProgram : AstNode
    {
        public AstClass Class { get; protected set; }
                public AstProgram(AstClass klass)
        {
            Class = klass;
        }

        public override void Accept(AstNodeVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                Class.Accept(visitor);
            }
        }
    }

    public class AstClass : AstNode
    {
        public AstIdExpression Name { get; protected set; }
        public AstClassBody Body { get; protected set; }

        public AstClass(AstIdExpression name, AstClassBody body)
        {
            Name = name;
            Body = body;
        }
        public override void Accept(AstNodeVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                Name.Accept(visitor);
                Body.Accept(visitor);
            }
        }
    }

    public class AstClassBody : AstNode
    {
        public List<AstClassField> ClassFields { get; protected set; }
        public List<AstClassMethod> ClassMethods { get; protected set; }

        public AstClassBody(List<AstClassField> classFields, List<AstClassMethod> classMethods)
        {
            ClassFields = classFields;
            ClassMethods = classMethods;
        }

        public override void Accept(AstNodeVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                foreach (AstClassField classField in ClassFields)
                {
                    classField.Accept(visitor);
                }
                foreach (AstClassMethod classMethod in ClassMethods)
                {
                    classMethod.Accept(visitor);
                }
            }
        }
    }

    public class AstVisibilityModifier : AstNode
    {
        public VisibilityModifier Value { get; protected set; }

        public AstVisibilityModifier(VisibilityModifier value)
        {
            Value = value;
        }
    }

    public class AstStaticModifier : AstNode
    {
        public StaticModifier Value { get; protected set; }

        public AstStaticModifier(StaticModifier value)
        {
            Value = value;
        }
    }

    public class AstClassField : AstNode
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

        public override void Accept(AstNodeVisitor visitor)
        {
            if (visitor.Visit(this))
            { }
        }
    }

    public class AstClassMethod : AstNode
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

        public override void Accept(AstNodeVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                ArgumentsDefinition.Accept(visitor);
                StatementsBlock.Accept(visitor);
            }
        }
    }

    public class AstArgumentsDefList : AstNode
    {
        public List<AstArgumentDef> ArgumentsDefinition { get; protected set; }

        public AstArgumentsDefList(List<AstArgumentDef> list)
        {
            ArgumentsDefinition = list;
        }

        public override void Accept(AstNodeVisitor visitor)
        {
            if (visitor.Visit(this))
            { }
        }
    }

    public class AstArgumentDef : AstNode
    {
        public AstIdExpression TypeDef { get; protected set; }
        public AstIdExpression Name { get; protected set; }

        public AstArgumentDef(AstIdExpression typeDef, AstIdExpression name)
        {
            TypeDef = typeDef;
            Name = name;
        }
    }

    public class AstStatementsBlock : AstNode
    {
        public AstStatementsList Statements { get; protected set; }

        public AstStatementsBlock(AstStatementsList statements)
        {
            Statements = statements;
        }

        public override void Accept(AstNodeVisitor visitor)
        {
            if (visitor.Visit(this))
            {
            }
        }
    }

    public class AstStatementsList : AstNode
    {
        public List<AstStatement> Statements { get; protected set; }

        public AstStatementsList(List<AstStatement> list)
        {
            Statements = list;
        }

        public override void Accept(AstNodeVisitor visitor)
        {
            foreach (var statement in Statements)
            {
                statement.Accept(visitor);
            }
        }
    }

    abstract public class AstStatement : AstNode
    {
    }
    
    abstract public class AstExpression : AstNode
    {
    }

    abstract public class AstFunctionCallExpression : AstExpression
    {
    }

    public class AstThisMethodCallExpression : AstFunctionCallExpression
    {
        public AstIdExpression Name { get; protected set; }
        public AstArgumentsCallList CallArgs { get; protected set; }

        public AstThisMethodCallExpression(AstIdExpression name, AstArgumentsCallList callArgs)
        {
            Name = name;
            CallArgs = callArgs;
        }
    }

    public class AstThisMethodCallStatement : AstStatement
    {
        public AstThisMethodCallExpression Expr { get; protected set; }

        public AstThisMethodCallStatement(AstThisMethodCallExpression expr)
        {
            Expr = expr;
        }
    }

    public class AstExternalMethodCallExpression : AstFunctionCallExpression
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

    public class AstExternalMethodCallStatement : AstStatement
    {
        public AstExternalMethodCallExpression Expr { get; protected set; }

        public AstExternalMethodCallStatement(AstExternalMethodCallExpression expr)
        {
            Expr = expr;
        }
    }

    public class AstReturnStatement : AstStatement
    {
        public AstExpression Expression { get; protected set; }

        public AstReturnStatement(AstExpression expr)
        {
            Expression = expr;
        }

        public override void Accept(AstNodeVisitor visitor)
        {
            if (visitor.Visit(this))
            {
               // Expression.Accept(visitor);
            }
        }
    }

    public class AstIfStatement : AstStatement
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

    public class AstAssignStatement : AstStatement
    {
        public AstIdExpression Variable { get; protected set; }
        public AstExpression NewValue { get; protected set; }

        public AstAssignStatement(AstIdExpression var, AstExpression newValue)
        {
            Variable = var;
            NewValue = newValue;
        }
    }
    
    public class AstBoolValueExpression : AstExpression
    {
        public BoolValue Value { get; protected set; }

        public AstBoolValueExpression(BoolValue val)
        {
            Value = val;
        }
    }

    public class AstIntegerValueExpression : AstExpression
    {
        public string Value { get; protected set; }

        public AstIntegerValueExpression(string val)
        {
            Value = val;
        }
    }

    public class AstIdExpression : AstExpression
    {
        public string Id { get; protected set; }

        public AstIdExpression(string id)
        {
            Id = id;
        }
    }

    public class AstArgumentsCallList : AstNode
    {
        public List<AstCallArgument> Arguments { get; protected set; }

        public AstArgumentsCallList(List<AstCallArgument> list)
        {
            Arguments = list;
        }
    }

    public class AstCallArgument : AstExpression
    {
        public AstExpression Expr { get; protected set; }

        public AstCallArgument(AstExpression expr)
        {
            Expr = expr;
        }
    }

    public class AstCompareOperation : AstExpression
    {
        public CompareOp Value { get; protected set; }

        public AstCompareOperation(CompareOp val)
        {
            Value = val;
        }
    }

    public class AstMulExpression : AstExpression
    {
        public AstUnaryExpr Left { get; protected set; }
        public AstExpression Right { get; protected set; }

        public AstMulExpression(AstUnaryExpr left, AstExpression right)
        {
            Left = left;
            Right = right;
        }
    }

    public class AstDivExpression : AstExpression
    {
        public AstUnaryExpr Left { get; protected set; }
        public AstExpression Right { get; protected set; }

        public AstDivExpression(AstUnaryExpr left, AstExpression right)
        {
            Left = left;
            Right = right;
        }
    }

    public class AstModExpression : AstExpression
    {
        public AstUnaryExpr Left { get; protected set; }
        public AstExpression Right { get; protected set; }

        public AstModExpression(AstUnaryExpr left, AstExpression right)
        {
            Left = left;
            Right = right;
        }
    }

    public class AstAddExpression : AstExpression
    {
        public AstExpression Left { get; protected set; }
        public AstExpression Right { get; protected set; }

        public AstAddExpression(AstExpression left, AstExpression right)
        {
            Left = left;
            Right = right;
        }
    }

    public class AstSubExpression : AstExpression
    {
        public AstExpression Left { get; protected set; }
        public AstExpression Right { get; protected set; }

        public AstSubExpression(AstExpression left, AstExpression right)
        {
            Left = left;
            Right = right;
        }
    }

    abstract public class AstUnaryExpr : AstExpression
    {
        public AstSimpleTermExpr SimpleTerm { get; protected set; }
    }

    public class AstNegateUnaryExpr : AstUnaryExpr
    {
        public AstNegateUnaryExpr(AstSimpleTermExpr expr)
        {
            SimpleTerm = expr;
        }
    }

    public class AstSimpleUnaryExpr : AstUnaryExpr
    {
        public AstSimpleUnaryExpr(AstSimpleTermExpr expr)
        {
            SimpleTerm = expr;
        }

    }

    public class AstSimpleTermExpr : AstExpression
    {
        public AstExpression Expr { get; protected set; }

        public AstSimpleTermExpr(AstExpression expr)
        {
            Expr = expr;
        }
    }
}