using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifePos : MonoBehaviour {

	public float X,Y,Z;
	public int PlayerNumb;
	void Start () {
		if (PlayerNumb == 1) {
			X = 0.11f;
			Y = 0.81f;
			Z = 0.7f;
			transform.position = Camera.main.ViewportToWorldPoint (new Vector3 (X, Y, Z));
			transform.localRotation = Quaternion.Euler (new Vector3 (0.472f, 168.102f, -1.817f));
		} else if (PlayerNumb == 2) {
			X = 0.87f;
			Y = 0.81f;
			Z = 0.7f;
			transform.position = Camera.main.ViewportToWorldPoint (new Vector3(X,Y,Z));
			transform.localRotation = Quaternion.Euler (new Vector3 (10.822f, 199.747f, 5.799f));
		}
	}
}
