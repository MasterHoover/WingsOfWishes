using UnityEngine;
using System.Collections;

public class DestructibleWall : MonoBehaviour
{
	public Canon[] turrets;

	void Update ()
	{
		bool oneActive = false;
		for (int i = 0; !oneActive && i < turrets.Length; i++)
		{
			if (turrets [i] != null)
			{
				if (!turrets [i].Destroyed)
				{
					oneActive = true;
				}
			}
		}
		if (!oneActive)
		{
			gameObject.SetActive (false);
		}
	}
		
	void OnDrawGizmos ()
	{
		if (turrets != null)
		{
			Gizmos.color = Color.cyan;
			for (int i = 0; i < turrets.Length; i++)
			{
				if (turrets [i] != null)
				{
					Gizmos.DrawLine (transform.position, turrets[i].transform.position);
				}
			}
		}
	}
}
