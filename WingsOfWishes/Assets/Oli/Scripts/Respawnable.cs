using UnityEngine;
using System.Collections;

public class Respawnable : MonoBehaviour 
{
	protected Vector3 savedPosition;

	void Awake ()
	{
		savedPosition = transform.position;
	}

	public virtual void Reset ()
	{
		transform.position = savedPosition;
	}

	public Vector3 SavedPosition
	{
		get{ return savedPosition; }
		set{savedPosition = value;}
	}

}
