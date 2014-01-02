using System;

namespace parserMaker
{
	public struct TermNonTermItem
	{
		public int number;
		public string elemStr;
		public bool isTerminal;
	}
	public class TermNonTermNode
	{
	    public TermNonTermItem item;
        public TermNonTermNode next;
		public TermNonTermNode(string str,bool isTerm,int Number)
		{
		   this.item.elemStr = str;
			this.item.isTerminal = isTerm;
			this.item.number = Number;
			this.next = null;
		}
	}
	
	public class TermNonTermList
	{
		TermNonTermNode first;
		int countTerminal;
		int countNonTerminal;
		public TermNonTermNode Head
		{
			get
			{
			  return first;
			}
		}

        public int Count
        {
            get
            {
                return this.countNonTerminal + this.countTerminal;
            }
        }

		public int NonTerminalCount
		{
			get
			{
			  return this.countNonTerminal;
			}
		}
		public int TerminalCount
		{
			get
			{
				return this.countTerminal;
			}
		}
		private int count;
		public TermNonTermList()
		{
			first = null;
			count = -1;
            countTerminal=0;
			countNonTerminal=0;
		}
		public void add(string str,bool isTerm)
		{
			TermNonTermNode temp =first;
			this.count++;
			if(isTerm)
				this.countTerminal++;
			else
				this.countNonTerminal++;
			if(first == null)
			{
				first = new TermNonTermNode(str,isTerm,count);
			}
			else
			{
				while(temp.next != null)
				{
					temp= temp.next;
				}
				temp.next = new TermNonTermNode(str,isTerm,count);
			}
		}
		public int this[string name]
		{
			get
			{
			  TermNonTermNode temp = first;
				while(temp != null)
				{
				  if(temp.item.elemStr == name)
					  return temp.item.number;
				  temp=temp.next;
				}
				return -1;
			}
		}

        public TermNonTermNode this[int index]
        {
            get
            {
                var i = 0;

                TermNonTermNode temp = first;
                while (i != index)
                {
                    i++;

                    if (temp.next == null)
                    {
                        throw new IndexOutOfRangeException("index must be < than count");
                    }

                    temp = temp.next;
                }
                return temp;
            }
        }
	}
}
