using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/New Character")]
public class Character : ScriptableObject {

	public string CharacterName = "New Name";
	public GameObject CharacterModel;
	public int PlayerNumber;
	public float Life;
    public GameObject LifeEmblem;
	public SpecialSkill Special;
}