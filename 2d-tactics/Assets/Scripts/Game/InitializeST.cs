using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeST : State
{
    public InitializeST(BattleSystem sm) : base(sm) {}

    public override void onClick(Vector2 position) {
        sm.GameCH.RaiseInit();
        sm.SetState(new ChooseCharacterST(sm));
    }
}
