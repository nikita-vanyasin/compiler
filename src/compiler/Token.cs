using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    public enum TokenType : sbyte
    {
        EOF = -127,
        ERROR = -1,

        ID = 0,
        COLON = 1,
        INTEGER_VALUE, BOOL, CHAR, CLASS, CONST, ELSE, ENUM, FALSE,
        IF, INT, PRIVATE, PUBLIC, RETURN, STATIC, TRUE,
        COMMA, ASSIGNMENT, LEFT_PAREN, RIGHT_PAREN,
        LEFT_BRACE, RIGHT_BRACE, LEFT_BRACKET,
        RIGHT_BRACKET, PLUS, MINUS, MULTIPLICATION, DIV, MOD,
        NOT, AND, OR, LT, GT, GTE, LTE, LINE_END,
        BLOCK_START, BLOCK_END, PASS, EQUAL, DOT
    }

    public class Token
    {
        public TokenType Type { get; set; }
        public string Attribute { get; set; }

        public Token(TokenType type, string value = "")
        {
            this.Type = type;
            this.Attribute = value;
        }

        public Token(TokenType type, char value)
        {
            this.Type = type;
            this.Attribute = "" + value;
        }

        override public string ToString()
        {
            string optPart = (this.Attribute.Length == 0) ? "" : ", \"" + this.Attribute + "\"";
            return "<" + this.Type + optPart + ">";
        }

        override public int GetHashCode()
        {
            return this.Type.GetHashCode() + this.Attribute.GetHashCode();
        }

        override public bool Equals(object obj)
        {
            Token otherToken = (Token)obj;
            if (otherToken == null)
            {
                return base.Equals(obj);
            }

            return (this.Type == otherToken.Type)
                    && (this.Attribute.CompareTo(otherToken.Attribute) == 0);
        }

        public static bool IsCorrectToken(Token t)
        {
            return (t.Type != TokenType.EOF) && (t.Type != TokenType.ERROR);
        }
    }
}
