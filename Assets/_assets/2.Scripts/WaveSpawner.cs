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
    public Transform followPoint;

    //public string[] waveFilesEasy;
    //public string[] waveFilesMedium;
    //public string[] waveFilesHard;
    public TextAsset[] waveFilesSixHits;
    public TextAsset[] waveFilesEightHits;
    public TextAsset[] waveFilesTenHits;
	public TextAsset[] waveFilesTwelveHits;
	public TextAsset[] waveFilesForteenHits;

    public GameObject Sea;

    private bool startWaves;
    private WaterGrid waterGrid;

    private float timeSinceLastWave;

    void Awake()
    {
        timeSinceLastWave = timeBetweenWaves;
        startWaves = false;
        Sea.transform.position = new Vector3(hiddenPoint.position.x, hiddenPoint.position.y-0.1f, hiddenPoint.position.z);
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
        SoundManager.Instance.PlaySound("Vague_" + Random.Range(1, 4));
        while (value < 1.0)
        {
            Sea.transform.position = Vector3.Lerp(
                new Vector3(hiddenPoint.position.x, hiddenPoint.position.y - 0.1f, hiddenPoint.position.z),
                new Vector3(firstWarningPoint.position.x, firstWarningPoint.position.y - 0.1f, firstWarningPoint.position.z),
                value);
            waterGrid.wg.transform.position = Vector3.Lerp(hiddenPoint.position, firstWarningPoint.position, value);
            value += stepForward;
            yield return new WaitForEndOfFrame();
        }
        SoundManager.Instance.PlaySound("Vague_" + Random.Range(1, 4));

        yield return new WaitForSeconds(timeBeforeBackUp);
        SoundManager.Instance.PlaySound("Vague_" + Random.Range(1, 4));

        value = 0f;
        while (value < 1.0)
        {
            Sea.transform.position = Vector3.Lerp(
                new Vector3(firstWarningPoint.position.x, firstWarningPoint.position.y - 0.1f, firstWarningPoint.position.z),
                new Vector3(hiddenPoint.position.x, hiddenPoint.position.y - 0.1f, hiddenPoint.position.z),
                value);
            waterGrid.wg.transform.position = Vector3.Lerp(firstWarningPoint.position, hiddenPoint.position, value);
            value += stepBackward;
            yield return new WaitForEndOfFrame();
        }
        SoundManager.Instance.PlaySound("Vague_" + Random.Range(1, 4));

        //second warning
        yield return new WaitForSeconds(timeBetweenWarnings);
        SoundManager.Instance.PlaySound("Vague_" + Random.Range(1, 4));

        value = 0f;
        while (value < 1.0)
        {
            Sea.transform.position = Vector3.Lerp(
                new Vector3(hiddenPoint.position.x, hiddenPoint.position.y - 0.1f, hiddenPoint.position.z),
                new Vector3(secondWarningPoint.position.x, secondWarningPoint.position.y - 0.1f, secondWarningPoint.position.z),
                value);
            waterGrid.wg.transform.position = Vector3.Lerp(hiddenPoint.position, secondWarningPoint.position, value);
            value += stepForward;
            yield return new WaitForEndOfFrame();
        }
        SoundManager.Instance.PlaySound("Vague_" + Random.Range(1, 4));

        yield return new WaitForSeconds(timeBeforeBackUp);
        SoundManager.Instance.PlaySound("Vague_" + Random.Range(1, 4));

        value = 0f;
        while (value < 1.0)
        {
            Sea.transform.position = Vector3.Lerp(
                new Vector3(secondWarningPoint.position.x, secondWarningPoint.position.y - 0.1f, secondWarningPoint.position.z),
                new Vector3(hiddenPoint.position.x, hiddenPoint.position.y - 0.1f, hiddenPoint.position.z),
                value);
            waterGrid.wg.transform.position = Vector3.Lerp(secondWarningPoint.position, hiddenPoint.position, value);
            value += stepBackward;
            yield return new WaitForEndOfFrame();
        }

        SoundManager.Instance.PlaySound("Vague_" + Random.Range(1, 4));
        //attack
        yield return new WaitForSeconds(timeBetweenWarnings);
        SoundManager.Instance.PlaySound("Vague_" + Random.Range(1, 4));

        value = 0f;
        while (value < 1.0)
        {
            Sea.transform.position = Vector3.Lerp(
                new Vector3(hiddenPoint.position.x, hiddenPoint.position.y - 0.1f, hiddenPoint.position.z),
                new Vector3(attackPoint.position.x, attackPoint.position.y - 0.1f, attackPoint.position.z),
                value);
            waterGrid.wg.transform.position = Vector3.Lerp(hiddenPoint.position, attackPoint.position, value);
            value += stepForward;
            yield return new WaitForEndOfFrame();
        }
        SoundManager.Instance.PlaySound("Vague_" + Random.Range(1, 4));

        StartCoroutine(waterGrid.moveForwardSmooth(waterGrid.waterMap.speed));
        value = 0f;
        while (value < 1.0)
        {
            Sea.transform.position = Vector3.Lerp(
                new Vector3(attackPoint.position.x, attackPoint.position.y - 0.1f, attackPoint.position.z),
                new Vector3(followPoint.position.x, followPoint.position.y - 0.1f, followPoint.position.z),
                value);
            value += stepForward;
            yield return new WaitForEndOfFrame();
        }
        SoundManager.Instance.PlaySound("Vague_" + Random.Range(1, 4));

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
