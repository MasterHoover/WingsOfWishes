using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TriggerNextLevel : MonoBehaviour 
{
	public string sceneName;
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject == ImportantPointers.Instance.Plane)
		{
			SceneManager.LoadScene (sceneName);
		}
	}
}
