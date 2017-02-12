using UnityEngine;
using System.Collections;

/*
 * @author SoftRare - www.softrare.eu
 * This class is part of a demo scene of the package uMMO.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 */
public class WelcomeConversation : Conversation {
	
	protected override void dialogContents() {
		diaStyle.padding = new RectOffset (10,0,10,0);
		GUI.BeginGroup (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 175, 400, 350));
		GUI.DrawTexture(new Rect(0, 0, 400, 350), diaTexture, ScaleMode.StretchToFill, true, 10.0f);
		
		diaStyle.fontSize = 18;
		diaStyle.padding = new RectOffset (10,0,10,0);
	
		float y = 48f;
		float yNext = 15f;
		float xRegular = 58f;
		float textWidth = 280;
		float xAnswer = 65f;
		
		GUI.Label( new Rect (xRegular,y,textWidth,250),"Weclome, fellow viking!", diaStyle );
		
		diaStyle.fontSize = 13;
		
		GUI.Label( new Rect (xRegular,y+=(yNext*2f),textWidth,250),"I am Ulf, the recently instituted road", diaStyle );
		GUI.Label( new Rect (xRegular,y+=yNext,textWidth,250),"guard. Feel free to roam around, but beware", diaStyle );
		GUI.Label( new Rect (xRegular,y+=yNext,textWidth,250),"of the woods. As long as you stay on the", diaStyle );
		GUI.Label( new Rect (xRegular,y+=yNext,textWidth,250),"road, you're much safer.", diaStyle );
		
		GUI.Label( new Rect (xRegular,y+=(yNext*2f),textWidth,250),"Did you hear?!", diaStyle );
		GUI.Label( new Rect (xRegular,y+=yNext,textWidth,250),"A BIG EVENT will take place somewhere", diaStyle );
		GUI.Label( new Rect (xRegular,y+=yNext,textWidth,250),"here. No idea what that's all about.", diaStyle );
		GUI.Label( new Rect (xRegular,y+=yNext,textWidth,250),"If you want to find out, you should", diaStyle );
		GUI.Label( new Rect (xRegular,y+=yNext,textWidth,250),"probably speak to Olaf. You will find", diaStyle );
		GUI.Label( new Rect (xRegular,y+=yNext,textWidth,250),"him further down the road..", diaStyle );	
		
		diaStyle.normal.background = null;
		diaStyle.hover.background = btnHover;
		diaStyle.fontSize = 18;
		

		diaStyle.normal.background = null;
		diaStyle.padding = new RectOffset (0,0,5,0);
		
		if(GUI.Button (new Rect (xAnswer,y+=(yNext*2.5f),150,30), "Ok, thanks!",diaStyle))
		{
			showDialog = false;
		}

		GUI.EndGroup ();
	}	

}