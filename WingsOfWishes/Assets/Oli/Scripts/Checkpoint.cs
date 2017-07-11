using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour 
{
	private bool[] canonStates;
	public Transform planePos;
	public Transform cloudPos;

	//private bool saved;
	//private Vector3 planePosSaved;
	//private Vector3 cloudPosSaved;

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player")
		{
			//saved = true;
			//planePosSaved = ImportantPointers.Instance.Plane.transform.position;
			//cloudPosSaved = ImportantPointers.Instance.Cloud.transform.position;
			SaveGameInfo ();
			CheckpointManager.Instance.ActivateCheckpoint (this);
		}
	}

	private void SaveGameInfo ()
	{
		Canon[] canons = ImportantPointers.Instance.Canons;
		canonStates = new bool[canons.Length];
		for (int i = 0; i < canonStates.Length && i < canons.Length; i++)
		{
			if (canons [i] != null)
			{
				canonStates [i] = !canons [i].Destroyed;
			}
		}
	}

	public bool[] CanonStates
	{
		get
		{
			if (canonStates == null)
			{
				SaveGameInfo ();
			}
			return canonStates;
		}
	}

	public Vector3 PlanePos
	{
		get{return planePos.position;}
	}

	public Vector3 CloudPos
	{
		get{return cloudPos.position;}
	}
}
