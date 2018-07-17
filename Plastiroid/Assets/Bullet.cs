using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed = 10;
	public float radius = 0.02f; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.up * speed * Time.deltaTime;
		for (int i = 0; i < Debris.all.Count; i++){
			Debris deb = Debris.all[i];
			if (Vector3.Distance(transform.position, deb.transform.position)<deb.radius+radius){
				Destroy(this.gameObject);
				deb.split = true;
			}
		}
	}
}
