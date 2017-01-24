using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Sceneloader : MonoBehaviour {
    
	// Use this for initialization
	public void Load () {
        SceneManager.LoadScene("Game");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
