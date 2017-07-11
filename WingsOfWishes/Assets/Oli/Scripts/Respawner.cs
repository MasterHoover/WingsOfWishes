using UnityEngine;
using System.Collections;

public class Respawner : MonoBehaviour 
{
	private static Respawner instance;

	private RespawnableCloud cloud;
	private RespawnablePlane plane;

	public float respawnDelay = 2f;
	private bool respawning;

	private Vector3 cloudSavedPos;
	private Vector3 planeSavedPos;

	private Canon[] canons;
	private bool[] canonStates;

	void Awake ()
	{
		instance = this;
	}

	void Start ()
	{
		cloud = GameObject.FindObjectOfType<RespawnableCloud> ();
		plane = GameObject.FindObjectOfType<RespawnablePlane> ();
		canons = GameObject.FindObjectsOfType<Canon> ();
		canonStates = new bool[canons.Length];
		cloudSavedPos = cloud.transform.position;
		planeSavedPos = plane.transform.position;
		respawning = true;
		Invoke ("StopRespawn", respawnDelay);
	}

	public void RespawnAll ()
	{
		cloud.Reset ();
		cloud.transform.position = cloudSavedPos;
		plane.Reset ();
		plane.transform.position = planeSavedPos;
		respawning = true;
		if (!IsInvoking ("StopRespawn"))
		{
			Invoke ("StopRespawn", respawnDelay);
		}
		for (int i = 0; i < canons.Length; i++)
		{
			if (canonStates[i])
			{
				canons [i].Rebuild ();
			}
		}
	}

	public void SaveState (Vector3 cloudPos, Vector3 planePos)
	{
		cloudSavedPos = cloudPos;
		planeSavedPos = planePos;
		canonStates = new bool[canons.Length];
		for (int i = 0; i < canons.Length; i++)
		{
			if (canons != null)
			{
				canonStates [i] = !canons[i].Destroyed;
			}
		}
	}

	private void StopRespawn ()
	{
		respawning = false;
	}

	public static Respawner Instance
	{
		get{return instance;}
	}

	public bool Respawning 
	{
		get{return respawning;}
	}
}
