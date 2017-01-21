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

    private bool useRock;
    private bool useSeaweed;



    void Awake()
    {
        sandQuantityCurrent = sandQuantityMax;
        rocksQuantity = 0;
        seaweedQuantity = 0;
        state = GameState.BUILDING;

        useRock = false;
        useSeaweed = false;
    }

    void Update()
    {
        switch (state)
        {
            case GameState.BUILDING:


                break;
            case GameState.FIGHTING:

                handleWaves();

                break;
        }
    }

    public void SpawnStructure(SandTile tile)
    {

        if (!tile.HasStructure() && currentPrefab)
        {
            if (currentPrefab == tourPrefab && sandQuantityCurrent >= tourSandValue)
            {
                sandQuantityCurrent -= tourSandValue;
                GameObject sandStructure = (GameObject)Instantiate(currentPrefab, tile.transform.position, new Quaternion());
                sandStructure.SendMessage("SetType", StructureType.TOUR_BASIC);
                tile.SetStructure(sandStructure);
            }
            else if (currentPrefab == wallPrefab && sandQuantityCurrent >= wallSandValue)
            {
                sandQuantityCurrent -= wallSandValue;
                GameObject sandStructure = (GameObject)Instantiate(currentPrefab, tile.transform.position, new Quaternion());
                sandStructure.SendMessage("SetType", StructureType.WALL_BASIC);
                tile.SetStructure(sandStructure);
            }
        }

        //else if (tile.HasStructure() && useRock)
        //{
        //    Debug.Log("in spawn rock");
        //    SandStructure structure = tile.GetStructure().GetComponent<SandStructure>();

        //    if (structure.GetStructureType() == StructureType.TOUR_BASIC) /*|| structure.GetStructureType() == StructureType.TOUR_SEAWEED*/
        //    {
        //        structure.SetType(StructureType.TOUR_ROCK);
        //    }
           
        //    if (structure.GetStructureType() == StructureType.WALL_BASIC) /*|| structure.GetStructureType() == StructureType.WALL_SEAWEED*/
        //    {
        //        structure.SetType(StructureType.WALL_ROCK);
        //    }
        //}
        //else if (tile.HasStructure() && useSeaweed)
        //{
        //    Debug.Log("in spawn sea");
        //    SandStructure structure = tile.GetStructure().GetComponent<SandStructure>();

        //    if (structure.GetStructureType() == StructureType.TOUR_BASIC) /*|| structure.GetStructureType() == StructureType.TOUR_ROCK*/
        //    {
        //        structure.SetType(StructureType.TOUR_SEAWEED);
        //    }
        //    if (structure.GetStructureType() == StructureType.WALL_BASIC) /* || structure.GetStructureType() == StructureType.WALL_ROCK*/
        //    {
        //        structure.SetType(StructureType.WALL_SEAWEED);
        //    }
        //}
    }


    public void ClickOnStructure(SandStructure structure)
    {
        if (useRock)
        {
            if (structure.GetStructureType() == StructureType.TOUR_BASIC)
            {
                structure.SetType(StructureType.TOUR_ROCK);
            }
            else if (structure.GetStructureType() == StructureType.WALL_BASIC)
            {
                structure.SetType(StructureType.WALL_ROCK);
            }
             
        }
        else if (useSeaweed)
        {
            if (structure.GetStructureType() == StructureType.TOUR_BASIC)
            {
                structure.SetType(StructureType.TOUR_SEAWEED);
            }
            else if (structure.GetStructureType() == StructureType.WALL_BASIC)
            {
                structure.SetType(StructureType.WALL_SEAWEED);
            }

        }
    }
    


    public void ChangeSandValue(int amount)
    {
        sandQuantityCurrent += amount;
    }

    public void ChangeRockValue(int amount)
    {
        rocksQuantity += amount;
    }

    public void ChangeSeaweedValue(int amount)
    {
        seaweedQuantity += amount;
    }


    public void SetTour()
    {
        useRock = false;
        useSeaweed = false;
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
        useRock = false;
        useSeaweed = false;
        if (currentPrefab != wallPrefab)
        {
            currentPrefab = wallPrefab;
        }
        else
        {
            currentPrefab = null;
        }
    }


    public void SetRock()
    {
        useRock = !useRock;
        if (useRock)
        {
            currentPrefab = null;
            useSeaweed = false;
        }
    }

    public void SetSeaweed()
    {
        useSeaweed = !useSeaweed;
        if (useSeaweed)
        {
            currentPrefab = null;
            useRock = false;
        }
    }


    public void StartFight()
    {
        state = GameState.FIGHTING;

    }

    private void handleWaves()
    {

    }
}
