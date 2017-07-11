using UnityEngine;
using System.Collections;

public class AirSpray : EventListener
{
	public enum ControllerType
	{
		Controller,
		Keyboard
	}
	public enum ShotType
	{
		Smooth,
		Direction_8
	}

	public ControllerType controllerType;
	public ShotType shotType;

	public PushPlane airSprayZone;
	public float distanceFromSprite = 0.2f;

	public float planePushStrength = 10f;
	public float projectilePushStrength = 30f;
	public float cerfVolantPushStrength = 10f;

	public float radialDeadZone = 0.15f;
	public float absoluteDeadZone = 0.1f;
	public ParticleSystem pSystem;

	//private float xDebug;
	//private float yDebug;
	//private float angleDegDebug;
	//private Vector2 newDirDebug;

	protected override void Start ()
	{
		base.Start ();
		if (airSprayZone == null)
		{
			Debug.LogWarning ("AirSpray : airSprayZone isn't assigned.");
			enabled = false;
		}
		else
		{
			airSprayZone.Disable ();
		}
	}

	void Update ()
	{
		if (_GameManager.Instance != null && !_GameManager.Instance.Respawning)
		{
			airSprayZone.SetAttributes (planePushStrength, projectilePushStrength, cerfVolantPushStrength);

			if (controllerType == ControllerType.Controller)
			{
				float x = Input.GetAxisRaw ("HorizontalRight");
				float y = Input.GetAxisRaw ("VerticalRight");

				Vector2 direction = new Vector2 (x, y);
				if (direction.magnitude > radialDeadZone)
				{
					if (x > absoluteDeadZone || x < -absoluteDeadZone)
					{
						//xDebug = x;
					} 
					else
					{
						//xDebug = 0f;
						direction.x = 0f;
					}

					if (y > absoluteDeadZone || y < -absoluteDeadZone)
					{
						//yDebug = y;
					} 
					else
					{
						//xDebug = 0f;
						direction.y = 0f;
					}

					if (direction.x != 0f || direction.y != 0f)
					{
						Debug.Log (direction);
						if (shotType == ShotType.Smooth)
						{
							airSprayZone.gameObject.SetActive (true);
							airSprayZone.transform.position = transform.position + VectorFunc.Make3D (direction.normalized) * distanceFromSprite;
							airSprayZone.transform.up = direction;
						}
						else if (shotType == ShotType.Direction_8)
						{
							Vector2 newDir = Vector2.zero;
							Vector2 right = Vector2.right;
							float angle_Deg = Vector2.Angle (right, direction);
							//angleDegDebug = angle_Deg;

							if (direction.y >= 0f)
							{
								if (angle_Deg < 22.5f)
								{
									newDir = new Vector2 (1f, 0f).normalized;
								}
								else if (angle_Deg < 67.5f)
								{
									newDir = new Vector2 (1f, 1f).normalized;
								}
								else if (angle_Deg < 112.5f)
								{
									newDir = new Vector2 (0f, 1f).normalized;
								}
								else if (angle_Deg < 157.5f)
								{
									newDir = new Vector2 (-1f, 1f).normalized;
								}
								else
								{
									newDir = new Vector2 (-1f, 0f);
								}
							}
							else
							{
								if (angle_Deg < 22.5f)
								{
									newDir = new Vector2 (1f, 0f).normalized;
								}
								else if (angle_Deg < 67.5f)
								{
									newDir = new Vector2 (1f, -1f).normalized;
								}
								else if (angle_Deg < 112.5f)
								{
									newDir = new Vector2 (0f, -1f).normalized;
								}
								else if (angle_Deg < 157.5f)
								{
									newDir = new Vector2 (-1f, -1f).normalized;
								}
								else
								{
									newDir = new Vector2 (-1f, 0f);
								} 
							}


							//newDirDebug = newDir;
							airSprayZone.gameObject.SetActive (true);
							airSprayZone.transform.position = transform.position + VectorFunc.Make3D (newDir.normalized) * distanceFromSprite;
							airSprayZone.transform.up = newDir;
						}
					}
					else
					{
						airSprayZone.Disable ();
					}
				} 
				else
				{
					airSprayZone.Disable ();
				}
			}
			else if (controllerType == ControllerType.Keyboard)
			{
				if (Input.GetMouseButton (0))
				{
					Vector3 mousePos = Input.mousePosition;
					Vector3 toWorld = Camera.main.ScreenToWorldPoint (mousePos);
					toWorld.z = 0f;

					Vector2 direction = new Vector2 (toWorld.x - transform.position.x, toWorld.y - transform.position.y);

					airSprayZone.gameObject.SetActive (true);
					airSprayZone.transform.position = transform.position + VectorFunc.Make3D (direction.normalized) * distanceFromSprite;
					airSprayZone.transform.up = direction;
				}
				else
				{
					airSprayZone.Disable ();
				}
			}
		}
	}

	public void Reset ()
	{
		if (airSprayZone != null)
		{
			airSprayZone.Disable ();
		}
	}

	protected override void OnPause (object source, System.EventArgs e)
	{
		enabled = false;
		pSystem.Pause ();
	}

	protected override void OnUnpause (object source, System.EventArgs e)
	{
		enabled = true;
		pSystem.Play ();
	}

	protected override void OnRespawn (object source, System.EventArgs e)
	{
		Debug.LogWarning ("AirSpray[" + name + "] : OnRespawn is not Implemented.");
	}
}
