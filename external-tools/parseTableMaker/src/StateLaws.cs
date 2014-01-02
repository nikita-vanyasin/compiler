using System;

namespace parserMaker
{
	/// <summary>
	/// Summary description for StateLaws.
	/// </summary>
	public struct StateLawItem
	{
		public int lawNum;
		public int dotPos;
		public bool IsDotEnded;
	}
	public class StateLawNode
	{
		public StateLawItem data;
		public StateLawNode next;
		public StateLawNode(int num)
		{
			data.dotPos=0;
			data.lawNum=num;
			data.IsDotEnded=false;
			next=null;
		}
		public StateLawNode(StateLawItem newItem)
		{
			this.data=newItem;
			next=null;
		}
	}
	public class StateLaws
	{
		StateLawNode first;
		int lawCount;
		public StateLaws()
		{
			first=null;
			lawCount=0;
		}
		public StateLawNode Head
		{
			get
			{
				return first;
			}
		}
		public int LawCount
		{
			get
			{
				return lawCount;
			}
		}
		public void add(StateLawItem newItem)
		{
			StateLawNode temp=first;
			if(first==null)
			{
				first=new StateLawNode(newItem);
				return;
			}
			while(temp.next!=null)
			{
				temp=temp.next;
			}
			temp.next=new StateLawNode(newItem);
			this.lawCount++;
		}
		public void add(int dotPos,int lawNum)
		{
			StateLawItem newItem;
			newItem.dotPos=dotPos;
			newItem.IsDotEnded=false;
			newItem.lawNum=lawNum;

			StateLawNode temp=first;
			if(first==null)
			{
				first=new StateLawNode(newItem);
				return;
			}
			while(temp.next!=null)
			{
				temp=temp.next;
			}
			temp.next=new StateLawNode(newItem);
			this.lawCount++;
		}
	}
}
