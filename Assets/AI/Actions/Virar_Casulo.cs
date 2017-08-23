using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class Virar_Casulo : RAINAction
{
    bool casulo;
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
        casulo = ai.WorkingMemory.GetItem<bool>("Casulo");
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        casulo = true;
        ai.WorkingMemory.SetItem("Casulo", casulo);
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}