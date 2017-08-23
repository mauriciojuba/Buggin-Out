using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Mask))]
[RequireComponent(typeof(ScrollRect))]
public class SelectCharacter : MonoBehaviour {
	public Character[] SelectableCharacter;
	public Data DataS;
	public bool SelectedCharacter;
	public DetectJoysticks DetectS;
	//pagina inicial.
	public int StartingPage = 0;
	[Header("Tempo de desaceleração da pagina")]
	[Tooltip("Valor padrão em 20, que eu achei uma velocidade boa")]
	[SerializeField] private float decelerationRate ;


	private ScrollRect ScrollRectComponent;
	private RectTransform ScrollRectRect;
	private RectTransform Container;

	private int PageCount;
	public int CurrentPage;
	public int PlayerNumber;

	private List<Vector2> PagePositions = new List<Vector2>();

	[Header("As Paginas Vem Na Horizontal?")]
	[SerializeField]private bool Horizontal;

	private bool _Lerp;
	private Vector2 _LerpTo;

	private float TimerMax = 3,Timer;

	void Start () {
		if (GameObject.FindWithTag ("DATA") != null)
			DataS = GameObject.FindWithTag ("DATA").GetComponent<Data> ();

		Timer = TimerMax;

		ScrollRectComponent = GetComponent<ScrollRect>();
		ScrollRectRect = GetComponent<RectTransform>();
		Container = ScrollRectComponent.content;
		PageCount = Container.childCount;

		//define se o movimento das paginas vai ser na horizontal ou não
		if (Horizontal) {
			ScrollRectComponent.horizontal = true;
			ScrollRectComponent.vertical = false;
		} else {
			ScrollRectComponent.horizontal = false;
			ScrollRectComponent.vertical = true;
		}

		_Lerp = false;

		//faz as primeiras definições.
		SetPagePositions();
		SetPage(StartingPage);
	}
	
	void Update() {
		DetectJoy ();
		if (!SelectedCharacter) {
			//Se esta em movimento
			if (_Lerp) {
				// previne de ultrapassar valores maiores que 1.
				float decelerate = Mathf.Min (decelerationRate * Time.deltaTime, 1f);
				Container.anchoredPosition = Vector2.Lerp (Container.anchoredPosition, _LerpTo, decelerate);
				// parar de mover
				if (Vector2.SqrMagnitude (Container.anchoredPosition - _LerpTo) < 0.25f) {
					// Move para o destino e para de se mover
					Container.anchoredPosition = _LerpTo;
					_Lerp = false;
					// para totalmente qualquer movimento no scrollrect
					ScrollRectComponent.velocity = Vector2.zero;
				}
			}

			//se o movimento nao for definido para horizontal, usa o axis vertical, caso contrario usa o axis horizontal.
			//coloquei essa função para fazermos testes se é melhor verticalmente ou horizontalmente.
			if (!Horizontal && !_Lerp) {
				if (Input.GetAxis ("Vertical P" + PlayerNumber) > 0.2f) {
					PreviousScreen ();
				}
				if (Input.GetAxis ("Vertical P" + PlayerNumber) < -0.2f) {
					NextScreen ();
				}
			} else if (Horizontal && !_Lerp) {
				if (Input.GetAxis ("Horizontal P" + PlayerNumber) > 0.2f) {
					PreviousScreen ();
				} else if (Input.GetAxis ("Horizontal P" + PlayerNumber) < -0.2f) {
					NextScreen ();
				}
			}

			if (Input.GetButtonDown ("A P" + PlayerNumber)) {
				if (Container.GetChild (CurrentPage).gameObject.GetComponent<Button> ().interactable) {
					OnSelectCharacter (PlayerNumber);
					DetectS.QuantSelected++;
					SelectedCharacter = true;				
				}
			}
		} else {
			if (Input.GetButtonDown ("B P" + PlayerNumber)) {
				OnDeselectCharacter (PlayerNumber);
				DetectS.QuantSelected--;
				SelectedCharacter = false;
			}
		}

		if (DetectS.QuantSelected == DetectS.ActiveJoy) {
			Timer -= Time.deltaTime;
			if (Timer <= 0)
				StartGame ("DU");
		} else
			Timer = TimerMax;

	}

