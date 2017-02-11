
var bounceSpeed = 1.0;
var bounceAmount = 2.0;

var player : GameObject;

function Update() {
    player.transform.Translate(Vector3.up * Time.deltaTime, Camera.main.transform);
}