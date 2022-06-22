using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCharacterST : State
{
    private Camera mainCamera;
    private string tagToFind;

    public ChooseCharacterST(BattleSystem sm) : base(sm) {
        this.mainCamera = sm.mainCamera;
        this.tagToFind = "unit";
    }
    
    public override void onClick(Vector2 position) {
        //check if clicked on character and send event characterSelected
        //sm.SetState(new ChooseActionST(this.sm));
        int charID = Util.checkClickedObject(mainCamera, position, tagToFind);
        Debug.Log("char choosen: " + charID);
        if(charID != -1) {
            sm.GameCH.RaiseCharacterSelected(charID);
            sm.SetState(new ChooseActionST(sm));
        }    
    }
}
