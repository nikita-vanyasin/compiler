using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    class ErrorsContainer : List<ErrorEvent>
    {
        public string[] GetAll()
        {
            var result = new List<string>();
            foreach (var e in this)
            {
                result.Add(e.ToString());
            }
            return result.ToArray();
        }

        public bool HasErrors()
        {
            return this.Count > 0;
        }
    }
}
