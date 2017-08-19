using UnityEngine;
using System.Collections;
using System;

public class _GameManager : MonoBehaviour 
{
	private static _GameManager instance;

	// HANDLERS
	public delegate void PauseHandler (object source, EventArgs e);
	public delegate void UnpauseHandler (object source, EventArgs e);
	public delegate void RespawnHandler (object source, EventArgs e);
	public delegate void SaveHandler (object source, EventArgs e);

	// EVENTS
	public event PauseHandler Pause;
	public event UnpauseHandler Unpause;
	public event RespawnHandler Respawn;
	public event SaveHandler Save;
	public float respawnTimer = 2f;

	private bool paused;

	void Awake ()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
		else
		{
			DestroyImmediate (gameObject);
		}
	}

	void Update ()
	{
		if (Input.GetButtonDown ("StartButton"))
		{
			TogglePause ();
		}
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			Application.Quit ();
		}
	}
		
	private void TogglePause ()
	{
		paused = !paused;
		if (paused)
		{
			OnPause ();
		}
		else
		{
			OnUnpause ();
		}
	}

	public void LaunchRespawn ()
	{
		OnRespawn ();
	}

	public void TriggerPause ()
	{
		OnPause ();
	}

	public void TriggerUnpause ()
	{
		OnUnpause ();
	}

	// EVENT CALLERS
	protected void OnRespawn ()
	{
		if (Respawn != null)
		{
			Respawn (this, EventArgs.Empty);
		}
	}

	protected void OnPause ()
	{
		if (Pause != null)
		{
			paused = true;
			Pause (this, EventArgs.Empty);
		}
	}

	protected void OnUnpause ()
	{
		if (Unpause != null)
		{
			paused = false;
			Unpause (this, EventArgs.Empty);
		}
	}

	protected void OnSave ()
	{
		if (Save != null)
		{
			Save (this, EventArgs.Empty);
		}
	}

	public void PauseToRespawn ()
	{
		Invoke ("PauseToRespawnDone", respawnTimer);
		TriggerPause ();
	}

	private void PauseToRespawnDone ()
	{
		CheckpointManager.Instance.Respawn ();
		TriggerUnpause ();
		OnRespawn ();
	}

	public static _GameManager Instance
	{
		get{return instance;}
	}

	public bool Paused
	{
		get{return paused;}
	}

	public bool Respawning
	{
		get{return IsInvoking ("PauseToRespawnDone");}
	}
}
