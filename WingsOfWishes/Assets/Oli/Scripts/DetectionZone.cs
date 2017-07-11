using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DetectionZone : MonoBehaviour 
{
	private bool detectingPlayer;
	private List<GameObject> detectedObjects = new List<GameObject> ();

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player")
		{
			detectingPlayer = true;
		}
		detectedObjects.Add (col.gameObject);
	}

	void OnTriggerExit2D (Collider2D col)
	{
		if (col.tag == "Player")
		{
			detectingPlayer = false;
		}
		detectedObjects.Remove (col.gameObject);
	}

	public bool DetectingPlayer
	{
		get{ return detectingPlayer; }
	}

	public GameObject[] DetectedObjects
	{
		get{return detectedObjects.ToArray ();}
	}
}
