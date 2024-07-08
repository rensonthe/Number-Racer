using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnswerBar : MonoBehaviour
{
	public RequiredAnswer requiredAnswer;
	public GeneratedBlockDisplay[] UIBlocks;
	public GameObject skipBlock;
	public GameObject answerUI;
	public GameObject answerWordUI;
	public TextMeshProUGUI answerText;
	public string correctText;
	public string wrongText;
	public int minValue;
	public int maxValue;

	public int totalValue;
	private int generatedAnswer;
	private bool canSkip;

	List<int> numbers;
	public bool isPositive = true;
	public int valueDeduction = 4;

	// Start is called before the first frame update
	void Start()
    {
		canSkip = false;
		GameEvents.current.onWaveEnd += CalculateValue;
		if (isPositive)
		{
			PositiveValues();
		}
		else
		{
			NegativeValues();
		}

		ShuffleArray(Random.Range(1,100));
		ShuffleArray(Random.Range(100, 1000));

		//GameEvents.current.onWaveStart += RandomAnswer;
		//RandomAnswer();
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKey(KeyCode.Space))
		{
			if (canSkip)
			{
				Skip();
			}
		}
	}
	public void PositiveValues()
	{
		//guranteed
		generatedAnswer = Random.Range(minValue, maxValue);
		int b = generatedAnswer / 2;
		int c = generatedAnswer / 3;
		int d = generatedAnswer / 4;
		int remainingNumber = generatedAnswer - b - d;

		//constrainted random smaller numbers
		int drng = Random.Range(1, d);
		int crng = Random.Range(1, generatedAnswer - c * 2);
		int erng = Random.Range(1, remainingNumber * 2);

		//constrainted random bigger numbers
		int brng = Random.Range(1, b);
		int frng = Random.Range(1, generatedAnswer - remainingNumber);

		//reroll if random halved number equals to half
		if(brng == b)
		{
			brng = Random.Range(1, generatedAnswer / 2);
		}

		//reroll if lower values are equal to each other
		if(erng == remainingNumber)
		{
			erng = Random.Range(1, remainingNumber * 2);
		}

		numbers = new List<int> { c, d, drng, crng, erng, brng, frng, remainingNumber};

		GameObject tmp = Instantiate(requiredAnswer.gameObject, transform.position, Quaternion.identity, transform);
		tmp.GetComponent<RequiredAnswer>().text.text = generatedAnswer.ToString();
	}
	public void NegativeValues()
	{
		//guranteed
		generatedAnswer = Random.Range(minValue, maxValue);
		int b = generatedAnswer / 2;
		int c = generatedAnswer / 3;
		int d = generatedAnswer / 4 - valueDeduction;
		//positive number addition
		int e = (b + c + d) * -1;
		int positiveNumberRemaining = e - (generatedAnswer * -1);
		//constrained random positive
		int pdrng = Random.Range(1, generatedAnswer / 4 * -1);
		int perng = Random.Range(1, generatedAnswer / 2 * -1);
		//constrained random negatives
		int nbrng = Random.Range(-1, b);
		int ndrng = Random.Range(-1, d);

		numbers = new List<int> { b, d, c, positiveNumberRemaining, pdrng, perng ,nbrng, ndrng};

		GameObject tmp = Instantiate(requiredAnswer.gameObject, transform.position, Quaternion.identity, transform);
		tmp.GetComponent<RequiredAnswer>().text.text = generatedAnswer.ToString();
	}
	void ShuffleArray(int seed)
	{
		for (int i = 0; i < UIBlocks.Length; i++)
		{
			UIBlocks[i].text.text = numbers[i].ToString();
		}

		System.Random prng = new System.Random(seed);

		int lastIndex = numbers.Count - 1;
		for (int i = 0; i < numbers.Count; i++)
		{
			int tempValue = numbers[lastIndex];
			int randomIndex = prng.Next(0, lastIndex);
			numbers[lastIndex] = numbers[randomIndex];
			numbers[randomIndex] = tempValue;
			lastIndex--;
		}
	}
	public void CalculateValue()
	{
		canSkip = true;
		skipBlock.SetActive(true);
		int currentIndex = this.transform.childCount - 1;

		for (int i = 0; i < this.transform.childCount - 1; i++)
		{
			//Debug.Log(totalValue);
		}

		//check value
		if (totalValue == generatedAnswer)
		{
			AudioManager.instance.PlaySound2D("y");
			answerUI.gameObject.SetActive(true);
			answerWordUI.gameObject.SetActive(true);
			answerText.text = correctText;
			LevelManager.instance.AddCorrectScore(1);
			//for (int i = 0; i < this.transform.childCount; i++)
			//{
			//	Destroy(GetComponent<Transform>().GetChild(i).gameObject);
			//}
			StartCoroutine(NewBoard());
		}
		else
		{
			AudioManager.instance.PlaySound2D("n");
			answerUI.gameObject.SetActive(true);
			answerWordUI.gameObject.SetActive(true);
			answerText.text = wrongText;
			LevelManager.instance.AddWrongScore(1);
			//for (int i = 0; i < this.transform.childCount; i++)
			//{
			//	Destroy(GetComponent<Transform>().GetChild(i).gameObject);
			//}
			StartCoroutine(NewBoard());
		}
	}
	IEnumerator NewBoard()
	{
		GameEvents.current.onWaveEnd -= CalculateValue;
		yield return new WaitForSeconds(6);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		//answerUI.gameObject.SetActive(false);
		//totalValue = 0;
		//GameEvents.current.WaveStart();
	}
	public void Skip()
	{
		GameEvents.current.onWaveEnd -= CalculateValue;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
