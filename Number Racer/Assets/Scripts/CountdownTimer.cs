using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
	public bool hasMiliseconds;
	public float timeValue;
	public TextMeshProUGUI timerText;
	public Image fill;
	public float max;

	RectTransform rectTransform;
	int scalingFramesLeft;
	void Start()
	{
		scalingFramesLeft = 600;
		rectTransform = GetComponent<RectTransform>();
	}
    void Update()
    {
		if (timeValue > 0)
		{
			timeValue -= Time.deltaTime;
		}
		else
		{
			timeValue = 0;
			Destroy(this.gameObject);
		}

		if (hasMiliseconds)
		{
			DisplayTime(timeValue);
		}
		else
		{
			DisplayTimeRounded(timeValue);
		}
	}
	void DisplayTimeRounded(float timeToDisplay)
	{
		if (timeToDisplay < 0)
		{
			timeToDisplay = 0;
		}
		else if(timeToDisplay > 0)
		{
			timeToDisplay += 1;
		}

		if (scalingFramesLeft > 0)
		{
			rectTransform.localScale = Vector3.Lerp(transform.localScale, transform.localScale * 1.4f, Time.deltaTime * 0.25f);
			scalingFramesLeft--;
		}
		fill.fillAmount = Mathf.Lerp(fill.fillAmount, 1.2f, Time.deltaTime * 0.3f);

		float seconds = Mathf.FloorToInt(timeToDisplay % 60);
		timerText.text = seconds.ToString();
	}
	void DisplayTime(float timeToDisplay)
	{
		if (timeToDisplay < 0)
		{
			timeToDisplay = 0;
			Destroy(this.gameObject);
		}

		float seconds = Mathf.FloorToInt(timeToDisplay % 60);
		float miliseconds = timeToDisplay % 1 * 9;

		timerText.text = string.Format("{0:0}:{1:0}", seconds, miliseconds);

		fill.fillAmount = timeValue / max;
	}
}
