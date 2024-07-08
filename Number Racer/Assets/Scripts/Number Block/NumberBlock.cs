using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NumberBlock : MonoBehaviour
{
	SpriteRenderer image;
	public ParticleSystem explosionEffect;
	public ParticleSystem dissolveEffect;
	public TextMeshPro text;
	public AnswerBlockDisplay answerBlock;
	AnswerBar answerBar;

	bool hasChangedColor;
	void Start()
    {
		hasChangedColor = false;
		image = GetComponent<SpriteRenderer>();
		answerBar = FindObjectOfType<AnswerBar>();
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
	private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
			AudioManager.instance.PlaySound2D("pick");
			ParticleSystem instance = Instantiate(dissolveEffect, transform.position, Quaternion.identity);
			Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
			Destroy(gameObject);
			//spawn the number block received to the bar
			GameObject answer = Instantiate(answerBlock.gameObject, answerBar.transform.position, Quaternion.identity, answerBar.transform);
			answer.GetComponent<AnswerBlockDisplay>().text.text = text.text;
			//add value to total to the bar for calculating
			answerBar.totalValue += int.Parse(answer.GetComponent<AnswerBlockDisplay>().text.text);
		}
		else if (collision.tag == "Ground")
        {
			AudioManager.instance.PlaySound2D("explosion");
			ParticleSystem instance = Instantiate(explosionEffect, transform.position, Quaternion.identity);
			Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
			Destroy(gameObject);
		}
    }
}
