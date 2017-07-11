using UnityEngine;
using System.Collections;

public class CloudControllerAround : MonoBehaviour 
{
	public Cloud cloud;
	public float deadZone = 0.1f;
	public float rotationSpeed = 5f;
	public float distanceWithCloud = 2f;

	public float angle;
	private Vector3 direction = new Vector3 (1f, 0f, 0f);

	void Start ()
	{
		if (cloud == null)
		{
			Debug.LogWarning ("CloudController : No cloud has been assigned.");
			enabled = false;
		} 
		else
		{
			cloud.transform.position = transform.position + direction * distanceWithCloud;
			cloud.transform.up = direction;
		}
	}

	void Update ()
	{
		float x = Input.GetAxisRaw ("Horizontal");
		x = x < deadZone || x > deadZone ? x : 0f;

		if (x != 0f)
		{
			if (x < 0f)
			{
				RotateLeft ();
			} 
			else
			{
				RotateRight ();
			}
		}

		PlaceCloudWithAngle ();

	}

	private Vector3 GetDirectionFromAngle (float angle)
	{
		float angleInRad = angle * Mathf.Deg2Rad;
		return new Vector3 (Mathf.Cos (angleInRad) * distanceWithCloud, Mathf.Sin (angleInRad) * distanceWithCloud, 0f);
	}
		
	private void PlaceCloudWithAngle ()
	{
		direction = GetDirectionFromAngle (angle);
		cloud.transform.position = transform.position + direction * distanceWithCloud;
		cloud.transform.up = direction;
	}

	private void RotateLeft ()
	{
		angle += (rotationSpeed * Time.deltaTime);
	}

	private void RotateRight ()
	{
		angle -= (rotationSpeed * Time.deltaTime);
	}
}
