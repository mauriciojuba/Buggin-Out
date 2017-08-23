using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePos : MonoBehaviour {

	public float X,Y,Z;
	public float Speed;
	public int PlayerNumber;
    public float ScaleToSet;
    public float MultiplierScaleSpeed;
    private bool SetS;
	public bool Tatuzinho;
	public GameObject Player;

	void Start () {
		if (PlayerNumber == 1 || PlayerNumber == 2) {
			if (Tatuzinho)
				Y = 0.9f;
			else
				Y = 0.87f;
		}

	}

    // Update is called once per frame
    void Update()
    {

		if (!Tatuzinho) {
			if (PlayerNumber == 1)
				X = Player.GetComponent<Life> ().X + 0.007f;
			else if (PlayerNumber == 2)
				X = Player.GetComponent<Life> ().X;
		}
        if (!SetS)
        {
			if (transform.position != Camera.main.ViewportToWorldPoint (new Vector3 (X, Y, Z))) {
				transform.position = Vector3.MoveTowards (transform.position, Camera.main.ViewportToWorldPoint (new Vector3 (X, Y, Z)), Speed * Time.deltaTime);
				transform.localEulerAngles = Vector3.Lerp (transform.localEulerAngles, new Vector3 (90, 0, 0), Speed * Time.deltaTime * 5);
			} else {
				if (!Tatuzinho) {
					if (GetComponent<RecoveryItem> () != null)
						GetComponent<RecoveryItem> ().PlusLife ();
					Destroy (this);
				}
			}

            if (transform.localScale != new Vector3(ScaleToSet, ScaleToSet, ScaleToSet))
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(ScaleToSet, ScaleToSet, ScaleToSet), Speed * Time.deltaTime * MultiplierScaleSpeed);
            }
        }
        
    }

	public IEnumerator SetScale(){
		yield return new WaitForSeconds (0.2f);
		SetS = true;
	}
}
