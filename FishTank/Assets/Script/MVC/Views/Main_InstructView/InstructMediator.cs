using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using strange.extensions.mediation.impl;

public class InstructMediator : EventMediator
{
    [Inject]
    public InstructView View_InstructView { get; set; }

    public override void OnRegister()
    {
        Debug.Log("ClourMediator is OnRegister");
    }

    public override void OnRemove()
    {
        Debug.Log("ClourMediator is OnRemove");
    }

}
