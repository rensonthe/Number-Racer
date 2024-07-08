using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    BlockSpawner blockSpawner;
    WaveConfigSO waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;
    public bool hasPath = false;
    void Awake()
    {
        blockSpawner = FindObjectOfType<BlockSpawner>();
    }
    void Start()
    {
        waveConfig = blockSpawner.GetCurrentWave();
        if (hasPath)
		{
			waypoints = waveConfig.GetWayPoints();
			transform.position = waypoints[waypointIndex].position;
		}
    }
    void Update()
    {
        if (hasPath)
        {
			FollowPath();
		}
    }
    void FollowPath()
    {
        if(waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            if(transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
