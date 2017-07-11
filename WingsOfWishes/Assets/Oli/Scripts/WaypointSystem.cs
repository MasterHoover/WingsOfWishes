using UnityEngine;
using System.Collections;

public class WaypointSystem : EventListener 
{
	public CerfVolantSpawner spawner;
	public Waypoint[] waypoints;
	private float speed;
	private float delay;
	private Vector3 direction;
	private int currentCheckpointIndex = -1;
	private bool moving;
	private bool returning = true;
	private bool thinkIsDead;
	private bool active = true;
	private bool addedScript;
	public float returnToWaypointDelay = 10f;
	public float returnToWaypointSpeed = 2f;
	private bool returningToWaypoint;
	private bool canReturn;

	void Awake ()
	{
		ArrayFunc.Trim (ref waypoints);
		if (waypoints != null && waypoints.Length > 0)
		{
			spawner.transform.position = waypoints[0].transform.position;
		}
	}

	protected override void Start ()
	{
		base.Start ();
		if (waypoints.Length > 1)
		{
			StartLoop ();
		}
		else
		{
			enabled = false;
		}
	}

	void Update ()
	{
		if (spawner.CerfVolantDead && !thinkIsDead)
		{
			thinkIsDead = true;
			if (returningToWaypoint)
			{
				returningToWaypoint = false;
			}
			if (!active)
			{
				active = true;
			}
			moving = false;
		}

		if (!spawner.CerfVolantDead && thinkIsDead)
		{
			thinkIsDead = false;
			StartLoop ();
		}

		if (!addedScript && !spawner.CerfVolantDead)
		{
			addedScript = true;
			LinkedToSystem script = spawner.CerfVolantInstance.gameObject.AddComponent<LinkedToSystem> ();
			script.System = this;
		}

		if (spawner.CerfVolantInstance.HasMomentum && active)
		{
			Debug.Log ("Pausing");
			active = false;
			if (IsInvoking ("ReturnToWaypoint"))
			{
				Debug.Log ("CancelInvoking");
				CancelInvoke ("ReturnToWaypoint");
			}
		}
		else if (!active && !spawner.CerfVolantInstance.HasMomentum)
		{
			if (!IsInvoking ("ReturnToWaypoint") && !returningToWaypoint)
			{
				Debug.Log ("Invoking");
				Invoke ("ReturnToWaypoint", returnToWaypointDelay);
			}
		}

		if (!spawner.CerfVolantDead)
		{
			if (!thinkIsDead)
			{
				if (active)
				{
					if (moving)
					{
						Vector3 targetPos = waypoints[TargetWaypointIndex].transform.position;
						float distanceToCheckpoint = Vector3.Distance (spawner.CerfVolantInstance.transform.position, targetPos);

						float moveDistance = speed * Time.deltaTime;
						if (distanceToCheckpoint > moveDistance)
						{
							spawner.CerfVolantInstance.transform.Translate (direction * moveDistance);
						}
						else
						{
							spawner.CerfVolantInstance.transform.position = targetPos;
							GoToNext ();
						}
					}
				}
			}
			else
			{
				thinkIsDead = false;
				addedScript = false;
				StartLoop ();
			}
		}
		else
		{
			if (!thinkIsDead)
			{
				thinkIsDead = true;
			}
		}
	}

	private void GoToNext ()
	{
		moving = false;
		currentCheckpointIndex += (returning ? -1 : 1);
		if (returning && currentCheckpointIndex - 1 == -1)
		{
			returning = false;
		}
		else if (!returning && currentCheckpointIndex + 1 == waypoints.Length)
		{
			returning = true;
		}
		CollectInfo (waypoints[currentCheckpointIndex]);
		UpdateDirection ();
		Go ();
	}

	public void StartLoop ()
	{
		Reset ();
		Go ();
	}

	private void Reset ()
	{
		currentCheckpointIndex = 0;
		returning = false;
		CollectInfo  (waypoints[0]);
		UpdateDirection ();
		active = true;
	}

	private void Go ()
	{
		Invoke ("MoveToCheckpoint", delay);
	}

	private void MoveToCheckpoint ()
	{
		moving = true;
	}

	private void UpdateDirection ()
	{
		direction = VectorFunc.GetDirection 
			(waypoints[currentCheckpointIndex].transform.position, waypoints[TargetWaypointIndex].transform.position);
	}

	private void CollectInfo (Waypoint checkpoint)
	{
		speed = checkpoint.speed;
		delay = checkpoint.delay;
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.cyan;
		if (waypoints != null)
		{
			for (int i = 0; i < waypoints.Length; i++)
			{
				if (waypoints[i] != null)
				{
					Gizmos.DrawLine (transform.position, waypoints[i].transform.position);
				}
			}
		}
	}

	void OnDrawGizmos ()
	{
		Gizmos.color = Color.blue;
		if (waypoints != null)
		{
			for (int i = 0; i < waypoints.Length - 1; i++)
			{
				int nextIndex = -1;
				for (int j = i + 1; nextIndex == -1 && j < waypoints.Length; j++)
				{
					if (waypoints[j] != null)
					{
						nextIndex = j;
					}
				}
				if (nextIndex != -1)
				{
					Gizmos.DrawLine (waypoints[i].transform.position, waypoints[nextIndex].transform.position);
				}
			}
		}
	}

	private void ReturnToWaypoint ()
	{
		if (spawner.CerfVolantInstance != null)
		{
			MoveToPos script = spawner.CerfVolantInstance.gameObject.AddComponent<MoveToPos> ();
			script.SetAttribute (waypoints[TargetWaypointIndex].transform.position, returnToWaypointSpeed);
			returningToWaypoint = true;
		}
	}

	public void Resume ()
	{
		Debug.Log ("Resuming");
		active = true;
	}

	public void CancelReturn ()
	{
		if (IsInvoking ("ReturnToWaypoint"))
		{
			CancelInvoke ("ReturnToWaypoint");
		}
		enabled = false;
	}

	public void RetryReturn ()
	{
		if (IsInvoking ("ReturnToWaypoint"))
		{
			CancelInvoke ("ReturnToWaypoint");
		}
		Invoke ("ReturnToWaypoint", returnToWaypointDelay);
		enabled = true;
	}

	public bool ReturningToWaypoint
	{
		set{returningToWaypoint = value;}
	}

	public int TargetWaypointIndex
	{
		get
		{
			return currentCheckpointIndex + (returning ? -1 : 1);
		}
	}

	protected override void OnPause (object source, System.EventArgs e)
	{
		enabled = false;
		Debug.LogWarning ("WaypointSystem[" + name + "] : OnPause is not fully Implemented.");
	}

	protected override void OnUnpause (object source, System.EventArgs e)
	{
		enabled = true;
		Debug.LogWarning ("WaypointSystem[" + name + "] : OnUnpause is not fully Implemented.");
	}

	protected override void OnRespawn (object source, System.EventArgs e)
	{
		Debug.LogWarning ("WaypointSystem[" + name + "] : OnRespawn is not Implemented.");
	}
}
