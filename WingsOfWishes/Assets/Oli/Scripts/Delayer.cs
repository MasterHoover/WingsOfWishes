using UnityEngine;
using System.Collections;
using System;

public class Delayer
{
	public delegate void DelayDoneHandler (object source, EventArgs e);
	public event DelayDoneHandler DelayDone;
	private float elapsedTime;
	private float lastTime;
	private bool forcePause;
	private float delay;
	private bool forceExit;
	private bool forceEnd;

	public IEnumerator Start (float delay)
	{
		this.delay = delay;
		forcePause = false;
		elapsedTime = 0f;
		lastTime = Time.time - 0.001f;
		forceExit = false;
		while (elapsedTime < delay)
		{
			if (!forceExit)
			{
				if (!forcePause)
				{
					elapsedTime += (Time.time - lastTime);
				}
				lastTime = Time.time;
				yield return null;
			}
			else
			{
				break;
			}
		}
		if (!forceExit)
		{
			OnDelayDone ();
		}
	}
		
	void OnDelayDone ()
	{
		elapsedTime = 0f;
		if (DelayDone != null)
		{
			DelayDone (this, EventArgs.Empty);
		}
	}

	public void Pause ()
	{
		forcePause = true;
	}

	public void Resume ()
	{
		forcePause = false;
	}

	public void Stop ()
	{
		forceExit = true;
	}

	public void End ()
	{
		elapsedTime = float.MaxValue;
	}

	public bool IsPlaying
	{
		get{return !forcePause && elapsedTime > 0f;}
	}

	public float Duration
	{
		get{return delay;}
	}

	public float ElapsedTime
	{
		get{return elapsedTime;}
	}
}
