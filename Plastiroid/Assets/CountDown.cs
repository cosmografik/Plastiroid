using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {

	float startTime;
	public int countFrom = 120;
	Text text;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		int timeLeft = Mathf.FloorToInt(Mathf.Max(0,countFrom - (Time.time-startTime)));
		int minutes = timeLeft/60;
		int seconds = timeLeft%60;
		string timestring = string.Format("{0:D2}:{1:D2}", minutes, seconds);
		text.text = timestring;
	}
}
