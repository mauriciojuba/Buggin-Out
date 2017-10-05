using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour {

    [SerializeField] GameObject[] players;
    Transform DollyCam;
    public int numPlayers = 0;
    [SerializeField] float velocidadeMovimento;
    public float distancia = 15;
    [SerializeField] float offSetY;
    //[SerializeField] float minDistancia;
    //[SerializeField] float velocidadeZoom;
    public Vector3 tgtvelocity;
    Vector3 tgtlastpos;
    bool alguemFora;
    Vector3 vel = Vector3.zero;

    public bool playerOnScreen;

    public Transform target;

	[SerializeField] private Image Fader;

	public List<GameObject> StunnedPlayers;

    public string LevelName;

    private void Start()
    {
        DollyCam = transform;
        ChecarQuantidadePlayers();
        DollyCam.position = posicionaCamera(CalculaCamTarget(numPlayers));
    }

    private void LateUpdate()
    {

        if (!playerOnScreen) {
            DollyCam.position = Vector3.Lerp(DollyCam.position, posicionaCamera(CalculaCamTarget(numPlayers)) + tgtvelocity * 2, (velocidadeMovimento / 10) * Time.smoothDeltaTime);
            target.position = CalculaCamTarget(numPlayers);
        }
        //ControlaBordaTela();
        tgtvelocity = target.position - tgtlastpos;
        tgtlastpos = target.position;

    }
    private void FixedUpdate()
    {
        

    }
    private void Update()
    {
		ResetGame ();
//        for (int i = 0; i < players.Length; i++)
//        {
//            if (players[i] != null|| players[i].GetComponent<Movimentacao3D>() != null )
//            {
//                if (players[i].GetComponent<Movimentacao3D>().onScreen == true || players[i].GetComponent<Movimentacao3D>().toWorld == true)
//                {
//                    playerOnScreen = true;
//                }
//                else
//                {
//                    playerOnScreen = false;
//                }
//            }
//        }
        
    }

    void ChecarQuantidadePlayers()
    {
        //alterar a tag quando não for mais necessário o "3D" no final
        for (int i = 0; i == numPlayers; i++)
        {
            //Debug.Log("entrou");
            if (GameObject.FindWithTag("Player"+(i+1)+"_3D") != null)
            {
                Array.Resize(ref players, i + 1);
                players[i] = GameObject.FindWithTag("Player"+(i+1)+"_3D");
                numPlayers++;
            }
            else {
                break;
            }
        }
    }

	public void ChecarNaTela(){
		for (int i = 0; i < players.Length; i++) {
			if (players [i].GetComponent<HornControl> ().natela || players [i].GetComponent<HornControl> ().Going) {
				playerOnScreen = true;
			} else {
				playerOnScreen = false;
			}
		}
	}

    //marca o ponto entre os jogadores que a camera ficará olhando
    public Vector3 CalculaCamTarget(int numPlayers)
    {
        
            switch (numPlayers)
            {
                case 1:
                    return players[0].transform.position;
                case 2:
                    return Vector3.Lerp(players[0].transform.position, players[1].transform.position, 0.5f);
                case 3:
                    return Vector3.Lerp(Vector3.Lerp(players[0].transform.position, players[1].transform.position, 0.5f), players[2].transform.position, 0.33f);
                case 4:
                    return Vector3.Lerp(Vector3.Lerp(Vector3.Lerp(players[0].transform.position, players[1].transform.position, 0.5f), players[2].transform.position, 0.33f), players[3].transform.position, 0.25f);
            }
            return Vector3.one;
    }

    Vector3 posicionaCamera(Vector3 _target)
    {
        if (!playerOnScreen)
        {
            return new Vector3(_target.x, _target.y + distancia, _target.z - distancia + offSetY);
        }
        else
        {
            return DollyCam.position;
        }
    }

    
    /*void ControlaBordaTela()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if(players[i].GetComponent<Movimentacao3D>() != null || players[i] != null){
                if (players[i].GetComponent<Movimentacao3D>().onScreen == false)
                {
                    Vector3 testOffScreen = Camera.main.WorldToViewportPoint(players[i].transform.position);
                    if ((testOffScreen.x <= 0.1 || testOffScreen.x >= 0.9 || testOffScreen.y <= 0.1 || testOffScreen.y >= 0.9))
                    {
                        alguemFora = true;
                        if (distancia >= maxDistancia)
                        {
                            lockPlayerMovement(players[i].transform);
                        }
                    }
                }
            }
        }
        for (int j = 0; j < players.Length; j++)
        {
            Vector3 testOnScreen = Camera.main.WorldToViewportPoint(players[j].transform.position);
            if ((testOnScreen.x > 0.1 && testOnScreen.x < 0.9 && testOnScreen.y > 0.1 && testOnScreen.y < 0.9) && !alguemFora )
            {
                if (players[j].GetComponent<Movimentacao3D>() != null || players[j] != null)
                {
                    if (players[j].GetComponent<Movimentacao3D>().InMovement)
                    {
                        distancia = Mathf.MoveTowards(distancia, minDistancia, velocidadeZoom * Time.deltaTime);
                        return;
                    }
                }
            }
            else if (players[j].GetComponent<Movimentacao3D>() != null || players[j] != null){
                if (players[j].GetComponent<Movimentacao3D>().onScreen == false)
                {
                    distancia = Mathf.MoveTowards(distancia, maxDistancia, velocidadeZoom * Time.deltaTime);
                    alguemFora = false;
                    return;
                }
            }
        }

    }*/
    
	void ResetGame(){
		if (StunnedPlayers.Count == players.Length) {

			Color c = Fader.color;
			c.a += Time.deltaTime / 5;
			Fader.color = c;
			//colocar o nome da cena aqui--------------------------\|/
			if (c.a >= 1) {
				UnityEngine.SceneManagement.SceneManager.LoadScene (LevelName);
			}
		}
	}

    void lockPlayerMovement(Transform t)
    {
	// travar o movimento do player
	}
}

