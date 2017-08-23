using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class ataque2DLagartixa : RAINAction
{
    public GameObject Lagartixa2D;
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
        Lagartixa2D = ai.WorkingMemory.GetItem<GameObject>("Lagartixa2D");
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        Lagartixa2D.SetActive(true);
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}