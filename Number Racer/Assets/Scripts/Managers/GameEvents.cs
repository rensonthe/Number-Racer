using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    private void Awake()
    {
        current = this;
    }

    public event Action onWaveEnd;
    public void WaveEnd()
    {
        if(onWaveEnd != null)
        {
            onWaveEnd();
        }
    }
	public event Action onWaveStart;
	public void WaveStart()
	{
		if (onWaveStart != null)
		{
			onWaveStart();
		}
	}
}
