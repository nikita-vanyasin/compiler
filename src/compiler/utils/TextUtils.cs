using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    class TextUtils
    {
        public static int GetLineNumber(string text, int index)
        {
            string[] lines = text.Split('\n');
            int counter = 0;
            for (int i = 0; i < lines.Length; ++i)
            {
                counter += lines[i].Length + 1;
                if (counter >= index)
                {
                    return i + 1;
                }
            }

            return 0;
        }

        public static int GetColumnNumber(string text, int index)
        {
            string[] lines = text.Split('\n');
            int lineNum = GetLineNumber(text, index) - 1;
            int counter = 0;
            for (int i = 0; i < lineNum; ++i)
            {
                counter += lines[i].Length + 1;
            }

            return index - counter + 1;
        }

		public static string WriteCompilerError(string source, ErrorEvent ev)
		{
            return string.Format("Error: {0} {1} [Line {2}, Column {3}]", 
				ev.Description, 
				ErrorEvent.GetTextByCode(ev.Code), 
				GetLineNumber(source, ev.Position), 
				GetColumnNumber(source, ev.Position));
		}

        public static string CharPosToText(string file, int index)
        {
            int line = GetLineNumber(file, index);
            int column = GetColumnNumber(file, index);
            return String.Format("line {0}, column {1}", line, column);
        }
    }
}
