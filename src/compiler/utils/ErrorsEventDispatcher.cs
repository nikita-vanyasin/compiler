using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    public delegate void ErrorEventHandler(object sender, ErrorEvent eventArgs);

    public class ErrorsEventDispatcher
    {
        public event ErrorEventHandler Error;

        public void DispatchError(SourcePosition position, string description, int code = 1)
        {
            ErrorEventHandler handler = Error;

            if (handler != null)
            {
                handler(this, new ErrorEvent(position, description, code));
            }
        }

        public void DispatchWarning(SourcePosition position, string description)
        {
            ErrorEventHandler handler = Error;

            if (handler != null)
            {
                var e = new ErrorEvent(position, description, 0);
                e.IsError = false;
                handler(this, e);
            }
        }
    }
}
