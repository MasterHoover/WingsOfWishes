using UnityEngine;
using System.Collections;

public class StunCloud : MonoBehaviour 
{
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Cloud")
		{
			col.GetComponent<CloudControllerFree> ().Stun ();
		}
	}
}
