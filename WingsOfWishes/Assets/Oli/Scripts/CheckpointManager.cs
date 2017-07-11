using UnityEngine;
using System.Collections;

public class CheckpointManager : MonoBehaviour 
{
	private static CheckpointManager instance;
	public float pauseDuration = 1.5f;
	public Checkpoint[] checkpoints;
	//public float deadZone = 0.2f;
	private int currentCheckpointIndex = -1;
	private Vector3 initialPlanePos;
	private Vector3 initialCloudPos;

	//private bool respawningTest;

	private bool pressed;
	public Color gizmosColor;

	void Awake ()
	{
		instance = this;
	}

	public void Initialize ()
	{
		initialCloudPos = ImportantPointers.Instance.Cloud.transform.position;
		initialPlanePos = ImportantPointers.Instance.Plane.transform.position;
		Respawn ();
	}

	void Update ()
	{
		//respawningTest = Respawning;

		if (Input.GetButtonDown ("LeftBumper"))
		{
			PreviousCheckpoint ();
		} 
		else if (Input.GetButtonDown ("RightBumper"))
		{
			NextCheckpoint ();
		}
		//float x = Input.GetAxisRaw ("PadHorizontal");

		/*
		if (!pressed)
		{
			if (x > deadZone)
			{
				NextCheckpoint ();
				pressed = true;
			} else if (x < -deadZone)
			{
				PreviousCheckpoint ();
				pressed = true;
			}
		} 
		else
		{
			if (x > -deadZone && x < deadZone)
			{
				pressed = false;
			}
		}
		*/
	}

	public int GetIndex (Checkpoint checkpoint)
	{
		if (checkpoint != null)
		{
			for (int i = 0; i < checkpoints.Length; i++)
			{
				if (checkpoints [i] != null && checkpoints[i] == checkpoint)
				{
					return i;
				}
			}
		}
		return -1;
	}

	public void ActivateCheckpoint (Checkpoint checkpoint)
	{
		ActivateCheckpoint (GetIndex (checkpoint));
	}

	public void ActivateCheckpoint (int index)
	{
		Debug.Log ("Activating checkpoint : " + index);
		if (index > -1 && index < checkpoints.Length)
		{
			if (checkpoints [index] != null)
			{
				currentCheckpointIndex = index;

				for (int i = 0; i < checkpoints.Length; i++)
				{
					if (checkpoints [i] != null)
					{
						if (i <= currentCheckpointIndex)
						{
							checkpoints [i].gameObject.SetActive (false);
						} else
						{
							checkpoints [i].gameObject.SetActive (true);	
						}
					}
				}
			}
		}
		else if (index == -1)
		{
			currentCheckpointIndex = index;
		}
	}

	public void Respawn ()
	{
		Respawn (currentCheckpointIndex);
	}

	private void Respawn (int index)
	{
		if (index != -1)
		{
			if (checkpoints [index] != null)
			{
				ImportantPointers.Instance.Cloud.transform.position = checkpoints [index].CloudPos;
				ImportantPointers.Instance.Plane.transform.position = checkpoints [index].PlanePos;

				bool[] canonStates = checkpoints [index].CanonStates;
				Canon[] canons = ImportantPointers.Instance.Canons;
				for (int i = 0; i < canonStates.Length; i++)
				{
					if (canonStates [i])
					{
						canons [i].Rebuild ();
					} else
					{
						canons [i].Destroy ();
					}
				}
			}
		}
		else
		{
			for (int i = 0; i < checkpoints.Length; i++)
			{
				if (checkpoints [i] != null)
				{
					checkpoints [i].gameObject.SetActive (true);
				}
			}

			ImportantPointers.Instance.Cloud.transform.position = initialCloudPos;
			ImportantPointers.Instance.Plane.transform.position = initialPlanePos;
		}

		Projectile[] projectiles = ImportantPointers.Instance.Projectiles.ToArray ();
		for (int i = 0; i < projectiles.Length; i++)
		{
			if (projectiles[i] != null)
			{
				Destroy (projectiles[i].gameObject);
			}
		}
		ImportantPointers.Instance.Cloud.GetComponent<CloudControllerFree> ().CancelStun ();
	}

	private void NextCheckpoint ()
	{
		Debug.Log ("NEXT CHECKPOINT");
		ActivateCheckpoint (NextCheckpointIndex ());
		Respawn ();
	}

	private void PreviousCheckpoint ()
	{
		Debug.Log ("PREVIOUS CHECKPOINT");
		ActivateCheckpoint (PreviousCheckpointIndex ());
		Respawn ();
	}

	public int NextCheckpointIndex ()
	{
		for (int i = currentCheckpointIndex + 1; i < checkpoints.Length; i++)
		{
			if (checkpoints [i] != null)
			{
				return i;
			}
		}
		return -1;
	}

	public int PreviousCheckpointIndex ()
	{
		for (int i = currentCheckpointIndex -1; i >= 0; i--)
		{
			if (checkpoints [i] != null)
			{
				return i;
			}
		}
		return -1;
	}

	public static CheckpointManager Instance
	{
		get{ return instance; }
	}

	void OnDrawGizmos ()
	{
		gizmosColor.a = 1f;
		Gizmos.color = gizmosColor;

		for (int i = 0; i < checkpoints.Length; i++)
		{
			if (checkpoints [i] != null)
			{
				Gizmos.DrawLine (transform.position, checkpoints [i].transform.position);
			}
		}
	}
}
