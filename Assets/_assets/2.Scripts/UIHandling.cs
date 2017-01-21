using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHandling : MonoBehaviour
{


    public InputHandling inputHandler;
    


    public void OnTourClick()
    {
        inputHandler.SetTour();
    }

    public void OnWallClick()
    {
        inputHandler.SetWall();
    }
    

}
