using UnityEngine;
using System.Collections;

public class PermaMusic : MonoBehaviour 
{
	private static PermaMusic instance;
	public AudioSource source;

	void Awake ()
	{
		if (instance == null)
		{
			DontDestroyOnLoad (gameObject);
			instance = this;
		} 
		else
		{
			Destroy (gameObject);
		}
	}

	void Update ()
	{
		if (Camera.main != null)
		{
			transform.position = Camera.main.transform.position;
		}
	}
}
