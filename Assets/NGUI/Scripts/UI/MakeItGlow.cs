using UnityEngine;
using System.Collections;

public class MakeItGlow : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
	}
	
    
	// Update is called once per frame
   

	public void Glow () {


        if (gameObject.tag == ("shovelbutton"))
        {
            if (gameObject.activeInHierarchy)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }

        if (gameObject.tag == ("rocksbutton"))
        {
            if (gameObject.activeInHierarchy)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
        if (gameObject.tag == ("seaweedbutton"))
        {
           
            if (gameObject.activeInHierarchy)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }

        }

      


    }
}
