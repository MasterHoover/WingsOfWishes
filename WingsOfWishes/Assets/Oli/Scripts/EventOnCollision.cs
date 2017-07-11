using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public abstract class EventOnCollision : MonoBehaviour 
{
	public string[] affectedTags = new string[1];

	void Awake ()
	{
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();
		rb.isKinematic = true;
	}

	void Start ()
	{
		ArrayFunc.Trim (ref affectedTags);
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		bool isAffectedTag = false;
		for (int i = 0; !isAffectedTag && i < affectedTags.Length; i++)
		{
			if (col.tag ==  affectedTags[i])
			{
				isAffectedTag = true;
			}
		}
		if (isAffectedTag)
		{
			FireEvent (col);
		}
	}

	void OnTriggerExit2D (Collider2D col)
	{
		bool isAffectedTag = false;
		for (int i = 0; !isAffectedTag && i < affectedTags.Length; i++)
		{
			if (col.tag == affectedTags[i])
			{
				isAffectedTag = true;
			}
		}
		if (isAffectedTag)
		{
			DisableEvent (col);
		}
	}

	protected abstract void FireEvent (Collider2D col);

	protected virtual void DisableEvent (Collider2D col) {}
}
