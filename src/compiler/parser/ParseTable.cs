using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    class ParseTable
    {
        public const int START_STATE = 0;
        public const int ERROR_STATE = -1;

        private Dictionary<KeyValuePair<int, TokenType>, ParseAction> actionsTable;
        private Dictionary<KeyValuePair<int, string>, int> gotoTable;
        private Dictionary<int, ProductionInfo> productionsInfoTable;

        public ParseAction GetAction(int s, TokenType t)
        {
            var pair = new KeyValuePair<int, TokenType>(s, t);

            if (actionsTable.ContainsKey(pair))
            {
                return actionsTable[pair];
            }

            return new ParseAction(ParseActionKind.ERROR);
        }

        public int GetGoTo(int s, string nt)
        {
            var pair = new KeyValuePair<int, string>(s, nt);

            if (gotoTable.ContainsKey(pair))
            {
                return gotoTable[pair];
            }

            return ERROR_STATE;
        }

        public ProductionInfo GetProductionInfo(int productionNum)
        {
            return productionsInfoTable[productionNum];
        }

        public int GetStartState()
        {
            return START_STATE;
        }

        public void SetActionsTable(Dictionary<KeyValuePair<int, TokenType>, ParseAction> table)
        {
            this.actionsTable = table;
        }

        public void SetGoToTable(Dictionary<KeyValuePair<int, string>, int> table)
        {
            this.gotoTable = table;
        }

        public void SetProductionsInfoTable(Dictionary<int, ProductionInfo> table)
        {
            this.productionsInfoTable = table;
        }
    }
}
