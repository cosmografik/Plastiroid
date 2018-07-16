using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour {

	public float power;
	public float turnPower;
	public float damp;
	public float turnDamp;
	public float gravity;
	public GameObject flame;
	float rotVel;
	[HideInInspector]
	public Vector3 vel;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Ocean ocean = Ocean.ocean;
		bool humanTorch = false;
		if (ocean.Submerged(transform.position)){
			if (Input.GetKey(KeyCode.K)){
				humanTorch = true;
				vel += transform.up * power * Time.deltaTime;
			}
			float turn = (Input.GetKey(KeyCode.Q) ? 1 : 0) + (Input.GetKey(KeyCode.D) ? -1 : 0);
			rotVel += turn * turnPower * Time.deltaTime;
			rotVel -= rotVel * turnDamp * Time.deltaTime;
			vel -= vel * damp * Time.deltaTime;
		} else {
			vel += Vector3.down * gravity * Time.deltaTime;
		}
		flame.SetActive(humanTorch);
		transform.position += vel * Time.deltaTime;
		if (Vector3.Distance(ocean.transform.position, transform.position)>ocean.radius){
			Vector3 fromTo = ocean.transform.position - transform.position;
			transform.position += fromTo * 2;
		}
		transform.rotation = Quaternion.AngleAxis(rotVel * Time.deltaTime, Vector3.forward) * transform.rotation;
	}
}
