using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/*
 * @author SoftRare - www.softrare.eu
 * This class is part of a demo scene of the package uMMO.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 */
public class RaceEvent : AEvent {
	
	public List<RacerNPC> racers = new List<RacerNPC>();
	
	public int maxLaps;
	
	[HideInInspector]
	public RacerNPC winnerNPC;
	
	public int waitForSecondsUntilInformingClients;
	
	[HideInInspector]
	public bool hasReceivedEventStartTime = false;
	
	public static int amountCoinsToStartBetting = 3;
	
	public static int maxAmountCoinsToCollect = 5;
	
	public static double timeLeft2Show;
	
	protected static Dictionary<NetworkPlayer,int> player2racer = new Dictionary<NetworkPlayer, int>();
	
	//http://stackoverflow.com/questions/463642/what-is-the-best-way-to-convert-seconds-into-hourminutessecondsmilliseconds
	public static string secondsToClockCountdownString(int seconds) {
		
		
		TimeSpan t = TimeSpan.FromSeconds( seconds );
		
		string answer = string.Format("{1:D2}m:{2:D2}s", 
						t.Hours,
		    			t.Minutes, 
		    			t.Seconds,
						t.Milliseconds
						);	
		
		return answer;
	}	
	
	
	[RPC]
	public void SetEventStartTimeOnClients(int secondsToStartOfEvent) {
		
		//if (!hasReceivedEventStartTime) {
			double now = uMMO_StaticLibrary.ts_now();
			
			this.eventStart = now + secondsToStartOfEvent;
			
			timeLeft2Show = this.eventStart - now;
		
			hasReceivedEventStartTime = true;
			
			uMMO_NetObject.LOCAL_PLAYER.GetComponent<LocalPlayer>().winner = false;
		//}
	}
	
	[RPC]
	public void eventEndOnClients() {
		
		BettingConversation.BirdBetOn = "";
		hasReceivedEventStartTime = false;
	}	
	
	[RPC]
	public void SetRacerLabelOnClients(int racerNo, string newLabel) {	
		
		foreach(RacerNPC racer in racers) {
		
			if (racer.racerNo == racerNo) {
				racer.GetComponent<Label>().SetLabel(newLabel);
				break;
			}
		}
	}
	
	[RPC] //executed only on server:
	public void SetPlayerBettingOnRacer(NetworkPlayer np, int racerNo) {
		
		uMMO_NetObject c = uMMO_StaticLibrary.getNetObjectsByNetworkPlayer(np)[0];
		int newAmount= c.GetComponent<PlayerCheckForCoinsOnServer>().amountCoins -= 3;
		c.GetComponent<NetworkView>().RPC ("UpdateAmountCoins",RPCMode.OthersBuffered,newAmount);
		
		if (!player2racer.ContainsKey(np)) {
			player2racer.Add (np,racerNo);	
		}
	}
	
	void OnGUI() {
		
		if (uMMO.get.architectureToCompile ==  uMMO_Architecture.Client) {
			
			string eventTitleStr = ""; 
			string eventSubtitleStr = "";
			
			if (hasReceivedEventStartTime) {
				
				double now = uMMO_StaticLibrary.ts_now();
		
				string convertedSeconds = secondsToClockCountdownString(int.Parse((eventStart - now)+""));
				//string convertedSeconds = secondsToClockCountdownString(int.Parse((timeLeft2Show)+""));
				
				bool spoke2Olaf = uMMO_NetObject.LOCAL_PLAYER.GetComponent<LocalPlayer>().spoke2Olaf;
					
				string eventName2Client;
				
				if (!spoke2Olaf) {
					eventName2Client = eventName;					
				} else {
					eventName2Client = secretEventName;
				}
				
				if(uMMO_NetObject.LOCAL_PLAYER != null && uMMO_NetObject.LOCAL_PLAYER.GetComponent<LocalPlayer>() != null) {
					
					if (!activated) {
						eventTitleStr = "'"+eventName2Client+"' will start in about "+convertedSeconds;	
					} else {	
						eventTitleStr = "'"+eventName2Client+"' is going on at the moment.";
					}
						
					if (!spoke2Olaf) {
						eventName2Client = eventName;
						eventSubtitleStr = "Talk to Olaf to get to know more about it.";
						
					} else {
						eventName2Client = secretEventName;
						
						if (activated) {
	
							if (BettingConversation.BirdBetOn == "") {
								eventSubtitleStr = "You did not bet on a bird.";		
							} else {
								eventSubtitleStr = "You bet on bird "+BettingConversation.BirdBetOn+".";	
							}
						} else {
							
							if (BettingConversation.BirdBetOn == "") {
								if (uMMO_NetObject.LOCAL_PLAYER.GetComponent<LocalPlayer>().amountCoins >= amountCoinsToStartBetting) {
									eventSubtitleStr = "Olaf takes bets. Bet on the winning bird to make a profit.";	
								} else {
									eventSubtitleStr = "You need at least 3 coins to bet. Collect coins in the woods.";
								}
							} else {
								eventSubtitleStr = "You bet on bird "+BettingConversation.BirdBetOn+" at the upcoming event.";		
							}							
							
							
						}
					}		
				
				}
			} else {
				if (uMMO_NetObject.LOCAL_PLAYER != null && uMMO_NetObject.LOCAL_PLAYER.GetComponent<LocalPlayer>().winner) {
					eventTitleStr = "CONGRATULATIONS!";
					eventSubtitleStr = "You won coins by betting on the bird which won the race!";
				}
			}
			
			if (eventTitleStr != "") {
				float x = Screen.width - (Screen.width * 0.40f);
				float y = Screen.height * 0.05f;
				
				GUI.Label( new Rect (x,y,390f,30f),"   "+eventTitleStr, eventManager.eventTitleStyle );
				
				GUI.Label( new Rect (x,y+12f,390f,30f),"   "+eventSubtitleStr, eventManager.eventSubtitleStyle );			
			}
			
		}

	}
		
