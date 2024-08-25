using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnClouds : MonoBehaviour
{
    public GameObject[] cloudPrefabs;

    private float lowerBound = -1000f;
    private float upperBound = 3000f;
    private float xBound = 4000f;
    private float zBound = 3000f;

    private float StartDelay = 2f;

    private float spawnInterval = 3f;

    private int spawnLoop;
    private int initialCloudNumber = 20;
    // Start is called before the first frame update
    void Start()
    {
        spawnLoop = 0;
        while(spawnLoop < initialCloudNumber)
        {
            StartCloudSpawn();
            spawnLoop++;    
        }

        InvokeRepeating("SpawnRandomCloud", StartDelay, spawnInterval);
    }

    // Update is called once per frame
    void SpawnRandomCloud() 
    { 
        int cloudIndex = Random.Range(0, cloudPrefabs.Length);
        cloudPrefabs[cloudIndex].transform.localScale = Vector3.one * Random.Range(1f, 5f);
        Vector3 spawnPos = new Vector3(-xBound,Random.Range(lowerBound, upperBound), Random.Range(-zBound,zBound));

        Instantiate(cloudPrefabs[cloudIndex], spawnPos, cloudPrefabs[cloudIndex].transform.rotation);
    }

    void StartCloudSpawn()
    {
        int cloudIndex = Random.Range(0, cloudPrefabs.Length);
        cloudPrefabs[cloudIndex].transform.localScale = Vector3.one * Random.Range(1f, 5f);
        Vector3 spawnPos = new Vector3(Random.Range(-xBound, xBound), Random.Range(lowerBound, upperBound), Random.Range(-zBound, zBound));

        Instantiate(cloudPrefabs[cloudIndex], spawnPos, cloudPrefabs[cloudIndex].transform.rotation);
    }
}
