using UnityEngine;
using System.Collections;

public class DontMoveUntilBlowed : EventListener 
{
	private Gravity gravityScript;
	private MomentumReceiver momentumReceiver;

	void Awake ()
	{
		momentumReceiver = GetComponent<MomentumReceiver> ();
		gravityScript = GetComponent<Gravity> ();
		if (momentumReceiver == null || gravityScript == null)
		{
			Destroy (this);
		}
		else
		{
			gravityScript.enabled = false;
		}
	}

	protected override void OnRespawn (object source, System.EventArgs e)
	{
		enabled = true;
		gravityScript.Reset ();
		gravityScript.enabled = false;
	}

	void Update ()
	{
		if (momentumReceiver.HasMomentum)
		{
			gravityScript.enabled = true;
			enabled = false;
		}
	}
}
