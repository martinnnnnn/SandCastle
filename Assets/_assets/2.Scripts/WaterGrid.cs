using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaterGrid : MonoBehaviour{
    //public Sprite[] spriteMap;
    public GameObject waterTile;
    //public GameObject[] sfx;
    //private List<GameObject> sfxInstanciated;

    public WaterMap waterMap;
    //public List<Column> waterColumns;
    //public List<Link> linkMap;

    public float xStart;
    public float yStart;
    public int xSize;
    public int ySize;

    private Bounds planeBounds;

    private const string NAME_COLUMN = "COL";
    private const string NAME_WATERGRID = "WG";
    private const string NAME_SANDGRID = "SandGrid";

    Coroutine waveM;//move
    Coroutine waveA;//attack
    Coroutine waveV;//vanish


    public GameObject wg;

    void Awake()
    {
        planeBounds = waterTile.GetComponent<MeshRenderer>().bounds;

    }

    public void Start() {
        //this.waterColumns = new List<Column>(xSize);

        //planeBounds = waterTile.GetComponent<MeshRenderer>().bounds;

        //sfxInstanciated = new List<GameObject>();

        //initMap();
        //createGrid();
        //initMap(FileReader.ReadWaveShape("Assets/wave.txt", 5, 5));
        //createGrid();
    }

    void initMap()
    {
        //Reading step wave here


        List<List<int>> map = new List<List<int>>(xSize);
        for (int j = 0; j < xSize; j++)//X
        {
            map.Add(new List<int>(ySize));
            for(int i = 0; i < ySize; i++)//Y
            {
                map[j].Add(1);

                if(j == xSize -1 && i == ySize - 1)
                    map[j][i] = 0;
            }
        }

        waterMap = new WaterMap(map);
    }


    public void initMap(int[][] waveShape)
    {
        
        waterMap = new WaterMap(waveShape);
    }

    bool once = true;
    public void createGrid()
    {
        //wg = GameObject.Find(NAME_WATERGRID);
        if(!wg)
        {
            wg = new GameObject(NAME_WATERGRID);
        }
        else
        {
            foreach (Transform child in wg.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            wg.transform.localRotation = Quaternion.identity;
            wg.transform.localPosition = Vector3.zero;
            wg.transform.localScale = Vector3.one;
        }

        //

        for (int i = 0; i < xSize; i++)
        {
            GameObject yObject = new GameObject(NAME_COLUMN + i);
            yObject.transform.parent = wg.transform;

            for (int j = 0; j < ySize; j++)
            {
                GameObject tile = (GameObject) Instantiate(waterTile, new Vector3(0, 0, 0), Quaternion.identity);
                tile.transform.position = new Vector3(j * planeBounds.size.z + yStart, 0, i * planeBounds.size.x + xStart);
                tile.transform.parent = yObject.transform;

                if(waterMap.columns[i].waterTiles[j] == 0)
                {
                    tile.GetComponent<MeshRenderer>().enabled = false;
                }
                //Sprite selection here


                //if(Random.Range(0,10) > 7)
                  //  sfxInstanciated.Add((GameObject)Instantiate(sfx[0], new Vector3(j * planeBounds.size.z + yStart, 0.5f, i * planeBounds.size.x + xStart), Quaternion.identity));
            }
        }


        //wg.transform.position += new Vector3(-49.5f, 0, 32.7f);
        /*if (once)
        {
            once = false;
        }*/
        wg.transform.Rotate(new Vector3(0, 90f, 0));
        //wg.transform.localPosition += new Vector3(0, 0, planeBounds.size.x * ySize);
        //wg.transform.position = GetComponent<WaveSpawner>().hiddenPoint.position;
    }

    public void destroyGrid()
    {
        //GameObject wg = GameObject.Find(NAME_WATERGRID);
        if (wg != null)
        {
            foreach (Transform child in wg.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    public IEnumerator moveForwardSmooth(float speed)//int xColumn, int numberTilesForward, float speed)
    {
        /*
        //yield return new WaitForSeconds(time);
        float endTime = Time.fixedTime + time;
        
        float yToMove = planeBounds.size.y * numberTilesForward;

        while (Time.fixedTime < endTime)
        {//fixedDeltaTime

            yield return new WaitForFixedUpdate();
        }
        */

        /*
        GameObject columnToMove = GameObject.Find(NAME_COLUMN + xColumn);
        float elapsedTime = 0;
        Vector3 startingPos = columnToMove.transform.position;
        Vector3 endPos = new Vector3(startingPos.x + planeBounds.size.z * numberTilesForward, 0, startingPos.z);

        Debug.Log("moving " + NAME_COLUMN + xColumn);


        float pathLength = (endPos - startingPos).magnitude;
        float durationSeconds = pathLength / speed;

        while (elapsedTime < durationSeconds)
        {
            columnToMove.transform.position = Vector3.Lerp(startingPos, endPos, (elapsedTime / durationSeconds));
            Debug.Log(startingPos.x + "/" + startingPos.z + "-" + endPos.x + "/" + endPos.z + "-" + columnToMove.transform.position.x + "/" + columnToMove.transform.position.z);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        columnToMove.transform.position = endPos;
        createGrid();
        */

        //Variables
        List<GameObject> columns = new List<GameObject>();
        List<Vector3> startingPositions = new List<Vector3>();
        List<Vector3> endingPositions = new List<Vector3>();
        List<float> durations = new List<float>();

        //Check collisions and set max y to go per column
        GameObject.Find(NAME_SANDGRID).SendMessage("computeCollisions", waterMap);

        //Init
        for (int i = 0; i < xSize; i++)
        {
            columns.Add(GameObject.Find(NAME_COLUMN + i));
            startingPositions.Add(columns[i].transform.localPosition);
            endingPositions.Add(new Vector3(startingPositions[i].x + planeBounds.size.x * (waterMap.columns[i].yPosColToMove + 1), 0, startingPositions[i].z));// "+ 1" because we want to go check y0 of sandgrid (0 => move once)

            float pathLength = (startingPositions[i] - endingPositions[i]).magnitude;
            durations.Add(pathLength / speed);

            waterMap.columns[i].yPosCol = waterMap.columns[i].yPosColToMove;
            waterMap.columns[i].yPosColToMove = 0;
        }

        //Updating pos
        float elapsedTime = 0;

        List<int> indexesLeft = new List<int>();
        for (int i = 0; i < xSize; i++)
        {
            indexesLeft.Add(i);
        }

        while (indexesLeft.Count > 0)//checkDurations(durations))
        {
            for (int i = 0; i < xSize; i++)
            {
                if (elapsedTime < durations[i])
                {
                    columns[i].transform.localPosition = Vector3.Lerp(startingPositions[i], endingPositions[i], (elapsedTime / durations[i]));
                }
                else if(indexesLeft.Contains(i))
                {
                    indexesLeft.Remove(i);
                }
            }
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        //waveA = StartCoroutine(waveAttacking());
        /*
        //Check attack => sand grid is doing it
        foreach (WaterColumn col in waterMap.columns)
        {
            //if(col.yCollisionGrid > -1)

            Debug.Log("Sending message damage to structure : y=" + col.yCollisionGrid + " x=" + col.xIndexCol);
        }
        */



        //VANISHING
        //waveV = StartCoroutine(waveVanishing());
    }

    IEnumerator waveVanishing()
    {
        yield return new WaitForSeconds(waterMap.vanishingTime);
        //StopCoroutine(waveA);
        destroyGrid();
    }

    /*
    IEnumerator waveAttacking()
    {
        while(true)
        {
            yield return new WaitForSeconds(waterMap.cooldown);
            foreach(WaterColumn col in waterMap.columns)
            {
                //if(col.yCollisionGrid > -1)

                Debug.Log("Sending message damage to structure : y=" + col.yCollisionGrid + " x=" + col.xIndexCol);
            }
        }
    }
    

    bool checkDurations(List<float> durations)
    {
        float total = 0f;
        foreach(float dur in durations)
        {
            total += dur;
        }
        if (total <= 0f)
            return false;
        return true;
    }*/

    float delta = 0;
    private void Update()
    {
        delta += Time.deltaTime;
        //if (delta > (ySize * planeBounds.size.z / waterMap.speed) + 1)
        if(delta >= 5)
        {
            //generateRandomMoves();
            //waveM = StartCoroutine(moveForwardSmooth(waterMap.speed));
            delta = -999;
        }

        //updateSfx();
    }

    /*
    void updateSfx()
    {
        float speed = waterMap.speed;
        foreach (GameObject fx in sfxInstanciated)
        {
            fx.transform.Translate(-Vector3.left * speed * Time.deltaTime);
        }
    }
    */

    //Liste de waterMap a pop ?

    void nextWaterMap()
    {
        /*
        if (waveM != null)
            StopCoroutine(waveM);
        if (waveV != null)
            StopCoroutine(waveV);
            */
        waveM = StartCoroutine(moveForwardSmooth(waterMap.speed));
    }

    void generateRandomMoves()
    {
        /*
        linkMap.Clear();
        for(int i = 0; i < xSize; i++)
        {
            linkMap.Add(new Link(i, Random.Range(0, ySize)));
        }
        */
        foreach(WaterColumn col in waterMap.columns)
        {
            col.yPosColToMove = Random.Range(0, ySize);
        }
    }

    /*
    public void logMap()
    {
        string mappy = "";
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                mappy += map[i][j] + " ";
            }
            mappy += "\n";
        }
        Debug.Log(mappy);
    }
    */

  
}
