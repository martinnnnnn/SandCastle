using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Sceneloader : MonoBehaviour {
    
	// Use this for initialization
	public void Load () {
        SceneManager.LoadScene("test-martin1");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
