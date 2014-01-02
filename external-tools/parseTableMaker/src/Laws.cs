using System;
using System.Collections;
using System.Windows.Forms;

namespace parserMaker
{
	/// <summary>
	/// Summary description for Raw.
	/// </summary>
	public struct LawItem
	{
		public int lawNum;
	}
	//==========================================================================
	public class LawsNode
	{
		LawItem item;
		public LawsNode next;  
		public Parts parts;
		public int Number
		{
			get
			{
			  return item.lawNum;
			}
		}
		public LawsNode(int lawnum,Laws law)
		{
		  this.parts=new Parts(law);
		  this.item.lawNum = lawnum;
          next=null;
		}
	}

	//=====================================================================

	public class Laws
	{
		LawsNode first;
		nonTerminalNode parent;
		public nonTerminalNode Parent
		{
			get
			{
				return parent;
			}
		}
		public LawsNode Head
		{
			get
			{
				return first;
			}
		}
		int lawsCount;

		public int Count
		{
			get
			{
				return lawsCount;
			}
		}
		public LawsNode findLaw(int lawNum)
		{
			LawsNode node=this.first;
			while(node!=null)
			{
				if(node.Number==lawNum)
					return node;
			}
			return null;
		}
      //...............................
		public Laws(nonTerminalNode parentNonTerm)
		{
			first=null;
			lawsCount=0;
			this.parent=parentNonTerm;
		}
		//.........................................
		public bool getFollow(string name,Stack follows)
		{
			LawsNode temp =first;
			bool checkFollows = false ;
			while( temp != null)
			{
			    
				if( temp.parts.findFollows(name , follows) == true )
                   checkFollows = true;
				temp = temp.next;
			}
            return checkFollows;
		}
		public bool getFirst(Stack firsts,Stack checkNonTerm)
		{
			LawsNode temp =first;
			bool checkFirsts = false ;
			while( temp != null)
			{
				if( temp.parts.findFirsts(firsts,checkNonTerm) == true )
					checkFirsts = true;
				temp = temp.next;
			}
			return checkFirsts;
		}
		//.........................................
		public void add(int lawnum,string lawContent)
		{
			try
			{
				LawsNode thisNode=null;
				LawsNode temp=first;
				if(first==null)
				{
					first=new LawsNode(lawnum,this);
					thisNode=first;
				}
				else
				{
					while(temp.next!=null)
						temp=temp.next;
					temp.next=new LawsNode(lawnum,this);
					thisNode=temp.next;
				}
				string []contents=lawContent.Split(' ');
				bool isFirst=true;
				foreach(string s in contents)
				{
					if(isFirst)
					{
						isFirst=false;
						continue;
					}
					if(s.Length==0)
						continue;
					if(s[0]=='#')
						thisNode.parts.add(s.Substring(1,s.Length-1),false);
					else
						thisNode.parts.add(s,true);
				}
				lawsCount++;
			}
			catch(Exception e1)
			{
				MessageBox.Show("in laws.add : "+e1.Message);
			}
		}
		//..................................
		public string save()
		{
			string sav="";
			LawsNode temp=first;
			while(temp!=null)
			{
				
				string t=temp.parts.save();
				if(t=="")
					sav+=" ";
				else
					sav+=t;
				sav+="$";
				temp=temp.next;
			}
			return sav;
		}
	}
}
