using UnityEngine;
using System.Collections;

public class VectorFunc : MonoBehaviour 
{
	public static Vector3 Make3D (Vector2 vec)
	{
		return new Vector3 (vec.x, vec.y, 0f);
	}

	public static Vector2 Make2D (Vector3 vec)
	{
		return new Vector2 (vec.x, vec.y);
	}

	public static Vector3 GetDirection (Vector3 pos, Vector3 targetPos)
	{
		return (targetPos - pos).normalized;
	}
}
