using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TesteTV : MonoBehaviour {

    public Text _TextGraphics;
    public int QualitySelected;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        QualitySelected = QualitySettings.GetQualityLevel();
        _TextGraphics.text = QualitySettings.names[QualitySelected];
    }
}
