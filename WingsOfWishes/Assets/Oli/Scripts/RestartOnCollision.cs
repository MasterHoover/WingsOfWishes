using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class RestartOnCollision : EventOnCollision 
{
	protected override void FireEvent (Collider2D col)
	{
		_GameManager.Instance.PauseToRespawn ();
	}
}
