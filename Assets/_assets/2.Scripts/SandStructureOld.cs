//using UnityEngine;
//using System.Collections;


////public enum StructureType
////{
////    WALL_BASIC,
////    WALL_ROCK,
////    WALL_SEAWEED,

////    TOUR_BASIC,
////    TOUR_ROCK,
////    TOUR_SEAWEED
////}

//public class SandStructure : MonoBehaviour
//{

//    public GameObject model;

//    private SandTile sandTile;
//    public int maxLife;    
//    public int midLife;
//    public int lowLife;
//    public int rockNseaweedBonus;

//    private int life;
//    private bool isMidLife;
//    private bool isLowLife;

//    private StructureType type;




//    private void updateModel()
//    {

//        Destroy(model);
//        Debug.Log("mlqsdkfjqmlsdfkj");
//        if (isMidLife)
//        {
//            GameObject newObj = GameObject.Find("StructureModels").GetComponent<StructureModels>().GetNewModel(type.ToString() + "_MID", transform);
//            newObj.transform.position = transform.position;
//            newObj.transform.rotation = new Quaternion();
//            //model = Resources.Load<GameObject>("Structures/" + type.ToString() + "_MID") as GameObject;
//        }
//        else if (isLowLife)
//        {
//            GameObject.Find("StructureModels").GetComponent<StructureModels>().GetNewModel(type.ToString() + "_LOW", transform);
//            //Destroy(model);
//            //model = Resources.Load<GameObject>("Structures/" + type.ToString() + "_LOW") as GameObject;
//        }
//        else
//        {
//            GameObject.Find("StructureModels").GetComponent<StructureModels>().GetNewModel(type.ToString() + "_HIGH", transform);
//            //Destroy(model);
//            //model = Resources.Load<GameObject>("Structures/" + type.ToString() + "_HIGH") as GameObject;
//        }
//        //GameObject newModel = Instantiate(model, transform.position, new Quaternion()) as GameObject;
//        //GameObject newModel = Instantiate(model,transform) as GameObject;
//        //newModel.transform.position = transform.position;
//        //newModel.transform.rotation = new Quaternion();
//       // Destroy(model);
//        //model = newModel;
//        //model = Instantiate(model, transform.position, new Quaternion()) as GameObject;


//    }


//    void Awake()
//    {
//        life = maxLife;
//        isMidLife = false;
//        isLowLife = false;
//    }
    
    
//    public void SetType(StructureType type)
//    {
//        Debug.Log("hello");
//        if (this.type != type)
//        {
//            Debug.Log("gello");
//            this.type = type;
//            updateModel();

//            switch(type)
//            {
//                case StructureType.TOUR_ROCK:
//                case StructureType.TOUR_SEAWEED:
//                case StructureType.WALL_ROCK:
//                case StructureType.WALL_SEAWEED:
//                    //ChangeLife(rockNseaweedBonus);
//                    break;
//            }
//        }
//    }

//    public StructureType GetStructureType()
//    {
//        return type;
//    }

//    public void SetTile(SandTile tile)
//    {
//        sandTile = tile;

//    }

//    public void ChangeLife(int value)
//    {
//        life += value;


//        if (life < 0)
//        {
//            sandTile.StructureDead();
//        }

//        else if (life < midLife && !isMidLife && !isLowLife)
//        {
//            isMidLife = true;
//            updateModel();
//        }
//        else if (life < lowLife && !isLowLife)
//        {
//            isLowLife = true;
//            updateModel();
//        }
//    }



//}
