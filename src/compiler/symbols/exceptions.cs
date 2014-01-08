using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{

    [Serializable]
    public class SymbolAlreadyDefinedException : Exception
    {
        public SymbolAlreadyDefinedException() { }
        public SymbolAlreadyDefinedException(string message) : base(message) { }
        public SymbolAlreadyDefinedException(string message, Exception inner) : base(message, inner) { }
        protected SymbolAlreadyDefinedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
    public class CallableSymbolAlreadyDefinedException : Exception
    {
        public CallableSymbolAlreadyDefinedException() { }
        public CallableSymbolAlreadyDefinedException(string message) : base(message) { }
        public CallableSymbolAlreadyDefinedException(string message, Exception inner) : base(message, inner) { }
        protected CallableSymbolAlreadyDefinedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
