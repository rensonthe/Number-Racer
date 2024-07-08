using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneratedBlockDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Image image;

    bool hasChangedColor;
    // Start is called before the first frame update
    void Start()
    {
        hasChangedColor = false;
    }

    private void Update()
    {
        if (!hasChangedColor)
        {
            hasChangedColor = true;

			if (int.Parse(text.text) < 0)
			{
				image.color = new Color(0, 0.15f, 0.2f);
			}
		}
	}
}
