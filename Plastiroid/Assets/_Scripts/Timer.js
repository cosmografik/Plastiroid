#pragma strict

var timer : float = 10.0;

function Update () {

timer -= Time.deltaTime;

if(timer <= 0){
// FAIRE QQCH QAUND TIMER <= 0
timer = 0;
Debug.Log("Timer = 0");
}
}

function OnGUI(){
GUI.Box(new Rect(10,10,50,25), timer.ToString("0"));
}