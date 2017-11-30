using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour {

    [SerializeField] bool IsAnimated,DecreaseSize;
    [SerializeField] float Timer;
    [SerializeField] float DamagePerSecond;
    [SerializeField] float Damp;
    [SerializeField] Vector3 LastSize;

	Transform particleEffect;
	Vector3 partLastSize;

	void Start(){
		if(transform.childCount>0){
		particleEffect = transform.GetChild(0); //está hardcoded como a primeira child do objeto, se houverem mais children é recomendável
		//alterar para produzir uma array ou list de children
			if (particleEffect != null) {
				partLastSize = particleEffect.localScale * (transform.localScale.sqrMagnitude / LastSize.sqrMagnitude);
			}
		}
	}

	// Update is called once per frame
	void Update () {
        if (IsAnimated)
        {
            if (!DecreaseSize) {
                transform.localScale = Vector3.Lerp(transform.localScale, LastSize, Time.deltaTime * Damp);
				if (particleEffect != null) {
					particleEffect.localScale = Vector3.Lerp (particleEffect.localScale, partLastSize, Time.deltaTime * Damp);
				}
            }
            else
            {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0, 0, 0), Time.deltaTime * Damp);
				if (particleEffect != null) {
					particleEffect.localScale = Vector3.Lerp (particleEffect.localScale, new Vector3 (0, 0, 0), Time.deltaTime * Damp);
				}
            }

            if(transform.localScale.x <= 0.01f)
            {
                Destroy(gameObject);
            }
            if (transform.localScale.x >= LastSize.x -0.01 && transform.localScale.x <= LastSize.x) {
                Timer += Time.deltaTime;
                if(Timer >= 3)
                {
                    DecreaseSize = true;
                }
            }
        }
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<PlayerLife>() != null)
        {
            //criar uma função igual a do player no script de vida do mosquito
            other.GetComponent<PlayerLife>().DamagePerSecond(DamagePerSecond);
        }
        if(other.GetComponent<EnemyIA>() != null)
        {
            other.GetComponent<EnemyIA>().DamagePerSecond(DamagePerSecond);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerLife>() != null)
        {
            //criar uma variavel de tempo pro script de vida do mosquito
            other.GetComponent<PlayerLife>().TimeInPoison = 0;
        }
        if (other.GetComponent<EnemyIA>() != null)
        {
            other.GetComponent<EnemyIA>().TimeInPoison = 0;
        }
    }
}
