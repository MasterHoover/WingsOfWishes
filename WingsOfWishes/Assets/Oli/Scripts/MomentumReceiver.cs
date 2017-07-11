using UnityEngine;
using System.Collections;

public class MomentumReceiver : EventListener 
{
	public bool getHitByWind = true;
	private Vector2 moveVector;

	public float maxMomentum = 5f;


	private bool gotPushedUpward;

	//private float debugMag;

	void LateUpdate ()
	{
		//debugMag = moveVector.magnitude;
		Move ();
	}

	public void AddExternalForce (Vector3 force)
	{
		AddExternalForce (VectorFunc.Make2D (force));
	}

	public void AddExternalForce (Vector2 dir, float strength)
	{
		moveVector += (dir * strength);
		if (moveVector.magnitude > maxMomentum)
		{
			moveVector = moveVector.normalized * maxMomentum;
		}
	}

	public void AddExternalForce (Vector2 force)
	{
		AddExternalForce (force.normalized, force.magnitude);
	}

	public void DecreaseMagnitude (float amount)
	{
		if (moveVector.magnitude - amount < 0f)
		{
			moveVector = Vector2.zero;
		}
		else
		{
			moveVector = moveVector.normalized * (moveVector.magnitude - amount);
		}
	}

	public void SetMagnitude (float newMag)
	{
		moveVector = moveVector.normalized * newMag;
	}

	private void Move ()
	{
		transform.Translate (moveVector * Time.deltaTime, Space.World);
	}

	public void Reset ()
	{
		moveVector = Vector2.zero;
		Gravity gravityScript = GetComponent<Gravity> ();
		if (gravityScript != null)
		{
			gravityScript.Reset ();
		}
	}

	public bool GotPushedUpward
	{
		get{ return gotPushedUpward; }
		set{ gotPushedUpward = value;}
	}

	public float VerticalMomentum
	{
		get{return moveVector.y;}
	}

	public Vector3 MoveVector
	{
		get{ return VectorFunc.Make3D (moveVector); }
	}

	public Vector3 MoveDirection
	{
		get {return VectorFunc.Make3D (moveVector.normalized);}
	}

	public float MoveMagnitude
	{
		get {return moveVector.magnitude;}
	}

	public bool HasMomentum
	{
		get{return moveVector != Vector2.zero;}
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
}
