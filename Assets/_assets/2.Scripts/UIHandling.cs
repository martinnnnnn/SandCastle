using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHandling : MonoBehaviour
{

    public GameManager gameManager;
    public GameObject fightButton;

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
        gameManager.SetWall2();
    }

    public void OnWall2Click()
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


    public void OnRockClick()
    {
        gameManager.SetRock();
    }

    public void OnSeaweedClick()
    {
        gameManager.SetSeaweed();
    }





    public void StartFight()
    {
        gameManager.StartFight();
        ShowFightButton(false);
    }

    public void ShowFightButton(bool show)
    {
        fightButton.gameObject.SetActive(show);
        
    }

}
