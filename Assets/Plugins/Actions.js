

var audiopreso : AudioSource;
var audiopresoapple : AudioSource;

var toccato : boolean;
var presa : boolean;

var presaapple : boolean;


var shellraccolte : int;
var appleraccolte : int;

function Start(){


}

function OnTriggerStay(collider : Collider)
    {

     
        //SHELL
            if(collider.tag == "Shell")
            {
                toccato = true;
                presa = true;
               // collider.GetComponent("Halo").enabled = true; 
                //collider.GetComponent("FlashingController").enabled = true;
                collider.GetComponent.<ParticleEmitter>().emit = true;
              

                if (Input.GetKeyDown(KeyCode.E)||  (Input.GetAxis ("PadX") == 1)){
                    if ( collider.GetComponent.<Renderer>().enabled == true){

                       
                        collider.GetComponent.<Animation>().Play(); 
                        audiopreso.Play(); 
                        collider.GetComponent.<Collider>().enabled = false;

                        yield WaitForSeconds (1);
                        collider.GetComponent.<ParticleEmitter>().emit = false;
                        collider.GetComponent.<Renderer>().enabled = false; 
                        
                        // collider.GetComponent("Halo").enabled = false; 
                        //shellraccolte = shellraccolte +1;
                        if(presa == true) {
                            shellraccolte += 1;
                            presa = false;
                        }
                       

                    }
                }
            }

        //APPLE

            if(collider.tag == "Apple")
            {
                toccato = true;
                presaapple = true;
                // collider.GetComponent("Halo").enabled = true; 
                //collider.GetComponent("FlashingController").enabled = true;
                collider.GetComponent.<ParticleEmitter>().emit = true;
              

                if (Input.GetKeyDown(KeyCode.E) ||  (Input.GetAxis ("PadX") == 1)){
                    if ( collider.GetComponent.<Renderer>().enabled == true){

                       
                        collider.GetComponent.<Animation>().Play(); 
                        audiopresoapple.Play(); 
                        collider.GetComponent.<Collider>().enabled = false;

                        yield WaitForSeconds (1);
                        collider.GetComponent.<ParticleEmitter>().emit = false;
                        collider.GetComponent.<Renderer>().enabled = false; 
                        
                        // collider.GetComponent("Halo").enabled = false; 
                        //appleraccolte = appleraccolte +1;
                        if(presaapple == true) {
                            appleraccolte += 1;
                            presaapple = false;
                        }
                       

                    }
                }
            }
    }

   

 function OnTriggerExit(collider : Collider)
{

     

            if(collider.tag == "Shell")
            {
               
                toccato = false;
                collider.GetComponent.<ParticleEmitter>().emit = false;

                // collider.GetComponent("FlashingController").enabled = false;
                //collider.GetComponent("Halo").enabled = false; 
              
            }
}
        

       