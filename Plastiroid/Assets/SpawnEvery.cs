using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEvery : MonoBehaviour {

	public GameObject prefab;
	public float seconds;
	float time = 0;

	// Use this for initialization
	void Start () {
		time = 0;
	}
	
	// Update is called once per frame
	void Update () {
		time -= Time.deltaTime;
		if (time<=0){
			time += seconds;
			GameObject go = GameObject.Instantiate(prefab, transform.position, transform.rotation);
			go.transform.parent = this.transform;
		}
	}
}
