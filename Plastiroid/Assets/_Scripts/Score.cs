using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Record {
	public int score;
	public int updoots;
	public int downdoots;
	public void Bump(){
		score++;
		updoots++;
	}
	public void Down(){
		score--;
		downdoots++;
	}
}


public class Score : MonoBehaviour {

	public Record recordDisp;
	public static Record record;

	void Awake(){
		record = new Record();
		recordDisp = record;
	}

	void Update(){
	}


}
