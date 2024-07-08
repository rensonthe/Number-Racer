using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public int correctAnswers;
    public int wrongAnswers;
    public int totalScore;

    private void Awake()
    {
        correctAnswers = 0;
        wrongAnswers = 0;
        totalScore = 0;

        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Z))
    //    {
    //        AddCorrectScore(1);
    //    }
    //    if (Input.GetKeyDown(KeyCode.X))
    //    {
    //        AddWrongScore(1);
    //    }
    //}
    public void AddCorrectScore(int score)
    {
        Debug.Log("run");
        correctAnswers += score;
		totalScore++;
	}

    public void AddWrongScore(int score)
    {
		wrongAnswers += score;
		totalScore++;
	}
}
