using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoUtils{
	public static void DrawCircle(Vector3 center, float radius, Vector3 forwards){
		int sides = 10;
		float angleStep = 360.0f / sides;
		Vector3 up = Vector3.Cross(forwards, Vector3.up).normalized;
		for (int i = 0; i <= sides; i++){
			float angleOne = i * angleStep;
			float angleTwo = (i + 1) * angleStep;
			Vector3 p1 = center + (Quaternion.AngleAxis(angleOne, forwards) * up) * radius;
			Vector3 p2 = center + (Quaternion.AngleAxis(angleTwo, forwards) * up) * radius;
			Gizmos.DrawLine(p1, p2);
		}
	}
	public static void DrawCircle(Vector3 center, float radius){
		DrawCircle(center, radius, Vector3.forward);
	}
	public static void DrawCircle(Vector3 center){
		DrawCircle(center, 1, Vector3.forward);
	}
	public static void DrawCircle(){
		DrawCircle(Vector3.zero, 1, Vector3.forward);
	}

}
