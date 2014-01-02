using System;
using System.Collections;

namespace parserMaker
{
	public class StateNode
	{
		private int             stateNum;
		public  StateNode       next;
		public  StateLaws       laws;
		public  StateExports    exports;
		public  int StateNumber
		{
			get
			{
			  return stateNum;
			}
		}
		public  StateNode(int number)
		{
			this.laws    = new StateLaws();
			this.exports = new StateExports();
			next=null;
			this.stateNum = number;
		}
	}
	/// <summary>
	/// 
	/// </summary>
	public class State
	{
		StateNode first;
		ParsHead  parsHead;
		int count;
		public State(ParsHead head)
		{
			count=0;
			parsHead=head;
			first=new StateNode(0);
			first.laws.add(0,0);
			this.stateTravers(first);
		}
		public int StateCount
		{
			get
			{
				return count;
			}
		}
		private StateNode add()
		{
			StateNode temp = first;
			count++;
			if(first == null)
			{
				first = new StateNode(count);
				return first;
			}
			while(temp.next!=null)
				temp=temp.next;
			temp.next = new StateNode(count); 
			return temp.next;
		}
		private StateNode add(StateNode newNode)
		{
			StateNode temp = first;
			count++;
			if(first == null)
			{
				first = newNode;
				return first;
			}
			while(temp.next!=null)
				temp=temp.next;
			temp.next =newNode; 
			return temp.next;
		}
		private bool addStateLaw(int stateNumber,int dotPos,int lawNumber)
		{
			StateLawItem newLaw;
			if(dotPos < 0 || lawNumber < 0)
				return true;
			newLaw.dotPos = dotPos;
			newLaw.lawNum=lawNumber;
			newLaw.IsDotEnded=false;
			StateNode temp = first;
			if(stateNumber <0)
				return false;
			if(count < stateNumber )
				return false;
			for(int i=1; i< stateNumber ;i++)
				temp = temp.next;
			temp.laws.add(newLaw);
			return true;
		}
		private bool addStateLaw(int stateNumber,StateLawItem newLaw)
		{
			StateNode temp = first;
			if(stateNumber <0)
				return false;
			if(count < stateNumber )
				return false;
			for(int i=1; i< stateNumber ;i++)
				temp = temp.next;
			temp.laws.add(newLaw);
			return true;
		}
		private bool addStateExport(int stateNumber,StateExportItem newExport)
		{
			StateNode temp = first;
			if(stateNumber <0)
				return false;
			if(count < stateNumber )
				return false;
			for(int i=1; i< stateNumber ;i++)
				temp = temp.next;
			temp.exports.add(newExport);
			return true;
		}

