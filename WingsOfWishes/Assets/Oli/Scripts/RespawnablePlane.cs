using UnityEngine;
using System.Collections;

public class RespawnablePlane : Respawnable 
{
	private MomentumReceiver moveScript;

	void Awake ()
	{
		savedPosition = transform.position;
		moveScript = GetComponent<MomentumReceiver> ();
		if (moveScript == null)
		{
			Debug.LogWarning ("RespawnablePlane : No CloudControllerFree script attached to object.");
		}
	}

	public override void Reset ()
	{
		if (moveScript != null)
		{
			moveScript.Reset ();
		}
	}
}
