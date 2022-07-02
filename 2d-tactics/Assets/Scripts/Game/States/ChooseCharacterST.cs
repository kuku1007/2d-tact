using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCharacterST : IState
{
    private Camera mainCamera;
    private string tagToFind;

    public ChooseCharacterST(UIInputSystem sm, TurnContext turnContext) : base(sm) {
        this.mainCamera = sm.mainCamera;
        this.tagToFind = turnContext.currentTeamTag;
    }
    
    public override void onClick(Vector2 position) {
        if(!tagToFindAvailable()) {
            Debug.Log("Waiting for initialization...");
            return;
        }

        checkClickedObject(position);
    }

    public override void onContextChanged(TurnContext turnContext)
    {
        this.tagToFind = turnContext.currentTeamTag;
    }

    private bool tagToFindAvailable() {
        if(tagToFind == null) 
            return false;
        
        return true;
    }

    private void checkClickedObject(Vector2 position) {
        int charID = Util.checkClickedObject(mainCamera, position, tagToFind);
        Debug.Log("char choosen: " + charID);
        if(charID != -1) {
            sm.GameCH.RaiseCharacterSelected(charID);
            sm.SetState(new ChooseActionST(sm));
        }  
    }
}
