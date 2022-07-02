using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeST : IState
{
    private TurnContext turnContext;

    public InitializeST(UIInputSystem sm, TurnContext turnContext) : base(sm) {
        this.turnContext = turnContext;
    }

    public override void onClick(Vector2 position) {
        sm.GameCH.RaiseInit();
    }

    public override void onContextChanged()
    {
       sm.SetState(new ChooseCharacterST(sm));
    }
}
