using UnityEngine;
using System.Collections;


public class SandStructure : MonoBehaviour
{


    private SandTile sandTile;
    public int maxLife;
    public int midLife;
    public int lowLife;

    private int life;
    private bool isMidLife;
    private bool isLowLife;

    void Awake()
    {
        life = maxLife;
        isMidLife = false;
        isLowLife = false;
    }
    


    public void SetTile(SandTile tile)
    {
        sandTile = tile;

    }

    public void ChangeLife(int value)
    {
        life += value;


        if (life < 0)
        {
            sandTile.StructureDead();
        }

        else if (life < midLife && !isMidLife && !isLowLife)
        {
            isMidLife = true;
            // change model to mid
        }
        else if (life < lowLife && !isLowLife)
        {
            isLowLife = true;
            // change model to low
        }
    }

}
