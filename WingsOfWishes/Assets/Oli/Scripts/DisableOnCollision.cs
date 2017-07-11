using UnityEngine;
using System.Collections;

public class DisableOnCollision : EventOnCollision 
{
	protected override void FireEvent (Collider2D col)
	{
		col.gameObject.SetActive (false);
	}
}
