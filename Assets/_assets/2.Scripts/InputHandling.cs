using UnityEngine;
using System.Collections;

public class InputHandling : MonoBehaviour
{


    public LayerMask layermask;

    public GameObject tourPrefab;
 
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
                    tile.SpawnStructure(tourPrefab);
                }
                else if (structure)
                {
                    
                }
                
            }
        }


    }
    
}