	//define a posição das paginas de personagens.
	private void SetPagePositions() {
		int width = 0;
		int height = 0;
		int offsetX = 0;
		int offsetY = 0;
		int containerWidth = 0;
		int containerHeight = 0;

		if (Horizontal) {
			// largura da tela em pixeis da janela de scroll.
			width = (int)ScrollRectRect.rect.width;
			// posição central de todas as paginas.
			offsetX = width / 2;
			// largura total.
			containerWidth = width * PageCount;
		} else {
			// Altura da tela em pixeis da janela de scroll.
			height = (int)ScrollRectRect.rect.height;
			// posição central de todas as paginas.
			offsetY = height / 2;
			// Altura total.
			containerHeight = height * PageCount;
		}

		// define a largura do container(onde ficam as imagens dos personagens)
		Vector2 newSize = new Vector2(containerWidth, containerHeight);
		Container.sizeDelta = newSize;
		Vector2 newPosition = new Vector2(containerWidth / 2, containerHeight / 2);
		Container.anchoredPosition = newPosition;

		// Deleta qualquer definição anterior.
		PagePositions.Clear();

		// Define as posiçoes de cada "child" do container.
		for (int i = 0; i < PageCount; i++) {
			RectTransform child = Container.GetChild(i).GetComponent<RectTransform>();
			Vector2 childPosition;
			if (Horizontal) {
				childPosition = new Vector2(i * width - containerWidth / 2 + offsetX, 0f);
			} else {
				childPosition = new Vector2(0f, -(i * height - containerHeight / 2 + offsetY));
			}
			child.anchoredPosition = childPosition;
			PagePositions.Add(-childPosition);
		}
	}

	//Define qual pagina esta mostrando
	private void SetPage(int aPageIndex) {
		aPageIndex = Mathf.Clamp(aPageIndex, 0, PageCount - 1);
		Container.anchoredPosition = PagePositions[aPageIndex];
		CurrentPage = aPageIndex;
	}

	//Proxima Pagina, se caso a pagina for a ultima, volta pra primeira.
	private void NextScreen() {
		if (CurrentPage >= PageCount - 1)
			LerpToPage (0);
		else
			LerpToPage(CurrentPage + 1);
	}

	//Pagina Anterior, se caso a pagina for a primeira, vai pra ultima.
	private void PreviousScreen() {
		if (CurrentPage <= 0)
			LerpToPage (PageCount - 1);
		else
			LerpToPage(CurrentPage - 1);
	}

	//Define o movimento de qual pagina e para qual pagina.
	private void LerpToPage(int aPageIndex) {
		aPageIndex = Mathf.Clamp(aPageIndex, 0, PageCount - 1);
		_LerpTo = PagePositions[aPageIndex];
		_Lerp = true;
		CurrentPage = aPageIndex;
	}

	public void OnSelectCharacter(int PlayerNumb){
		if (PlayerNumb == 1) {
			DataS.P1SelectedCharacter = SelectableCharacter [CurrentPage];
			DataS.P1SelectedCharacter.PlayerNumber = PlayerNumb;
		}
		else if(PlayerNumb == 2){
			DataS.P2SelectedCharacter = SelectableCharacter [CurrentPage];
			DataS.P2SelectedCharacter.PlayerNumber = PlayerNumb;
				
		}
	}

	public void OnDeselectCharacter(int PlayerNumb){
		if (PlayerNumb == 1)
			DataS.P1SelectedCharacter = null;
		else if(PlayerNumb == 2)
			DataS.P2SelectedCharacter = null;
	}

	void DetectJoy(){
		if (DetectS.P1Active && DetectS.P2Active && DetectS.P3Active && DetectS.P4Active) {
			DetectS.ActiveJoy = 4;
		}else if(DetectS.P1Active && DetectS.P2Active && DetectS.P3Active){
			DetectS.ActiveJoy = 3;
		}else if(DetectS.P1Active && DetectS.P2Active){
			DetectS.ActiveJoy = 2;
		}else if(DetectS.P1Active){
			DetectS.ActiveJoy = 1;
		}
	}

	public void StartGame(string PhaseName){
		UnityEngine.SceneManagement.SceneManager.LoadScene (PhaseName);
	}
}
