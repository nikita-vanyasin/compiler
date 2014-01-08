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

        public void DispatchError(int position, string description, int code)
        {
            ErrorEventHandler handler = Error;

            if (handler != null)
            {
                handler(this, new ErrorEvent(position, description, code));
            }
        }
    }
}
