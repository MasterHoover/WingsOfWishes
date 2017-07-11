using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PushPlane : EventListener 
{
	public const string planeTag = "Player";
	public const string projectileTag = "Projectile";
	public const string cerfVolantTag = "CerfVolant";
	private float planePushStrength;
	private float projPushStrength;
	private float cerfVolantPushStrength;
	private List<MomentumReceiver> allHitObjects = new List<MomentumReceiver> ();
	private bool active = true;

	void OnTriggerStay2D (Collider2D col)
	{
		if (active)
		{
			if (col.tag == planeTag)
			{
				MomentumReceiver momentumScript = col.GetComponent<MomentumReceiver> ();
				if (momentumScript == null)
				{
					Debug.LogWarning ("PushPlane : colliding with plane, but no momentum script on plane.");
				}
				else
				{
					if (momentumScript.getHitByWind)
					{
						if (allHitObjects.IndexOf (momentumScript) == -1)
						{
							allHitObjects.Add (momentumScript);
						}

						momentumScript.AddExternalForce (transform.up * planePushStrength * Time.deltaTime);
						if (transform.up.y > 0f)
						{
							momentumScript.GotPushedUpward = true;
						}
						else
						{
							momentumScript.GotPushedUpward = false;
						}
					}
				}
			}

			if (col.tag == projectileTag)
			{
				MomentumReceiver momentumScript = col.GetComponent<MomentumReceiver> ();
				if (momentumScript == null)
				{
					Debug.LogWarning ("PushPlane : colliding with plane, but no momentum script on plane.");
				}
				else
				{
					momentumScript.AddExternalForce (transform.up * projPushStrength * Time.deltaTime);
				}
			}

			if (col.tag == cerfVolantTag)
			{
				MomentumReceiver momentumScript = col.GetComponent<MomentumReceiver> ();
				if (momentumScript != null)
				{
					momentumScript.AddExternalForce (transform.up * cerfVolantPushStrength * Time.deltaTime);
				}
				else
				{
					Debug.LogWarning ("PushPlane : colliding with cerfVolant, but no momentum script on cerfVolant.");
				}
			}
		}
	}

	void OnTriggerExit2D (Collider2D col)
	{
		if (col.tag == planeTag)
		{
			MomentumReceiver momentumScript = col.GetComponent<MomentumReceiver> ();
			if (momentumScript != null)
			{
				momentumScript.GotPushedUpward = false;
			}
		}
	}

	public void SetAttributes (float planePushStrength, float projPushStrength, float cerfVolantPushStrength)
	{
		this.planePushStrength = planePushStrength;
		this.projPushStrength = projPushStrength;
		this.cerfVolantPushStrength = cerfVolantPushStrength;
	}

	public void Disable ()
	{
		foreach (MomentumReceiver m in allHitObjects)
		{
			if (m != null)
			{
				m.GotPushedUpward = false;
			}
		}

		allHitObjects.Clear ();
		gameObject.SetActive (false);
	}

	protected override void OnPause (object source, System.EventArgs e)
	{
		active = false;
	}

	protected override void OnUnpause (object source, System.EventArgs e)
	{
		active = true;
	}
}
