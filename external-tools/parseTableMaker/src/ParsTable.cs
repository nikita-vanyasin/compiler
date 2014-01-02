using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using System.Data;

namespace parserMaker
{
	
	public class ParsTable
	{
		int rowCount , colCount;
		ParsTableElement [,] parsTable;
		TermNonTermList termNonTerm;
		public DataGrid dgGrid;
//		public DataGrid Grid 
//		{
//			set
//			{
//				dgGrid= value;
//			}
//		}
		public ParsTableElement this[int row,int col]
		{
			get
			{
				if(row < rowCount && col < colCount && row>=0 && col>=0)
					return this.parsTable[row,col];
				return null;
			}
		}
		public ParsTableItem this[int row,string expression]
		{
			set
			{
                int number=termNonTerm[expression];
				if(this.parsTable[row,number]==null)
				{
					this.parsTable[row,number]=new ParsTableElement();
				}
				this.parsTable[row,number].add(value.method,value.number);  
			}
		}
		public ParsTable(int stateCount ,Stack terminal,nonTerminals nonTerminal,DataGrid dg)
		{
			dgGrid = new DataGrid();
			dgGrid=dg;
			termNonTerm = new TermNonTermList();
			this.rowCount = stateCount+1;
			this.colCount = nonTerminal.NonTerminalCount+terminal.Count+1;
			this.parsTable = new ParsTableElement[this.rowCount,this.colCount];  
			while(terminal.Count>0)
			{
				termNonTerm.add(terminal.Pop().ToString(),true);
			}
			termNonTerm.add("$",true);
			nonTerminalNode temp=nonTerminal.NonTerminalHead;
			while(temp!=null)
			{
				if(temp.item.Name!=nonTerminals.ExtraNonTerm)
				{
					termNonTerm.add(temp.item.Name,false);
				}
				temp=temp.next;
			}
		}
		public void showContents(ListBox result)
        {
            result.Items.Add("// PARSE ACTIONS DICTIONARY");
            result.Items.Add("public Dictionary<KeyValuePair<int, TokenType>, ParseAction> CreateActionsTable()");
            result.Items.Add("{");
            result.Items.Add("return new Dictionary<KeyValuePair<int, TokenType>, ParseAction>()");
            result.Items.Add("{");
            {
                TermNonTermNode temp;
                for (var j = 0; j < termNonTerm.TerminalCount; ++j)
                {
                    temp = termNonTerm[j];
                    var item = temp.item;
                    var name = item.elemStr;

                    if (name == "$")
                    {
                        name = "EOF";
                    }

                    for (int i = 0; i < this.rowCount; i++)
                    {
                        ParsTableElement element = this[i, j];
                        var oneLine = "{ new KeyValuePair<int, TokenType>(" + i + ", TokenType." + name + "), new ParseAction(ParseActionKind.";

                        if (element != null)
                        {
                            ParsTableNode node = element.Head;
                            if (node != null)
                            {
                                while (node != null)
                                {
                                    if (node.item.number == 0 && node.item.method == METHOD.R)
                                    {
                                        oneLine += "ACCEPT";
                                    }
                                    else
                                    {
                                        if (node.next != null)
                                        {
                                            MessageBox.Show("Error: grammar is ambigious!");
                                            return;
                                         //   oneLine += node.item.method.ToString() + node.item.number.ToString() + "/";
                                        }
                                        else
                                        {
                                            var method = node.item.method;

                                            if (method == METHOD.G)
                                            {
                                                throw new Exception("wrong code path!");
                                            }

                                            var methodName = method == METHOD.R ? "REDUCE" : "SHIFT";
                                            oneLine += methodName + ", " + node.item.number.ToString();
                                            
                                        }
                                    }
                                    result.Items.Add("\t" + oneLine + ") },");
                                    node = node.next;
                                }
                            }
                        }
                    }
                }
                var last = result.Items[result.Items.Count - 1] as string;
                last = last.Remove(last.Length - 1);
                result.Items[result.Items.Count - 1] = last;
            }

            result.Items.Add("};");
            result.Items.Add("}");

            result.Items.Add("");
            result.Items.Add("");
            result.Items.Add("// GOTO DICTIONARY ");
            result.Items.Add("public Dictionary<KeyValuePair<int, string>, int> CreateGoToTable()");
            result.Items.Add("{");
            result.Items.Add("return new Dictionary<KeyValuePair<int, string>, int>()");
            result.Items.Add("{");
            {
                TermNonTermNode temp;
                for (var j = termNonTerm.TerminalCount; j < termNonTerm.Count; ++j)
                {
                    temp = termNonTerm[j];
                    var item = temp.item;
                    var name = item.elemStr;

                    for (int i = 0; i < this.rowCount; i++)
                    {
                        ParsTableElement element = this[i, j];
                        var oneLine = "\t{ new KeyValuePair<int, string>("+ i + ", \"" + name + "\"" + "), ";

                        if (element != null)
                        {
                            ParsTableNode node = element.Head;
                            if (node != null)
                            {
                                while (node != null)
                                {
                                    if (node.item.number == 0 && node.item.method == METHOD.R)
                                    {
                                        MessageBox.Show("errr!!!!!!!!!!!!!!");
                                        return;
                                    }
                                    else
                                    {
                                        if (node.next != null)
                                        {
                                            MessageBox.Show("Error: grammar is ambigious!");
                                            return;
                                        }
                                        else
                                        {
                                            oneLine += node.item.number.ToString() + "},";
                                        }
                                    }
                                    result.Items.Add(oneLine);
                                    node = node.next;
                                }
                            }
                        }
                    }
                }

                var last = result.Items[result.Items.Count - 1] as string;
                last = last.Remove(last.Length - 1);
                result.Items[result.Items.Count - 1] = last;
            }
            result.Items.Add("} ;");
            result.Items.Add("}");

            /*		result.Items.Add(this.rowCount.ToString()+" "+this.colCount.ToString());
                    result.Items.Add(this.termNonTerm.TerminalCount.ToString()+" "+this.termNonTerm.NonTerminalCount.ToString());
                    string oneLine="";
                    TermNonTermNode temp=this.termNonTerm.Head;
	
                    while(temp!=null)
                    {
                        if(temp.next!=null)
                        {
                            oneLine+=temp.item.elemStr+" ";
                        }
                        else
                            oneLine+=temp.item.elemStr;
                        temp=temp.next;
                    }
                    result.Items.Add(oneLine);
                    oneLine="";
                    for(int i=0;i<this.rowCount;i++)
                    {
                        oneLine+=i.ToString()+":";
                        for(int j=0;j<this.colCount;j++)
                        {
                            ParsTableElement element=this[i,j];
                            if(element!=null)
                            {
                                //sw.Write(element.Count.ToString());
                                ParsTableNode node=element.Head;
                                if(node!=null)
                                {
                                    oneLine+=" ";
                                    while(node!=null)
                                    {
                                        if(node.item.number==0 && node.item.method==METHOD.R)
                                        {
                                            oneLine+="Accept ";
                                        }
                                        else
                                        {
                                            if(node.next !=null)	
                                                oneLine+=node.item.method.ToString()+node.item.number.ToString()+"/";
                                            else
                                                oneLine+=node.item.method.ToString()+node.item.number.ToString();
                                        }
                                        node = node.next;
                                        result.Items.Add(oneLine);
                                        oneLine = "";
                                    }
                                }
                                else
                                {
                                    oneLine+=" -";
                                }
                            }
                            else
                            {
                                oneLine+=" -";
                            }
                            //sw.Write(" ");
                        }
                        result.Items.Add(oneLine);
                        oneLine="";
                    }*/

        }
		public void writeGrid()
		{
			bool flag=false;
			object[] val=new object[this.colCount+1];
			TermNonTermNode temp=this.termNonTerm.Head;
			DataTable tblPart = new DataTable("Tabel");
			DataSet dsPart = new DataSet("dsPart1");
			tblPart = dsPart.Tables.Add("Tabel");	
			tblPart.Columns.Add(" ");
			while(temp!=null)
			{
				tblPart.Columns.Add(temp.item.elemStr);
				temp=temp.next;
			}
			for(int i=0;i<this.rowCount;i++)
			{
				for(int k=0;k<val.Length;k++)
					val[k]="";			
				val[0]=i.ToString();
				for(int j=0;j<this.colCount;j++)
				{
					ParsTableElement element=this[i,j];
					if(element!=null)
					{
						ParsTableNode node=element.Head;
						if(node!=null)
						{
							flag=false;
							while(node!=null)
							{
								if(node.item.number==0 && node.item.method==METHOD.R)
								{
									val[j+1]="Accept";
								}
								else
								{
									if(node.next !=null)
									{
										if(!flag)
											val[j+1]=node.item.method.ToString()+node.item.number.ToString()+"/";
										flag=true;
									}
									else
									{
										if(flag)
										{
											val[j+1]+=node.item.method.ToString()+node.item.number.ToString();
											dgGrid.PreferredColumnWidth = 50;
											flag=false;
										}
										else
											val[j+1]=node.item.method.ToString()+node.item.number.ToString();
									}
								}
								node = node.next;
							}
						}
						else
						{
							val[j+1]="";
						}
					}
					else
					{
						val[j+1]="";
					}
				}//for j
				tblPart.Rows.Add(val);
			}//for i			
			dgGrid.SetDataBinding(dsPart,tblPart.TableName);
		}
		public void saveTo(StreamWriter sw)
		{
			
//			object[] val=new object[this.colCount];
			TermNonTermNode temp=this.termNonTerm.Head;
//			DataTable tblPart = new DataTable("Tabel");
//			DataSet dsPart = new DataSet("dsPart1");
//			tblPart = dsPart.Tables.Add("Tabel");	
//			while(temp!=null)
//			{
//				tblPart.Columns.Add(temp.item.elemStr);
//				temp=temp.next;
//			}
//			val[0]="sfd";
//			val[1]="sfd2";
//			tblPart.Rows.Add(val);
			//dgGrid.SetDataBinding(dsPart,tblPart.TableName);

			sw.WriteLine(this.rowCount.ToString()+" "+this.colCount.ToString());
			sw.WriteLine(this.termNonTerm.TerminalCount.ToString()+" "+this.termNonTerm.NonTerminalCount.ToString());
			
			//write terminal and nonTerminals in one line
			while(temp!=null)
			{
				if(temp.next!=null)
				{
					sw.Write(temp.item.elemStr+" ");
				}
				else
					sw.Write(temp.item.elemStr);
				temp=temp.next;
			}
			sw.WriteLine();
			for(int i=0;i<this.rowCount;i++)
			{
				sw.Write(i.ToString()+":");
				for(int j=0;j<this.colCount;j++)
				{
					ParsTableElement element=this[i,j];
					if(element!=null)
					{
						//sw.Write(element.Count.ToString());
						ParsTableNode node=element.Head;
						if(node!=null)
						{
							sw.Write(" ");
							while(node!=null)
							{
								if(node.item.number==0 && node.item.method==METHOD.R)
								{
									sw.Write("Accept");
								}
								else
								{
									if(node.next !=null)	
									{
										sw.Write(node.item.method.ToString()+node.item.number.ToString()+"/");
									}
									else
										sw.Write(node.item.method.ToString()+node.item.number.ToString());
								}
								node = node.next;
							}
						}
						else
						{
						 sw.Write(" -");
						}
					}
					else
					{
						sw.Write(" -");
					}
					//sw.Write(" ");
				}
				sw.WriteLine();
			}
			sw.Close();
		}
	}
}
