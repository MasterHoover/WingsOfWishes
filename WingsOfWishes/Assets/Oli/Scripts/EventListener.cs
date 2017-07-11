using UnityEngine;
using System.Collections;
using System;

public abstract class EventListener : MonoBehaviour 
{
	// EVENTS
	protected virtual void OnPause (object source, EventArgs e){}
	protected virtual void OnUnpause (object source, EventArgs e){}
	protected virtual void OnRespawn (object source, EventArgs e){}
	protected virtual void OnSave (object source, EventArgs e) {}

	protected virtual void Start ()
	{
		Subscribe ();
	}

	protected virtual void OnDestroy ()
	{
		UnSubscribe ();
	}

	private void Subscribe ()
	{
		if (_GameManager.Instance != null)
		{
			_GameManager.Instance.Pause += OnPause;
			_GameManager.Instance.Unpause += OnUnpause;
			_GameManager.Instance.Respawn += OnRespawn;
			_GameManager.Instance.Save += OnSave;
		}
		else
		{
			Debug.LogWarning ("EventListener[" + name + "] : _GameManager.Instance is null. Cannot subscribe.");
		}
	}

	private void UnSubscribe ()
	{
		if (_GameManager.Instance != null)
		{
			_GameManager.Instance.Pause -= OnPause;
			_GameManager.Instance.Unpause -= OnUnpause;
			_GameManager.Instance.Respawn -= OnRespawn;
			_GameManager.Instance.Save -= OnSave;
		}
		else
		{
			Debug.LogWarning ("EventListener[" + name + "] : _GameManager.Instance is null. Cannot unsubscribe.");
		}
	}
}
