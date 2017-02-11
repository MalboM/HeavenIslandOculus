var ovrrig : GameObject;

var time : float;
var deltatime : float;
var attivato : boolean;

function Start () {
    this.GetComponent("OVRPlayerController").enabled = false;
    ovrrig.GetComponent("CameraYMouse").enabled = false;
}

function Update () {

    if(attivato == false)
        Attivato();

}


function Attivato() {

    yield WaitForSeconds (2.2);
    this.GetComponent("OVRPlayerController").enabled = true;
    ovrrig.GetComponent("CameraYMouse").enabled = true;
    attivato = true;
}