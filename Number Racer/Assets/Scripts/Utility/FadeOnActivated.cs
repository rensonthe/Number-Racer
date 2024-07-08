using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOnActivated : MonoBehaviour
{
	public CanvasGroup fadeImage;

	private bool isInTransition;
	private float transition;
	private bool isShowing;
	private float duration;
	public void Fade(bool showing, float duration)
	{
		isShowing = showing;
		isInTransition = true;
		this.duration = duration;
		transition = (isShowing) ? 0 : 1;
	}
	private void Start()
	{
		StartCoroutine(FadingIn());
	}

	private void Update()
	{
		if (!isInTransition)
			return;

		transition += isShowing ? Time.deltaTime * (1 / duration) : -Time.deltaTime * (1 / duration);
		fadeImage.alpha = transition;

		if (transition > 1 || transition < 0)
			isInTransition = false;
	}
	public IEnumerator FadingIn()
	{
		fadeImage.gameObject.SetActive(true);
		Fade(true, 1);
		yield return null;
	}
}
