using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BlockBar : MonoBehaviour
{
	public TextMeshProUGUI[] textArray;
    [SerializeField] private GameObject blockPrefab;
	public NumberBlock numberBlock;

	[SerializeField] private int maxAmountofBlocks;
	[SerializeField] private float spawnDelay;
    [SerializeField] private float spawnInterval;
	[SerializeField] float paddingLeft;
	[SerializeField] float paddingRight;
	[SerializeField] float paddingTop;
	[SerializeField] float paddingBottom;

	Vector2 minBounds;
	Vector2 maxBounds;
	void Start()
    {
		StartCoroutine(FadeManager.Instance.FadingIn());
		GameEvents.current.onWaveStart += GenerateBlocks;
        GenerateBlocks();
		StartCoroutine(SpawnBlock());
	}
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GenerateBlocks();
		}
		if (Input.GetKeyDown(KeyCode.Q))
		{

        }
	}

    //Spawn and parent the blocks on the bar
    void GenerateBlocks()
    {
        for (int i = 0; i < maxAmountofBlocks; i++)
        {
            Instantiate(blockPrefab, transform.position, Quaternion.identity, transform);
        }
    }
    IEnumerator SpawnBlock()
    {
		yield return new WaitForSeconds(spawnDelay);
		int currentIndex = this.transform.childCount;

		for (int i = currentIndex - 1; i < currentIndex; i--)
		{
            if(currentIndex > 0)
            {
				AudioManager.instance.PlaySound2D("spawn");
				currentIndex--;
				Destroy(GetComponent<Transform>().GetChild(i).gameObject);
                //get generated value and display it on block
                string currentString = textArray[i].text;
                numberBlock.text.text = currentString;
				//spawn with the generated value from block

				Vector2 newPos = new Vector2();
				float randomX = Random.Range(-1.15f, 1.15f);
				float randomY = Random.Range(4.5f, 5);
				newPos.x = Mathf.Clamp(randomX, minBounds.x - paddingLeft, maxBounds.x + paddingRight);
				newPos.y = Mathf.Clamp(randomY, minBounds.y - paddingBottom, maxBounds.y + paddingTop);
				GameObject tmp = Instantiate(numberBlock.gameObject, newPos, Quaternion.identity);
                tmp.GetComponent<NumberBlock>().text.text = currentString;
				yield return new WaitForSeconds(spawnInterval);
			}
            else
            {
				Debug.Log("run");
				yield return new WaitForSeconds(1f);
				GameEvents.current.WaveEnd();
			}
		}
	}
}
