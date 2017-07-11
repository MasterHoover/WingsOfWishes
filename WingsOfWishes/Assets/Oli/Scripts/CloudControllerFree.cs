using UnityEngine;
using System.Collections;

public class CloudControllerFree : EventListener
{
	public float maxSpeed = 50f;
	public float speedInc = 15f;
	public float speedDec = 15f;

	private float currentXSpeed;
	private float currentYSpeed;

	public float radialDeadZone = 0.2f;
	public float absoluteDeadZone = 0.1f;

	public float stunDuration = 1f;

	public BoxCollider spriteCollider;
	private Delayer delayer;
	private bool stunned;

	protected override void Start ()
	{
		base.Start ();
		delayer = new Delayer ();
		delayer.DelayDone += OnDelayDone;
	}

	void Update ()
	{
		if (_GameManager.Instance != null && !_GameManager.Instance.Respawning && !stunned)
		{
			UpdateSpeed ();
			Move ();
		}
	}

	private void Move ()
	{
		transform.Translate (new Vector3 (currentXSpeed * Time.deltaTime, currentYSpeed * Time.deltaTime, 0f), Space.World);
	}

	private void UpdateSpeed ()
	{
		float x = Input.GetAxisRaw ("Horizontal");
		float y = Input.GetAxisRaw ("Vertical");

		if (new Vector2 (x, y).magnitude > radialDeadZone)
		{
			// HORIZONTAL
			if (x > absoluteDeadZone || x < -absoluteDeadZone)
			{
				UpdateHorizontal (x);
			} 
			else
			{
				UpdateHorizontal ();
			}

			// VERTICAL
			if (y > absoluteDeadZone || y < -absoluteDeadZone)
			{
				UpdateVertical (y);
			}
			else
			{
				UpdateVertical ();
			}
		} 
		else
		{
			UpdateHorizontal ();
			UpdateVertical ();
		}
	}

	// Decrease horizontal speed
	private void UpdateHorizontal ()
	{
		float inc = speedDec * Time.deltaTime;
		if (currentXSpeed < 0f)
		{
			if (currentXSpeed + inc < 0f)
			{
				currentXSpeed += inc;
			}
			else
			{
				currentXSpeed = 0f;
			}
		} 
		else if (currentXSpeed > 0f)
		{
			if (currentXSpeed - inc > 0f)
			{
				currentXSpeed -= inc;
			} 
			else
			{
				currentXSpeed = 0f;
			}
		}
	}

	// Increase horizontal speed
	private void UpdateHorizontal (float xInput)
	{
		if (currentXSpeed * xInput < 0f)
		{
			UpdateHorizontal ();
		}

		float inc = speedInc * Time.deltaTime;

		if (xInput < 0f)
		{
			if (currentXSpeed - inc > -maxSpeed)
			{
				currentXSpeed -= inc;
			}
			else
			{
				currentXSpeed = -maxSpeed;
			}
		}
		else if (xInput > 0f)
		{
			if (currentXSpeed + inc < maxSpeed)
			{
				currentXSpeed += inc;
			}
			else
			{
				currentXSpeed = maxSpeed;
			}
		}
	}

	// Decrease vertical speed
	private void UpdateVertical ()
	{
		float inc = speedDec * Time.deltaTime;
		if (currentYSpeed < 0f)
		{
			if (currentYSpeed + inc < 0f)
			{
				currentYSpeed += inc;
			} 
			else
			{
				currentYSpeed = 0f;
			}
		} 
		else
		{
			if (currentYSpeed - inc > 0f)
			{
				currentYSpeed -= inc;
			}
			else
			{
				currentYSpeed = 0f;
			}
		}
	}

	// Increase vertical speed
	private void UpdateVertical (float yInput)
	{
		if (currentYSpeed * yInput < 0f)
		{
			UpdateVertical ();
		}

		float inc = speedInc * Time.deltaTime;

		if (yInput < 0f)
		{
			if (currentYSpeed - inc > -maxSpeed)
			{
				currentYSpeed -= inc;
			}
			else
			{
				currentYSpeed = -maxSpeed;
			}
		}
		else if (yInput > 0f)
		{
			if (currentYSpeed + inc < maxSpeed)
			{
				currentYSpeed += inc;
			}
			else
			{
				currentYSpeed = maxSpeed;
			}
		}
	}

	public void Reset ()
	{
		currentXSpeed = 0f;
		currentYSpeed = 0f;
		AirSpray sprayScript = GetComponent<AirSpray> ();
		if (sprayScript != null)
		{
			sprayScript.Reset ();
		}
	}

	public void Pause ()
	{
		ParticleSystem sys = GetComponentInChildren<ParticleSystem> ();
		if (sys != null)
		{
			sys.Pause ();
		}
	}

	public void Unpause ()
	{
		ParticleSystem sys = GetComponentInChildren<ParticleSystem> ();
		if (sys != null)
		{
			sys.Play ();
		}
	}

	public void Stun ()
	{
		if (!stunned)
		{
			stunned = true;
			//GetComponent<Pausable> ().PauseObject ();
			Reset ();
			StartCoroutine (delayer.Start (stunDuration));
		}
	}

	public void CancelStun ()
	{
		if (delayer != null) 
		{		
			delayer.Pause ();
		}
	}

	public Vector3 Direction
	{
		get{return new Vector3 (currentXSpeed, currentYSpeed, 0f).normalized;}
	}

	public bool Stunned
	{
		get{return stunned;}
	}

	protected override void OnPause (object source, System.EventArgs e)
	{
		enabled = false;
	}

	protected override void OnUnpause (object source, System.EventArgs e)
	{
		enabled = true;
	}

	protected override void OnRespawn (object source, System.EventArgs e)
	{
		Reset ();
	}

	protected void OnDelayDone (object source, System.EventArgs e)
	{
		if ((Delayer) source == delayer)
		{
			stunned = false;
		}
	}
}
