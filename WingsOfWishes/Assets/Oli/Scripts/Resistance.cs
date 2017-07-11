using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class Resistance : MonoBehaviour 
{
	public float hp = 6;
	public DamagePerTag[] affectedTags;

	void Awake ()
	{
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();
		rb.isKinematic = true;
		ArrayFunc.Trim (ref affectedTags);
	}

	void Update ()
	{
		if (hp <= 0)
		{
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		bool foundIt = false;
		for (int i = 0; !foundIt && i < affectedTags.Length; i++)
		{
			if (col.tag == affectedTags[i].tag && !affectedTags[i].constantDps)
			{
				foundIt = true;
				hp -= affectedTags[i].dmg;
			}
		}
	}

	void OnTriggerStay2D (Collider2D col)
	{
		bool foundIt = false;
		for (int i = 0; !foundIt && i < affectedTags.Length; i++)
		{
			if (col.tag == affectedTags[i].tag && affectedTags[i].constantDps)
			{
				foundIt = true;
				hp -= affectedTags[i].dmg;
			}
		}
	}

	[System.Serializable]
	public class DamagePerTag
	{
		public bool constantDps;
		public string tag;
		public float dmg;
	}
}
