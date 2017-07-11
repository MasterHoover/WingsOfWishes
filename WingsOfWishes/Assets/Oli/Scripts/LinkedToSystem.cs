using UnityEngine;
using System.Collections;

public class LinkedToSystem : MonoBehaviour 
{
	private WaypointSystem system;

	public WaypointSystem System
	{
		get{return system;}
		set{system = value;}
	}
}
