using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler.libraries
{
	//todo: другие либы, никогда
	internal static class LibrariesHelper
	{
		private static readonly StandardLibrary s_stl = new StandardLibrary();

		public static StandardLibrary GetSTL()
		{
			return s_stl;
		}
	}
}
