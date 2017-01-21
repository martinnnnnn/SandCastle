using UnityEngine;
using System.Collections;

public class InputHandling : MonoBehaviour
{

    public GameManager gameManager;

    public LayerMask layermask;

    //public GameObject tourPrefab;
    //public GameObject wallPrefab;


    //private GameObject currentPrefab;


    private Vector2 beginTouchPosition;
    
 
    void Update()
    {

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            beginTouchPosition = Input.GetTouch(0).position;
            handleInput(beginTouchPosition);

        }

        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            handleInput(Input.GetTouch(0).position);
        }

        else if (Input.GetMouseButtonDown(0))
        {
            handleInput(Input.mousePosition);

        }


    }

    
    private void handleInput(Vector2 position)
    {
        RaycastHit info;
        Ray ray = Camera.main.ScreenPointToRay(position);
        if (Physics.Raycast(ray, out info, 100, layermask))
        {
            SandTile tile = info.collider.GetComponent<SandTile>();
            SandStructure structure = info.collider.GetComponent<SandStructure>();

            if (tile)
            {
                gameManager.SpawnStructure(tile);

            }
            else if (structure)
            {

            }

        }
    }
    
}
