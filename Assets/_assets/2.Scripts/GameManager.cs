using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public enum GameState
{
    BUILDING,
    FIGHTING
}


public class GameManager : MonoBehaviour
{

    public GameObject tourPrefab;
    public int tourSandValue;
    public GameObject wallPrefab;
    public int wallSandValue;

    private GameObject currentPrefab;


    public int sandQuantityMax;
    private int sandQuantityCurrent;

    private int rocksQuantity;
    private int seaweedQuantity;

    private GameState state;


    void Awake()
    {
        sandQuantityCurrent = sandQuantityMax;
        rocksQuantity = 0;
        seaweedQuantity = 0;
        state = GameState.BUILDING;
    }


    public void SpawnStructure(SandTile tile)
    {
        if (!tile.HasStructure() && currentPrefab)
        {
            if (currentPrefab == tourPrefab && sandQuantityCurrent >= tourSandValue)
            {
                sandQuantityCurrent -= tourSandValue;
                GameObject sandStructure = (GameObject)Instantiate(currentPrefab, tile.transform.position, new Quaternion());
                tile.SetStructure(sandStructure);
            }
            else if (currentPrefab == wallPrefab && sandQuantityCurrent >= wallSandValue)
            {
                sandQuantityCurrent -= wallSandValue;
                GameObject sandStructure = (GameObject)Instantiate(currentPrefab, tile.transform.position, new Quaternion());
                tile.SetStructure(sandStructure);
            }

        }
    }


    public void SpawnRock(SandTile tile)
    {

    }

    public void SetTour()
    {
        if (currentPrefab != tourPrefab)
        {
            currentPrefab = tourPrefab;
        }
        else
        {
            currentPrefab = null;
        }
    }


    public void SetWall()
    {
        if (currentPrefab != wallPrefab)
        {
            currentPrefab = wallPrefab;
        }
        else
        {
            currentPrefab = null;
        }
    }
}
