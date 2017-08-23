using UnityEngine;

[CreateAssetMenu(menuName = "Character/Special")]
public class SpecialSkill : ScriptableObject
{
	public bool Rush;
	#region Rush Skill
	public float RushDistance;
	public float RushDamage;
	public float RushForce;
	public float RushPushForce;
	#endregion

	public bool Magic;
	#region Magic Skill
	public Object MagicPrefab;
	public enum MagicStyle {Projectile, AOE}
	public MagicStyle MyStyle;

	#region Projectile
	public int ProjectileQuant;
	public float ProjectileForce;
	public float ProjectileDamage;
	#endregion

	#region AOE
	public float AOEDistance;
	public float AOEDamage;
	public float AOEForce;
	public float AOEPushForce;
	public bool Pull;
	public bool Push;
	#endregion
	#endregion

	public string ButtonToUse;
}
