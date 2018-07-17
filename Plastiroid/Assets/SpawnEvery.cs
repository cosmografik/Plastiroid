using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEvery : MonoBehaviour {

	public GameObject prefab;
	public float seconds;
	public float maximumMass = 3;
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
			Guardian guardian = FindObjectOfType<Guardian>();
			if (guardian.mass<maximumMass){
				DoSpawn();
			}
		}
	}

	public void DoSpawn(){
		GameObject go = GameObject.Instantiate(prefab, transform.position + Vector3.right*(Random.value > 0.5f ? 6 : -6), transform.rotation);
				go.transform.parent = this.transform;
	}

}
