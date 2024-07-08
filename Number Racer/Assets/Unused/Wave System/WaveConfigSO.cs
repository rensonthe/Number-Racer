using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
	[SerializeField] List<GameObject> blockPrefabs;
	[SerializeField] Transform pathPrefab;
	[SerializeField] float dropSpeed;
	[SerializeField] float timeBetweenSpawns = 1f;
	[SerializeField] float spawnTimeVariance = 0f;
	[SerializeField] float minSpawnTime = 0.2f;
	public int GetBlockCount()
	{
		return blockPrefabs.Count;
	}

	public GameObject GetBlockPrefab(int index)
	{
		return blockPrefabs[index];
	}

	public Transform GetStartingWayPoint()
	{
		return pathPrefab.GetChild(0);
	}
	public List<Transform> GetWayPoints()
	{
		List<Transform> waypoints = new List<Transform>();
		foreach(Transform child in pathPrefab)
		{
			waypoints.Add(child);
		}
		return waypoints;
	}
	public float GetMoveSpeed()
	{
		return dropSpeed;
	}
	public float GetRandomSpawnTime()
	{
		float spawnTime = Random.Range(timeBetweenSpawns - spawnTimeVariance, timeBetweenSpawns + spawnTimeVariance);
		return Mathf.Clamp(spawnTime, minSpawnTime, float.MaxValue);
	}
}
