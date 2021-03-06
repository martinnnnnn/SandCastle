﻿using UnityEngine;
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
    public GameObject wallPrefab2;


    private GameObject currentPrefab;


    public ButtonsHandler buttonsHandler;
    
    public int sandQuantityMax;
    private int sandQuantityCurrent;

    private int rocksQuantity;
    private int seaweedQuantity;

    private GameState state;

    private bool useRock;
    private bool useSeaweed;

    private WaterGrid waterGrid;

    private static int towerNumber;
    private static int maxTower = 5;

    void Awake()
    {
        towerNumber = 0;
        //sandQuantityCurrent = sandQuantityMax;
        rocksQuantity = 0;
        seaweedQuantity = 0;
        state = GameState.BUILDING;

        useRock = false;
        useSeaweed = false;
    }

    void Start()
    {
        waterGrid = GetComponent<WaterGrid>();
        //waterGrid.initMap(FileReader.ReadWaveShape("Assets/wave.txt", 5, 5));
        //waterGrid.createGrid();
        ChangeSandValue(sandQuantityMax);
        //waterGrid.wg.transform.position = GetComponent<WaveSpawner>().hiddenPoint.position;
    }
    
    void Update()
    {

    }

    public void SpawnStructure(SandTile tile)
    {

        if (!tile.HasStructure() && currentPrefab)
        {
            if (currentPrefab == tourPrefab && sandQuantityCurrent >= tourSandValue && towerNumber < maxTower)
            {
                towerNumber++;
                ChangeSandValue(-tourSandValue);
                //sandQuantityCurrent -= tourSandValue;
                GameObject sandStructure = (GameObject)Instantiate(currentPrefab, new Vector3(tile.transform.position.x, -3f, tile.transform.position.z), new Quaternion());
                
                //sandStructure.SendMessage("SetType", StructureType.TOUR_BASIC);
                tile.SetStructure(sandStructure);
            }
            else if (currentPrefab == wallPrefab && sandQuantityCurrent >= wallSandValue)
            {
                ChangeSandValue(-wallSandValue);
                //sandQuantityCurrent -= wallSandValue;
                GameObject sandStructure = (GameObject)Instantiate(currentPrefab, tile.transform.position, new Quaternion());
                //sandStructure.transform.position = new Vector3(tile.transform.position.x, -0.9f, tile.transform.position.z);

                //sandStructure.SendMessage("SetType", StructureType.WALL_BASIC);
                tile.SetStructure(sandStructure);
            }
            else if (currentPrefab == wallPrefab2 && sandQuantityCurrent >= wallSandValue)
            {
                ChangeSandValue(-wallSandValue);
                //sandQuantityCurrent -= wallSandValue;
                GameObject sandStructure = (GameObject)Instantiate(currentPrefab, tile.transform.position, new Quaternion());
                //sandStructure.SendMessage("SetType", StructureType.WALL_BASIC);
                tile.SetStructure(sandStructure);
            }
        }

 
    }


    public void ClickOnStructure(SandStructure structure)
    {
        if (useRock && rocksQuantity > 0)
        {
            if (structure.GetStructureType() == StructureType.BASIC)
            {
                rocksQuantity--;

                structure.SetType(StructureType.ROCK);
                SoundManager.Instance.PlaySound("Upgrade_Batiment");
            }
        }
        else if (useSeaweed && seaweedQuantity > 0)
        {
            if (structure.GetStructureType() == StructureType.BASIC)
            {
                seaweedQuantity--;
                SoundManager.Instance.PlaySound("Upgrade_Batiment");
                structure.SetType(StructureType.SEA);
            }
        }
    }
    


    public void ChangeSandValue(int amount)
    {
        sandQuantityCurrent += amount;
        if (sandQuantityCurrent > 4) sandQuantityCurrent = 4;
        buttonsHandler.ChangeBucketAmount(amount);
    }

    public bool ChangeRockValue(int amount)
    {
        bool ans = true;
        rocksQuantity += amount;
        if (rocksQuantity > 3)
        {
            rocksQuantity = 3;
            ans = false;
        }
        buttonsHandler.ChangeRockAmount(amount);
        return ans;
    }

    public bool ChangeSeaweedValue(int amount)
    {
        bool ans = true;
        seaweedQuantity += amount;
        if (seaweedQuantity > 3)
        {
            seaweedQuantity = 3;
            ans = false;
        }

        buttonsHandler.ChangeSeaAmount(amount);
        return ans;
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

    public void SetWall2()
    {
        useRock = false;
        useSeaweed = false;
        if (currentPrefab != wallPrefab2)
        {
            currentPrefab = wallPrefab2;
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

        GetComponent<WaveSpawner>().StartWaves();
    }
    
}
