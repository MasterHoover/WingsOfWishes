using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour 
{
	public string levelToLoad = "LDCam1";
	private int index;

	void Update ()
	{
		if (Input.GetButtonDown ("Submit"))
		{
			SceneManager.LoadScene (levelToLoad);
		}
	}
}
