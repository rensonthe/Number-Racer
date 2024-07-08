using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerBlockDisplay : MonoBehaviour
{
	public TextMeshProUGUI text;

	Image image;
	bool hasChangedColor;

	// Start is called before the first frame update
	void Start()
    {
		hasChangedColor = false;
		image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
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
