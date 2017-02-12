using UnityEngine;
using System.Collections;

/*
 * @author SoftRare - www.softrare.eu
 * This class is part of a demo scene of the package uMMO.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 */
public class BettingConversation : Conversation 
{
	protected bool showBirdChoiceWindow = false;
	public bool hasEnoughMoney;
	public static string BirdBetOn = "";
	
	protected override void dialogContents() {
		diaStyle.padding = new RectOffset (10,0,10,0);
		GUI.BeginGroup (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 175, 400, 350));
		GUI.DrawTexture(new Rect(0, 0, 400, 350), diaTexture, ScaleMode.StretchToFill, true, 10.0f);
		
		uMMO_NetObject.LOCAL_PLAYER.GetComponent<LocalPlayer>().spoke2Olaf = true;
		float xBigText = 66f;
		float y = 43f;
		float yNext = 15f;
		float xRegular = 56f;
		float textWidth = 280;		
		//diaStyle.padding = new RectOffset (0,0,5,0);
		diaStyle.normal.background = null;
		if(showBirdChoiceWindow )
		{
			
			diaStyle.fontSize = 18;
			GUI.Label( new Rect (xRegular,y,textWidth,250),"I will bet on...", diaStyle );
			
			//diaStyle.normal.background = null;
			diaStyle.hover.background = btnHover;
			diaStyle.fontSize = 12;
			
			GameObject goRaceEvent = GameObject.Find("RaceEvent");
			
			if(GUI.Button (new Rect (xBigText,y+=(yNext*3),textWidth,30), "1. Vilma",diaStyle))
			{
				goRaceEvent.GetComponent<NetworkView>().RPC ("SetPlayerBettingOnRacer",RPCMode.Server,uMMO_NetObject.LOCAL_PLAYER.nplayer,1);
				BirdBetOn = "Vilma";
				showBirdChoiceWindow = false;
			}
			if(GUI.Button (new Rect (xBigText,y+=(yNext*2),textWidth,30), "2. Otto",diaStyle))
			{
				goRaceEvent.GetComponent<NetworkView>().RPC ("SetPlayerBettingOnRacer",RPCMode.Server,uMMO_NetObject.LOCAL_PLAYER.nplayer,2);
				BirdBetOn = "Otto";
				showBirdChoiceWindow = false;
			}
			if(GUI.Button (new Rect (xBigText,y+=(yNext*2),textWidth,30), "3. Matilda",diaStyle))
			{
				goRaceEvent.GetComponent<NetworkView>().RPC ("SetPlayerBettingOnRacer",RPCMode.Server,uMMO_NetObject.LOCAL_PLAYER.nplayer,3);
				BirdBetOn = "Matilda";
				showBirdChoiceWindow = false;
			}
			if(GUI.Button (new Rect (xBigText,y+=(yNext*2),textWidth,30), "4. Akseli",diaStyle))
			{
				goRaceEvent.GetComponent<NetworkView>().RPC ("SetPlayerBettingOnRacer",RPCMode.Server,uMMO_NetObject.LOCAL_PLAYER.nplayer,4);
				BirdBetOn = "Akseli";
				showBirdChoiceWindow = false;
			}
			
			//diaStyle.normal.background = null;
			diaStyle.padding = new RectOffset (0,0,5,0);		
			
			//diaStyle.normal.background = null;
			diaStyle.hover.background = btnHover;
			diaStyle.fontSize = 18;				
			
			if(GUI.Button (new Rect (xBigText,y+=(yNext*3f),textWidth,30), "Thanks, maybe later!",diaStyle))
			{
				showDialog = false;
				showBirdChoiceWindow = false;
			}
		}
		else
		{
			
			diaStyle.fontSize = 16;
			diaStyle.padding = new RectOffset (10,0,10,0);
			
			GUI.Label( new Rect (xRegular,y,textWidth,250),"Hi there!", diaStyle );
			
			diaStyle.fontSize = 12;
			
			GUI.Label( new Rect (xRegular,y+=(yNext*1.7f),textWidth,250),"I am Olaf. Good to see you!", diaStyle );
			
			GUI.Label( new Rect (xRegular,y+=(yNext*1.7f),textWidth,250),"By the way, I am the organizing A BIG EVENT", diaStyle );
			GUI.Label( new Rect (xRegular,y+=yNext,textWidth,250),"soon. It's a BIRD RACE. You can bet on one of", diaStyle );
			GUI.Label( new Rect (xRegular,y+=yNext,textWidth,250),"my birds. If this particular bird wins you can", diaStyle );
			GUI.Label( new Rect (xRegular,y+=yNext,textWidth,250),"make quite some profits! Betting costs you ", diaStyle );
			GUI.Label( new Rect (xRegular,y+=yNext,textWidth,250),+RaceEvent.amountCoinsToStartBetting+" golden coins.", diaStyle );
			
			
			GameObject goRaceEvent = GameObject.Find("RaceEvent");
			
			RaceEvent raceEvent = goRaceEvent.GetComponent<RaceEvent>();
		
			if (!raceEvent.activated) {
			
				hasEnoughMoney = (uMMO_NetObject.LOCAL_PLAYER.GetComponent<LocalPlayer>().amountCoins >= RaceEvent.amountCoinsToStartBetting)?true:false;
				
				if (!hasEnoughMoney) {
					
					if (BirdBetOn == "") {
						GUI.Label( new Rect (xRegular,y+=(yNext*1.7f),textWidth,250),"If you cannot afford that right now, I heard ", diaStyle );	
						GUI.Label( new Rect (xRegular,y+=yNext,textWidth,250),"recently a raid took place. A group of fearsome ", diaStyle );	
						GUI.Label( new Rect (xRegular,y+=yNext,textWidth,250),"vikings raided a town, and lost loot in the ", diaStyle );	
						GUI.Label( new Rect (xRegular,y+=yNext,textWidth,250),"nearby woods when escaping... ", diaStyle );	
						
						diaStyle.normal.background = null;
						diaStyle.hover.background = btnHover;
						diaStyle.fontSize = 18;
				
						//diaStyle.normal.background = null;
						diaStyle.padding = new RectOffset (0,0,5,0);
						
						if(GUI.Button (new Rect (xBigText,y+=(yNext*1.7f),150,30), "Thanks for the tip!",diaStyle))
						{
							showDialog = false;
						}					
					} else {
						diaStyle.fontSize = 12;
						GUI.Label( new Rect (xRegular,y+=(yNext*1.7f),textWidth,250),"The race is starting soon. You bet on ", diaStyle );	
						GUI.Label( new Rect (xRegular,y+=yNext,textWidth,250),"bird "+BirdBetOn+", I wish you good luck! ", diaStyle );	
						
						diaStyle.normal.background = null;
						diaStyle.padding = new RectOffset (0,0,5,0);		
						
						diaStyle.hover.background = btnHover;
						diaStyle.fontSize = 18;				
						
						if(GUI.Button (new Rect (xBigText,y+=(yNext*2f),textWidth,30), "Thanks, bye!",diaStyle))
						{
							showDialog = false;
							showBirdChoiceWindow = false;
						}							
					}
					
				} else if (BirdBetOn == ""){
					diaStyle.fontSize = 12;
					
					GUI.Label( new Rect (xRegular,y+=(yNext*2f),textWidth,250),"Will you bet on my birds?", diaStyle );	
					
					diaStyle.fontSize = 18;
					diaStyle.padding = new RectOffset (0,0,5,0);
					if (GUI.Button(new Rect (xBigText,y+=(yNext*2f),textWidth,30),"Choose a bird...",diaStyle))
					{
						showBirdChoiceWindow = true;
					}
					
					diaStyle.normal.background = null;
					//diaStyle.padding = new RectOffset (0,0,5,0);		
					
					//diaStyle.normal.background = null;
					diaStyle.hover.background = btnHover;
					diaStyle.fontSize = 18;				
					
					if(GUI.Button (new Rect (xBigText,y+=(yNext*2f),textWidth,30), "Thanks, maybe later!",diaStyle))
					{
						showDialog = false;
						showBirdChoiceWindow = false;
					}				
								
				} else {
					
					diaStyle.fontSize = 12;
					GUI.Label( new Rect (xRegular,y+=(yNext*1.7f),textWidth,250),"The race is starting soon. You bet on ", diaStyle );	
					GUI.Label( new Rect (xRegular,y+=yNext,textWidth,250),"bird "+BirdBetOn+", I wish you good luck! ", diaStyle );	
					
					//diaStyle.normal.background = null;
					diaStyle.padding = new RectOffset (0,0,5,0);		
					
					//diaStyle.normal.background = null;
					diaStyle.hover.background = btnHover;
					diaStyle.fontSize = 18;				
					
					if(GUI.Button (new Rect (xBigText,y+=(yNext*2f),textWidth,30), "Thanks, bye!",diaStyle))
					{
						showDialog = false;
						showBirdChoiceWindow = false;
					}						
					
				}
			} else {
				
				if (BirdBetOn == "") {
					GUI.Label( new Rect (xRegular,y+=(yNext*1.7f),textWidth,250),"The race is already in progress. I am  ", diaStyle );	
					GUI.Label( new Rect (xRegular,y+=yNext,textWidth,250),"sorry but I don't take bets for the moment. ", diaStyle );	
					GUI.Label( new Rect (xRegular,y+=yNext,textWidth,250),"But that's not a problem, I am organizing ", diaStyle );	
					GUI.Label( new Rect (xRegular,y+=yNext,textWidth,250),"this event again soon enough. Just come", diaStyle );	
					GUI.Label( new Rect (xRegular,y+=yNext,textWidth,250),"back soon and you will have the opportunity. ", diaStyle );	
				} else {
					
					GUI.Label( new Rect (xRegular,y+=(yNext*1.7f),textWidth,250),"The race is already in progress. You bet on ", diaStyle );	
					GUI.Label( new Rect (xRegular,y+=yNext,textWidth,250),"bird "+BirdBetOn+", I wish you good luck! ", diaStyle );		
					
				}
				
				//diaStyle.normal.background = null;
				diaStyle.hover.background = btnHover;
				diaStyle.fontSize = 18;
				
		
				//diaStyle.normal.background = null;
				diaStyle.padding = new RectOffset (0,0,5,0);
				
				if(GUI.Button (new Rect (xBigText,y+=(yNext*1.7f),150,30), "Thanks, bye!",diaStyle))
				{
					showDialog = false;
				}						
				
			}

		}

		GUI.EndGroup ();
	}	

}