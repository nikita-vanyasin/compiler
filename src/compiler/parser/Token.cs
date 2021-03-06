﻿using System;
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
        SPACE = 2,
        INTEGER_VALUE, BOOL, CHAR, CLASS, CONST, ELSE, ENUM, FALSE,
        IF, INT, PRIVATE, PUBLIC, RETURN, STATIC, TRUE,
        COMMA, ASSIGNMENT, LEFT_PAREN, RIGHT_PAREN,
        LEFT_BRACE, RIGHT_BRACE, LEFT_BRACKET,
        RIGHT_BRACKET, PLUS, MINUS, MULTIPLICATION, DIV, MOD,
        NOT, AND, OR, LT, GT, GTE, LTE, LINE_END,
        BLOCK_START, BLOCK_END, PASS, EQUAL, DOT, WHILE, NOT_EQUAL,
        STRING_LITERAL, STRING
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

    static class TokenFactory
    {
        private static Dictionary<string, TokenType> keywords = new Dictionary<string, TokenType>() 
        {
                {"return", TokenType.RETURN},
                {"static", TokenType.STATIC},                
                {"class", TokenType.CLASS},
                {"public", TokenType.PUBLIC},
                {"private", TokenType.PRIVATE},
                {"if", TokenType.IF},
                {"else", TokenType.ELSE},
                {"int", TokenType.INT},
                {"bool", TokenType.BOOL},
                {"true", TokenType.TRUE},
                {"false", TokenType.FALSE},
                {"pass", TokenType.PASS},
                {"div", TokenType.DIV},
                {"mod", TokenType.MOD},
                {"while", TokenType.WHILE},
                {"string", TokenType.STRING}
        };

        public static Token CreateByString(string strKeyword)
        {
            TokenType type = keywords.ContainsKey(strKeyword) ? keywords[strKeyword]
                                                              : TokenType.ID;
            return new Token(type, strKeyword);
        }
    }
}
