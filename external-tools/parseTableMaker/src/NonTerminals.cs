using System;
using System.Windows.Forms;
using System.Collections;

namespace parserMaker
{
	/// <summary>
	/// Summary description for nonTerminals.
	/// </summary>
	public struct nonTerminalItem
     {
		public string Name;
     }
	public class nonTerminalNode
	{
		public nonTerminalItem item;
		public nonTerminalNode next;
		public Laws  lawLink;
		nonTerminals nonTerms;
		public nonTerminals NonTerminals
		{
			get
			{
				return this.nonTerms;
			}
		}
		public nonTerminalNode(string nam,nonTerminals nonTerminal)
		{
			nonTerms=nonTerminal;
			lawLink = new Laws(this);
			next=null;
			item.Name=nam;  
		}
		public LawsNode findLaw(int lawNum)
		{
			LawsNode node=this.lawLink.Head;
			while(node!=null)
			{
				if(node.Number==lawNum)
					return node;
				node=node.next;
			}
			return null;
		}
	}
	public class nonTerminals
	{
		public static string ExtraNonTerm="##S'#";
		nonTerminalNode first;
		int count;
        ParsHead parent;
		public ParsHead Parent
		{
			get
			{
			  return parent;
			}
		}
		public int NonTerminalCount
		{
			get
			{
				return count;
			}
		}
		public nonTerminalNode findLaw(int lawNum)
		{
			nonTerminalNode node=first;
			while(node!=null)
			{
				if(node.lawLink.findLaw(lawNum)!=null)
					  break;
				node=node.next;
			}
			return node;
		}
		public bool findLaws(string nonTerm,Stack lawNums)
		{
			nonTerminalNode node=first;
			while(node!=null)
			{
				if(node.item.Name == nonTerm)
				{
				    LawsNode temp=node.lawLink.Head;
					while(temp!=null)
					{
					  lawNums.Push(temp.Number);
                      temp = temp.next; 
					}
				  return true;
				}
				node=node.next;
			}
            return false;
		}
		public nonTerminalNode NonTerminalHead
		{
			get
			{
				return first;
			}
		}
		public nonTerminals(ParsHead PH)
		{
			this.parent = PH;
			first=null;
		}
		public bool getFirst(string name,Stack firsts,bool checkFollow)
		{
			Stack checkNonTerm=new Stack();
			return this.getFirst(name,firsts,checkNonTerm,checkFollow);
		}
		public bool getFirst(string name,Stack firsts,Stack checkedNonTerm,bool checkFollow)
		{
			nonTerminalNode temp=first;
			bool ifGoToEndLaw = false;
			while(temp!=null)
			{
					if(temp.item.Name==name)
					{
							if( temp.lawLink.getFirst(firsts,checkedNonTerm)==true)
							{
								if(checkFollow)
								{
									Stack checkedTerm = new Stack();							
									getFollow(temp.item.Name,firsts,checkedTerm);					
								}
								ifGoToEndLaw = true;
							}
						
							return ifGoToEndLaw;
					}
				temp=temp.next;
			}
		
			return ifGoToEndLaw;
		}
		public bool haveEpsilonLaw(string name)
		{
			nonTerminalNode temp=first;
			while(temp!=null)
			{
				if(temp.item.Name==name)
				{
					LawsNode law=temp.lawLink.Head;
					while(law!=null)
					{
						if(law.parts.Count==0)
							return true;
						law=law.next;
					}
					return false;
				}
				temp=temp.next;
			}
			return false;
		}
		public void getFollow(string name,Stack follows , Stack checkedNonTerm)
		{
			if(name==nonTerminals.ExtraNonTerm)
			{
				follows.Push("$");
				return;
			}
			nonTerminalNode temp=first;
			while(temp!=null)
			{
				if( temp.lawLink.getFollow(name,follows)==true)
				{
					if( !checkedNonTerm.Contains(temp.item.Name))
					{
						checkedNonTerm.Push(temp.item.Name);
						getFollow(temp.item.Name , follows,checkedNonTerm);
					}
				}
	        	temp=temp.next;
			}			
		}
		public void checkAdd(string nonTerm,int lawNum)
		{
			try
			{
				nonTerminalNode thisNode=null;
				string []part=nonTerm.Split(' ');
				string NonTerm=part[0].Substring(1,part[0].Length-1);
				nonTerminalNode temp;
				if(first==null)
				{
					first = new nonTerminalNode(nonTerminals.ExtraNonTerm,this);
					first.lawLink.add(0,nonTerminals.ExtraNonTerm+" #"+NonTerm);
					this.checkAdd(nonTerm,lawNum);
					return;
				}
				else
				{
					temp=first;
					while(temp.next!=null)
					{
						if(temp.item.Name==NonTerm)
						{
							thisNode=temp;
							break;
						}
						temp=temp.next;
					}
					if(thisNode==null)
					{
						if(temp.item.Name!=NonTerm)
						{
							temp.next = new nonTerminalNode(NonTerm,this);
							thisNode=temp.next;
							count++;
						}
						else
							thisNode=temp;
					}
				}
				thisNode.lawLink.add(lawNum,nonTerm);
			}
			catch(Exception e1)
			{
				MessageBox.Show("in nonTerminals.CheckAdd : "+e1.Message);
			}
		}
		public string save()
		{
			nonTerminalNode temp=first;
			string contents="";
			while(temp!=null)
			{
				string thisNonTerminalLaws=temp.lawLink.save();
				string []laws=thisNonTerminalLaws.Split('$');
				if(laws.Length==0)
				{
					contents+=temp.item.Name+"@";
					continue;
				}
                foreach(string s in laws)
					if(s!="")
					   contents+="#"+temp.item.Name+" "+s+"@";
				temp=temp.next;
			}
			return contents;
		}


	}
}
