using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    public enum VisibilityModifier
    {
        PUBLIC,
        PRIVATE
    }

    public enum StaticModifier
    {
        INSTANCE,
        STATIC
    }

    public enum BoolValue
    {
        TRUE,
        FALSE
    }

    public enum CompareOp
    {
        LT,
        GT,
        LTE,
        GTE,
        EQUAL
    }


}
