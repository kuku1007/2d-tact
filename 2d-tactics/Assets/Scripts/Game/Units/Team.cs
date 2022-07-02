using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Team", menuName = "Game/Team")]
public class Team : ScriptableObject
{
    public string teamTag;
    private Dictionary<int, Unit> charIDToUnit = new Dictionary<int, Unit>();

    public void addUnit(int charID, Unit unit) {
        charIDToUnit.Add(charID, unit);
    }
}
