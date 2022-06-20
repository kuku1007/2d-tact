using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseActionST : State
{
    private StateMachine sm;
    public ChooseActionST(StateMachine sm) {
        this.sm = sm;
    }

    public void onClick(Vector2 position) {
        Debug.Log("action choosen");
        // sm.SetState(next state depending on the action choosen);
    }
}
