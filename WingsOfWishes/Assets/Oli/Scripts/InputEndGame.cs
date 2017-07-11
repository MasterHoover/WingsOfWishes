using UnityEngine;
using System.Collections;

public class InputEndGame : MonoBehaviour 
{
	void Update () 
	{
		if (Input.GetButtonDown ("Fire1")
			|| Input.GetButtonDown ("Submit"))
		{
			Application.Quit ();
		}
	}
}
