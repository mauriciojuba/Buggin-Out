using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class ataqueTrono : RAINAction
{
    
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
        
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        GameObject.Find("Collider_Dano_Sentado").GetComponent<BOSS_ataqueTrono>().EnableCollider();
        return ActionResult.SUCCESS;


    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}