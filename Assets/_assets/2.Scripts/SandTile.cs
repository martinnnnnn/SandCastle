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

    public void SpawnStructure(GameObject structurePrefab)
    {
        if (!hasStructure)
        {
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
        sandStructure = null;
        hasStructure = false;
    }
    

}
