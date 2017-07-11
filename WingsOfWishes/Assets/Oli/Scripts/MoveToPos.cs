using UnityEngine;
using System.Collections;

public class MoveToPos : EventListener 
{
	private Vector3 targetPos;
	private float speed;
	private MomentumReceiver momentumScript;
	private LinkedToSystem systemRef;

	protected override void Start ()
	{
		base.Start ();
		momentumScript = GetComponent<MomentumReceiver> ();
		systemRef = GetComponent<LinkedToSystem> ();
	}

	void Update ()
	{
		if (momentumScript != null)
		{
			if (!momentumScript.HasMomentum)
			{
				float moveDist = speed * Time.deltaTime;
				if (Vector3.Distance (transform.position, targetPos) < moveDist)
				{
					transform.position = targetPos;
					systemRef.System.ReturningToWaypoint = false;
					systemRef.System.Resume ();
					Destroy (this);
				}
				else
				{
					Vector3 dir = VectorFunc.GetDirection (transform.position, targetPos);
					transform.Translate (dir * moveDist, Space.World);
				}
			}
			else
			{
				Debug.Log ("SystemRef : " + (systemRef != null).ToString ());
				if (systemRef != null)
				{
					Debug.Log ("SystemRef.System : " + (systemRef.System != null).ToString ());
				}
				systemRef.System.ReturningToWaypoint = false;
				Destroy (this);
			}
		}
	}

	public void SetAttribute (Vector3 targetPos, float speed)
	{
		this.targetPos = targetPos;
		this.speed = speed;
	}

	protected override void OnPause (object source, System.EventArgs e)
	{
		enabled = false;
	}

	protected override void OnUnpause (object source, System.EventArgs e)
	{
		enabled = true;
	}
}
