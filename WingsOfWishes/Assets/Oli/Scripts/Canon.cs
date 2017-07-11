using UnityEngine;
using System.Collections;
using System;

public class Canon : EventListener
{
	public MomentumReceiver bulletPrefab;
	public Transform sprite;
	public DetectionZone detectionZone;
	public CanonShot[] shots = new CanonShot[1];

	protected override void Start ()
	{
		base.Start ();
		for (int i = 0; i < shots.Length && i <= ushort.MaxValue; i++)
		{
			if (shots[i] != null)
			{
				shots[i].Delayer = new Delayer ();
				shots[i].Delayer.DelayDone += OnDelayDone;
				StartCoroutine (shots[i].Delayer.Start (shots[i].initialDelay));
			}
		}
	}

	private void Shoot (int index)
	{
		CanonShot shot = shots[index];
		if (shot.direction != null)
		{
			Vector2 direction = (shot.direction.position - sprite.position).normalized;
			MomentumReceiver bullet = (MomentumReceiver) Instantiate (bulletPrefab, VectorFunc.Make2D (sprite.position) + direction * shot.spawnDistance, Quaternion.identity);
			bullet.AddExternalForce (direction * shot.launchSpeed);
			bullet.GetComponent<ConstantMomentum> ().constant = shot.launchSpeed;
			StartCoroutine (shots[index].Delayer.Start (shots[index].shootDelay));
		}
	}

	void OnDrawGizmos ()
	{
		if (sprite != null && shots != null)
		{
			Gizmos.color = Color.blue;
			for (int i = 0; i < shots.Length; i++)
			{
				if (shots[i].direction != null)
				{
					Gizmos.DrawLine (sprite.position, sprite.position - ((sprite.position - shots[i].direction.position).normalized * shots[i].spawnDistance));
				}
			}
		}
	}

	public bool Destroyed
	{
		get{ return !sprite.gameObject.activeSelf; }
	}

	public void Rebuild ()
	{
		sprite.gameObject.SetActive (true);
	}

	public void Destroy ()
	{
		sprite.gameObject.SetActive (false);
	}

	private int DelayerIndex (Delayer d)
	{
		for (int i = 0; i < shots.Length; i++)
		{
			if (shots[i].Delayer == d)
			{
				return i;
			}
		}
		return -1;
	}

	private void OnDelayDone (object source, EventArgs e)
	{
		int index = DelayerIndex ((Delayer) source);
		if (sprite != null && sprite.gameObject.activeSelf && (detectionZone == null || (detectionZone != null && detectionZone.DetectingPlayer)))
		{
			Shoot (index);	
		}
	}

	protected override void OnPause (object source, System.EventArgs e)
	{
		for (int i = 0; i < shots.Length; i++)
		{
			if (shots[i] != null && shots[i].Delayer != null)
			{
				shots[i].Delayer.Pause ();
			}
		}
	}

	protected override void OnUnpause (object source, System.EventArgs e)
	{
		for (int i = 0; i < shots.Length; i++)
		{
			if (shots[i] != null && shots[i].Delayer != null)
			{
				shots[i].Delayer.Resume ();
			}
		}
	}

	protected override void OnRespawn (object source, System.EventArgs e)
	{
		Debug.LogWarning ("Canon[" + name + "] : OnRespawn is not Implemented.");
	}

	[System.Serializable]
	public class CanonShot
	{
		public Transform direction;
		public float launchSpeed = 7f;
		public float initialDelay;
		public float shootDelay = 4f;
		public float spawnDistance = 3f;
		private Delayer delayer;

		public Delayer Delayer
		{
			get{return delayer;}
			set{delayer = value;}
		}
	}
}
