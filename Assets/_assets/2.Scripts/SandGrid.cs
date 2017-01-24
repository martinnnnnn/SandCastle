using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SandGrid : MonoBehaviour
{

    public int x;
    public int y;
    public GameObject tilePrefab;
    private Vector3 tileSize;
    public Transform topLeftCornerPosition;
    public GameObject nexus;
    private SandTile[,] tiles;

    void Awake()
    {
        tiles = new SandTile[x,y];
        tileSize = tilePrefab.GetComponent<SandTile>().Size;
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Vector3 position = new Vector3(topLeftCornerPosition.position.x + tileSize.x * i, topLeftCornerPosition.position.y, topLeftCornerPosition.position.z - tileSize.x * j);
                GameObject tileObj = (GameObject)Instantiate(tilePrefab, position, new Quaternion());
                
                tiles[i,j] = tileObj.GetComponent<SandTile>();

                if (i == 3 && j == 3)
                {
                    tiles[i, j].SetStructure(nexus);
                }
            }
        }
    }


    int GetFirstStructureIndex(int x)
    {

        for (int j = 0; j < y; j++)
        {
            if (tiles[x, j].HasStructure())
            {
                return j;
            }
        }

        return -1;
    }

    public void computeCollisions(WaterMap waterMap)
    {
        foreach(WaterColumn column in waterMap.columns)
        {
            //Default values
            column.yCollisionGrid = -1;
            column.yPosColToMove = waterMap.wantedYStep;
            //Check if collision one by one (y0,y1,.. until reaching step end)
            for (int i = 0; i <= waterMap.wantedYStep; i++)
            {
                if (!checkWantedStep(i, column, waterMap.wantedYStep, waterMap.speed)) break;
            }
        
            Debug.Log("column: " + column.xIndexCol + " togo:" + column.yPosColToMove);
        }

    }

    IEnumerator takeDamage(SandTile tile, float waitSeconds, int damage)
    {
        yield return new WaitForSeconds(waitSeconds);
        tile.GetStructure().SendMessage("ChangeLife", damage);
    }

    public bool checkWantedStep(int index, WaterColumn column, int wantedYStep, float speed)
    {
        bool res = true;
        int back = 0;
        for (int i = column.waterTiles.Count - 1; i >= (column.waterTiles.Count - 1 - index); i--)
        {
            Debug.Log("column:" + column.xIndexCol + " icheck:" + i);
            if (column.waterTiles[i] != 0 && tiles[column.xIndexCol, index - back].HasStructure())
            {
                column.yCollisionGrid = index - back;
                column.yPosColToMove = index - 1;


                //if will be destroyed continue !! not done yet

                //Take damage
                float time = (tileSize.x * (column.yPosColToMove + 1)) / speed;
                Debug.Log("time damage: " + time + " damage:" + -(wantedYStep - index - 1));
                StartCoroutine(takeDamage(tiles[column.xIndexCol, index - back], time, -(wantedYStep - index - 1)));

                res = false;
                break;
            }
            else
            {
                back++;
            }
        }
        return res;

        /*

        //Get last tile water, beginning from bottom
        int j = column.waterTiles.Count - 1;
        int numberblanck = 0;
        while (column.waterTiles[j] == 0)
        {
            numberblanck++;
            j--;
            if(j < 0)
            {
                return false;
            }
        }
        numberblanck--;

        numberblanck = Mathf.Clamp(numberblanck, 0, column.waterTiles.Count);
           

        //if (column.waterTiles[j] > 0)
        //{
        //Debug.Log("index: " + index);
        //Debug.Log("x: " + column.xIndexCol + " y:" + j + " " + (tiles[2, 2].HasStructure() ? "yes" : "no"));
        //Debug.Log("TILE 2 2 : " + (tiles[2, 2].HasStructure() ? "yes" : "no"));


        if (tiles[column.xIndexCol, j].HasStructure())
        {
            column.yCollisionGrid = j;
            column.yPosColToMove = index;// + numberblanck;// - 1 ;//!!!!!
            //Debug.Log("yPosColToMove: " + column.yPosColToMove);

            //Take damage

            float time = (tileSize.x * (column.yPosColToMove)) / speed;
            Debug.Log("time damage: " + time);

            StartCoroutine(takeDamage(tiles[column.xIndexCol, j], time, -(wantedYStep - index)));

            return false;
        }
        else
        {
            return true;
        }


        */











        //}
        //return true;

        /*
        int numberblanck = 0;
        for (int j = column.waterTiles.Count - 1; j >= column.waterTiles.Count - (index + 1); j--)
        {
            if (column.waterTiles[j] > 0)
            {
                Debug.Log("index: " + index);
                Debug.Log("x: " + column.xIndexCol + " y:" + j + " " + (tiles[2, 2].HasStructure() ? "yes" : "no"));
                //Debug.Log("TILE 2 2 : " + (tiles[2, 2].HasStructure() ? "yes" : "no"));
                if (tiles[column.xIndexCol, index].HasStructure())
                {
                    Debug.Log("OKOKOK 222");
                    column.yCollisionGrid = j;
                    column.yPosColToMove = index - 1 + numberblanck;//!!!!!
                    Debug.Log("yPosColToMove: " + column.yPosColToMove);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                numberblanck++;
            }
        }
        return true;
        */
    }
}
