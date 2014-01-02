using System;

namespace parserMaker
{
	public enum METHOD
	{
		S,R,G
	}
	public struct ParsTableItem
	{
		public METHOD method;
		public int number;
	}
	public class ParsTableNode
	{
	   public ParsTableItem item;
	   public ParsTableNode next;
	   public ParsTableNode(METHOD M,int Number)
		{
	   	  this.item.method = M;
		  this.item.number = Number;
		  next = null;
		}
	}
	/// <summary>
	/// 
	/// </summary>
	public class ParsTableElement
	{
		ParsTableNode first;
		int count;
		public int Count
		{
			get
			{
				return count;
			}
		}
		public ParsTableNode Head
		{
			get
			{
				return first;
			}
		}
		public ParsTableElement()
		{
			first = null;
			this.count = 0;
		}
		public void add(METHOD M,int Number)
		{
			count++;
			ParsTableNode temp =first;
			if(first == null)
			{
				first = new ParsTableNode(M,Number);
			}
			else
			{
				while(temp.next != null)
				{
				  temp =temp.next;
				}
				temp.next= new ParsTableNode(M,Number);
			}
		}

	}
}
