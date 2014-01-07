using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;

namespace compiler
{
    public class Parser : ErrorsEventDispatcher
    {
        private ParseTable table;
        private Scanner scanner;
        private Stack<int> stack;
        private AstBuilder astBuilder;

        public Parser()
        {
            table = ParseTableBuilder.Build();
            scanner = new Scanner();
        }

        public bool Parse(string inputText)
        {
            scanner.SetText(inputText);
            stack = new Stack<int>();
            stack.Push(table.GetStartState());

            astBuilder = new AstBuilder();

            return ParseProgram();
        }

        public AstProgram GetRootNode()
        {
            return astBuilder.GetRootNode();
        }

        private bool ParseProgram()
        {
            int s;
            var currentToken = scanner.GetNextToken();
            LogLine("Token: " + currentToken);
            while (true)
            {
                s = stack.Peek();
                Token a = currentToken;

                if (a.Type == TokenType.ERROR)
                {
                        DispatchError(scanner.GetSourcePosition(), "Error: " + a.Attribute, 1);
                        return false;
                }

                ParseAction action = table.GetAction(s, a.Type);

                if (action.Kind == ParseActionKind.SHIFT)
                {
                    stack.Push(action.Number);
                    currentToken = scanner.GetNextToken();
                    LogLine("Token: " + currentToken);

                    switch (a.Type)
                    {
                        case TokenType.ID:
                            astBuilder.AddAstIdNode(a);
                            break;
                        case TokenType.INTEGER_VALUE:
                            astBuilder.AddAstIntegerValueNode(a);
                            break;
                    }
                }
                else if (action.Kind == ParseActionKind.REDUCE)
                {
                    var productionInfo = table.GetProductionInfo(action.Number);
                    int count = productionInfo.Length;
                    for (int i = 0; i < count; ++i)
                    {
                        stack.Pop();
                    }

                    LogLine("Reduce: " + productionInfo.Head);
                    ReduceProduction(productionInfo);

                    int newState = table.GetGoTo(stack.Peek(), productionInfo.Head);
                    stack.Push(newState);
                }
                else if (action.Kind == ParseActionKind.ACCEPT)
                {
                    break;
                }
                else
                {
                    DispatchError(scanner.GetSourcePosition(), "Error: ", 2);
                    return false;
                }
                
                Log("Stack: ");
                foreach (var i in stack)
                {
                    Log(i + " ");
                }
                LogLine();
            }

            return true;
        }

        private void ReduceProduction(ProductionInfo production)
        {
            astBuilder.CallAction(production.Action);
        }

        private void Log(string message)
        {
       //     Trace.Write(message);
        }

        private void LogLine(string message = "")
        {
      //      Trace.WriteLine(message);
        }
    }
}
