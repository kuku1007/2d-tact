using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackST : IState
{
    private Camera mainCamera;
    private string tagToFind;

    public AttackST(UIInputSystem sm) : base(sm) {
        this.mainCamera = sm.mainCamera;
        this.tagToFind = sm.turnContext.enemyTeamTag;
        Debug.Log("Choose character to attack"); // TODO: should be in the scope of different module
    }

    public override void onClick(Vector2 position) {
        int charID = Util.checkClickedObject(mainCamera, position, tagToFind);
        Debug.Log("char choosen: " + charID);
        if(charID != -1) {
            Debug.Log("attacking character: " + charID);
            sm.SetState(new ChooseCharacterST(sm));
        }
    }

}
