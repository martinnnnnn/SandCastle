using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaterColumn {
    public int xIndexCol;//column index in water grid
    public int yCollisionGrid;//y index for the collision with a structure
    public int yPosCol;//current y position
    public int yPosColToMove;//number of y to move forward next time translating

    public List<int> waterTiles;//its own tiles

    public WaterColumn(int xIndexCol, List<int> waterTiles)//, List<WaterTile> tiles)
    {
        this.xIndexCol = xIndexCol;
        this.yCollisionGrid = -1;
        this.yPosCol = 0;
        this.yPosColToMove = 0;
        this.waterTiles = waterTiles;
        //tilesY = tiles;
    }

    /*
    public WaterColumn(int xIndexCol, int yCollisionGrid, int yPosCol)
    {
        this.xIndexCol = xIndexCol;
        this.yCollisionGrid = yCollisionGrid;
        this.yPosCol = this.yPosColToMove = yPosCol;
    }
    */
}