	protected override void initializeConcrete() {
		
		//gameObject.name = "RaceEvent";	
		
		winnerNPC = null;
		
		racers.Clear();
		
		RacerNPC[] npcs = (RacerNPC[])GameObject.FindSceneObjectsOfType(typeof (RacerNPC));
		
		foreach(RacerNPC npc in npcs) {
			racers.Add(npc);	
		}

		if (uMMO.get.architectureToCompile == uMMO_Architecture.Server) {
			
			StartCoroutine(activateSendToClients());
			
		}	
	}
	
	public IEnumerator activateSendToClients() {

		yield return new WaitForSeconds(waitForSecondsUntilInformingClients);

		if(uMMO.get.players2ts_lastActivity.Count > 0) {
			GetComponent<NetworkView>().RPC("SetEventStartTimeOnClients",RPCMode.Others, int.Parse((eventStart - uMMO_StaticLibrary.ts_now())+"") ); // must delay sending further because an RPC cannot be send to noone.
			
		}
		
		if (!activated) //stop broadcasting when event started
			StartCoroutine(activateSendToClients()); 
		else
			StopAllCoroutines();
	}
	
	public void updateLapCount(RacerNPC racer) {
		if (uMMO.get.architectureToCompile == uMMO_Architecture.Server) {

				
			string racerLabel = "";
						
			
			if (!activated) {
				
				racerLabel = racer.name;
				
			} else if (racer.currentLap < maxLaps) {
			
				racerLabel = racer.name + " (lap "+racer.currentLap+" of "+maxLaps+")";
				
			} else if (racer.currentLap == maxLaps) {
				
				racerLabel = racer.name + " (final lap)";
				
			} else if (racer.currentLap == maxLaps+1 && winnerNPC == null) {
				
				racerLabel = racer.name + " (WINNER)";
				winnerNPC = racer;
				racer.racing = false;
				
				if (racers.Count == 1)
					destroyEvent();
			} else {
				
				racerLabel = racer.name + " (LOSER)";
				racer.racing = false;
				
				bool someoneStillRacing = false;
				foreach(RacerNPC racerC in racers) {
					if (racerC.racing) {
						someoneStillRacing = true;
						break;
					}
				}				
				
				if (!someoneStillRacing) {
					destroyEvent();
				}
			}
			
			if (preparationActivated || activated)
			//racer.GetComponent<Label>().SetLabel(racerLabel);
				GetComponent<NetworkView>().RPC("SetRacerLabelOnClients",RPCMode.OthersBuffered,racer.racerNo, racerLabel);			
			
		}
	}	
	
	public override void startEvent() {
		
		WaypointManager[] wpms = (WaypointManager[])GameObject.FindSceneObjectsOfType(typeof (WaypointManager));
		
		
		if (player2racer.Count > 0) {
			
			foreach(WaypointManager wpm in wpms) {
				wpm.chooseNextWaypoint(false);
			}
			
			foreach(RacerNPC racer in racers) {
				racer.racing = true;
				updateLapCount(racer);
			}
		} else {
			
			foreach(WaypointManager wpm in wpms) {
				wpm.chooseNextWaypoint(true);
			}			
			
			destroyEvent();	
		}
		
	}
	
	public override void prepare() {
		
		foreach(RacerNPC racer in racers) {
			racer.get2StartLine();	
			updateLapCount(racer);
		}		
	
	}
	
	[RPC]
	private void AnnounceWinner() {
		//RPC dummy
	}
	
	protected override void endEvent() {
		
		GetComponent<NetworkView>().RPC ("eventEndOnClients",RPCMode.OthersBuffered);
		
		foreach(RacerNPC racer in racers) {
			racer.idlePos.isActivated = true;
			racer.currentLap = 0;
			GetComponent<NetworkView>().RPC("SetRacerLabelOnClients",RPCMode.OthersBuffered,racer.racerNo, racer.name);
			racer.wpManager.waypoints.Clear();
			racer.wpManager.current = null;			
			//racer.wpManager.current = racer.idlePos;
		}

		foreach(var entry in player2racer) {
			NetworkPlayer np = (NetworkPlayer)entry.Key;
			int racerNo = (int)entry.Value;
			
			if (racerNo == winnerNPC.racerNo) {
				//here we have a winner
				
				uMMO_NetObject c = uMMO_StaticLibrary.getNetObjectsByNetworkPlayer(np)[0];
				if (c != null) {
					int newAmount = c.GetComponent<PlayerCheckForCoinsOnServer>().amountCoins += (amountCoinsToStartBetting*(racers.Count-1));
					c.GetComponent<NetworkView>().RPC ("AnnounceWinner",RPCMode.OthersBuffered,newAmount);
				}
			}
		}
		
		player2racer.Clear();
		
	}
}
