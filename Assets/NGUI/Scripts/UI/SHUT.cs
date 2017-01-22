using UnityEngine;
using System.Collections;

public class SHUT : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    public void Shutdown()
    {
        gameObject.SetActive(false);
    }
}
