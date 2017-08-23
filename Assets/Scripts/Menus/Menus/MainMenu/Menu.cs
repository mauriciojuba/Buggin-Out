using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CanvasGroup))]
public class Menu : MonoBehaviour {

	private Animator Animator;
	private CanvasGroup _CanvasGroup;

	//booleana para definir se o menu esta aberto, para ativar a animação de abrir.
	public bool IsOpen{
		get{ return Animator.GetBool ("IsOpen"); }
		set{ Animator.SetBool ("IsOpen", value); }
	}

	public void Awake(){
		Animator = GetComponent<Animator> ();
		_CanvasGroup = GetComponent<CanvasGroup> ();
	}

	public void Update(){
		//ativa a animção de um menu para abrir, e de outro menu para fechar.
		if (!Animator.GetCurrentAnimatorStateInfo (0).IsName ("Open")) {
			_CanvasGroup.blocksRaycasts = _CanvasGroup.interactable = false;
		} else
			_CanvasGroup.blocksRaycasts = _CanvasGroup.interactable = true;
	}
}
