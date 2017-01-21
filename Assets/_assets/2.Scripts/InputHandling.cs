using UnityEngine;
using System.Collections;

public class InputHandling : MonoBehaviour
{


    public LayerMask layermask;

    public GameObject tourPrefab;
    public GameObject wallPrefab;


    private GameObject currentPrefab;


    
 
    void Update()
    {

        
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit info;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out info, 100, layermask))
            {
                SandTile tile = info.collider.GetComponent<SandTile>();
                SandStructure structure = info.collider.GetComponent<SandStructure>();

                if (tile)
                {
                    if (currentPrefab)
                    {
                        tile.SpawnStructure(currentPrefab);
                    }
                }
                else if (structure)
                {
                    
                }
                
            }
        }


    }


    public void SetTour()
    {
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
        if (currentPrefab != wallPrefab)
        {
            currentPrefab = wallPrefab;
        }
        else
        {
            currentPrefab = null;
        }
    }
}
