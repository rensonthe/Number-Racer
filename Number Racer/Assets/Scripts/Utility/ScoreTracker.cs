using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
		text.text = string.Format("{0:0}/{1:0}", LevelManager.instance.correctAnswers, LevelManager.instance.totalScore);
	}
}
