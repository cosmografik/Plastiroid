using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorebar : MonoBehaviour {

	public float fullwidth = 400;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		RectTransform rt = GetComponent<RectTransform>();
		Record rec = Score.record;
		float score = (float) rec.downdoots/(rec.downdoots+rec.updoots);
		rt.sizeDelta = new Vector2(fullwidth*score, rt.sizeDelta.y);
	}
}
