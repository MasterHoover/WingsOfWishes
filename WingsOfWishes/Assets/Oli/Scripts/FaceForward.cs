using UnityEngine;
using System.Collections;

public class FaceForward : EventListener 
{
	public bool instant;
	public MomentumReceiver moveScript;
	public enum Direction
	{
		x,
		y,
		z
	}

	public Direction direction;
	public bool invert;
	public float maxRotationSpeed = 160f;
	public float speedModifier = 25f;

	private Quaternion initialRot;

	void Awake ()
	{
		initialRot = transform.rotation;
	}

	void LateUpdate ()
	{
		if (instant)
		{
			if (direction == Direction.x)
			{
				transform.right = moveScript.MoveDirection * (invert ? -1f : 1f);
			}
			else if (direction == Direction.y)
			{
				transform.up = moveScript.MoveDirection * (invert ? -1f : 1f);
			}
			else
			{
				transform.forward = moveScript.MoveDirection * (invert ? -1f : 1f);
			}
		}
		else
		{
			Quaternion currentRotation = transform.rotation;
			Quaternion targetRotation = Quaternion.identity;
			Quaternion newRotation = Quaternion.identity;
			float speed = moveScript.MoveMagnitude * speedModifier < maxRotationSpeed ? moveScript.MoveMagnitude * speedModifier : maxRotationSpeed;

			if (direction == Direction.x)
			{
				transform.right = moveScript.MoveVector * (invert ? -1f : 1f);
			}
			else if (direction == Direction.y)
			{
				transform.up = moveScript.MoveVector * (invert ? -1f : 1f);
			}
			else
			{
				transform.forward = moveScript.MoveVector * (invert ? -1f : 1f);
			}
			targetRotation = transform.rotation;
			transform.rotation = currentRotation;

			newRotation = Quaternion.RotateTowards (currentRotation, targetRotation, speed * Time.deltaTime);
			transform.rotation = newRotation;
		}
	}

	protected override void OnPause (object source, System.EventArgs e)
	{
		enabled = false;
	}

	protected override void OnUnpause (object source, System.EventArgs e)
	{
		enabled = true;
	}

	protected override void OnRespawn (object source, System.EventArgs e)
	{
		transform.rotation = initialRot;
	}
}
