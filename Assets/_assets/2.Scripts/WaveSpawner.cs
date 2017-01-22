using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    
    public float timeBetweenWaves;
    public float timeBetweenWarnings;
    public float timeBeforeBackUp;

    public Transform hiddenPoint;
    public Transform firstWarningPoint;
    public Transform secondWarningPoint;
    public Transform attackPoint;

    private bool startWaves;
    private WaterGrid waterGrid;

    private float timeSinceLastWave;

    void Awake()
    {
        timeSinceLastWave = timeBetweenWaves;
        startWaves = false;
    }

    void Start()
    {
        waterGrid = GetComponent<WaterGrid>();
    }
    
    public void StartWaves()
    {
        startWaves = true;
    }

    Coroutine waves;
    void Update()
    {
        if (startWaves)
        {
            timeSinceLastWave += Time.deltaTime;
            if (timeSinceLastWave > timeBetweenWaves)
            {
               // waterGrid.destroyGrid();
                waterGrid.initMap(FileReader.ReadWaveShape("Assets/wave.txt", 5, 5));
                waterGrid.createGrid();
                Debug.Log("mescouilles");
                timeSinceLastWave = 0;
                if (waves != null) StopCoroutine(spawnWave());
                waves = StartCoroutine(spawnWave());
            }
        }
    }



    float step = 0.05f;
    float value = 0f;
    private IEnumerator spawnWave()
    {


        //first warning
        Vector3 startingPos = waterGrid.wg.transform.position;
        value = 0f;
        while (value < 1.0)
        {
            waterGrid.wg.transform.position = Vector3.Lerp(hiddenPoint.position, firstWarningPoint.position, value);
            value += step;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(timeBeforeBackUp);

        value = 0f;
        while (value < 1.0)
        {
            waterGrid.wg.transform.position = Vector3.Lerp(firstWarningPoint.position, hiddenPoint.position, value);
            value += step;
            yield return new WaitForEndOfFrame();
        }

        //second warning
        yield return new WaitForSeconds(timeBetweenWarnings);
        value = 0f;
        while (value < 1.0)
        {
            waterGrid.wg.transform.position = Vector3.Lerp(hiddenPoint.position, secondWarningPoint.position, value);
            value += step;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(timeBeforeBackUp);

        value = 0f;
        while (value < 1.0)
        {
            waterGrid.wg.transform.position = Vector3.Lerp(secondWarningPoint.position, hiddenPoint.position, value);
            value += step;
            yield return new WaitForEndOfFrame();
        }

        //attack
        yield return new WaitForSeconds(timeBetweenWarnings);
        value = 0f;
        while (value < 1.0)
        {
            waterGrid.wg.transform.position = Vector3.Lerp(hiddenPoint.position, attackPoint.position, value);
            value += step;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(waterGrid.moveForwardSmooth(waterGrid.waterMap.speed));

    }



}
