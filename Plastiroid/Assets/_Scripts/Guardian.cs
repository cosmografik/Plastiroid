using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardian : MonoBehaviour {

	public int maxPlastics;
	public float microplastic;
	[HideInInspector]
	public float mass = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		ClearExcessMicroplastics();
		mass = 0;
		for (int i = 0; i < Debris.all.Count; i++){
			Debris deb = Debris.all[i];
			mass += Mathf.PI * deb.radius * deb.radius;
		}
	}

	// void OnGUI(){
	// 	GUI.Label(new Rect(10, 10, 100, 100), "Plastics: " + Debris.all.Count);
	// 	GUI.Label(new Rect(10, 20, 100, 100), "Mass: " + mass);
	// }

	void ClearExcessMicroplastics(){
		if (Debris.all.Count>maxPlastics){
			float msize = microplastic;
			while (true) {
				for (int i = 0; i<Debris.all.Count; i++) {
					Debris deb = Debris.all[i];
					if (deb.radius <= msize) {
						GameObject.Destroy(deb.gameObject);
						return;
					}
				}
				msize *= 2;
			}
		}
	}
}
