using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CerfVolantSwitch : EventOnCollision 
{
	public GameObject door;
	private List<GameObject> objectsInside = new List<GameObject> ();

	void Update ()
	{
		if (door != null)
		{
			if (door.activeSelf && objectsInside.Count > 0)
			{
				door.SetActive (false);
			}
			else if (!door.activeSelf && objectsInside.Count == 0)
			{
				door.SetActive (true);
			}
		}
	}

	protected override void FireEvent (Collider2D col)
	{
		objectsInside.Add (col.gameObject);
		LinkedToSystem sys = col.gameObject.GetComponent<LinkedToSystem> ();
		if (sys != null)
		{
			sys.System.CancelReturn ();
		}
	}

	protected override void DisableEvent (Collider2D col)
	{
		objectsInside.Remove (col.gameObject);
		LinkedToSystem sys = col.gameObject.GetComponent<LinkedToSystem> ();
		if (sys != null)
		{
			sys.System.RetryReturn ();
		}
	}
}
