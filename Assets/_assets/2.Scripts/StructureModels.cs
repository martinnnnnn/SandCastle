using UnityEngine;
using System.Collections;



public class StructureModels : MonoBehaviour
{

    public GameObject[] models;

    public GameObject GetNewModel(string name, Transform parent)
    {

        for (int i = 0; i < models.Length; i++)
        {
            if (models[i].name == name)
            {
                return Instantiate(models[i], parent) as GameObject;
            }
        }

        return null;
    }
}
