using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace compiler
{
	internal static class TokenColor
	{
		private static readonly Dictionary<TokenType, System.Drawing.Color> s_colors = new Dictionary<TokenType, System.Drawing.Color>
		{
			{TokenType.BOOL				, Color.Blue},
			{TokenType.CHAR				, Color.Blue},
			{TokenType.CLASS			, Color.Blue},
			{TokenType.CONST			, Color.Blue},
			{TokenType.ELSE				, Color.Blue},
			{TokenType.ENUM				, Color.Blue},
			{TokenType.FALSE			, Color.Blue},
			{TokenType.IF				, Color.Blue},
			{TokenType.INT				, Color.Blue},
			{TokenType.PRIVATE			, Color.Blue},
			{TokenType.PUBLIC			, Color.Blue},
			{TokenType.RETURN			, Color.Blue},
			{TokenType.STATIC			, Color.Blue},
			{TokenType.TRUE				, Color.Blue},
			{TokenType.WHILE			, Color.Blue},
			{TokenType.INTEGER_VALUE	, Color.Green}, // TODO: CHAR_VALUE, FLOAT_VALUE...
		};

		public static Color GetColor(Token token)
		{
			if (!s_colors.ContainsKey(token.Type)) return Color.Black;

			return s_colors[token.Type];
		}
	}
}
