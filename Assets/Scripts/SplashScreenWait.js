

var scena : int;
var time : float;

function Start () {

    StartCoroutine(WaitandLoad(time)) ;
        
}



function WaitandLoad (waitTime : float) {
  
    yield WaitForSeconds (4);
    Application.LoadLevel(scena);
}