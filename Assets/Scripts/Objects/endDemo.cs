using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endDemo : MonoBehaviour {

	void OnTriggerEnter(Collider hit){
		if(hit.CompareTag("Player1_3D") || hit.CompareTag("Player2_3D")){
            if (GameObject.FindWithTag("Loading") != null)
                GameObject.FindWithTag("Loading").GetComponent<Loading>().StartCoroutine(GameObject.FindWithTag("Loading").GetComponent<Loading>().LoadAsync("Level 1B"));
            else
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Level 1B");
        }
	}
}
