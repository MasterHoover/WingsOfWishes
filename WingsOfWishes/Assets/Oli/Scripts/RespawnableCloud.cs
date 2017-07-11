using UnityEngine;
using System.Collections;

public class RespawnableCloud : Respawnable
{
	private CloudControllerFree moveScript;

	void Awake ()
	{
		savedPosition = transform.position;
		moveScript = GetComponent<CloudControllerFree> ();
	}

	public override void Reset ()
	{
		moveScript.Reset ();
	}

	public Vector3 SavedPosition
	{
		get{ return savedPosition; }
		set{savedPosition = value;}
	}
}
