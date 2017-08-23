using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CursorControl : MonoBehaviour {
	[Header("Objeto que esta com a Imagem do cursor")]
	[SerializeField] private GameObject Spriter;
	 private Vector2 Movement;
	[Header("Velocidade do cursor")]
	[SerializeField] private float Speed;

	[Tooltip("Objeto que esta com o rigidbody do cursor")]
	[SerializeField] private Rigidbody2D Rb;
	[Tooltip("Coloque o Canvas aqui")]
	[SerializeField] private Canvas CanvasC;
	private Button SelectedButton;
	private Toggle SelectedToggle;
	private Slider SelectedSlider;
	void Start () {
		
	}
	
	void Update(){
		//ativa a função do botão quando for clicado.
		if (SelectedButton != null) {
			if (Input.GetButtonDown ("A P1") && SelectedButton.interactable) {
				StartCoroutine (ClickButton ());
			}
		} else if (SelectedToggle != null) {
			if (Input.GetButtonDown ("A P1") && SelectedToggle.interactable) {
				SelectedToggle.animator.SetTrigger ("Pressed");
				SelectedToggle.isOn = !SelectedToggle.isOn;
				SelectedToggle.onValueChanged.Invoke (SelectedToggle.isOn);
			}
		} 
	}

	void FixedUpdate () {
		Moviment ();
		TestPosition ();
	}


	void Moviment(){
	//Define a direção no eixo Y
	float InputY = Input.GetAxisRaw("Vertical P1");
	//Define a direção no eixo X
	float InputX = Input.GetAxisRaw ("Horizontal P1");

	//Define pra qual lado o sprite vai estar virado
		if (InputX > 0)
			Spriter.transform.localScale = new Vector3 (1, 1, 1);
	else if (InputX < 0)
			Spriter.transform.localScale = new Vector3 (-1, 1, 1);

	//Registra a direção que o personagem vai se mover
	Movement = new Vector2 (InputX * Speed, InputY * Speed);

	//Move o personagem na direção indicada.
	Rb.velocity = Movement;

	}
		
	//testa a posição do personagem em relação a camera
	void TestPosition(){
		//Calcula a distancia entre o personagem e a camera.
		//var DistanceZ = (transform.position - Camera.main.transform.position).z;

		//Calcula o ponto maximo de movimentação pra esquerda.
		var Leftborder = 10;

		//Calcula o ponto maximo de movimentação pra direita.
		var Rightborder = CanvasC.GetComponent<RectTransform>().sizeDelta.x - 10;

		//Calcula o ponto maximo de movimentação pra Baixo.
		var Bottomborder = 10;

		//Calcula o ponto maximo de movimentação pra Cima.
		var Topborder = CanvasC.GetComponent<RectTransform>().sizeDelta.y - 10;

		//Mantem o personagem sempre dentro do espaço da camera.
		gameObject.GetComponent<RectTransform>().position = new Vector3 (
			Mathf.Clamp (gameObject.GetComponent<RectTransform>().position.x, Leftborder, Rightborder),
			Mathf.Clamp (gameObject.GetComponent<RectTransform>().position.y, Bottomborder, Topborder),
			transform.position.z);
	}

	//Quando o "mouse" colide com algum botao, pega o botao e armazena em uma variavel, caso ele for interagivel.
	void OnTriggerEnter2D (Collider2D Col){
		if (Col.gameObject.GetComponent<Button> () != null) {
			SelectedButton = Col.gameObject.GetComponent<Button> ();
			if (SelectedButton.interactable) {
				SelectedButton.animator.SetTrigger ("Highlighted");
			}
		} else if (Col.gameObject.GetComponent<Toggle> () != null) {
			SelectedToggle = Col.gameObject.GetComponent<Toggle> ();
			if (SelectedToggle.interactable) {
				SelectedToggle.animator.SetTrigger ("Highlighted");
			}
		} 
	}

	//Quando o "mouse" sai do botao, retira o botao da variavel, tornando ela vazia.
	void OnTriggerExit2D (Collider2D Col){
		if (SelectedButton != null) {
			if (SelectedButton.interactable) {
				SelectedButton.animator.SetTrigger ("Normal");
			}
			SelectedButton = null;
		}
		if (SelectedToggle != null) {
			if (SelectedToggle.interactable) {
				SelectedToggle.animator.SetTrigger ("Normal");
			
			}
			SelectedToggle = null;
		}
	}
		
	//Ativa a função que o botao tem, como selecionar a fase, selecionar personagem.
	//estou fazendo testes ainda.. por isso deixei o yield return como 0.
	IEnumerator ClickButton(){
		SelectedButton.animator.SetTrigger ("Pressed");
		yield return new WaitForSeconds (0.01f);
		SelectedButton.onClick.Invoke ();
	}

	//Torna o botao selecionado como nulo
	public void TurnNull(){
		StartCoroutine (Deselect());
	}

	IEnumerator Deselect(){
		yield return new WaitForSeconds (0.4f);
		SelectedButton = null;
	}
}
