using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArrayFunc 
{
	public static void Trim (ref string[] array)
	{
		List<string> list = new List<string> ();
		for (int i = 0; i < array.Length; i++)
		{
			list.Add (array[i]);
		}

		for (int i = 0; i < list.Count; i++)
		{
			if (list[i] == "")
			{
				list.RemoveAt (i--);
			}
		}
		array = list.ToArray ();
	}

	public static void Trim (ref Waypoint[] array)
	{
		List<Waypoint> list = new List<Waypoint> ();
		for (int i = 0; i < array.Length; i++)
		{
			list.Add (array[i]);
		}

		for (int i = 0; i < list.Count; i++)
		{
			if (list[i] == null)
			{
				list.RemoveAt (i--);
			}
		}
		array = list.ToArray ();
	}

	public static void Trim (ref Resistance.DamagePerTag[] array)
	{
		List<Resistance.DamagePerTag> list = new List<Resistance.DamagePerTag> ();
		for (int i = 0; i < array.Length; i++)
		{
			list.Add (array[i]);
		}

		for (int i = 0; i < list.Count; i++)
		{
			if (list[i] == null || list[i].tag == "")
			{
				list.RemoveAt (i--);
			}
		}
		array = list.ToArray ();
	}
}
