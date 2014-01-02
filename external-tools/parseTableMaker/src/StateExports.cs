using System;

namespace parserMaker
{
	/// <summary>
	/// Summary description for StateExports.
	/// </summary>
	public struct StateExportItem
	{
		public string expStr;
		public bool isTerminal;        
		public int distinationState; 
	}
	public class StateExportNode
	{
		public StateExportItem data;
		public StateExportNode next;
		public StateExportNode(string str,bool isTerm,int number )
		{
			next = null;
			data.distinationState = number;
			data.isTerminal = isTerm;
			data.expStr = str;
		}
		public StateExportNode(StateExportItem newItem)
		{
			next = null;
			data = newItem;
		}                
	}
	public class StateExports
	{
		StateExportNode first;
		int count;
		public StateExportNode Head
		{
			
			get
			{
				return first;
			}
		}
		public StateExports()
		{
			first = null;
			count=0;
		}
		public int ExportCount
		{
			get
			{
				return count;
			}
		}
		public void add(StateExportItem newItem)
		{
			StateExportNode temp=first;
            this.count++;
			if(first==null)
			{
				first=new StateExportNode(newItem);
				return;
			}
			while(temp.next!=null)
			{
				temp=temp.next;
			}
			temp.next=new StateExportNode(newItem);
			
		}
		public void add(string expStr,bool isTerm,int distination)
		{
			StateExportItem newItem;
			newItem.distinationState = distination;
			newItem.expStr = expStr;
			newItem.isTerminal = isTerm;
		    this.add(newItem); 
		}

	}
}
