using System;
using System.IO;
using System.Windows.Forms;
using System.Collections;

namespace parserMaker
{
	/// <summary>
	/// 
	/// </summary>
	public class ParsHead
	{
		internal static Stack terminals=new Stack();
		nonTerminals first;
		/// <summary>
		/// count of laws
		/// </summary>
		int     count;
		int terminalCnt;
		int nonTerminalCnt;
		public ParsHead()
		{
			first=new nonTerminals(this);
			count=0;
			terminalCnt=0;
		}
		public int TerminalCount
		{
			get
			{
				return terminalCnt;
			}
		}
		public int LawCount
		{
			get
			{
				return count;
			}
		}
		public nonTerminals Head
		{
			get
			{
				return first;
			}
		}
		public LawsNode findLaw(int lawNumber)
		{
			nonTerminalNode nonTerm=this.first.NonTerminalHead;
			while(nonTerm!=null)
			{
				LawsNode law=nonTerm.lawLink.Head;
				while(law!=null)
				{
					if(law.Number==lawNumber)
						return law;
					law=law.next;
				}
				nonTerm=nonTerm.next;
					
			}
			return null;
		}
		public static void ErrorHandler(string Error)
		{
			MessageBox.Show(Error,"Error in input rules");
		}
		public void save(StreamWriter sw)
		{
			string content=first.save();
			string []contents=content.Split('@');
			foreach(string s in contents)
			{
				if(s!="")
				  sw.WriteLine(s);
			}
			sw.Close();
		}
		public void load(string newLaw)
		{
			try
			{
				if(newLaw=="")
					return;
				count++;
			
				//..............make a new terminal node ..................

				first.checkAdd(newLaw,count);
				terminalCnt=ParsHead.terminals.Count;
			}
			catch(Exception excp)
			{
				MessageBox.Show("in ParseHead.load :"+excp.Message);
			}
		}
	}
}
