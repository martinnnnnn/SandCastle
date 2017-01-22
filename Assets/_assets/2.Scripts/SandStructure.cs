using UnityEngine;
using System.Collections;


public enum StructureType
{
    BASIC,
    ROCK,
    SEA,
}

public class SandStructure : MonoBehaviour
{

    public GameObject basicHigh;
    public GameObject basicMid;
    public GameObject basicLow;

    public GameObject rockHigh;
    public GameObject rockMid;
    public GameObject rockLow;

    public GameObject seaHigh;
    public GameObject seaMid;
    public GameObject seaLow;

    private SandTile sandTile;

    public int maxLife;    
    public int midLife;
    public int lowLife;
    public int rockBonus;
    public int seaBonus;

    private int life;
    private bool isMidLife;
    private bool isLowLife;

    private StructureType type;




    private void updateModel()
    {
        Debug.Log("life:: " + life);
        SetFalse();
        if (isMidLife)
        {
            if (type == StructureType.BASIC)
            {
                Debug.Log("mescouilles");
                basicMid.SetActive(true);
            }
            else if (type == StructureType.ROCK)
            {
                rockMid.SetActive(true);
            }
            else if (type == StructureType.SEA)
            {
                seaMid.SetActive(true);
            }
        }
        else if (isLowLife)
        {
            Debug.Log("low");
            if (type == StructureType.BASIC)
            {
                basicLow.SetActive(true);
            }
            else if (type == StructureType.ROCK)
            {
                rockLow.SetActive(true);
            }
            else if (type == StructureType.SEA)
            {
                seaLow.SetActive(true);
            }
        }
        else
        {
            Debug.Log("low");
            if (type == StructureType.BASIC)
            {
                basicHigh.SetActive(true);
            }
            else if (type == StructureType.ROCK)
            {
                rockHigh.SetActive(true);
            }
            else if (type == StructureType.SEA)
            {
                seaHigh.SetActive(true);
            }
        }
    }


    void Awake()
    {
        type = StructureType.BASIC;
        life = maxLife;
        isMidLife = false;
        isLowLife = false;

        SetFalse();
        basicHigh.SetActive(true);
        ChangeLife(-1);
    }
    
    
    public void SetType(StructureType type)
    {
        if (this.type != type)
        {
            this.type = type;
            updateModel();
        }
    }

    public StructureType GetStructureType()
    {
        return type;
    }

    public void SetTile(SandTile tile)
    {
        sandTile = tile;

    }

    public void ChangeLife(int value)
    {
        
        life += value;
        Debug.Log("take damage " + value + " life:" + life);
        /*
        if (life < 0)
        {
            sandTile.StructureDead();
        }
        */

        
        if (life <= lowLife && !isLowLife)
        {
            isLowLife = true;
            updateModel();
            sandTile.StructureDead();
        }
        else if(life <= midLife && !isMidLife && !isLowLife)
        {
            isMidLife = true;
            updateModel();
        }
    }

    private void SetFalse()
    {
        basicHigh.SetActive(false);
        basicMid.SetActive(false);
        basicLow.SetActive(false);

        rockHigh.SetActive(false);
        rockMid.SetActive(false);
        rockLow.SetActive(false);

        seaHigh.SetActive(false);
        seaMid.SetActive(false);
        seaLow.SetActive(false);
    }


}
