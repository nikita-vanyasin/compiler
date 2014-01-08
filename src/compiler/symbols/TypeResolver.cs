using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    [Serializable]
    public class SymbolNotFoundException : Exception
    {
        public AstExpression Expr { get; set; }
        public string Id { get; set; }

        public SymbolNotFoundException() { }
        public SymbolNotFoundException(string message) : base(message) { }
        public SymbolNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected SymbolNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    class TypeResolver
    {
        private SymbolTable table;

        public TypeResolver(SymbolTable table)
        {
            this.table = table;
        }
                
        public string Resolve(AstExpression expr)
        {
            if (expr is AstMathExpression || expr is AstIntegerValueExpression)
            {
                return BuiltInTypes.INT;
            }
            else if (expr is AstBoolValueExpression || expr is AstBoolExpression)
            {
                return BuiltInTypes.BOOL;
            }
            else if (expr is AstIdExpression)
            {
                var s = table.Lookup((expr as AstIdExpression).Id);
                return GetSymbolType(expr, s, (expr as AstIdExpression).Id);
            }
            else if (expr is AstThisMethodCallExpression)
            {
                var key = (expr as AstThisMethodCallExpression).Name.Id;
                var s = table.LookupFunction(key);
                return GetSymbolType(expr, s, (expr as AstThisMethodCallExpression).Name.Id);
            }
            else if (expr is AstExternalMethodCallExpression)
            {
                var key = (expr as AstExternalMethodCallExpression).Target + (expr as AstExternalMethodCallExpression).Name.Id;
                var s = table.LookupFunction(key);
                return GetSymbolType(expr, s, (expr as AstExternalMethodCallExpression).Name.Id);
            }
            else if (expr is AstNegateUnaryExpr)
            {
                return BuiltInTypes.INT;
            }
            else if (expr is AstSimpleUnaryExpr)
            {
                return this.Resolve((expr as AstSimpleUnaryExpr).SimpleTerm);
            }
            else if (expr is AstSimpleTermExpr)
            {
                return this.Resolve((expr as AstSimpleTermExpr).Expr);
            }
            else
            {
                throw new Exception("Unknown expression type.");
            }            
        }

        private string GetSymbolType(AstExpression expr, Symbol s, string id)
        {
            if (s == null)
            {
                var e = new SymbolNotFoundException();
                e.Expr = expr;
                e.Id = id;
                throw e;
            }

            return s.Type;
        }
    }
}
