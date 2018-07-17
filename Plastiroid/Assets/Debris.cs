using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Debris : MonoBehaviour {

	float _radius;
	public float radius;
	public float bouyancy;
	public float dampening;
	public float oceanPush;
	public float pushaway;
	public float lerpRot = 1;
	bool inside = false;
	[HideInInspector]
	public Vector3 vel;
	public bool split;
	public float splitRadiusBoost;
	public GameObject splitPrefab;
	Spaceship spaceship;
    static List<Debris> _all;
    public static List<Debris> all{
        get {
            if (_all==null){
                _all = new List<Debris>();
            }
            return _all;
        }
    }
	public float microplastic = 0.05f;


	// Use this for initialization
	void Start () {
		spaceship = FindObjectOfType<Spaceship>();
		split = false;
		all.Add(this);
		Debug.Log("Debris: "+all.Count);
		_radius = radius;
		DrawDebris();
	}
	void OnDestroy(){
		all.Remove(this);
	}
	
	bool Broad(Debris other){
		float rsum = other.radius + radius;
		Vector3 d = transform.position-other.transform.position;
		return (
			Mathf.Abs(d.y)<rsum &&
			Mathf.Abs(d.x)<rsum
		);
	}
	bool Fine(Debris other){
		return Vector3.Distance(other.transform.position, transform.position) <= other.radius + this.radius;
	}
	public int splitCount = 2;
	public float splitForce;
	void DoSplit(){
		float rang = Random.value * 360;
		float rad = Mathf.Sqrt((radius * radius) / splitCount)*splitRadiusBoost;
		for (int i = 0; i < splitCount; i++){
			GameObject go = GameObject.Instantiate(splitPrefab, transform.position, transform.rotation);
			Debris deb = go.GetComponent<Debris>();
			deb.radius = rad;
			deb.vel = vel + Quaternion.AngleAxis(rang + i * (360 / splitCount), Vector3.forward) * Vector3.right * splitForce;
			go.transform.parent = transform.parent;
		}
		SoundTarget.Play("explosion"+Random.Range(1,4));
		Destroy(this.gameObject);
	}

	int segments = 12;
	void DrawDebris(){
		LineRenderer lr = GetComponent<LineRenderer>();
		if (lr==null){
			lr = gameObject.AddComponent<LineRenderer>();
		}
		Vector3[] positions = new Vector3[segments];
		for (int i = 0; i < segments; i++){
			float angle = i * (360 / segments);
			positions[i] = Quaternion.AngleAxis(angle, Vector3.forward) *
							Vector3.up * Random.Range(radius*0.5f,radius*1.25f);
		}
		lr.positionCount = segments;
		lr.SetPositions(positions);
	}

	// Update is called once per frame
	void Update () {
		if (_radius!=radius){
			_radius = radius;
			DrawDebris();
		}
		Ocean ocean = Ocean.ocean;
		if (ocean.Submerged(transform.position)){
			Vector3 pushField = ocean.PushField(transform.position);
			Vector3 nfield = vel.normalized;
			Quaternion trot = Quaternion.AngleAxis(Mathf.Atan2(nfield.y, nfield.x) * Mathf.Rad2Deg, Vector3.forward);
			transform.rotation = Quaternion.Slerp(transform.rotation, trot, lerpRot * Time.deltaTime);
			vel += pushField * oceanPush * Time.deltaTime;
			vel += Vector3.up * bouyancy * Time.deltaTime;
			if (Vector3.Distance(ocean.transform.position, transform.position)>ocean.radius-radius){
				Vector3 pushDir = (transform.position - ocean.transform.position);
				if (!inside){
					pushDir.y = 0;
				}
				vel -= pushDir.normalized * pushaway * Time.deltaTime;
			}
			vel -= vel * dampening * Time.deltaTime;
		} else {
			vel += Physics.gravity * Time.deltaTime;
		}
		if (inside && radius>microplastic){
			for (int i = all.Count - 1; i >= 0; i--) {
				Debris deb = all[i];
				if (deb != this)
				{
					if (Broad(deb) && Fine(deb)) {
						vel += (transform.position - deb.transform.position).normalized * pushaway * Time.deltaTime;
					}
				}
			}
		}
	}

	void LateUpdate() {
		Ocean ocean = Ocean.ocean;
		bool spacehit = Vector3.Distance(spaceship.transform.position, transform.position) <= 0.2f + this.radius;
		if (spacehit){
			Debug.Log("Spacehit!");
			vel = (transform.position - spaceship.transform.position).normalized * spaceship.vel.magnitude;
			Vector3 fromTo = (transform.position - spaceship.transform.position).normalized;
			transform.position = spaceship.transform.position + fromTo * (0.2f + radius)+fromTo*Time.deltaTime;
		}
		transform.position += vel * Time.deltaTime;
		if (Vector3.Distance(ocean.transform.position, transform.position) > ocean.radius - radius) {
			if (inside) {
				Vector3 fromTo = (transform.position - ocean.transform.position).normalized;
				transform.position = ocean.transform.position + fromTo * (ocean.radius - radius);
			}
		} else {
			inside = true;
		}
		if (split){
			split = false;
			DoSplit();
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.white;
		GizmoUtils.DrawCircle(transform.position, radius);
	}

}
