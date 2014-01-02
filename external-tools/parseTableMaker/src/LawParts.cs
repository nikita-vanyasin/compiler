using System;
using System.Windows.Forms;
using System.Collections;
namespace parserMaker
{
	/// <summary>
	/// Summary description for rowParts.
	/// </summary>
	public struct PartItem
	{
		public string name;
		public bool isTerminal;
	}
	public class PartsNode
	{
		public PartItem item;
		public PartsNode next;
		public PartsNode(string nam,bool isTerm)
		{
		  item.name=nam;
          next=null; 
		  item.isTerminal=isTerm;
		}
		public PartsNode(PartItem newItem)
		{
			item=newItem;
			next=null;
		}
	
	}
	public class Parts
	{
        PartsNode first;
		Laws parent;
		public PartsNode Head
		{
			get
			{
				return first;
			}
		}
		int  partCount;
		/// <summary>
		/// count of parts
		/// </summary>
		public int Count
		{
			get
			{
				return this.partCount;
			}
		}
		public Parts(Laws thisParent)
		{
			first=null;
			this.parent=thisParent;
		}
		public Laws Parent
		{
			get
			{
				return parent;
			}
		}
		public bool findFollows(string name,Stack follows)
		{
			PartsNode temp=first;
			while(temp!=null)
			{		
				if(temp.item.name==name)
				{
					if(temp.next==null)//if this token is the last token and there is no token next of it
					{
						return true;
					}
					else if(temp.next.item.isTerminal)//if next token is a terminal element
					{
						if( !follows.Contains(temp.next.item.name))
						  follows.Push(temp.next.item.name);
					}
					else  //if next token is a nonterminal element
					{
						PartsNode t = temp.next;
						nonTerminals head=this.parent.Parent.NonTerminals;
						while(head.getFirst(t.item.name,follows,false) ==true )  //we must chaeck next part
						{
						  t = t.next;
                          if( t == null)
							return true;
							if(t.item.isTerminal)
							{
							 if(!follows.Contains(t.item.name) )
								follows.Push(t.item.name);
                              break; 
							}//if
						}//while
					}//else
				}//if
				temp = temp.next;
			}//while
			return false;
		}
		public bool findFirsts(Stack firsts,Stack checkNonTerm)
		{
		    PartsNode temp = first;
			if(first == null)
				return true;
			while(temp!=null)
			{
				if(temp.item.isTerminal)
				{
				   if(!firsts.Contains(temp.item.name))
					   firsts.Push(temp.item.name);
					return false;
				}
				if(!checkNonTerm.Contains(temp.item.name))
				{
					checkNonTerm.Push(temp.item.name);

					if(!this.parent.Parent.NonTerminals.getFirst(temp.item.name,firsts,checkNonTerm,false))
					{
						return false;
					}
				}
				else
				{
					if(!this.parent.Parent.NonTerminals.haveEpsilonLaw(temp.item.name))
					{
					  return false;
					}
				}
                temp= temp.next; 
			}
			return true;
		}
		public PartsNode this [int PartNum]
		{
			get
			{
				int cnt=0;
				PartsNode part=this.first;
				while(part!=null)
				{
					if(cnt>=PartNum)
						return part;
					part=part.next;
					cnt++;
				}
				return null;
			}
		}
		public void add(string Name,bool isTerminal)
		{
			try
			{
				PartsNode temp=first;
				//...............creating new data object
				PartItem newPartItem;
				newPartItem.isTerminal=isTerminal;
				newPartItem.name=Name;
				if(isTerminal)
				{
					if(!ParsHead.terminals.Contains(Name))
					{
						ParsHead.terminals.Push(Name);
					}
				}
				if(first==null)
					first=new PartsNode(newPartItem);
				else
				{
					while(temp.next!=null)
						temp=temp.next;
					temp.next=new PartsNode(newPartItem);
				}	
				this.partCount++;
			}
			catch(Exception e1)
			{
				MessageBox.Show("in lawParts.add : "+e1.Message);
			}
		}
		public string save()
		{
			PartsNode temp=first;
			string  content="";
			while(temp!=null)
			{
				if(temp.item.isTerminal)
					content+="#"+temp.item.name;
				else
				    content+=temp.item.name;
				content+=" ";
				temp=temp.next;
			}
			return content;
		}

	}
}
