using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class NumberGenerator : MonoBehaviour
{
	public RequiredAnswer markedBlock;
	public GeneratedBlockDisplay[] UIBlocks;
	public int minValue;
	public int maxValue;

	List<int> numbers;

	int generatedAnswer;
	private void Start()
	{
		PositiveValues();
		ShuffleArray();
	}
	public void PositiveValues()
	{
		//guranteed
		generatedAnswer = Random.Range(minValue, maxValue);
		int halvedNumber = generatedAnswer / 2;
		int treeNumber = generatedAnswer / 3;
		int quateredNumber = generatedAnswer / 4;
		int remainingNumber = generatedAnswer - halvedNumber - quateredNumber;

		//constrainted random smaller numbers
		int quarterRandomNumber = Random.Range(1, generatedAnswer / 4);
		int thirdRandomNumber = Random.Range(1, generatedAnswer  - treeNumber * 2);
		int fifthRandomNumber = Random.Range(1, remainingNumber * 2);

		//constrainted random bigger numbers
		int halfRandomNumber = Random.Range(1, generatedAnswer / 2);
		int fullRandomNumber = Random.Range(1, generatedAnswer - remainingNumber);

		numbers = new List<int> { treeNumber, quateredNumber, fullRandomNumber, halfRandomNumber, thirdRandomNumber,
		quarterRandomNumber, fifthRandomNumber, remainingNumber};

		GameObject tmp = Instantiate(markedBlock.gameObject, transform.position, Quaternion.identity, transform);
		tmp.GetComponent<RequiredAnswer>().text.text = generatedAnswer.ToString();
	}
	void ShuffleArray()
	{
		for (int i = 0; i < UIBlocks.Length; i++)
		{
			UIBlocks[i].text.text = numbers[i].ToString();
		}

		System.Random prng = new System.Random(5);

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

	//void Shuffle()
	//{
	//	System.Random prng = new System.Random(5);

	//	int lastIndex = numbers.Count - 1;
	//	while(lastIndex > 0){
	//		int tempValue = numbers[lastIndex];
	//		int randomIndex = prng.Next(0, lastIndex);
	//		numbers[lastIndex] = numbers[randomIndex];
	//		numbers[randomIndex] = tempValue;
	//		lastIndex--;
	//	}
	//}
}
