using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonsHandler : MonoBehaviour
{


    public GameObject bucketBase;
    public GameObject bucket1;
    public GameObject bucket2;
    public GameObject bucket3;
    public GameObject bucket4;

    public GameObject rockBase;
    public GameObject rock1;
    public GameObject rock2;
    public GameObject rock3;

    public GameObject seaBase;
    public GameObject sea1;
    public GameObject sea2;
    public GameObject sea3;



    private int rockValue;
    private int seaValue;
    private int bucketValue;

    void Awake()
    {
        bucketValue = 0;
        rockValue = 0;
        seaValue = 0;
        ChangeBucketAmount(0);
        ChangeRockAmount(0);
        ChangeSeaAmount(0);
    }

    public void ChangeRockAmount(int amount)
    {
        rockValue += amount;
        setInactive("rock");
        if (rockValue == 0)
        {
            rockBase.SetActive(true);
        }
        else if (rockValue == 1)
        {
            rock1.SetActive(true);
        }
        else if (rockValue == 2)
        {
            rock2.SetActive(true);
        }
        else if (rockValue >= 3)
        {
            rock3.SetActive(true);
        }
    }

    public void ChangeSeaAmount(int amount)
    {

        seaValue += amount;
        setInactive("sea");
        if (seaValue == 0)
        {
            seaBase.SetActive(true);
        }
        else if (seaValue == 1)
        {
            sea1.SetActive(true);
        }
        else if (seaValue == 2)
        {
            sea2.SetActive(true);
        }
        else if (seaValue >= 3)
        {
            sea3.SetActive(true);
        }
    }

    public void ChangeBucketAmount(int amount)
    {

        bucketValue += amount;
        setInactive("bucket");
        if (bucketValue == 0)
        {
            bucketBase.SetActive(true);
        }
        else if (bucketValue == 1)
        {
            bucket1.SetActive(true);
        }
        else if (bucketValue == 2)
        {
            bucket2.SetActive(true);
        }
        else if (bucketValue == 3)
        {
            bucket3.SetActive(true);
        }
        else if (bucketValue >= 4)
        {
            bucket4.SetActive(true);
        }
    }

    private void setInactive(string type)
    {
        if (type == "rock")
        {
            rockBase.SetActive(false);
            rock1.SetActive(false);
            rock2.SetActive(false);
            rock3.SetActive(false);
}
        else if (type == "sea")
        {
            seaBase.SetActive(false);
            sea1.SetActive(false);
            sea2.SetActive(false);
            sea3.SetActive(false);
        }
        else
        {
            bucketBase.SetActive(false);
            bucket1.SetActive(false);
            bucket2.SetActive(false);
            bucket3.SetActive(false);
            bucket4.SetActive(false);
        }
    }


}
