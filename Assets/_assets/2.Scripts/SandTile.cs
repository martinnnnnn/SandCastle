using UnityEngine;
using System.Collections;

public class SandTile : MonoBehaviour
{

    // taille de la tile
    private Vector2 _size;
    public Vector2 Size
    {
        get
        {
            return GetComponent<MeshRenderer>().bounds.size;
        }
        set
        {
            _size = value;
        }
    }

    private bool hasStructure;

    // prefab de la structure à afficher a l'emplacement représenté par cette tile
    private GameObject sandStructure;


    void Awake()
    {
        hasStructure = false;
    }

    void Update()
    {

        


    }

    public void SetStructure(GameObject structure)
    {
        if (sandStructure != null)
        {
            Destroy(sandStructure);
            sandStructure = null;
        }
        sandStructure = structure;
        sandStructure.SendMessage("SetTile", this);
        hasStructure = true;
    }

    public GameObject GetStructure()
    {
        return sandStructure;
    }

    public void SpawnStructure(GameObject structurePrefab)
    {
        if (!hasStructure)
        {
            if(sandStructure != null)
            {
                Destroy(sandStructure);
                sandStructure = null;
            }
            hasStructure = true;
            sandStructure = (GameObject)Instantiate(structurePrefab, transform.position, new Quaternion());
            sandStructure.SendMessage("SetTile",this);
        }
    }

    public bool HasStructure()
    {
        return hasStructure;
    }

    public void StructureDead()
    {
        Debug.Log("Structure dead");
        //Destroy(sandStructure);
        
        foreach (Transform child in sandStructure.transform)
        {
            if(child.gameObject.activeSelf)
            {
                Debug.Log("active" + child.gameObject.name);
                child.GetComponent<CapsuleCollider>().enabled = false;
            }
        }
        //sandStructure = null;
        hasStructure = false;
    }
    

}
