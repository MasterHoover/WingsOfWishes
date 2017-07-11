using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class DestroyOnCollision : EventOnCollision 
{
	protected override void FireEvent (Collider2D col)
	{
		Destroy (col.gameObject);
	}
}
