using UnityEngine;
using System.Collections.Generic;

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
        Debug.Log("tilesize:" + tileSize);
        //tilePrefab.GetComponent<SandTile>().Size = tileSize;
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Vector3 position = new Vector3(topLeftCornerPosition.position.x + tileSize.x * i, topLeftCornerPosition.position.y, topLeftCornerPosition.position.z - tileSize.x * j);
                //Debug.Log(position);
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
            //Check if collision
            for (int i = 0; i <= waterMap.wantedYStep; i++)
            {
                if (!checkWantedStep(i, column)) break;
            }

            Debug.Log("column: " + column.xIndexCol + " togo:" + column.yPosColToMove);
        }
    }

    public bool checkWantedStep(int index, WaterColumn column)
    {
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
    }
}
