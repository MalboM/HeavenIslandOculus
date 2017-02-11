
/*var sabbia : AudioSource; 
var roccia : AudioSource; 
var terra : AudioSource; 
*/

var sabbiapiede : GameObject;
var sabbiapiede2 : GameObject;
var sabbiapiede3 : GameObject;
var sabbiapiede4 : GameObject;

var personaggio : GameObject;

var verticale : float;
var orizzontale : float;

var numerorandom : int;

var time : float;
var deltatime : float;

function Start () {
    numerorandom = Random.Range(1,5);
  //  roccia.Play();
}

function Update () {

    orizzontale = Input.GetAxisRaw("Horizontal");
    verticale = Input.GetAxisRaw("Vertical");

    
    if (verticale != 0 || orizzontale != 0){

        
        time = time + 0.1;

        if (time >= deltatime){
            time = 0;
            Passo();
        }
           
        /*
        numerorandom = Random.Range(1,3);

        if( !sabbiapiede.GetComponent.<AudioSource>().isPlaying){
            
            if (numerorandom == 1){
                
                sabbiapiede.GetComponent.<AudioSource>().Play();
            //    yield WaitForSeconds (2);
            }
        }

        if( !sabbiapiede2.GetComponent.<AudioSource>().isPlaying){
            if(numerorandom == 2){
           
                sabbiapiede2.GetComponent.<AudioSource>().Play();
              //  yield WaitForSeconds (5);
            }
        }
        
        */
    }

    else if (verticale == 0 || orizzontale == 0) {
        sabbiapiede.GetComponent.<AudioSource>().Stop();
        sabbiapiede2.GetComponent.<AudioSource>().Stop();
        sabbiapiede3.GetComponent.<AudioSource>().Stop();
        sabbiapiede4.GetComponent.<AudioSource>().Stop();
    }
 
}

function Passo(){
    
    numerorandom = Random.Range(1,5);

    if(numerorandom == 1)
        sabbiapiede.GetComponent.<AudioSource>().Play();

    if(numerorandom == 2)
        sabbiapiede2.GetComponent.<AudioSource>().Play();

    if(numerorandom == 3)
        sabbiapiede3.GetComponent.<AudioSource>().Play();

    if(numerorandom == 4)
        sabbiapiede4.GetComponent.<AudioSource>().Play();


    yield WaitForSeconds (1);
}