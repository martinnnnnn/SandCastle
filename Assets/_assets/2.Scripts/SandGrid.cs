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

        //if (column.waterTiles[j] > 0)
        //{
        //Debug.Log("index: " + index);
        //Debug.Log("x: " + column.xIndexCol + " y:" + j + " " + (tiles[2, 2].HasStructure() ? "yes" : "no"));
        //Debug.Log("TILE 2 2 : " + (tiles[2, 2].HasStructure() ? "yes" : "no"));
        if (tiles[column.xIndexCol, index].HasStructure())
        {
            column.yCollisionGrid = j;
            column.yPosColToMove = index - 1 + numberblanck;//!!!!!
            //Debug.Log("yPosColToMove: " + column.yPosColToMove);

            //Take damage

            float time = (tileSize.x * (column.yPosColToMove + 1)) / speed;
            Debug.Log("time damage: " + time);

            StartCoroutine(takeDamage(tiles[column.xIndexCol, index], time, -(wantedYStep - index)));

            return false;
        }
        else
        {
            return true;
        }
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
