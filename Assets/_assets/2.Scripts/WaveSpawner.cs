using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{


    public int waveForceIncrementation;

    public float timeBetweenWaves;

    private float timeSinceLastWave;

    void Awake()
    {
        timeSinceLastWave = 0;
    }

    void Update()
    {
        timeSinceLastWave += Time.deltaTime;
        if (timeSinceLastWave > timeBetweenWaves)
        {
            timeSinceLastWave = 0;
            spawnWave();
        }
    }


    private void spawnWave()
    {
        // make a wave spawn with force
    }
    

}
