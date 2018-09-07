using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocean : MonoBehaviour {

	static Ocean _ocean;
	public static Ocean ocean {
		get {
			if (_ocean==null)
				_ocean = FindObjectOfType<Ocean>();
			return _ocean;
		}
	}

	public float radius;
	public float waterLevel;
	public float waveAmplitude;
	public float waveFrequency;
	public float waveOffsetRate;
	public float wavePushWob;
	public float wavePushWobRate;

	public MeshFilter porthole;
	public float outerPortHoleRadius;
	public int portholeSegments;

	public MeshFilter sea;
	public int seaPoints = 50;


	// Use this for initialization
	void Start () {
		GeneratePorthole();
	}
	
	// Update is called once per frame
	void Update () {
		GenerateSeaSurface();
	}

	void GeneratePorthole(){
		Mesh mesh = new Mesh();
		Vector3[] verts = new Vector3[2 * portholeSegments];
		int[] tris = new int[6 * portholeSegments];
		for (int i = 0; i < portholeSegments; i++){
			Quaternion rot = Quaternion.AngleAxis(i * 2 * (360.0f / portholeSegments), Vector3.forward);
			verts[i * 2] = rot * Vector3.up * radius;
			verts[i * 2+1] = rot * Vector3.up * (radius + outerPortHoleRadius);
			tris[i * 6 + 0] = (i*2+0) % (2 * portholeSegments);
			tris[i * 6 + 1] = (i*2+2) % (2 * portholeSegments);
			tris[i * 6 + 2] = (i*2+1) % (2 * portholeSegments);
			tris[i * 6 + 3] = (i*2+1) % (2 * portholeSegments);
			tris[i * 6 + 4] = (i*2+2) % (2 * portholeSegments);
			tris[i * 6 + 5] = (i*2+3) % (2 * portholeSegments);
		}
		mesh.vertices = verts;
		mesh.triangles = tris;
		porthole.mesh = mesh;
	}

	void GenerateSeaSurface(){
		Mesh mesh = new Mesh();
		Vector3[] verts = new Vector3[seaPoints*2+2];
		int[] tris = new int[seaPoints*6+6];
		for (int i = 0; i < seaPoints+1; i++){
			float xpos = Mathf.Lerp(-radius, radius, (float) i / (float) seaPoints);
			float ypos = WavePoint(xpos);
			verts[i * 2] = new Vector3(xpos, ypos, 0);
			verts[i * 2+1] = new Vector3(xpos, -radius, 0);
		}
		for (int i = 0; i < seaPoints; i++){
			tris[i * 6 + 0] = i * 2 + 0;
			tris[i * 6 + 1] = i * 2 + 2;
			tris[i * 6 + 2] = i * 2 + 1;
			tris[i * 6 + 3] = i * 2 + 1;
			tris[i * 6 + 4] = i * 2 + 2;
			tris[i * 6 + 5] = i * 2 + 3;
		}
		mesh.vertices = verts;
		mesh.triangles = tris;
		sea.mesh = mesh;
	}

	public Vector3 WavePoint (Vector3 pos){
		pos.y = WavePoint(pos.x);
		return pos;
	}
	public float WavePoint (float pos){
		float p = pos;
		if (radius==0){
			return 0;
		}
		p /= (radius * 2);
		p *= waveFrequency;
		p += waveOffsetRate * Time.time;
		p *= Mathf.PI;
		float h = waterLevel + Mathf.Sin(p) * waveAmplitude;
		return h;
	}

	public Vector3 PushField(Vector3 pos){
		float p = pos.x * Mathf.Sin((pos.y*wavePushWob+Time.time*wavePushWobRate)/Mathf.PI);
		if (radius==0){
			return Vector3.zero;
		}
		p /= (radius * 2);
		p *= waveFrequency;
		p += waveOffsetRate * Time.time;
		//p *= Mathf.PI;
		return Quaternion.AngleAxis(p*180, Vector3.forward) * Vector3.left;
	}

	public bool InOcean(Vector3 point){
		point.z = transform.position.z;
		return Vector3.Distance(point, transform.position) <= radius;
	}

	public bool Submerged(Vector3 point){
		Vector3 wp = WavePoint(point);
		return point.y <= wp.y;
	}

	Quaternion WaveRotation (){
		return Quaternion.identity;
	}

	void OnDrawGizmos(){

		Gizmos.color = Color.grey;
		for (int x = -10; x <= 10; x++){
			for (int y = -10; y <= 10; y++){
				Vector3 p = new Vector3(x/2.0f, y/2.0f, 0);
				Vector3 push = PushField(p);
				Gizmos.DrawRay(p, push*0.1f);
			}
		}

		Gizmos.color = Color.white;
		//GizmoUtils.DrawCircle(transform.position, radius);
		if (seaPoints<=0)
			return;
		for (float w = -radius; w <= radius; w+=radius/seaPoints){
			Vector3 p1 = WavePoint(transform.position + Vector3.right * w);
			Vector3 p2 = WavePoint(transform.position + Vector3.right * (w+radius/seaPoints));
			//if (InOcean(p1)||InOcean(p2))
				Gizmos.DrawLine(p1, p2);
		}

	}

}
