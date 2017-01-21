using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHandling : MonoBehaviour
{


    public GameManager gameManager;

    private CameraHandler cameraHandler;

    void Start()
    {
        cameraHandler = Camera.main.GetComponent<CameraHandler>();
    }


    public void OnTourClick()
    {
        gameManager.SetTour();
    }

    public void OnWallClick()
    {
        gameManager.SetWall();
    }
    


    public void OnLeftClick()
    {
        cameraHandler.GoLeft();
    }


    public void OnRightClick()
    {
        cameraHandler.GoRight();
    }

}
