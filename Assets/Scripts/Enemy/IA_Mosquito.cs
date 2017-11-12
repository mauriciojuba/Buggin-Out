using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Mosquito : EnemyIA {

	public GameObject[] hitbox;
	GameObject LifeEmblem;
	public float LifeDist = 0.4f;
	float initialTargetLife;
	Transform Parent;

    //variavel pra trocar os colliders quando morre só pro mosquito ir pro chão
    public Collider NormalCollider, DeathCollider;


    //variaveis Particula lifedrain
    private GameObject part;
    public GameObject ParticulaLifeDrain;
    public Transform rootJoint;
    private bool StartedParticle;

    public override void Start ()
	{
		base.Start ();
		LifeEmblem = GameObject.Find ("LifeEmblem");
		onScreenScale = new Vector3(2f,2f,2f);
		Parent = transform.parent;
	}



    #region Override EnemyIA

    public override void TakeDamage()
    {
        if (hitted)
        {
            if (playerStr == 0)
                playerStr = 50;

            Life -= playerStr;
            hitted = false;
        }
        if (!onScreen)
        {
            if (_anim != null)
                _anim.SetBool("FightingWalk", false);
            if (Life > 0 && Life <= lifeMax * 0.2f)
            {
                ActualState = State.Flee;
            }
            else if (Life <= 0) ActualState = State.Dead;
        }
        else
        {
            if (_anim != null)
            {
                _anim.SetTrigger("TakeDamageScreen");
                _anim.SetBool("LifeDrain", false);
            }

            //particulas
            /*if (part != null) {
				ParticleSystem particleemitter = part.GetComponent<ParticleSystem> ();
				if (particleemitter != null) {
					ParticleSystem.EmissionModule emit = particleemitter.emission;
					emit.enabled = false;
				}
				Destroy(part, 5f);
			}*/

            //if (Life > 0) {
            ActualState = State.GoingToWorld;
            //} else {
            //	ActualState = State.Dead;
            //}
        }
        //hitted = false;
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Take Damage") && _anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f && Life > 0)
        {
            ActualState = State.Chase;
        }
    }

    public override void OnScreenIdle ()
	{
		_anim.SetBool("GoingToScreen", false);
		ActualState = State.OnScreenChase;
	}
	public override void OnScreenChase ()
	{
        LifeEmblem = Target.GetComponent<PlayerLife>().LifeInGame;
		RB.isKinematic = true;
		LifeDist = Vector3.Distance(transform.position, LifeEmblem.transform.position);
		Debug.Log (LifeDist);
		if (LifeDist > 0.2f) {
			LifeEmblemChase ();
		} else {
			RB.velocity = Vector3.zero;
			_anim.SetBool("walkScreen", false);
			initialTargetLife = Target.GetComponent<PlayerLife> ().LifeAtual;
			ActualState = State.OnScreenAttack;
		}

	}
	public override void OnScreenAttack ()
	{
		float targetLife;
		targetLife = Target.GetComponent<PlayerLife> ().LifeAtual;
        if (!StartedParticle)
        {
            DrainLifeStart();
        }

        if (targetLife >= initialTargetLife - onScreenAtkStr) {
			_anim.SetBool("LifeDrain", true);
			targetLife -= Time.fixedDeltaTime*10;
            Life += Time.fixedDeltaTime * 10;
			Target.GetComponent<PlayerLife> ().LifeAtual = targetLife;

		} else {
			_anim.SetBool("LifeDrain", false);
			ActualState = State.GoingToWorld;
            DrainLifeEnd();

        }
	}

	public override void DownToGround ()
	{
        _anim.SetBool("GoingToWorld", true);
        if (Screen.GoOffScreen (worldPos, gameObject, Parent)) {
			RB.isKinematic = false;
			RB.useGravity = true;
			onScreen = false;
			ActualState = State.Idle;
			_anim.SetBool("GoingToWorld", false);
			_anim.SetBool("UsingWings", true);
		}
	}

    //trocar os colliders quando morre e ativar animação.
    public override void Die()
    {
        _anim.SetTrigger("Death");
        _anim.SetBool("UsingWings", false);
        NormalCollider.enabled = false;
        DeathCollider.enabled = true;
        if(_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
        Destroy(gameObject, 3f);
    }
    #endregion

    #region Ataques Mosquito

    public void LifeEmblemChase(){
		Vector3 mov;
		mov = new Vector3 (LifeEmblem.transform.position.x - transform.position.x, LifeEmblem.transform.position.y - transform.position.y, 0);
		mov = Camera.main.transform.TransformVector (mov);
		_anim.SetBool("walkScreen", true);
		transform.Translate (mov * transform.localScale.magnitude * Time.deltaTime * screenSpeed, Space.World);
		transform.LookAt (Camera.main.transform, transform.up + mov);
	}

	public void HitBoxOn()
	{
		if (!onScreen)
			hitbox[0].GetComponent<Collider>().enabled = true;

		else
			hitbox[1].GetComponent<Collider>().enabled = true;
	}

	public void HitBoxOff()
	{
		if (!onScreen)
			hitbox[0].GetComponent<Collider>().enabled = false;

		else
			hitbox[1].GetComponent<Collider>().enabled = false;
	}

    #endregion

    #region Particle LifeDrain (arruma depois no lugar certo)
    public void DrainLifeStart()
    {
        part = Instantiate(ParticulaLifeDrain, LifeEmblem.transform.position, Quaternion.identity) as GameObject;
        part.transform.parent = LifeEmblem.transform;
        part.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        part.GetComponent<ParticleHoming>().target = rootJoint;

        part.SetActive(true);
        StartedParticle = true;
    }

    public void DrainLifeEnd()
    {
        ParticleSystem particleemitter = part.GetComponent<ParticleSystem>();
        if (particleemitter != null)
        {
            ParticleSystem.EmissionModule emit = particleemitter.emission;
            emit.enabled = false;
        }
        Destroy(part, 5f);
    }
    #endregion
}
