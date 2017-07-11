using UnityEngine;
using System.Collections;

public class Bourasque : EventListener 
{
	public DetectionZone detectionZone;
	public float forceOnPlane = 4f;
	public float forceOnBullet = 60f;
	public float forceOnCerf = 10f;
	public ParticleSystem pSystem;

	void Update ()
	{
		GameObject[] detectedObjects = detectionZone.DetectedObjects;
		for (int i = 0; i < detectedObjects.Length; i++)
		{
			if (detectedObjects [i] != null)
			{
				if (detectedObjects [i].tag == "Projectile")
				{
					MomentumReceiver script = detectedObjects [i].GetComponent<MomentumReceiver> ();
					if (script != null)
					{
						script.AddExternalForce (detectionZone.transform.up.normalized * forceOnBullet);
					}
				} 
				else if (detectedObjects [i].tag == "Player")
				{
					MomentumReceiver script = detectedObjects [i].GetComponent<MomentumReceiver> ();
					if (script != null)
					{
						script.AddExternalForce (detectionZone.transform.up.normalized * forceOnPlane);
					}
				}
				else if (detectedObjects[i].tag == "CerfVolant")
				{
					MomentumReceiver script = detectedObjects[i].GetComponent<MomentumReceiver> ();
					if (script != null)
					{
						script.AddExternalForce (detectionZone.transform.up.normalized * forceOnCerf);
					}
				}
			}
		}
	}

	void OnDrawGizmos ()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawLine (transform.position, transform.position + detectionZone.transform.up * detectionZone.transform.localScale.y);
	}

	protected override void OnPause (object source, System.EventArgs e)
	{
		enabled = false;
		pSystem.Pause ();
	}

	protected override void OnUnpause (object source, System.EventArgs e)
	{
		enabled = true;
		pSystem.Play ();
	}
}
