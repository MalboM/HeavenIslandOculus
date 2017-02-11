var splash : GameObject;



function OnTriggerExit(collider : Collider)
    {
        if(collider.tag == "Water")
        {
            splash.GetComponent.<AudioSource>().Play();
        }
    }


    function OnTriggerEnter(collider : Collider)
        {
            if(collider.tag == "Water")
            {
                splash.GetComponent.<AudioSource>().Play();
            }
        }