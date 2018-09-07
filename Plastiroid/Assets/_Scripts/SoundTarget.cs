using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTarget : MonoBehaviour {

    // Static collection
    static Dictionary<string, SoundTarget> _targets;
    public static Dictionary<string, SoundTarget> targets{
        get {
            if (_targets==null){
                _targets = new Dictionary<string, SoundTarget>();
            }
            return _targets;
        }
    }


	// Use this for initialization
	void Start () {

        this.GetComponent<AudioSource>();
		targets.Add(name, this);
	}

	// when the object is removed
	void OnDestroy(){
		targets.Remove(name);
	}

	public static void Play(string name){
        if (targets.ContainsKey(name))
            targets[name].GetComponent<AudioSource>().Play();
        else
            Debug.Log("No sound with name " + name + " in scene");
	}
}
