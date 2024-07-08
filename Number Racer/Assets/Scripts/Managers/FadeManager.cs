using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
	public static FadeManager Instance { get; set; }

	public Image fadeImage;
	public GameObject countdownTimer;

	private bool isInTransition;
	private float transition;
	private bool isShowing;
	private float duration;

	private void Awake()
	{
		Instance = this;
	}

	public void Fade(bool showing, float duration)
	{
		isShowing = showing;
		isInTransition = true;
		this.duration = duration;
		transition = (isShowing) ? 0 : 1;
	}

	private void Update()
	{
		if (!isInTransition)
			return;

		transition += isShowing ? Time.deltaTime * (1 / duration) : -Time.deltaTime * (1 / duration);
		fadeImage.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, transition);

		if (transition > 1 || transition < 0)
			isInTransition = false;
	}
	public IEnumerator FadingIn()
	{
		fadeImage.gameObject.SetActive(true);
		Fade(false, 0.5f);
		yield return new WaitForSeconds(0.5f);
		countdownTimer.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		Destroy(fadeImage.gameObject);
	}
}
