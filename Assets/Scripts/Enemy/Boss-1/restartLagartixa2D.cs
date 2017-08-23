using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restartLagartixa2D : MonoBehaviour {

    public Vector2 posInicial;
    void OnDisable(){
        transform.localPosition = posInicial;
    }
}
