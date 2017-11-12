using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetActualScene : MonoBehaviour {

	void Start () {
        GameObject.FindWithTag("Loading").GetComponent<Loading>().currentScene = SceneManager.GetActiveScene();

    }
}
