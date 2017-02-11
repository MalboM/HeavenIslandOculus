
var angolazioneX : float;
var angolazionesommatoria : float;
var angolazioneYfinale : float;
var lerpizzato : float;

 var yVelocity = 0.0;
private var tempointervallo = 0.07;


var usopad : boolean;

function Update () {
   
    //if(usopad ==false)
    angolazionesommatoria +=  Input.GetAxis("Mouse Y") *3.0;
    angolazionesommatoria = Mathf.Clamp(angolazionesommatoria, -60, 60);
    
   // lerpizzato = Mathf.SmoothDamp(angolazionesommatoria*0.4, angolazionesommatoria, yVelocity, tempointervallo);
    
  //  lerpizzato = Mathf.Clamp(10, 1,angolazionesommatoria);
   

    lerpizzato = Mathf.SmoothDamp(lerpizzato, angolazionesommatoria, yVelocity, tempointervallo);
    

    transform.eulerAngles.x =  -lerpizzato;

    if (Input.GetAxis("VerticalPadSecond") >= 0.1 || Input.GetAxis("VerticalPadSecond") <= -0.1) {
        angolazionesommatoria +=  Input.GetAxis("VerticalPadSecond") * -5.0;
        //usopad = true;
    }
}