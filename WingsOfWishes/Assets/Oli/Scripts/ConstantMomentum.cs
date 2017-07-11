using UnityEngine;
using System.Collections;

[RequireComponent (typeof (MomentumReceiver))]
public class ConstantMomentum : MonoBehaviour 
{
	public float constant = 3f;
	private MomentumReceiver script;

	void Awake ()
	{
		script = GetComponent<MomentumReceiver> ();
		Friction friction = GetComponent<Friction> ();
		if (friction != null)
		{
			Debug.LogWarning ("ConstantMomentum[" + name + "]/Awake () : Friction and ConstantMomentum script cannot coexists. Destroying self.");
			Destroy (this);
		}
	}

	void LateUpdate ()
	{
		script.SetMagnitude (constant);
	}
}
