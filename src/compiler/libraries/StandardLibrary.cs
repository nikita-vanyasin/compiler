using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler.libraries
{
	// TODO : все, и никогда.
	internal class StandardLibrary
	{
		public virtual string[] GetClassNames()
		{
			return new[] { "Console", "Math", "String" };
		}
	}
}
