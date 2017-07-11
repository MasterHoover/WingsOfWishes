using UnityEngine;
using System.Collections;

public class Timer : EventListener 
{
	private float shownTime;

	public float boxXRatio = 0.5f;
	public float boxYRatio = 0.2f;
	public float boxWidth = 200f;
	public float boxWidthPerNb = 10f;
	public float boxHeight = 100f;

	private float time;
	private int nbOfMins;
	private int nbOfSecs;

	private bool paused;
	private float pausedInit;
	private float pausedAmount;

	void OnGUI ()
	{
		if (!paused)
		{
			time = Time.time - pausedAmount;
		}

		int nbOfNb = 4;

		float xPos = Screen.width * boxXRatio - boxWidth / 2f;
		float yPos = Screen.height * boxYRatio - boxHeight / 2f;

		nbOfMins = (int) (time / 60);
		nbOfSecs = (int) (time % 60);

		string timerText = "";
		if (nbOfMins < 10) timerText += "0";
		timerText += nbOfMins;
		timerText += ":";
		if (nbOfSecs < 10) timerText  += "0";
		timerText += nbOfSecs;
		Rect r = new Rect (xPos, yPos, boxWidth + nbOfNb * boxWidthPerNb, boxHeight);
		GUI.TextArea (r, timerText);
	}

	protected override void OnPause (object source, System.EventArgs e)
	{
		pausedInit = Time.time;
		paused = true;	
	}

	protected override void OnUnpause (object source, System.EventArgs e)
	{
		pausedAmount += (Time.time - pausedInit);
		paused = false;
	}
}
