﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace compiler
{
    abstract class CodeGenerator : AstNodeVisitor
    {
        public ErrorsEventDispatcher ErrorDispatcher { get; protected set; }
        protected SymbolTable table;

        protected CodeGenerator()
        {
            ErrorDispatcher = new ErrorsEventDispatcher();
        }

        public void SetSymbolTable(SymbolTable table)
        {
            this.table = table;
        }

        protected void DispatchError(SourcePosition position, string description, int code)
        {
            ErrorDispatcher.DispatchError(position, description, code);
        }

        abstract public bool Generate(AstProgram astRootNode, Stream outStream);
    }
}
