using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveST : IState
{
    public MoveST(UIInputSystem sm) : base(sm) {}
    public override void onClick(Vector2 position) {
        Debug.Log("moving character");
        sm.GameCH.RaiseMove(position);
        sm.SetState(new ChooseCharacterST(sm, null)); // TODO
    }
}
