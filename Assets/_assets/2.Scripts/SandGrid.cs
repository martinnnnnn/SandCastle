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


}
