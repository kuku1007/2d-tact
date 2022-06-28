using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCharacterST : IState
{
    private Camera mainCamera;
    private string tagToFind;

    public ChooseCharacterST(BattleSystem sm) : base(sm) {
        this.mainCamera = sm.mainCamera;
        this.tagToFind = sm.tagOfCharacter;
    }
    
    public override void onClick(Vector2 position) {
        int charID = Util.checkClickedObject(mainCamera, position, tagToFind);
        Debug.Log("char choosen: " + charID);
        if(charID != -1) {
            sm.GameCH.RaiseCharacterSelected(charID);
            sm.SetState(new ChooseActionST(sm));
        }    
    }
}
