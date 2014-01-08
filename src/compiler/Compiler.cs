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

            var parser = new Parser();
            parser.Error += this.OnErrorOccurred;
           
            if (parser.Parse(inputText))
            {
                var checker = new TypeEvaluator();
                checker.ErrorDispatcher.Error += this.OnErrorOccurred;

                if (checker.Evaluate(parser.GetRootNode()))
                {
                    var generator = new LLVMCodeGenerator();
                    generator.SetSymbolTable(checker.GetSymbolTable());
                    generator.ErrorDispatcher.Error += this.OnErrorOccurred;
                    return generator.Generate(parser.GetRootNode(), outStream);
                }
            }

            return false;
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
