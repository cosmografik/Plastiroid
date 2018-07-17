using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTarget : MonoBehaviour {

	// Static collection
	public static Dictionary<string, SoundTarget> targets;


	// Use this for initialization
	void Start () {

		this.GetComponent<AudioSource>();


		if (targets==null){
			targets = new Dictionary<string, SoundTarget>();
		}
		targets.Add(name, this);
	}

	// when the object is removed
	void OnDestroy(){
		targets.Remove(name);
	}

	public static void Play(string name){
		targets[name].GetComponent<AudioSource>().Play();
	}
}
