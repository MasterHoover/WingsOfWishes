using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ImportantPointers : MonoBehaviour 
{
	private static ImportantPointers instance;
	private GameObject plane;
	private GameObject cloud;
	private Canon[] canons;
	private List<Projectile> projectiles = new List<Projectile> ();

	void Awake ()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			DestroyImmediate (gameObject);
		}
	}

	void Start ()
	{
		plane = GameObject.FindGameObjectWithTag ("Player");
		cloud = GameObject.FindGameObjectWithTag ("Cloud");
		canons = GameObject.FindObjectsOfType<Canon> ();
		CheckpointManager.Instance.Initialize ();
	}

	public void CleanLists ()
	{
		for (int i = 0; i < projectiles.Count; i++)
		{
			if (projectiles[i] == null)
			{
				projectiles.RemoveAt (i--);
			}
		}
	}

	public static ImportantPointers Instance
	{
		get{return instance;}
	}

	public GameObject Plane
	{
		get{return plane;}
	}

	public GameObject Cloud
	{
		get{ return cloud; }
	}

	public Canon[] Canons
	{
		get{ return canons; }
	}

	public List<Projectile> Projectiles
	{
		get{return projectiles;}
	}
}
