using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCharacterST : State
{
    private StateMachine sm;

    public ChooseCharacterST(StateMachine sm) {
        this.sm = sm;
    }
    
    public void onClick(Vector2 position) {
        Debug.Log("char choosen");
        sm.SetState(new ChooseActionST(sm));
        //check if clicked on character and send event characterSelected
        //sm.SetState(new ChooseActionST(this.sm));
    }
}
