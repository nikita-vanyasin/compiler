using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace compiler
{
    class Compiler
    {
        private ErrorsContainer errorsContainer;

        public bool Compile(string inputText, Stream outStream)
        {
            errorsContainer = new ErrorsContainer();

            if (!outStream.CanWrite)
            {
                return false;
            }

            // parse
            var parser = new Parser();
            parser.ErrorDispatcher.Error += this.OnErrorOccurred;
            if (!parser.Parse(inputText))
            {
                return false;
            }

            var rootNode = parser.GetRootNode();

            // check code paths
            var codePathChecker = new CodePathChecker();
            codePathChecker.ErrorDispatcher.Error += this.OnErrorOccurred;
            if (!codePathChecker.Check(rootNode))
            {
                return false;
            }

            // fill types && semantic checks
            var typeEval = new TypeEvaluator();
            typeEval.ErrorDispatcher.Error += this.OnErrorOccurred;
            if (!typeEval.Evaluate(rootNode))
            {
                return false;
            }

            // generate code
            var generator = new LLVMCodeGenerator();
            generator.SetSymbolTable(typeEval.GetSymbolTable());
            generator.ErrorDispatcher.Error += this.OnErrorOccurred;
            return generator.Generate(rootNode, outStream);
        }

        public ErrorsContainer GetErrorsContainer()
        {
            return errorsContainer;
        }

        public void OnErrorOccurred(object sender, ErrorEvent error)
        {
            errorsContainer.Add(error);
        }
    }
}
