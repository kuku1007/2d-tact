using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseActionST : IState
{
    public ChooseActionST(UIInputSystem sm) : base(sm) {}
    public override void onUIClick(string name)
    {
        switch(name){
            case "attack":
            sm.SetState(new AttackST(sm));
            break;

            case "defend":
            sm.SetState(new DefendST(sm));
            break;

            case "move":
            sm.SetState(new MoveST(sm));
            break;
        }
    }
}
