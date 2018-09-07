using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomiseDebris : MonoBehaviour {

	public float minBouy;
	public float maxBouy;
	public float minDamp;
	public float maxDamp;
	public float minPush;
	public float maxPush;
	public float minRadi;
	public float maxRadi;
	public bool randomiseRadii = true;

	// Use this for initialization
	void Start () {
		//transform.position = transform.position + Vector3.right * (Random.value>0.5f ? 5: -5);
		Debris deb = GetComponent<Debris>();
		deb.bouyancy = Mathf.Lerp(minBouy, maxBouy, Random.value);
		deb.dampening = Mathf.Lerp(minDamp, maxDamp, Random.value);
		deb.oceanPush = Mathf.Lerp(minPush, maxPush, Random.value);
		if (randomiseRadii)
			deb.radius = Mathf.Lerp(minRadi, maxRadi, Random.value);
	}
}
