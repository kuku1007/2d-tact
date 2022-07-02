using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendST : IState
{
    public DefendST(UIInputSystem sm) : base(sm) {}
    public override void onClick(Vector2 position) {
        Debug.Log("defending");
        // attack other char
        sm.SetState(new ChooseCharacterST(sm, null)); // TODO
    }

}
