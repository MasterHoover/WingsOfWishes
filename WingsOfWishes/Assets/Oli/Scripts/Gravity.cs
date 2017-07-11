using UnityEngine;
using System.Collections;

[RequireComponent (typeof (MomentumReceiver))]
public class Gravity : EventListener
{
	public float maxGrav = 20f;
	public float gravStrength = 1f;
	//public float gravDec = 5f;

	//private float currentGravity;

	private MomentumReceiver momentumScript;
	private float initialGravInc;

	void Awake ()
	{
		momentumScript = GetComponent<MomentumReceiver> ();
		initialGravInc = gravStrength;
	}

	void LateUpdate ()
	{
		if (!_GameManager.Instance.Respawning && momentumScript.VerticalMomentum > -maxGrav)
		{
			momentumScript.AddExternalForce (Vector2.down * gravStrength * Time.deltaTime);
		}
		/*
		else
		{
			float dec = gravDecX2 * Time.deltaTime;
			gravInc -= dec;
		}

	
		float inc = gravInc * Time.deltaTime;

		if (inc > 0f)
		{
			if (currentGravity + inc < maxGrav)
			{
				currentGravity += inc;
			}
		}
		else
		{
			if (currentGravity + inc > 0f)
			{
				currentGravity += inc;
			}
			else
			{
				currentGravity = 0f;
				gravInc = initialGravInc;
			}
		}

		*/
 	}

	public void Reset ()
	{
		//currentGravity = 0f;
		gravStrength = initialGravInc;
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
		Debug.LogWarning ("Gravity[" + name + "] : OnRespawn is not Implemented.");
	}
}
