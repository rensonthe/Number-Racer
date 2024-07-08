using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    WaveConfigSO currentWave;
    public bool isPathing;

	[SerializeField] float paddingLeft;
	[SerializeField] float paddingRight;
	[SerializeField] float paddingTop;
	[SerializeField] float paddingBottom;

	public NumberBlock numberBlock;
	Vector2 minBounds;
	Vector2 maxBounds;

	[SerializeField] private GameObject blockPrefab;
	[SerializeField] private int maxAmountofBlocks;
	void Start()
    {
		GameEvents.current.onWaveEnd += onBlockSpawned;

		InitBlocks();
		InitBounds();
        if (isPathing)
        {
			StartCoroutine(SpawnPathedWaves());
		}
		else
		{
			StartCoroutine(SpawnWaves());
		}
	}
	void InitBlocks()
	{
		for (int i = 0; i < maxAmountofBlocks; i++)
		{
			Instantiate(blockPrefab, transform.position, Quaternion.identity, transform);
		}
	}
	void onBlockSpawned()
	{
		Debug.Log("event ran");
	}
	void InitBounds()
	{
		Camera mainCamera = Camera.main;
		minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
		maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
	}
	public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }
	void GenerateRandomPosition()
	{
		Vector2 newPos = new Vector2();
		float randomX = Random.Range(-1.35f, 1.35f);
		float randomY = Random.Range(4.5f, 5);
		newPos.x = Mathf.Clamp(randomX, minBounds.x - paddingLeft, maxBounds.x + paddingRight);
		newPos.y = Mathf.Clamp(randomY, minBounds.y - paddingBottom, maxBounds.y + paddingTop);
	}

	//stumped, can't figure out how to integrate the wave system and keeping track of the UI to remove on the left
	//I'd have to generate the blocks on the bar based on the SO and then iterate through them whilst deleting
	IEnumerator SpawnWaves()
	{
		foreach (WaveConfigSO wave in waveConfigs)
		{
			currentWave = wave;
			int currentIndex = this.transform.childCount;

			for (int i = 0; i < currentWave.GetBlockCount(); i++)
			{
				Debug.Log(i);
				//track and delete last in index
				for (int l = currentIndex - 1; l < currentIndex; l--)
				{
					if (currentIndex > 0)
					{
						currentIndex--;
						Destroy(GetComponent<Transform>().GetChild(i).gameObject);
					}
				}

				Vector2 newPos = new Vector2();
				float randomX = Random.Range(-1.35f, 1.35f);
				float randomY = Random.Range(4.5f, 5);
				newPos.x = Mathf.Clamp(randomX, minBounds.x - paddingLeft, maxBounds.x + paddingRight);
				newPos.y = Mathf.Clamp(randomY, minBounds.y - paddingBottom, maxBounds.y + paddingTop);

				//get generated value and display it on block
				string currentString = FindObjectOfType<GeneratedBlockDisplay>().text.text;
				numberBlock.text.text = currentString;
				//spawn with the generated value from block
				GameObject tmp = Instantiate(currentWave.GetBlockPrefab(i), newPos, Quaternion.identity, transform);
				tmp.GetComponent<NumberBlock>().text.text = currentString;

				yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
			}
			//change to after the answer has been resolved and be reflected in the UI
			yield return new WaitForSeconds(timeBetweenWaves);
		}
	}
	IEnumerator SpawnPathedWaves()
    {
        foreach(WaveConfigSO wave in waveConfigs)
        {
            currentWave = wave;
			for (int i = 0; i < currentWave.GetBlockCount(); i++)
			{
				Debug.Log(i);
				Instantiate(currentWave.GetBlockPrefab(i), currentWave.GetStartingWayPoint().position, Quaternion.identity, transform);
				yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
			}
			//change to after the answer has been resolved and be reflected in the UI
			yield return new WaitForSeconds(timeBetweenWaves);
		}
    }
}
