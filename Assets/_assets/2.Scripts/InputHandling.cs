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
            

            if (tile)
            {
                gameManager.SpawnStructure(tile);
                return;
            }
            SandStructure structure = info.collider.GetComponentInParent<SandStructure>();
            if (structure)
            {
                gameManager.ClickOnStructure(structure);
                return;
            }
            Sand sand = info.collider.GetComponent<Sand>();
            if (sand)
            {
                SoundManager.Instance.PlaySound("Ramassage_Sable");
                gameManager.ChangeSandValue(1);
                return;
            }

            Rock rock = info.collider.GetComponent<Rock>();
            if (rock)
            {
                SoundManager.Instance.PlaySound("Ramassage_Galet");
                if (gameManager.ChangeRockValue(1))
                {
                    Destroy(rock.gameObject);
                }
                return;
            }
            Seaweed sea = info.collider.GetComponent<Seaweed>();
            if (sea)
            {
                SoundManager.Instance.PlaySound("Ramassage_Algue");
                if (gameManager.ChangeSeaweedValue(1))
                {
                    Destroy(sea.gameObject);
                }
                return;
            }
        }
    }
    
}
