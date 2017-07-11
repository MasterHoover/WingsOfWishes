using UnityEngine;
using System.Collections;

public class Projectile : EventListener 
{
	public const string planeTag = "Player";
	public const string destructibleTag = "Destructible";
	public const string destroyerTag = "Destroyer";

	protected override void Start ()
	{
		base.Start ();
		if (!ImportantPointers.Instance.Projectiles.Contains (this))
		{
			ImportantPointers.Instance.Projectiles.Add (this);
		}
	}
	/*
	void OnTriggerEnter (Collider col)
	{
		if (col.tag == planeTag)
		{
			Explosion ();
			CheckpointManager.Instance.PauseToRespawn ();
		}
		else if (col == ImportantPointers.Instance.Cloud.GetComponent<CloudControllerFree> ().spriteCollider)
		{
			ImportantPointers.Instance.Cloud.GetComponent<CloudControllerFree> ().Stun ();
			Explosion ();
		}
		else if (col.tag == destructibleTag)
		{
			col.gameObject.SetActive (false);
			Explosion ();
		}
		else if (col.tag == destroyerTag)
		{
			Explosion ();
		}
	}

	private void Explosion ()
	{
		ImportantPointers.Instance.Projectiles.Remove (this);
		Destroy (gameObject);
	}
	*/
}
