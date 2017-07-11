using UnityEngine;
using System.Collections;

[RequireComponent (typeof (MomentumReceiver))]
public class Friction : EventListener 
{
	private MomentumReceiver script;
	public float momentumDecrement = 5f;

	void Awake ()
	{
		script = GetComponent<MomentumReceiver> ();
		ConstantMomentum constant = GetComponent<ConstantMomentum> ();
		if (constant != null)
		{
			Debug.LogWarning ("ConstantMomentum[" + name + "]/Awake () : Friction and ConstantMomentum script cannot coexists. Destroying self.");
			Destroy (this);
		}
	}

	void LateUpdate ()
	{
		DecreaseMomentum ();
	}

	public void DecreaseMomentum ()
	{
		script.DecreaseMagnitude (momentumDecrement * Time.deltaTime);
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
