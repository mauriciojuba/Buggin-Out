using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class casuloHit : RAINAction
{
    float vida;
    public override void Start(RAIN.Core.AI ai)
    {
        
        base.Start(ai);
        vida = ai.WorkingMemory.GetItem<float>("vida");
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        vida -= 10f;
        ai.WorkingMemory.SetItem("vida", vida);
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}