using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnMenu : MonoBehaviour {

	void ReturnToMenu(){

		UnityEngine.SceneManagement.SceneManager.LoadScene ("Creditos");
	}
}