		public StateNode this [int index]
		{
			get
			{
				if(index>0 &&index<this.StateCount)
				{
					StateNode node=first;
					while(node!=null)
					{
						if(node.StateNumber==index)
							break;
						node=node.next;
					}
					return node;
				}
				else
					throw new ArgumentOutOfRangeException("index","index is less than zero or is geater than count of all states");
			}
		}
		private void makeStats(int stateNumber)
		{
			StateNode newStateNode;
			Stack exports=new Stack();
			StateNode temp = this.first;
			while(temp!=null)
			{
				if(temp.StateNumber == stateNumber)
				{
					StateLawNode stateLaw = temp.laws.Head;
					while(stateLaw!=null)
					{
						LawsNode law = this.parsHead.findLaw(stateLaw.data.lawNum);
						PartsNode part = law.parts[stateLaw.data.dotPos];
						if(part != null)
						{
							if(!exports.Contains(part.item.name))
							{
								exports.Push(part.item.name);							
								newStateNode=new StateNode(this.count+1);
								newStateNode.laws.add(stateLaw.data.dotPos+1,stateLaw.data.lawNum);
								StateLawNode remainLaw = stateLaw.next;
								while(remainLaw!=null)
								{
									LawsNode remLaw = this.parsHead.findLaw(remainLaw.data.lawNum);
									PartsNode remPart = remLaw.parts[remainLaw.data.dotPos];
									if(remPart != null)//if2
									{
										if(remPart.item.name==part.item.name)
										{
											newStateNode.laws.add(remainLaw.data.dotPos+1,remainLaw.data.lawNum);
										}
									}//if2
									remainLaw = remainLaw.next;
								}//while
								this.stateTravers(newStateNode);
								StateNode existState;
								existState=this.isCreated(newStateNode);
								if(existState==null)
								{
									this.add(newStateNode);
									temp.exports.add(part.item.name,part.item.isTerminal,newStateNode.StateNumber);    
									this.makeStats(newStateNode.StateNumber);
								}
								else
								{
									temp.exports.add(part.item.name,part.item.isTerminal,existState.StateNumber);
								}
							}//if new export
						}//if dot place is not end
						else
						{
							stateLaw.data.IsDotEnded = true;  
						}
						stateLaw = stateLaw.next;
					}
				}//if find state
                temp = temp.next;
			}//while in state
		}//end func
		public void makeParsTable(ParsTable parsTable)
		{
		   StateNode stateTemp = first;
					ParsTableItem item;
			while(stateTemp != null)
			{
			    StateExportNode stateExport = stateTemp.exports.Head;
				while(stateExport != null)
				{
					item.number=stateExport.data.distinationState;
					if(stateExport.data.isTerminal)
					{
						item.method=METHOD.S;
					}
					else
					{
						item.method=METHOD.G;
					}
					parsTable[stateTemp.StateNumber,stateExport.data.expStr]=item;
					stateExport = stateExport.next;
				}
				StateLawNode stateLaw = stateTemp.laws.Head;
				item.method=METHOD.R;
				while(stateLaw !=null)
				{
					if(stateLaw.data.IsDotEnded)
					{
                        LawsNode law=parsHead.findLaw(stateLaw.data.lawNum);
						Stack follows=new Stack();
						Stack checkedNonTerm= new Stack();
						nonTerminals nonTerm = law.parts.Parent.Parent.NonTerminals;
						nonTerm.getFollow(law.parts.Parent.Parent.item.Name,follows,checkedNonTerm);
						item.number = stateLaw.data.lawNum;
						while(follows.Count !=0)
						{
							parsTable[stateTemp.StateNumber,follows.Pop().ToString()]=item;                            
						}
					}
						stateLaw = stateLaw.next;
				}
				stateTemp =stateTemp.next;
			}
		   
		}
		private void stateTravers(int stateNumber)
		{			
			StateNode node=this.first;
			Stack lawExist=new Stack();
			while(node!=null)
			{
				if(node.StateNumber == stateNumber)
				{
					StateLawNode lawNode=node.laws.Head;
					while(lawNode!=null)
					{
						LawsNode law=parsHead.findLaw(lawNode.data.lawNum);
						if(law!=null)
						{
							PartsNode part=law.parts[lawNode.data.dotPos];
							if(part!=null)
							{
								if(!part.item.isTerminal)
								{
									Stack lawNumbers = new Stack();
									lawNumbers.Clear();
									law.parts.Parent.Parent.NonTerminals.findLaws(part.item.name,lawNumbers);   
									while(lawNumbers.Count != 0)
									{
										
										this.addStateLaw(node.StateNumber,0,(int)lawNumbers.Pop());
									}
								}
							}
						}
						lawNode=lawNode.next;
					}
				    return;
				}
				node=node.next;
			}
		}
		private void stateTravers(StateNode tra)
		{			
			StateLawNode lawNode=tra.laws.Head;
			Stack lawExists=new Stack();
			while(lawNode!=null)
			{
				LawsNode law=parsHead.findLaw(lawNode.data.lawNum);
				if(law!=null)
				{
					PartsNode part=law.parts[lawNode.data.dotPos];
					if(part!=null)
					{
						if(!part.item.isTerminal )
						{
							if(!lawExists.Contains(part.item.name))
							{
								Stack lawNumbers = new Stack();
								lawNumbers.Clear();
								law.parts.Parent.Parent.NonTerminals.findLaws(part.item.name,lawNumbers);   
								while(lawNumbers.Count != 0)
									tra.laws.add(0,(int)lawNumbers.Pop());
								lawExists.Push(part.item.name);
							}
						}
					}
				}
				lawNode=lawNode.next;
			}
		}

		public bool create()
		{
			this.makeStats(0);
			return true;
		}
		private StateNode isCreated(StateNode newNode)
		{
			bool foundMatchLaw=false;
			StateNode node=first;
			  //.......... for all states search for new state
			while(node!=null)
			{
				//.............check count of laws of two state
				if(node.laws.LawCount!=newNode.laws.LawCount)
				{
					node = node.next;
					continue;
				}

				StateLawNode temp=node.laws.Head;
				//........for any laws in this state search for a law in new state 
				while(temp!=null)
				{
					StateLawNode newData=newNode.laws.Head;
					foundMatchLaw=false;//is found any mached law in new state
					while(newData!=null)
					{
						if(temp.data.lawNum==newData.data.lawNum)
							if(temp.data.dotPos==newData.data.dotPos)
							{
								foundMatchLaw=true;
								break;
							}
						newData=newData.next;
					}
					if(!foundMatchLaw)
						break;
					//....do search for other laws
					temp=temp.next;
				}
				if(foundMatchLaw)
				{
					return node;
				}
				node=node.next;
			}
			return null;
		}
	}
}
