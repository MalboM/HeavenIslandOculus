var underwatercamera : GameObject;

var underwatersound : GameObject;
var underwaterplants : GameObject;


function OnTriggerEnter(collider : Collider)
    {
        if(collider.tag == "Underwater")
        {
            underwatercamera.GetComponent.<Renderer>().enabled = true;
            // m.enabled = true;

            underwatersound.GetComponent.<AudioReverbZone>().enabled = true;
            underwatersound.GetComponent.<AudioSource>().enabled = true;
            underwaterplants.SetActive(true);
        }
    }


 function OnTriggerExit(collider : Collider)
        {
            if(collider.tag == "Underwater")
            {
                underwatercamera.GetComponent.<Renderer>().enabled = false;
                // m.enabled = false;
                underwatersound.GetComponent.<AudioReverbZone>().enabled = false;
                underwatersound.GetComponent.<AudioSource>().enabled = false;
                underwaterplants.SetActive(false);
            }
        }