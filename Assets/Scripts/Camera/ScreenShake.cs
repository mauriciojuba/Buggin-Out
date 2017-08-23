using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class ScreenShake : MonoBehaviour {

	public float _amplitude = 0.1f;
    float _blurTransition;
    private Vector3 initialPosition;
    private bool isShaking = false;
    private bool isBlur = false;
    public static ScreenShake Instance;
    public PostProcessingProfile ppProfile;

	void Start () {
        initialPosition = transform.localPosition;
        Instance = this;
        var focalLenght = ppProfile.depthOfField.settings;
        focalLenght.focalLength = 40;
        ppProfile.depthOfField.settings = focalLenght;

    }
    
    public void Shake(float amplitude, float duration)
    {
		initialPosition = transform.localPosition;
        amplitude = _amplitude;
        isShaking = true;
        CancelInvoke("StopShaking");
        Invoke("StopShaking", duration);
    }
    public void Blur()
    {
        _blurTransition = 20;
        isBlur = true;
        //CancelInvoke("ClearBlur");
        //Invoke("ClearBlur", 1f);
    }
    public void StopShaking()
    {
        isShaking = false;
    }
    public void ClearBlur(){
        isBlur = false;
    }

    void Update () {
        if (isShaking)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * _amplitude;
        }

        if(isBlur){
            var focalLenght = ppProfile.depthOfField.settings;
            if(focalLenght.focalLength <= 80){
                focalLenght.focalLength += Time.deltaTime * _blurTransition * 8;
                ppProfile.depthOfField.settings = focalLenght;
            }
            else isBlur = false;
        }
        else{
            var focalLenght = ppProfile.depthOfField.settings;
            if(focalLenght.focalLength >= 40){
                focalLenght.focalLength -= Time.deltaTime * _blurTransition / 2;
                ppProfile.depthOfField.settings = focalLenght;
            }
        }

//        if (Input.GetKeyDown(KeyCode.T))
//        {
//            Shake(0.2f,0.5f);
//        }
//        if (Input.GetKeyDown(KeyCode.Y))
//        {
//            Blur();
//            
//        }
		
	
	}
}
