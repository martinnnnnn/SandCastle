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
        SoundManager.Instance.PlaySound("Menu_Bouton");

        gameManager.SetTour();
    }

    public void OnWallClick()
    {
        SoundManager.Instance.PlaySound("Menu_Bouton");

        gameManager.SetWall2();
    }

    public void OnWall2Click()
    {
        SoundManager.Instance.PlaySound("Menu_Bouton");

        gameManager.SetWall();
    }



    public void OnLeftClick()
    {
        SoundManager.Instance.PlaySound("Menu_Bouton");

        cameraHandler.GoLeft();
    }


    public void OnRightClick()
    {
        SoundManager.Instance.PlaySound("Menu_Bouton");

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
        SoundManager.Instance.StartFightMusic();
        gameManager.StartFight();
        ShowFightButton(false);
        SoundManager.Instance.PlaySound("Bouton_Maree");
    }

    public void ShowFightButton(bool show)
    {
        fightButton.gameObject.SetActive(show);
        
    }

}
