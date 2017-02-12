using UnityEngine;
using System.Collections;
using Oculus.Platform;

public class EntitlementCheck : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Oculus.Platform.Core.Initialize("1302050986523647");
        Oculus.Platform.Entitlements.IsUserEntitledToApplication().OnComplete(EntitlementChecked);
    }

    void Update()
    {
        //not needed anymore
     //   Request.RunCallbacks();
    }

    void EntitlementChecked(Message msg)
    {

        // Ok
        if (!msg.IsError)
        {
            // Do what you want, load main menu

          //  proceedAsNormal();
        }
        // Not Ok
        else
        {
            UnityEngine.Application.Quit();
          //  showMessageThatTheUserDoesntOwnThis();
        }
    }
}
