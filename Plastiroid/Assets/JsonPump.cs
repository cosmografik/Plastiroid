using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class JsonPump : MonoBehaviour {

	public string testpath;
	static string testy;

	void Start(){
		testy = testpath;
	}

	// Update is called once per frame
	public static void Dump<T>(T obj) {
		string path = testy;
		string[] args = System.Environment.GetCommandLineArgs ();
		for (int i = 0; i < args.Length; i++) {
			if (args [i] == "-output") {
				path = args [i + 1];
			}
		}


		//Write some text to the test.txt file
		StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine(JsonUtility.ToJson(obj));
        writer.Close();
	}
}
