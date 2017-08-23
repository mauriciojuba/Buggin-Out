using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnMenu : MonoBehaviour {

	void ReturnToMenu(){
		SoundManager.StopSFX();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Creditos");
	}
}
