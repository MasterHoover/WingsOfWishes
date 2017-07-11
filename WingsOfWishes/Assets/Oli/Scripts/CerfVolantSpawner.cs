using UnityEngine;
using System.Collections;

public class CerfVolantSpawner : EventListener 
{
	public float respawnDelay = 6f;
	public MomentumReceiver prefab;
	private MomentumReceiver instance;

	public float guiWidth = 30f;
	public float guiHeight = 30f;

	private Delayer delayer = new Delayer ();

	protected override void Start ()
	{
		base.Start ();
		if (prefab != null)
		{
			RespawnCerfVolant ();
		}
	}

	void Update ()
	{
		if (CerfVolantDead && !Respawning)
		{
			Debug.Log ("CerfVolant is dead. Starting Coroutine.");
			StartCoroutine (delayer.Start (respawnDelay));
			delayer.DelayDone += OnDelayDone;
		}
	}

	private void RespawnCerfVolant ()
	{
		if (prefab != null)
		{
			if (CerfVolantDead)
			{
				instance = (MomentumReceiver) Instantiate (prefab, transform.position, Quaternion.identity);
			}
			else
			{
				instance.transform.position = transform.position;
			}
		}
	}

	void OnGUI ()
	{
		if (Respawning)
		{
			Vector3 guiPos = Camera.main.WorldToScreenPoint (transform.position);
			Rect r = new Rect (guiPos.x - guiWidth/2f, Screen.height - (guiPos.y + guiHeight/2f), guiWidth, guiHeight);
			//float elapsedTime = delayer.ElapsedTime;
			float remainingTime = delayer.Duration - delayer.ElapsedTime;
			if (remainingTime < 0f) remainingTime = 0f;
			string text = ((int) remainingTime).ToString ();
			GUI.TextArea (r, text);
		}
	}

	public bool CerfVolantDead
	{
		get
		{
			return instance == null;
		}
	}

	public void ForceRespawn ()
	{
		if (Respawning)
		{
			delayer.End ();
		}
		else
		{
			RespawnCerfVolant ();
		}
	}

	private bool Respawning
	{
		get{return delayer.IsPlaying;}
	}

	public MomentumReceiver CerfVolantInstance
	{
		get{return instance;}
	}

	protected void OnDelayDone (object source, System.EventArgs e)
	{
		Debug.Log ("Delay is done. Respawning CerfVolant");
		RespawnCerfVolant ();
	}

	protected override void OnPause (object source, System.EventArgs e)
	{
		enabled = false;
		delayer.Pause ();
	}

	protected override void OnUnpause (object source, System.EventArgs e)
	{
		enabled = true;
		delayer.Resume ();
	}

	protected override void OnRespawn (object source, System.EventArgs e)
	{
		Debug.LogWarning ("CerfVolantSpawner[" + name + "] : OnRespawn is not Implemented.");
	}
}
