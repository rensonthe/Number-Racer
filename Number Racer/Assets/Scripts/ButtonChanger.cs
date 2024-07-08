using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonChanger : MonoBehaviour
{
	public GameObject currentPanel;
	public GameObject nextPanel;
	public GameObject proceedButton;
	public GameObject rightArrow;
	public GameObject leftArrow;
	public void StartScene(string levelName)
	{
		SceneManager.LoadScene(levelName);
	}
	public void NextPanel()
	{	
		currentPanel.SetActive(false);
		nextPanel.SetActive(true);
		proceedButton.SetActive(true);
		rightArrow.SetActive(true);
	}
	public void NextPanelLeft()
	{
		currentPanel.SetActive(false);
		nextPanel.SetActive(true);
		rightArrow.SetActive(false);
		leftArrow.SetActive(true);
	}
}
