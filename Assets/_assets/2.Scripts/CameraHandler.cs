using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraHandler : MonoBehaviour
{


    public Transform CastlePosition;
    public Transform SeaPosition;
    public Transform BeachPosition;



    private Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;


    void Awake()
    {
        target = CastlePosition;
    }


    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smoothTime);
    }



    public void GoLeft()
    {
        if (target == SeaPosition)
        {
            target = CastlePosition;
        }
        else if (target == CastlePosition)
        {
            target = BeachPosition;
        }
    }

    public void GoRight()
    {
        if (target == BeachPosition)
        {
            target = CastlePosition;
        }
        else if (target == CastlePosition)
        {
            target = SeaPosition;
        }
    }


}
