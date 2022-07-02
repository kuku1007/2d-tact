using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnContext
{
    public string currentTeamTag;
    public string enemyTeamTag;

    public TurnContext(string currentTeamTag, string enemyTeamTag) {
        this.currentTeamTag = currentTeamTag;
        this.enemyTeamTag = enemyTeamTag;
    }
}
