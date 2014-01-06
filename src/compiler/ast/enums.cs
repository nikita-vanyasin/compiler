using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    enum VisibilityModifier
    {
        PUBLIC,
        PRIVATE
    }

    enum StaticModifier
    {
        INSTANCE,
        STATIC
    }

    enum BoolValue
    {
        TRUE,
        FALSE
    }

    enum CompareOp
    {
        LT,
        GT,
        LTE,
        GTE,
        EQUAL
    }


}
