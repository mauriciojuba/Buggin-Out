using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour {
    private void Start()
    {
        Cursor.visible = false;
    }

    public void ChangeScene(int SceneIndex){
		SceneManager.LoadScene(SceneIndex);
	}
}
