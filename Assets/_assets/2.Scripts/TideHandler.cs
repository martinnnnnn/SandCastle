using UnityEngine;
using System.Collections;

public class TideHandler : MonoBehaviour
{
    
    public Transform highTide;
    public Transform lowTide;

    //public Transform 

    public GameObject SeaweedPrefab;

    public float speed;
    public float frequency;

    private bool high;

    private float timeSinceLastMove;


    void Awake()
    {
        high = false;
        timeSinceLastMove = frequency;
    }

    void Update()
    {

        timeSinceLastMove += Time.deltaTime;
        if (timeSinceLastMove > frequency)
        {
            timeSinceLastMove = 0;
            StartCoroutine(MoveTide());
        }
        else if (high)
        {
            //Instantiate(SeaweedPrefab,)
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
