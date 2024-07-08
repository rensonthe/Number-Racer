using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance;

	public GameObject[] gameObjects;
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AnimationExit()
    {
		for (int i = 0; i < gameObjects.Length; i++)
		{
			gameObjects[i].SetActive(true);
		}
	}
	public void AnimationEnter()
	{
		for (int i = 0; i < gameObjects.Length; i++)
		{
			gameObjects[i].SetActive(false);
		}
	}
}
