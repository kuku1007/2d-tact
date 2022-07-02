using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    // TODO: make fields private serializable or getter/setter
    public GameCH gameCH;
    public int currentTurn = 1;
    public Team currentTeam; //new clss Team with units clss in umit class all stats, turn poimts etc it
    // should have reference to charID
    public Team enemyTeam;

    private TurnContext turnContext;

    void Awake()
    {
        this.turnContext = new TurnContext(currentTeam.teamTag, enemyTeam.teamTag);
        this.gameCH.initEvent += onInit;
    }

    private void onInit()
    {   
        gameCH.RaiseTurnContextChanged(turnContext);
    }
}
