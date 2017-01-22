using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    
    public float numberOfWaves;
    public float timeBetweenWaves;
    public float timeBetweenWarnings;
    public float timeBeforeBackUp;

    public Transform hiddenPoint;
    public Transform firstWarningPoint;
    public Transform secondWarningPoint;
    public Transform attackPoint;

    //public string[] waveFilesEasy;
    //public string[] waveFilesMedium;
    //public string[] waveFilesHard;
    public TextAsset[] waveFilesSixHits;
    public TextAsset[] waveFilesEightHits;
    public TextAsset[] waveFilesTenHits;
	public TextAsset[] waveFilesTwelveHits;
	public TextAsset[] waveFilesForteenHits;

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
            if (timeSinceLastWave > timeBetweenWaves && numberOfWaves > 0)
            {
                numberOfWaves--;

                if (waves != null) StopCoroutine(waves);
                waves = StartCoroutine(spawnWave());

                waterGrid.initMap(FileReader.ReadWaveShape(getPathToWave(), 6, 4));
                waterGrid.createGrid();
                timeSinceLastWave = 0;
                
            }
        }
    }

    //Assets/wave.txt


    public float stepForward;
    public float stepBackward;

    float value = 0f;
    private IEnumerator spawnWave()
    {


        //first warning
        Vector3 startingPos = waterGrid.wg.transform.position;
        value = 0f;
        while (value < 1.0)
        {
            waterGrid.wg.transform.position = Vector3.Lerp(hiddenPoint.position, firstWarningPoint.position, value);
            value += stepForward;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(timeBeforeBackUp);

        value = 0f;
        while (value < 1.0)
        {
            waterGrid.wg.transform.position = Vector3.Lerp(firstWarningPoint.position, hiddenPoint.position, value);
            value += stepBackward;
            yield return new WaitForEndOfFrame();
        }

        //second warning
        yield return new WaitForSeconds(timeBetweenWarnings);
        value = 0f;
        while (value < 1.0)
        {
            waterGrid.wg.transform.position = Vector3.Lerp(hiddenPoint.position, secondWarningPoint.position, value);
            value += stepForward;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(timeBeforeBackUp);

        value = 0f;
        while (value < 1.0)
        {
            waterGrid.wg.transform.position = Vector3.Lerp(secondWarningPoint.position, hiddenPoint.position, value);
            value += stepBackward;
            yield return new WaitForEndOfFrame();
        }

        //attack
        yield return new WaitForSeconds(timeBetweenWarnings);
        value = 0f;
        while (value < 1.0)
        {
            waterGrid.wg.transform.position = Vector3.Lerp(hiddenPoint.position, attackPoint.position, value);
            value += stepForward;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(waterGrid.moveForwardSmooth(waterGrid.waterMap.speed));

    }

    private TextAsset getPathToWave()
    {
        if (numberOfWaves < 3)
        {
			return waveFilesForteenHits[Random.Range(0, waveFilesForteenHits.Length)];
        }
        else if (numberOfWaves < 5)
        {
			return waveFilesTwelveHits[Random.Range(0, waveFilesTwelveHits.Length)];
        }
		else if (numberOfWaves < 7)
		{
			return waveFilesTenHits[Random.Range(0,  waveFilesTenHits.Length)];
		}
		else if (numberOfWaves < 9)
		{
			return waveFilesEightHits[Random.Range(0, waveFilesEightHits.Length)];
		}
        else
        {
			return waveFilesSixHits[Random.Range(0, waveFilesSixHits.Length)];
        }
    }
}
