using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


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
	public Bullet bulletPrefab;

	public UnityEvent fire;
	public float fireRate = 7;
	public bool bounceOnEdges;
	float fireCool;


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
		if (Vector3.Distance(ocean.transform.position, transform.position)>=ocean.radius){
			Vector3 fromTo = ocean.transform.position - transform.position;
			if (bounceOnEdges){
				transform.position = ocean.transform.position - fromTo.normalized*ocean.radius;
			} else {
				transform.position += fromTo * 1.99f;
			}
		}
		transform.rotation = Quaternion.AngleAxis(rotVel * Time.deltaTime, Vector3.forward) * transform.rotation;

		fireCool = Mathf.Max(fireCool - Time.deltaTime * fireRate, 0);
		if (fireCool<=0 && Input.GetKey(KeyCode.M)){
			fireCool += 1;
			fire.Invoke();
			Instantiate(bulletPrefab, transform.position, transform.rotation);
		}
		
	}
}
