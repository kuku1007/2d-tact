using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseActionST : IState
{
    public ChooseActionST(BattleSystem sm) : base(sm) {}

    public override void onClick(Vector2 position) {
        Debug.Log("action choosen");
        // sm.SetState(next state depending on the action choosen);
        // TODO: tmp move by default
        sm.GameCH.RaiseMove(position);
        sm.SetState(new ChooseCharacterST(sm));
    }
}
