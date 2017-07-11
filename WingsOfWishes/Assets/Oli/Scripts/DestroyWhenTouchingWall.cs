using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class DestroyWhenTouchingWall : MonoBehaviour 
{
	public const string destroyerTag = "Destroyer";
	public const string destructibleTag = "Destructible";

	void OnTriggerEnter (Collider col)
	{
		if (col.tag == destroyerTag || col.tag == destructibleTag)
		{
			DestroySelf ();
		}
	}

	private void DestroySelf ()
	{
		if (tag == "Player")
		{
			if (CheckpointManager.Instance != null)
			{
				_GameManager.Instance.PauseToRespawn ();
			}
			else
			{
				Debug.LogWarning ("DestroyWhenTouchingWall/DestroySelf () : Tried to respawn, but no Respawner in scene.");
			}
		}
		else
		{
			Destroy (gameObject);
		}
	}
}
