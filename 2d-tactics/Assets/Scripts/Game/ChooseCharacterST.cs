using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCharacterST : State
{
    public ChooseCharacterST(BattleSystem sm) : base(sm) {}
    
    public override void onClick(Vector2 position) {
        Debug.Log("char choosen");
        sm.SetState(new ChooseActionST(sm));
        //check if clicked on character and send event characterSelected
        //sm.SetState(new ChooseActionST(this.sm));
    }
}
