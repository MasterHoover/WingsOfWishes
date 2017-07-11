using UnityEngine;
using System.Collections;

public class CanonRespawnAfterDelay : MonoBehaviour 
{
	public GameObject toRespawn;
	private Vector3 initialPos;
	private Quaternion initialRot;
	public float respawnDelay;

	void Awake ()
	{
		if (respawnDelay <= 0f)
		{
			enabled = false;
		}
	}

	void Update ()
	{
		if (!toRespawn.activeSelf && !IsInvoking ("Respawn"))
		{
			Invoke ("Respawn", respawnDelay);
		}
	}

	void Respawn ()
	{
		toRespawn.SetActive (true);
	}
}
