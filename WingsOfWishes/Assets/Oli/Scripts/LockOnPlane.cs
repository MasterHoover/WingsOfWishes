using UnityEngine;
using System.Collections;

public class LockOnPlane : MonoBehaviour 
{
	private Transform target;

	void Start ()
	{
		GameObject plane = GameObject.FindGameObjectWithTag ("Cloud");
		if (plane != null) 
		{
			target = plane.transform;
		} 
		else 
		{
			Debug.LogWarning ("No Plane was found in scene.");
			enabled = false;
		}
	}

	void LateUpdate () 
	{
		Vector3 targetPos = target.position;
		float z = transform.position.z;
		transform.position = new Vector3 (targetPos.x, targetPos.y, z);
	}
}
