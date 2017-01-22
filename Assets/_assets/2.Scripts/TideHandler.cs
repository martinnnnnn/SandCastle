using UnityEngine;
using System.Collections;

public class TideHandler : MonoBehaviour
{
    
    public Transform highTide;
    public Transform lowTide;

    public Transform topLeftSpawn;
    public Transform bottomRightSpawn;

    public GameObject SeaweedPrefab;

    public float speed;
    public float tideFrequency;

    private bool high;

    private float timeSinceLastMove;

    private float spawnfrequency = 2;
    private float timeSinceLastSpawn;

    void Awake()
    {
        high = false;
        timeSinceLastMove = tideFrequency;
        timeSinceLastSpawn = 0;
    }

    void Update()
    {

        timeSinceLastMove += Time.deltaTime;
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastMove > tideFrequency)
        {
            timeSinceLastMove = 0;
            StartCoroutine(MoveTide());
        }
        // si la mer est haute
        else if (high && timeSinceLastSpawn > spawnfrequency)
        {
            timeSinceLastSpawn = 0;
            for (int i = 0; i < Random.Range(2,6); i++)
            {
                float x = Random.Range(topLeftSpawn.position.x, bottomRightSpawn.position.x);
                float z = Random.Range(bottomRightSpawn.position.z, topLeftSpawn.position.z);
                Instantiate(SeaweedPrefab, new Vector3(x, transform.position.y, z), new Quaternion());                                      
            }
        }

    }

    float value = 0f;
    float step = 0.005f;

    IEnumerator MoveTide()
    {
        if (high)
        {
            
            value = 0f;
            while (value < 1.0)
            {
                transform.position = Vector3.Lerp(highTide.position, lowTide.position, value);
                value += step;
                yield return new WaitForEndOfFrame();
            }

        }
        else
        {
            value = 0f;
            while (value < 1.0)
            {
                transform.position = Vector3.Lerp(lowTide.position, highTide.position, value);
                value += step;
                yield return new WaitForEndOfFrame();
            }
        }
        high = !high;
    }

}
