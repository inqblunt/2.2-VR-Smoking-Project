using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	// Use this for initialization
	void LoadByIndex(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
	}

}
