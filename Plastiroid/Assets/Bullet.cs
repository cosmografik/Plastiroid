using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed = 10;
	public float radius = 0.02f;
	public float impactForce = 2;
	public int loops = 1;
	int looped = 0;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Ocean ocean = Ocean.ocean;
		transform.position += transform.up * speed * Time.deltaTime;
		if (Vector3.Distance(ocean.transform.position, transform.position)>ocean.radius){
			if (looped>=loops){
				Destroy(this.gameObject);
			} else {
				Vector3 fromTo = ocean.transform.position - transform.position;
				transform.position += fromTo * 1.99f;
				looped++;
			}
		}
		for (int i = 0; i < Debris.all.Count; i++){
			Debris deb = Debris.all[i];
			if (Vector3.Distance(transform.position, deb.transform.position)<deb.radius+radius){
				if (!deb.CompareTag("plastic")){
					Score.record.Down();
				}
				deb.vel += transform.up * impactForce;
				Destroy(this.gameObject);
				deb.split = true;
			}
		}
	}
}
