using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance { get; set; }

    public static bool GameIsPaused;

    public GameObject pauseMenuUI;
    public GameObject menuTab;
    public Animator layoutAnimator;
    public Animator buttonAnimator;
    public float menuDelay;

	private void Awake()
	{
		Instance = this;
	}
	// Update is called once per frame
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
			if (GameIsPaused)
			{
                ButtonResume();
			}
			else
			{
                Pause();
			}
		}
    }
    public void Resume()
    {
		pauseMenuUI.SetActive(false);
		menuTab.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
		menuTab.SetActive(false);
		pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void LoadLevel(string levelName)
    {
		Time.timeScale = 1f;
        SceneManager.LoadScene(levelName);
	}
    public void ButtonResume()
    {
		layoutAnimator.SetTrigger("closeMenu");
        buttonAnimator.SetTrigger("dropMenu");
	}
}
